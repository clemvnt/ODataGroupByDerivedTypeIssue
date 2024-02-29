using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using ODataGroupByDerivedTypeIssue.Models;

namespace ODataGroupByDerivedTypeIssue.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ODataController
{
  private static readonly Category[] categories = [
    new Category { CategoryName = "Category 1" },
    new Category { CategoryName = "Category 2" }
  ];

  private static readonly Product[] products = [
      new Product { ProductName = "Product 1", Category = categories[0] },
      new Product { ProductName = "Product 2", Category = categories[0] },
      new Product { ProductName = "Product 3", Category = categories[1] },
      new Product { ProductName = "Product 4", Category = categories[1] }
  ];

  [HttpGet]
  [EnableQuery]
  public IActionResult Get()
  {
    return Ok(products);
  }
}