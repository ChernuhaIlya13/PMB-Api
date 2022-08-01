using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PMB.Dal.Bll.Services;
using PMB.Dal.Models;
using PMB.Dal.Models.Dals;
using PMB.Dal.Repositories;

namespace PMB.Tests
{
    [TestClass]
    public class UpsertForksServiceTests
    {
        private readonly UpsertForksService _service;
        private readonly ForkRepository _forkRepository;
        private readonly Fixture _fixture;
        private static readonly Random Rnd = new();
        
        public UpsertForksServiceTests()
        {
            _forkRepository = Init.ServiceProvider.GetRequiredService<ForkRepository>();
            _service = new UpsertForksService(_forkRepository, Init.ServiceProvider.GetRequiredService<BetRepository>());
            
            _fixture = new Fixture();
        }
        
        [TestMethod]
        public async Task UpsertFork_Tests()
        {
            var fork = _fixture.Build<V1ForkDal>().With(x => x.CreatedAt, DateTimeOffset.Now.AddMinutes(-3)).Create();
            await _service.UpsertForks(new []{fork});

            var query = new SelectForksQueryModel
            {
                Bookmakers = new[] {fork.Bookmakers},
                Sports = new[] {fork.Sport},
                BetTypes = new[] {fork.BetType},
                CridIds = new[] {fork.CridId}
            };
            
            var oldForkDal = await _forkRepository.SelectAsync(query);
            
            fork.Lifetime += 4;
            fork.Profit -= 0.01m;

            fork.FirstBet.Coefficient -= 0.2m;
            fork.SecondBet.Coefficient += 0.2m;

            await _service.UpsertForks(new[] {fork});

            var newForkDal = await _forkRepository.SelectAsync(query);

            oldForkDal.Should().NotBeEmpty();

            var oldFork = oldForkDal.FirstOrDefault();
            var newFork = newForkDal.FirstOrDefault();
            
            oldFork.Should().NotBeNull();
            newFork.Should().NotBeNull();

            oldFork!.Id.Should().Be(newFork!.Id);
            oldFork.Lifetime.Should().NotBe(newFork.Lifetime);
            oldFork.Profit.Should().NotBe(newFork.Profit);
            oldFork.FirstBet.Id.Should().Be(newFork.FirstBet.Id);
            oldFork.FirstBet.Coefficient.Should().NotBe(newFork.FirstBet.Coefficient);
            oldFork.SecondBet.Id.Should().Be(newFork.SecondBet.Id);
            oldFork.SecondBet.Coefficient.Should().NotBe(newFork.SecondBet.Coefficient);
        }

        [TestMethod]
        public async Task BatchUpsertFork_Tests()
        {
            var forks = new List<V1ForkDal>();
            
            for (int i = 0; i < 20; i++)
            {
                forks.Add(_fixture.Build<V1ForkDal>().With(x => x.CreatedAt, DateTimeOffset.Now.AddMinutes(-3)).Create());
            }
            
            await _service.UpsertForks(forks.ToArray());

            var query = new SelectForksQueryModel
            {
                Bookmakers = forks.Select(x => x.Bookmakers).ToArray(),
                Sports = forks.Select(x => x.Sport).ToArray(),
                BetTypes = forks.Select(x => x.BetType).ToArray(),
                CridIds = forks.Select(x => x.CridId).ToArray(),
            };
            
            var oldForks = await _forkRepository.SelectAsync(query);

            foreach (var fork in forks)
            {
                fork.Lifetime += Rnd.Next(3, 10);
                fork.Profit -= Rnd.Next(-20, 20) / 10m;
                fork.FirstBet.Coefficient -= Rnd.Next(-20, 20) / 10m;
                fork.SecondBet.Coefficient += Rnd.Next(-20, 20) / 10m;
            }

            await _service.UpsertForks(forks.ToArray());

            var newForks = await _forkRepository.SelectAsync(query);

            oldForks.Should().NotBeEmpty();

            var oldFork = oldForks.FirstOrDefault();
            var newFork = newForks.FirstOrDefault();
            
            oldFork.Should().NotBeNull();
            newFork.Should().NotBeNull();

            oldFork!.Id.Should().Be(newFork!.Id);
            oldFork.Lifetime.Should().NotBe(newFork.Lifetime);
            oldFork.Profit.Should().NotBe(newFork.Profit);
            oldFork.FirstBet.Id.Should().Be(newFork.FirstBet.Id);
            oldFork.FirstBet.Coefficient.Should().NotBe(newFork.FirstBet.Coefficient);
            oldFork.SecondBet.Id.Should().Be(newFork.SecondBet.Id);
            oldFork.SecondBet.Coefficient.Should().NotBe(newFork.SecondBet.Coefficient);
        }
    }
}