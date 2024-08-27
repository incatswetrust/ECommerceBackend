using AutoMapper;
using ECommerceBackend.Middleware;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ProductsController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
    {
        var products = await _context.Products.ToListAsync();
        var productDtos = _mapper.Map<List<ProductDto>>(products);
        return Ok(productDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        var productDto = _mapper.Map<ProductDto>(product);
        return Ok(productDto);
    }

    [HttpPost]
    public async Task<ActionResult<ProductDto>> CreateProduct(ProductDto productDto)
    {
        var product = _mapper.Map<Product>(productDto);
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, productDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, ProductDto productDto)
    {
        if (id != productDto.Id)
        {
            return BadRequest();
        }

        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        _mapper.Map(productDto, product);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
