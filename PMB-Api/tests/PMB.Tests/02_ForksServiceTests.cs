using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PMB.Dal.Bll.Dtos;
using PMB.Dal.Bll.Services;
using PMB.Dal.Models;
using PMB.Dal.Models.Dals;
using PMB.Dal.Repositories;
using DeleteForksModel = PMB.Dal.Bll.Dtos.DeleteForksModel;

namespace PMB.Tests
{
    [TestClass]
    public class ForksServiceTests
    {
        private readonly ForksService _forksService;
        private readonly UpsertForksService _upsertForksService;
        private readonly ForkRepository _forkRepository;
        private readonly Fixture _fixture;

        public ForksServiceTests()
        {
            _fixture = new Fixture();

            _forkRepository = Init.ServiceProvider.GetRequiredService<ForkRepository>();

            _forksService = new ForksService(_forkRepository);
            _upsertForksService = Init.ServiceProvider.GetRequiredService<UpsertForksService>();
        }

        [DataTestMethod]
        [DataRow(70, -90, 0)]
        [DataRow(10, 0, 1)]
        [DataRow(100, -20, 0)]
        [DataRow(120, 0, 0)]
        [DataRow(0, -120, 0)]
        public async Task Check_Deleted(long lifetime, int lag, int count)
        {
            var forks = new List<V1ForkDal>();

            var f = _fixture.Build<V1ForkDal>().Create();
            f.Lifetime = lifetime;
            f.CreatedAt = DateTimeOffset.Now.AddSeconds(lag);
            forks.Add(f);

            await _upsertForksService.UpsertForks(forks.ToArray());

            var forksDal = await _forkRepository.SelectAsync(new SelectForksQueryModel
            {
                Bookmakers = new[] {f.Bookmakers},
                Sports = new[] {f.Sport},
                BetTypes = new[] {f.BetType},
                CridIds = new[] {f.CridId}
            });

            forksDal.Should().NotBeNull().And.NotBeEmpty().And.NotContainNulls();

            await _forksService.DeleteWithBets(new DeleteForksModel
            {
                LifetimeBefore = 120
            });

            forksDal = await _forkRepository.SelectAsync(new SelectForksQueryModel
            {
                Bookmakers = new[] {f.Bookmakers},
                Sports = new[] {f.Sport},
                BetTypes = new[] {f.BetType},
                CridIds = new[] {f.CridId}
            });

            forksDal.Length.Should().Be(count);
        }
    }
}