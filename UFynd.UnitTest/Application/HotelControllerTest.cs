using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UFynd.API.Controllers;
using UFynd.Application.Feature.Hotel.Queries;
using UFynd.Application.Features.Hotel.Dtos;
using UFynd.Domain.Entities;

namespace UFynd.UnitTest.Application
{
    [TestFixture]
    public class HotelControllerTest
    {
        private readonly Mock<GetHotelRatesQuery> _getHotelRatesQuery;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<ILogger<HotelController>> _loggerMock;

        public HotelControllerTest()
        {
            _mediatorMock = new Mock<IMediator>();
            _getHotelRatesQuery = new Mock<GetHotelRatesQuery>();
            _loggerMock = new Mock<ILogger<HotelController>>();
        }

        [Test]
        public async Task GetHotelInformation()
        {
            var fakeDynamicResult = new HotelWithRatesDto();
            _mediatorMock.Setup(x => x.Send(It.IsAny<GetHotelRatesQuery>(), default(CancellationToken)))
                .Returns(Task.FromResult(fakeDynamicResult));

            //Act
            var hotelController = new HotelController(_loggerMock.Object, _mediatorMock.Object);
            var actionResult = await hotelController.Get(7294, new DateTime(2016, 03, 15)) as OkObjectResult;
            Assert.IsNotNull(actionResult);
            if (actionResult != null)
            {
                Assert.AreEqual(actionResult.StatusCode, (int)System.Net.HttpStatusCode.OK);
            }

        }
    }
}
