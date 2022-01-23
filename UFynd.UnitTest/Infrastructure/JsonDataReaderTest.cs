using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFynd.Application.Exceptions;
using UFynd.Common.Constants;
using UFynd.Domain.Entities;
using UFynd.Infrastructure;

namespace UFynd.UnitTest.Infrastructure
{
    [TestFixture]
    public  class JsonDataReaderTest
    {
        [Test]
        public void VerifyThatWhenPathIsNullAndThrowException()
        {
            var jsonReader = new JsonDataReader<HotelSummary>();
            var result = jsonReader.LoadAsync("");
            if (result.Exception?.InnerException != null)
            {
                Assert.AreEqual(ErrorMessage.InvalidPath, result.Exception.InnerException.Message);
            }
        }

        [Test]
        public void VerifyThatWhenPathIsNotNullAndFileNotFound()
        {
            var jsonReader = new JsonDataReader<HotelSummary>();
            var result = jsonReader.LoadAsync("c://test/");
            if (result.Exception?.InnerException != null)
            {
                Assert.AreEqual(ErrorMessage.InvalidPath, result.Exception.InnerException.Message);
            }
        }

        [Test]
        public async Task VerifyThatWhenPathIsNotNullAndReturnJsonResult()
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, UFyndAppConstant.HotelRateJsonFile);
            var jsonReader = new JsonDataReader<HotelSummary>();
            var result = await jsonReader.LoadAsync(filePath);
            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result);
        }
    }
}
