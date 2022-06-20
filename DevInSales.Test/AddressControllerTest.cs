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
    public class AddressControllerTest
    {
        private readonly DbContextOptions<SqlContext> _contextOptions;

        public AddressControllerTest()
        {
            _contextOptions = new DbContextOptionsBuilder<SqlContext>()
                .UseInMemoryDatabase("AddressControllerTest")
                .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            using var context = new SqlContext(_contextOptions);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        [Test]
        public async Task DeveListarEnderecosComSucessoTest()
        {
            var context = new SqlContext(_contextOptions);
            var quantidade = context.Address.Count();
            var controller = new AddressController(context);

            var sut = await controller.GetAddress();

            Assert.That(sut.Value.Count, Is.EqualTo(quantidade));
        }

        [Test]
        public async Task DeveRetornarEnderecosComSucessoTest()
        {
            var context = new SqlContext(_contextOptions);
            var endereco = context.Address.Where(a => a.Id == 1).ToList();
            var controller = new AddressController(context);

            var sut = await controller.GetAddress(1);

            Assert.That(sut.Value, Is.EqualTo(endereco.First()));
        }
    }
}
