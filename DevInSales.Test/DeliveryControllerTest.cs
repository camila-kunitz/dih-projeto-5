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
    public class DeliveryControllerTest
    {
        private readonly DbContextOptions<SqlContext> _contextOptions;

        public DeliveryControllerTest()
        {
            _contextOptions = new DbContextOptionsBuilder<SqlContext>()
                .UseInMemoryDatabase("DeliveryControllerTest")
                .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            using var context = new SqlContext(_contextOptions);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        [Test]
        public async Task DeveListarTodosListarTodosRegistroDeDeliveryTest()
        {
            var context = new SqlContext(_contextOptions);
            var quantidadeDeDeliveries = context.Delivery.Count();
            var controller = new DeliveryController(context);

            var sut = await controller.GetDelivery(0, 0);
            var resultado = (sut.Result as ObjectResult);
            var conteudo = resultado.Value as List<Delivery>;

            Assert.That(conteudo.Count, Is.EqualTo(quantidadeDeDeliveries));
            Assert.That(resultado.StatusCode.ToString(), Is.EqualTo("200"));
        }
    }
}
