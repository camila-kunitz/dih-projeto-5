using DevInSales.Context;
using DevInSales.Controllers;
using DevInSales.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevInSales.Test
{
    public class StateControllerTest
    {
        private readonly DbContextOptions<SqlContext> _contextOptions;

        public StateControllerTest()
        {
            _contextOptions = new DbContextOptionsBuilder<SqlContext>()
                .UseInMemoryDatabase("StateControllerTest")
                .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            using var context = new SqlContext(_contextOptions);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        [Test]
        public async Task DeveListarEstado_ComNome_ComSucessoTest()
        {
            var context = new SqlContext(_contextOptions);
            var nomeEstado = "Acre";
            var quantidade = context.State.Where(state => state.Name == nomeEstado).Count();
            var controller = new StateController(context);

            var sut = await controller.GetState(nomeEstado);
            var resultado = (sut.Result as ObjectResult);
            var conteudo = resultado.Value as List<State>;

            Assert.That(conteudo.Count, Is.EqualTo(quantidade));
            Assert.That(resultado.StatusCode.ToString(), Is.EqualTo("200"));
        }

        [Test]
        public async Task DeveListarTodosEstadosTest()
        {
            var context = new SqlContext(_contextOptions);
            var quantidade = context.State.Count();
            var controller = new StateController(context);

            var sut = await controller.GetState(null);
            var resultado = (sut.Result as ObjectResult);
            var conteudo = resultado.Value as List<State>;

            Assert.That(conteudo.Count, Is.EqualTo(quantidade));
            Assert.That(resultado.StatusCode.ToString(), Is.EqualTo("200"));
        }
    }
}
