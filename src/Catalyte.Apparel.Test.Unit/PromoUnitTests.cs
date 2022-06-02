using Catalyte.Apparel.API.Validators;
using Catalyte.Apparel.Data.Interfaces;
using Catalyte.Apparel.Data.Models;
using Catalyte.Apparel.Providers.Interfaces;
using Catalyte.Apparel.Providers.Providers;
using Catalyte.Apparel.Utilities.HttpResponseExceptions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Catalyte.Apparel.Test.Unit
{
    public class PromoUnitTests
    {
        private readonly List<Promo> promos;
        private readonly IPromoProvider promoProvider;
        private readonly Mock<IPromoRepository> promoRepo;
        private readonly Mock<ILogger<PromoProvider>> logger;

        public PromoUnitTests()
        {
            promoRepo = new Mock<IPromoRepository>();

            logger = new Mock<ILogger<PromoProvider>>();

            promoProvider = new PromoProvider(promoRepo.Object, logger.Object);

            promos = new List<Promo>()
            {
                new Promo()
                {
                    Id = 1,
                    Title = "Test1",
                    Description = "This is a valid test",
                    Type = "flat",
                    Rate = 50,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(7),
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow
                }
            };
        }

        [Fact]
        public async void GetPromos_ServiceUnavailable_Returns503()
        {
            promoRepo.Setup(r => r.GetPromosAsync()).ThrowsAsync(new Exception("test message"));

            await Assert.ThrowsAsync<ServiceUnavailableException>(() => promoProvider.GetPromosAsync());
        }

        [Fact]
        public async void GetPromos_ValidRequest_Returns200()
        {
            promoRepo.Setup(r => r.GetPromosAsync()).ReturnsAsync(promos);

            var actual = await promoProvider.GetPromosAsync();

            Assert.NotNull(actual);
            Assert.IsType<List<Promo>>(actual);
        }

        [Fact]
        public async void GetPromoById_NotFound_Returns404()
        {
            promoRepo.Setup(r => r.GetPromoByIdAsync(It.IsAny<int>())).ReturnsAsync((int promoId) =>
            {
                return null;
            });

            await Assert.ThrowsAsync<NotFoundException>(() => promoProvider.GetPromoByIdAsync(1));

            promoRepo.Setup(r => r.GetPromoByIdAsync(It.IsAny<int>())).ReturnsAsync((int promoId) =>
            {
                return default;
            });

            await Assert.ThrowsAsync<NotFoundException>(() => promoProvider.GetPromoByIdAsync(1));
        }

        [Fact]
        public async void GetPromoById_ServiceUnavailable_Returns503()
        {
            promoRepo.Setup(r => r.GetPromoByIdAsync(It.IsAny<int>())).ThrowsAsync(new Exception("test message"));

            await Assert.ThrowsAsync<ServiceUnavailableException>(() => promoProvider.GetPromoByIdAsync(1));
        }

        [Fact]
        public async void GetPromoByID_ValidRequest_Returns200()
        {
            promoRepo.Setup(r => r.GetPromoByIdAsync(It.IsAny<int>())).ReturnsAsync(promos[0]);

            var actual = await promoProvider.GetPromoByIdAsync(promos[0].Id);

            Assert.NotNull(actual);
            Assert.IsType<Promo>(actual);
        }

        [Fact]
        public async void CreatePromo_ValidPromo_Returns201()
        {
            promoRepo.Setup(r => r.CreatePromoAsync(It.IsAny<Promo>())).ReturnsAsync((Promo target) =>
            {
                // ensures id is set to be the next one in list
                target.Id = promos.Count + 1;

                // adds current promo to list
                promos.Add(target);

                // return updated promo
                return target;
            });

            var promo = new Promo()
            {
                Title = "Test2",
                Description = "This is also a valid test",
                Type = "percent",
                Rate = 50,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(7),
                DateCreated = DateTime.UtcNow,
                DateModified = DateTime.UtcNow
            };

            var actual = await promoProvider.CreatePromoAsync(promo);

            Assert.NotNull(actual);
            Assert.IsType<Promo>(actual);
            Assert.Equal(promos.Count, actual.Id);
        }

        [Fact]
        public async void CreatePromo_DuplicatePromoTitle_Returns409()
        {
            promoRepo.Setup(r => r.GetPromoByTitleAsync(It.IsAny<string>())).ReturnsAsync(promos[0]);

            await Assert.ThrowsAsync<ConflictException>(() => promoProvider.CreatePromoAsync(promos[0]));
        }

        [Fact]
        public async void CreatePromo_ServiceUnavailableOnTitleRequest_Returns503()
        {
            promoRepo.Setup(r => r.GetPromoByTitleAsync(It.IsAny<string>())).ThrowsAsync(new Exception("test message"));

            await Assert.ThrowsAsync<ServiceUnavailableException>(() => promoProvider.CreatePromoAsync(promos[0]));
        }

        [Fact]
        public async void CreatePromo_ServiceUnavailable_Returns503()
        {
            promoRepo.Setup(r => r.CreatePromoAsync(It.IsAny<Promo>())).ThrowsAsync(new Exception("test message"));

            var promo = new Promo()
            {
                Title = "Test3",
                Description = "This is also a valid test",
                Type = "percent",
                Rate = 50,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(7),
                DateCreated = DateTime.UtcNow,
                DateModified = DateTime.UtcNow
            };

            await Assert.ThrowsAsync<ServiceUnavailableException>(() => promoProvider.CreatePromoAsync(promo));
        }

        [Fact]
        public void ValidatePromo_InvalidTitle_Returns400()
        {
            Promo promoWithLowercase = new()
            {
                Title = "invalidtest1",
                Description = "This is an invalid test",
                Type = "flat",
                Rate = 50,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(7)
            };

            var actualLower = Assert.Throws<BadRequestException>(() => Validator.ValidatePromo(promoWithLowercase));

            Assert.Equal("Promo title must only contain capital letters and numbers and no spaces", actualLower.Value.ErrorMessage);

            Promo promoWithUnderscore = new()
            {
                Title = "INVALID_TEST1",
                Description = "This is an invalid test",
                Type = "flat",
                Rate = 50,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(7)
            };

            var actualUnderscore = Assert.Throws<BadRequestException>(() => Validator.ValidatePromo(promoWithUnderscore));

            Assert.Equal("Promo title must only contain capital letters and numbers and no spaces", actualUnderscore.Value.ErrorMessage);

            Promo promoWithSpace = new()
            {
                Title = "INVALID TEST1",
                Description = "This is an invalid test",
                Type = "flat",
                Rate = 50,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(7)
            };

            var actualSpace = Assert.Throws<BadRequestException>(() => Validator.ValidatePromo(promoWithSpace));

            Assert.Equal("Promo title must only contain capital letters and numbers and no spaces", actualSpace.Value.ErrorMessage);
        }

        [Fact]
        public void ValidatePromo_InvalidType_Returns400()
        {
            Promo promoInvalidType = new()
            {
                Title = "INVALIDTEST1",
                Description = "This is an invalid test",
                Type = "NotAType",
                Rate = 50,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(7)
            };

            var actual = Assert.Throws<BadRequestException>(() => Validator.ValidatePromo(promoInvalidType));

            Assert.Equal("Promo type must be flat or percent", actual.Value.ErrorMessage);
        }

        [Fact]
        public void ValidatePromo_InvalidRate_Returns400()
        {
            Promo promoInvalidRate = new()
            {
                Title = "INVALIDTEST1",
                Description = "This is an invalid test",
                Type = "flat",
                Rate = -50,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(7)
            };

            var actual = Assert.Throws<BadRequestException>(() => Validator.ValidatePromo(promoInvalidRate));

            Assert.Equal("Promo rate must be positive", actual.Value.ErrorMessage);
        }

        [Fact]
        public void ValidatePromo_InvalidRateWhenPercent_Returns400()
        {
            Promo promoInvalidRateUnder = new()
            {
                Title = "INVALIDTEST1",
                Description = "This is an invalid test",
                Type = "percent",
                Rate = -50,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(7)
            };

            var actualUnder = Assert.Throws<BadRequestException>(() => Validator.ValidatePromo(promoInvalidRateUnder));

            Assert.Equal("Promo percentage rate must be between 1 and 100", actualUnder.Value.ErrorMessage);

            Promo promoInvalidRateOver = new()
            {
                Title = "INVALIDTEST1",
                Description = "This is an invalid test",
                Type = "percent",
                Rate = 150,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(7)
            };

            var actualOver = Assert.Throws<BadRequestException>(() => Validator.ValidatePromo(promoInvalidRateOver));

            Assert.Equal("Promo percentage rate must be between 1 and 100", actualOver.Value.ErrorMessage);
        }

        [Fact]
        public void ValidatePromo_InvalidEndDateBeforeStartDate_Returns400()
        {
            Promo promoInvalidEndDate = new()
            {
                Title = "INVALIDTEST1",
                Description = "This is an invalid test",
                Type = "percent",
                Rate = 50,
                StartDate = DateTime.Now.AddDays(8),
                EndDate = DateTime.Now.AddDays(7)
            };

            var actual = Assert.Throws<BadRequestException>(() => Validator.ValidatePromo(promoInvalidEndDate));

            Assert.Equal("Promo cannot end before it starts", actual.Value.ErrorMessage);
        }

        [Fact]
        public void ValidatePromo_InvalidEndDateBeforeNow_Returns400()
        {
            Promo promoInvalidEndDate = new()
            {
                Title = "INVALIDTEST1",
                Description = "This is an invalid test",
                Type = "percent",
                Rate = 50,
                StartDate = DateTime.Parse("2022-01-01"),
                EndDate = DateTime.Parse("2022-01-02")
            };

            var actual = Assert.Throws<BadRequestException>(() => Validator.ValidatePromo(promoInvalidEndDate));

            Assert.Equal("Promo cannot end before it's created", actual.Value.ErrorMessage);
        }

        [Fact]
        public void ValidatePromo_ValidPromo_Returns100()
        {
            Promo promoValid = new()
            {
                Title = "VALIDTEST1",
                Description = "This is a valid test",
                Type = "percent",
                Rate = 50,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(7)
            };
            
            Validator.ValidatePromo(promoValid);
        }
    }
}