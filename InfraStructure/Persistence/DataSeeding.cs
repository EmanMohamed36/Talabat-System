using DomainLayer.Contracts;
using DomainLayer.Models.IdentityModels;
using DomainLayer.Models.OrderModels;
using DomainLayer.Models.ProductModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PersistenceLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PersistenceLayer
{
    public class DataSeeding(StoreDbContext _storeDbContext,
                             UserManager<ApplicationUser> _userManager,
                             RoleManager<IdentityRole> _roleManager) 
        : IDataSeeding
    {
        public async Task DataSeedAsync()
        {
            try
            {
                if ((await _storeDbContext.Database.GetPendingMigrationsAsync()).Any())
                {
                   await _storeDbContext.Database.MigrateAsync();
                }

                #region Add Product Brand Data

                if (!_storeDbContext.ProductBrand.Any())
                {
                    var productBrandsData = File.OpenRead(@"..\InfraStructure\Persistence\Data\DataSeed\brands.json");
                    //Conver string to c# object
                    var brand =  await JsonSerializer.DeserializeAsync<List<ProductBrand>>(productBrandsData);
                    if (brand is not null && brand.Any())
                    {
                        await _storeDbContext.ProductBrand.AddRangeAsync(brand);
                    }
                }

                #endregion

                #region Add Product Types Data
                if (!_storeDbContext.ProductTypes.Any())
                {
                    var productTypesData = File.OpenRead(@"..\InfraStructure\Persistence\Data\DataSeed\types.json");
                    //Conver string to c# object
                    var type =  await JsonSerializer.DeserializeAsync<List<ProductType>>(productTypesData);
                    if (type is not null && type.Any())
                    {
                       await _storeDbContext.ProductTypes.AddRangeAsync(type);
                    }
                }
                #endregion

                #region Add Product Data
                if (!_storeDbContext.Products.Any())
                {
                    var productData = File.OpenRead(@"..\InfraStructure\Persistence\Data\DataSeed\products.json");
                    //Conver string to c# object
                    var products = await JsonSerializer.DeserializeAsync<List<Product>>(productData);
                    if (products is not null && products.Any())
                    {
                       await _storeDbContext.Products.AddRangeAsync(products);
                    }
                }
                #endregion

                #region Add Delivery Data

                if (!_storeDbContext.Set<DeliveryMethod>().Any())
                {
                    var DeliveryMethodData = File.OpenRead(@"..\InfraStructure\Persistence\Data\DataSeed\delivery.json");
                    //Conver json to c# object
                    var DeliveryMethods = await JsonSerializer.DeserializeAsync<List<DeliveryMethod>>(DeliveryMethodData);
                    if (DeliveryMethods is not null && DeliveryMethods.Any())
                    {
                        await _storeDbContext.AddRangeAsync(DeliveryMethods);
                    }
                }
                #endregion

                await _storeDbContext.SaveChangesAsync();

            }
            catch (Exception)
            {

                //ToDO
            }
        }

        public async Task IdentityDataSeedAsync()
        {
            try
            {
                if (!_roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));

                }
                if (!_userManager.Users.Any())
                {
                    var user01 = new ApplicationUser
                    {
                        Email = "eman@gmail.com",
                        UserName = "emanmohamed",
                        DisplayName = "Eman Mohamed",
                        PhoneNumber = "1234567890"
                    };
                    var user02 = new ApplicationUser
                    {
                        Email = "mohamed@gmail.com",
                        UserName = "mohamedibrahem",
                        DisplayName = "Mohamed Ibrahem",
                        PhoneNumber = "1234567890"
                    };
                    await _userManager.CreateAsync(user01, "P@ssw0rd");
                    await _userManager.CreateAsync(user02, "P@ssw0rd");

                    await _userManager.AddToRoleAsync(user01, "Admin");
                    await _userManager.AddToRoleAsync(user02, "SuperAdmin");

                }


            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
