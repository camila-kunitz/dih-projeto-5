using DevInSales.Context;
using DevInSales.Controllers;
using DevInSales.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DevInSales.Test
{
    public class UserControllerTest
    {
        private readonly DbContextOptions<SqlContext> _contextOptions;

        public UserControllerTest()
        {
            _contextOptions = new DbContextOptionsBuilder<SqlContext>()
                .UseInMemoryDatabase("UserControllerTest")
                .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            using var context = new SqlContext(_contextOptions);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        [Test]
        public async Task DeveListarTodosUsuariosComPerfilClienteTest()
        {
            var context = new SqlContext(_contextOptions);
            var quantidadeDeUsuarios = context.User.Where(user => user.ProfileId == 1).Count();
            var controller = new UserController(context);

            var sut = await controller.Get(null, null, null);
            var resultado = (sut.Result as ObjectResult);
            var conteudo = resultado.Value as List<UserResponseDTO>;

            Assert.That(conteudo.Count, Is.EqualTo(quantidadeDeUsuarios));
            Assert.That(resultado.StatusCode.ToString(), Is.EqualTo("200"));
        }

        [Test]
        public async Task DeveListarUsuariosFiltradosPorNomeTest()
        {
            var context = new SqlContext(_contextOptions);
            var user = context.User.FirstAsync(user => user.Name.Contains("Romeu"));
            var controller = new UserController(context);

            var sut = await controller.Get("Romeu", null, null);
            var resultado = (sut.Result as ObjectResult);
            var conteudo = resultado.Value as List<UserResponseDTO>;

            Assert.That(resultado.StatusCode.ToString(), Is.EqualTo("200"));
            Assert.That(conteudo[0].Name.Contains(user.Result.Name));
        }

        [Test]
        public async Task DeveListarUsuariosFiltradosPorDataMinimaTest()
        {
            var context = new SqlContext(_contextOptions);
            var data = new DateTime(2000, 02, 01);
            var controller = new UserController(context);

            var sut = await controller.Get(null, "01/02/2000", null);
            var resultado = (sut.Result as ObjectResult);
            var conteudo = resultado.Value as List<UserResponseDTO>;

            Assert.That(conteudo[0].BirthDate >= data);
            Assert.That(resultado.StatusCode.ToString(), Is.EqualTo("200"));
        }

        [Test]
        public async Task DeveListarUsuariosFiltradosoPorDataMaximaTest()
        {
            var context = new SqlContext(_contextOptions);
            var data = new DateTime(1974, 04, 11);
            var controller = new UserController(context);

            var sut = await controller.Get(null, null, "11/04/1974");
            var resultado = (sut.Result as ObjectResult);
            var conteudo = resultado.Value as List<UserResponseDTO>;

            Assert.That(resultado.StatusCode.ToString(), Is.EqualTo("200"));
            Assert.That(conteudo[0].BirthDate <= data);
        }

        [Test]
        public async Task DeveCriarUsuarioComSucessoTest()
        {
            UserCreateDTO userDTO = new UserCreateDTO();
            userDTO.BirthDate = "01/01/2000";
            userDTO.Email = "teste@mail.com";
            userDTO.Name = "Teste";
            userDTO.Password = "teste@123";
            userDTO.ProfileId = 1;
            var context = new SqlContext(_contextOptions);
            var controller = new UserController(context);

            var sut = await controller.Create(userDTO);
            var resultado = (sut.Result as ObjectResult);

            Assert.That(resultado.StatusCode.ToString(), Is.EqualTo("201"));
        }

        [Test]
        public async Task DeveRetornarCodigo400_QuandoIdadeDoUsuarioForInvalidaTest()
        {
            UserCreateDTO userDTO = new UserCreateDTO();
            userDTO.BirthDate = "01/01/2015";
            userDTO.Email = "teste@mail.com";
            userDTO.Name = "Teste";
            userDTO.Password = "teste@123";
            userDTO.ProfileId = 1;
            var context = new SqlContext(_contextOptions);
            var controller = new UserController(context);

            var sut = await controller.Create(userDTO);
            var resultado = (sut.Result as ObjectResult);

            Assert.That(resultado.StatusCode.ToString(), Is.EqualTo("400"));
        }

        [Test]
        public async Task DeveRetornarCodigo400_QuandoSenhaDoUsuarioForInvalidaTest()
        {
            UserCreateDTO userDTO = new UserCreateDTO();
            userDTO.BirthDate = "01/01/2000";
            userDTO.Email = "teste@mail.com";
            userDTO.Name = "Teste";
            userDTO.Password = "teste";
            userDTO.ProfileId = 1;
            var context = new SqlContext(_contextOptions);
            var controller = new UserController(context);

            var sut = await controller.Create(userDTO);
            var resultado = (sut.Result as ObjectResult);

            Assert.That(resultado.StatusCode.ToString(), Is.EqualTo("400"));
        }

        [Test]
        public async Task DeveRetornarCodigo400_QuandoEmailJaExisteTest()
        {
            UserCreateDTO userDTO = new UserCreateDTO();
            userDTO.BirthDate = "01/01/2000";
            userDTO.Email = "romeu@lenda.com";
            userDTO.Name = "Teste";
            userDTO.Password = "teste";
            userDTO.ProfileId = 1;
            var context = new SqlContext(_contextOptions);
            var controller = new UserController(context);

            var sut = await controller.Create(userDTO);
            var resultado = (sut.Result as ObjectResult);

            Assert.That(resultado.StatusCode.ToString(), Is.EqualTo("400"));
        }

        [Test]
        public async Task DeveDeletarUsuarioComSucesso()
        {
            var context = new SqlContext(_contextOptions);
            var quantidadeDeUsuarios = context.User.Count();
            var controller = new UserController(context);

            var sut = await controller.DeleteUser(4);
            var resultado = (sut as ObjectResult);

            Assert.That(resultado.Value.ToString(), Is.EqualTo("4"));
            Assert.That(resultado.StatusCode.ToString(), Is.EqualTo("200"));
            Assert.That(context.User.Count() == quantidadeDeUsuarios - 1);
        }
    }
}