using System;
using Xunit;
using Greggs.Products.Api.DataAccess;
using Greggs.Products.Api.Models;
using System.Linq;

namespace Greggs.Products.UnitTests;

public class ProductTest
{
    [Fact]
    public void Test1()
    {
        throw new NotImplementedException("We have no tests :-(");
    }

    [Fact]
    public void Is_Latest_Menu_Product()
    {
        IDataAccess<Product> products = new ProductAccess();
        var proudctList = products.List(1,8).ToList();
        string actual = string.Empty;
        string expected = "Coca Cola";

        if (proudctList.Any())
        { 
          var product = proudctList.FirstOrDefault(x => x.Name == expected);
          if(product != null)
            {
                actual = product.Name;
            }
        }

        Assert.Equal(expected, actual);
    }
}