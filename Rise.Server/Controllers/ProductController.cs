using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rise.Shared.Auth;
using Rise.Shared.Products;

namespace Rise.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService productService;

    public ProductController(IProductService productService)
    {
        this.productService = productService;
    }

    [HttpGet]
    [Authorize(Roles = Role.Administrator)]
    public async Task<IEnumerable<ProductDto>> Get()
    {
        Console.WriteLine("Logging from Controller");
        Console.WriteLine("=====================================");
        foreach (var claim in User.Claims)
        {
            Console.WriteLine($"{claim.Type}: {claim.Value}");
        }
        Console.WriteLine("User is in role 'Administrator': {0}", User.IsInRole("Administrator"));
        Console.WriteLine("User is {0}", User.Identity!.Name);
        Console.WriteLine("=====================================");
        var products = await productService.GetProductsAsync();
        return products;
    }
}
