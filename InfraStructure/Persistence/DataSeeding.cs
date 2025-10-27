using DomainLayer.Contracts;
using DomainLayer.Models;
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
    public class DataSeeding(StoreDbContext _storeDbContext) : IDataSeeding
    {
        public void DataSeed()
        {
            try
            {
                if (_storeDbContext.Database.GetPendingMigrations().Any())
                {
                    _storeDbContext.Database.Migrate();
                }

                #region Add Product Brand Data

                if (!_storeDbContext.ProductBrand.Any())
                {
                    var productBrandsData = File.ReadAllText(@"..\InfraStructure\Persistence\Data\DataSeed\brands.json");
                    //Conver string to c# object
                    var brand = JsonSerializer.Deserialize<List<ProductBrand>>(productBrandsData);
                    if (brand is not null && brand.Any())
                    {
                        _storeDbContext.ProductBrand.AddRange(brand);
                    }
                }

                #endregion

                #region Add Product Types Data
                if (!_storeDbContext.ProductTypes.Any())
                {
                    var productTypesData = File.ReadAllText(@"..\InfraStructure\Persistence\Data\DataSeed\types.json");
                    //Conver string to c# object
                    var type = JsonSerializer.Deserialize<List<ProductType>>(productTypesData);
                    if (type is not null && type.Any())
                    {
                        _storeDbContext.ProductTypes.AddRange(type);
                    }
                }
                #endregion

                #region Add Product Data
                if (!_storeDbContext.Products.Any())
                {
                    var productData = File.ReadAllText(@"..\InfraStructure\Persistence\Data\DataSeed\products.json");
                    //Conver string to c# object
                    var products = JsonSerializer.Deserialize<List<Product>>(productData);
                    if (products is not null && products.Any())
                    {
                        _storeDbContext.Products.AddRange(products);
                    }
                }
                #endregion

                _storeDbContext.SaveChanges();

            }
            catch (Exception)
            {

                //ToDO
            }
        }
    }
}
