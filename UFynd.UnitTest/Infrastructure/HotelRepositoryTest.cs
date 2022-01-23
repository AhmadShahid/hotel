using Moq;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFynd.Application.Contracts;
using UFynd.Common.Constants;
using UFynd.Domain.Entities;
using UFynd.Infrastructure.Repositories;

namespace UFynd.UnitTest.Infrastructure
{
    [TestFixture]
    public class HotelRepositoryTest
    {
        private readonly Mock<IDataReader<HotelSummary>> _dataReader;
        public HotelRepositoryTest()
        {
            _dataReader = new Mock<IDataReader<HotelSummary>>();
        }

        [SetUp]
        public void Setup()
        {
            _dataReader.Setup(hotelRepo => hotelRepo.LoadAsync(It.IsAny<string>()))
               .Returns(Task.FromResult<IList<HotelSummary>>(GetFakeData()));
        }

        [TestCase(7294, "03/15/2016")]
        public void GetHotelInformationShouldBeTypeOfHotelSummary(int hotelId, DateTime arrivalDate)
        {
            Task<HotelSummary> actual = InvokeGetHotelsWithRates(hotelId, arrivalDate);
            actual.Result.ShouldBeOfType<HotelSummary>();
        }


        [TestCase(7294, "03/15/2016")]
        public void GetHotelsWithRates_WhenResponseIsValid_DoesNotThrowException(int hotelId, DateTime arrivalDate)
        {
            Assert.DoesNotThrow(() => InvokeGetHotelsWithRates(hotelId, arrivalDate));
        }

        [TestCase(1, "03/15/2016")]
        [TestCase(2, "03/15/2016")]
        public void GetHotelsWithRates_WhenInvokedWithInvalidData_ReturnsExpectedException(int hotelId, DateTime arrivalDate)
        {
            Task<HotelSummary> actual = InvokeGetHotelsWithRates(hotelId, new DateTime(2016, 03, 15));
            if (actual.Exception?.InnerException != null)
            {
                Assert.AreEqual($"Hotel ({hotelId}) is not found", actual.Exception.InnerException.Message);
            }
        }

        [TestCase(7294, "03/15/2016")]
        public void GetHotelsWithRates_WhenThrowException_ReturnsExpectedExceptionWithMessage(int hotelId, DateTime arrivalDate)
        {
            Task<HotelSummary> actual = InvokeGetHotelsWithRates(hotelId, arrivalDate);
            if (actual.Exception?.InnerException != null)
            {
                Assert.AreEqual(ErrorMessage.UnExpectedErrorOccurred, actual.Exception.InnerException.Message);
            }
        }

        [TestCase(7294, "03/15/2016")]
        public void GetHotelInformationIfHotelIsFound(int hotelId, DateTime arrivalDate)
        {
            Task<HotelSummary> actual = InvokeGetHotelsWithRates(hotelId, arrivalDate);
            Assert.AreEqual(actual.Result.Hotel.HotelID, 7294);
        }

        [TestCase(7294, "03/15/2016")]
        public void GetHotelRateCountIfCorrectDateFilterApplied(int hotelId, DateTime arrivalDate)
        {
            Task<HotelSummary> actual = InvokeGetHotelsWithRates(hotelId, arrivalDate);
            Assert.AreEqual(actual.Result.HotelRates.ToList().Count, 1);
        }

        [TestCase(7294, "03/15/2016")]
        public void GetHotelRateCountIfWrongDateFilterApplied(int hotelId, DateTime arrivalDate)
        {
            Task<HotelSummary> actual = InvokeGetHotelsWithRates(hotelId, arrivalDate);
            Assert.AreEqual(actual.Result.HotelRates.ToList().Count, 1);
        }

        private Task<HotelSummary> InvokeGetHotelsWithRates(int hotelId, DateTime arrivalDate)
        {
            var repository = new HotelRepository(_dataReader.Object);
            return repository.GetHotelsWithRates(hotelId, arrivalDate);
        }


        private IList<HotelSummary> GetFakeData()
        {
            return new List<HotelSummary>() {
                new HotelSummary()
                {
                    Hotel = new Hotel() { Classification = 5, Name = "Kempinski Bristol Berlin", HotelID = 7294, Reviewscore = 8.3 },
                    HotelRates = new List<HotelRate>()
                    {
                        new HotelRate() { Adults = 2, Los = 1, TargetDay = new DateTime(2016,03,15) }
                    },
                }
            };
        }
    }
}
