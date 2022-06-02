using Catalyte.Apparel.Data.Context;
using Catalyte.Apparel.Data.SeedData;

namespace Catalyte.Apparel.Test.Integration.Utilities
{
    public static class DatabaseSetupExtensions
    {
        public static void InitializeDatabaseForTests(this ApparelCtx context)
        {
            var productFactory = new ProductFactory();
            var promoFactory = new PromoFactory();
            var products = productFactory.GenerateRandomProducts(250);
            var promos = promoFactory.GenerateRandomPromos(10);

            context.Promos.AddRange(promos);
            context.Products.AddRange(products);
            context.SaveChanges();
        }

        public static void ReinitializeDatabaseForTests(this ApparelCtx context)
        {
            context.Promos.RemoveRange(context.Promos);
            context.Products.RemoveRange(context.Products);
            context.InitializeDatabaseForTests();
        }
    }
}
