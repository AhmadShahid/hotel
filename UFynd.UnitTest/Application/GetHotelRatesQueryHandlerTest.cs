using AutoMapper;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFynd.Application.Contracts;
using UFynd.Application.Feature.Hotel.Queries;
using UFynd.Application.Features.Hotel.Dtos;
using UFynd.Application.Mappings;
using UFynd.Common.Constants;
using UFynd.Domain.Entities;
using UFynd.Infrastructure.Repositories;
using Shouldly;

namespace UFynd.UnitTest.Application
{
    [TestFixture]
    public class GetHotelRatesQueryHandlerTest
    {
        private readonly Mock<IHotelRateRepository> _hotelRateRepository;
        private readonly IMapper _mapper;

        public GetHotelRatesQueryHandlerTest()
        {
            _hotelRateRepository = new Mock<IHotelRateRepository>();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();

        }

        [SetUp]
        public void Setup()
        {
            _hotelRateRepository.Setup(hotelRepo => hotelRepo.GetHotelsWithRates(It.IsAny<int>(), It.IsAny<DateTime>()))
               .Returns(Task.FromResult<HotelSummary>(FakeHotelWithRateDetail()));
        }

        [Test]
        public void GetHotelInformation_WhenResponseIsValid_DoesNotThrowException()
        {
            Assert.DoesNotThrow(() => InvokeGetHotelInformation(FakeGetHotelRateQuery()));
        }

        [Test]
        public void GetHotelInformationShouldBeTypeOfHotelWithRatesDto()
        {
            Task<HotelWithRatesDto> actual = InvokeGetHotelInformation(FakeGetHotelRateQuery());
            actual.Result.ShouldBeOfType<HotelWithRatesDto>();
        }

        [TestCase(7294)]
        public void GetHotelInformationIfHotelIsFound(int hotelId)
        {
            Task<HotelWithRatesDto> actual = InvokeGetHotelInformation(FakeGetHotelRateQuery());
            Assert.AreEqual(actual.Result.HotelId, hotelId);
        }

        [Test]
        public void GetHotelRateCountIfCorrectDateFilterApplied()
        {
            Task<HotelWithRatesDto> actual = InvokeGetHotelInformation(FakeGetHotelRateQuery());
            Assert.AreEqual(actual.Result.HotelRates.ToList().Count, 1);
        }

        [Test]
        public void GetHotelRateCountIfWrongDateFilterApplied()
        {
            var query = FakeGetHotelRateQuery();
            query.ArrivalDate = DateTime.Now;
            Task<HotelWithRatesDto> actual = InvokeGetHotelInformation(FakeGetHotelRateQuery());
            var hotelRates = actual.Result.HotelRates;
            Assert.AreEqual(hotelRates.Where(hr => hr.ArrivalDate == query.ArrivalDate).ToList().Count, 0);
        }

        private Task<HotelWithRatesDto> InvokeGetHotelInformation(GetHotelRatesQuery hotelRateQuery)
        {
            var handler = new GetHotelRatesQueryHandler(_hotelRateRepository.Object, _mapper);
            var cltToken = new System.Threading.CancellationToken();
            return handler.Handle(hotelRateQuery, cltToken);
        }

        private HotelSummary FakeHotelWithRateDetail()
        {
            return new HotelSummary()
            {
                Hotel = new Hotel() { Classification = 5, Name = "Kempinski Bristol Berlin", HotelID = 7294, Reviewscore = 8.3 },
                HotelRates = new List<HotelRate>()
                {
                    new HotelRate() { Adults = 2, Los = 1, TargetDay = new DateTime(2016,03,15) }
                },
            };
        }

        private GetHotelRatesQuery FakeGetHotelRateQuery()
        {
            return new GetHotelRatesQuery() { HotelId = 7294, ArrivalDate = new DateTime(2016, 03, 15) };
        }
    }
}
