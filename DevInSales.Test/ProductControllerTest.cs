using DevInSales.Context;
using DevInSales.Controllers;
using DevInSales.DTOs;
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
    public class ProductControllerTest
    {
        private readonly DbContextOptions<SqlContext> _contextOptions;

        public ProductControllerTest()
        {
            _contextOptions = new DbContextOptionsBuilder<SqlContext>()
            .UseInMemoryDatabase("ProductControllerUnitTest")
            .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;

            using var context = new SqlContext(_contextOptions);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        [Test]
        public async Task DeveRetornarCodigo201_QuandoCriarProdutoComSucessoTest()
        {
            var produto = new ProductPostAndPutDTO
            {
                Name = "Curso de Redux",
                CategoryId = 1,
                Suggested_Price = 120m
            };
            var context = new SqlContext(_contextOptions);
            var controller = new ProductController(context);

            var sut = await controller.PostProduct(produto);
            var resultado = (sut.Result as ObjectResult);

            Assert.That(resultado.StatusCode.ToString(), Is.EqualTo("201"));
        }

        [Test]
        public async Task DeveListarTodosProdutosComSucessoTest()
        {
            var context = new SqlContext(_contextOptions);
            var quantidade = context.Product.Count();
            var controller = new ProductController(context);

            var sut = await controller.GetProduct(null, null, null);
            var resultado = (sut.Result as ObjectResult);
            var conteudo = resultado.Value as List<ProductGetDTO>;

            Assert.That(resultado.StatusCode.ToString(), Is.EqualTo("200"));
            Assert.That(conteudo.Count(), Is.EqualTo(quantidade));
        }

        [Test]
        public async Task DeveListarProdutos_PorNomeTest()
        {
            var context = new SqlContext(_contextOptions);
            var controller = new ProductController(context);

            var sut = await controller.GetProduct("React", null, null);
            var resultado = (sut.Result as ObjectResult);
            var conteudo = resultado.Value as List<ProductGetDTO>;

            Assert.That(resultado.StatusCode.ToString(), Is.EqualTo("200"));
            Assert.That(conteudo.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task DeveListarProdutos_PorPrecoMaximoTest()
        {
            var context = new SqlContext(_contextOptions);
            var controller = new ProductController(context);

            var sut = await controller.GetProduct(null, null, 200);
            var resultado = (sut.Result as ObjectResult);
            var conteudo = resultado.Value as List<ProductGetDTO>;

            Assert.That(resultado.StatusCode.ToString(), Is.EqualTo("200"));
            Assert.That(conteudo.Count(), Is.EqualTo(3));
        }

        [Test]
        public async Task DeveListarProdutos_PorPrecoMinimoTest()
        {
            var context = new SqlContext(_contextOptions);
            var controller = new ProductController(context);

            var sut = await controller.GetProduct(null, 140, null);
            var resultado = (sut.Result as ObjectResult);
            var contudo = resultado.Value as List<ProductGetDTO>;

            Assert.That(resultado.StatusCode.ToString(), Is.EqualTo("200"));
            Assert.That(contudo.Count(), Is.EqualTo(9));
        }

        [Test]
        public async Task DeveRetornarCodigo204_QuandoFiltrarPorNomeInexistenteTest()
        {
            var context = new SqlContext(_contextOptions);
            var controller = new ProductController(context);

            var sut = await controller.GetProduct("teste", null, null);
            var resultado = (sut.Result as StatusCodeResult);

            Assert.That(resultado.StatusCode.ToString(), Is.EqualTo("204"));
        }

        [Test]
        public async Task DeveRetornarCodigo400_QuandoPrecoMax_MenorQuePrecoMinTest()
        {
            var context = new SqlContext(_contextOptions);
            var controller = new ProductController(context);

            var sut = await controller.GetProduct("Curso de Java", 200, 150);
            var resultado = (sut.Result as ObjectResult);

            Assert.That(resultado.StatusCode.ToString(), Is.EqualTo("400"));
        }
    }
}
