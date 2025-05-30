using EShop.Application.Service;
using EShopDomain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EShopService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private IProductService _productService;
    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    // wyjmij wszystkie produkty
    // GET: api/<ProductController>
    [HttpGet]
    public async Task<ActionResult> Get()
    {
        var result = await _productService.GetAllAsync();
        return Ok(result);
    }

    //wyjmij produkt o danym {id}
    // GET api/<ProductController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult> Get(int id)
    {
        var result = await _productService.GetAsync(id);
        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    // dodaj nowy produkt
    // POST api/<ProductController>
    [Authorize(Policy = "EmployeeOnly")]
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] Product product)
    {
        var result = await _productService.AddAsync(product);

        return Ok(result);
    }

    // zaktualizuj produkt lub utwórz go, jeśli nie istnieje
    // do naprawienia - zawsze tworzy nowy element zamiast aktualizować poprzedni
    // PUT api/<ProductController>/5
    [Authorize(Policy = "EmployeeOnly")]
    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody] Product product)
    {
        if (product == null)
        {
            return BadRequest("null product");
        }
        //product.Id = id;
        try
        {
            var current_product = await _productService.GetAsync(id);
            current_product = product;
            current_product.Id = id;
            var result = await _productService.UpdateAsync(current_product);
            return Ok(result);
        }
        catch (Exception ex)
        {
            product.Id = id;
            var result = await _productService.AddAsync(product);
            return Ok(result);
        }
    }

    // usuń produkt - to znaczy, ustaw deleted = true
    // DELETE api/<ProductController>/5
    [Authorize(Policy = "EmployeeOnly")]
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var product = await _productService.GetAsync(id);
        product.Deleted = true;
        var result = await _productService.UpdateAsync(product);

        return Ok(result);
    }
    
    //zaktualizuj produkt
    [Authorize(Policy = "EmployeeOnly")]
    [HttpPatch("{id}")]
    public async Task<ActionResult> PatchName([FromBody] string name, int id)
    {
        var product = await _productService.GetAsync(id);
        product.Name = name;
        var result = await _productService.UpdateAsync(product);

        return Ok(result);
    }
}