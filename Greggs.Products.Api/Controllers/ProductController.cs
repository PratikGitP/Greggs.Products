using System;
using System.Collections.Generic;
using System.Linq;
using Greggs.Products.Api.DataAccess;
using Greggs.Products.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Greggs.Products.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private static readonly string[] Products = new[]
    {
        "Sausage Roll", "Vegan Sausage Roll", "Steak Bake", "Yum Yum", "Pink Jammie"
    };

    private readonly ILogger<ProductController> _logger;

    public ProductController(ILogger<ProductController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<Product> Get(int pageStart = 0, int pageSize = 5)
    {
        if (pageSize > Products.Length)
            pageSize = Products.Length;

        var rng = new Random();
        return Enumerable.Range(1, pageSize).Select(index => new Product
            {
                PriceInPounds = rng.Next(0, 10),
                Name = Products[rng.Next(Products.Length)]
            })
            .ToArray();
    }


    [HttpGet("{pageStart}/{pageSize}")]
    public IEnumerable<Product> GetFanaticProducts(int pageStart = 1, int pageSize = 10)
    {
        IDataAccess<Product> products = new ProductAccess();

        var proudctList = products.List(pageStart, pageSize);

        var rtnValue = Enumerable.Empty<Product>();

        if (proudctList.Any())
        {
            rtnValue = proudctList;
        }

        return rtnValue;
    }

    [HttpGet("{pageStart}/{pageSize}/{exchangeRate}")]
    public IEnumerable<Product> GetEntrepreneurProduts(int pageStart = 1, int pageSize = 10, decimal exchangeRate = 1)
    {
        IDataAccess<Product> products = new ProductAccess();

        var proudctList = products.List(pageStart, pageSize);

        var rtnValue = Enumerable.Empty<Product>();

        if (proudctList.Any())                 
        {
            rtnValue = proudctList.Select(x => new Product {   
                Name = x.Name,
                PriceInPounds = x.PriceInPounds * exchangeRate
            }).ToList();
        }

        return rtnValue;

    }