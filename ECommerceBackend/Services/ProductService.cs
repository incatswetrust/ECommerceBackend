using AutoMapper;
using ECommerceBackend.Middleware;
using ECommerceBackend.Repositories;

namespace ECommerceBackend.Services;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllProductsAsync();
    Task<ProductDto> GetProductByIdAsync(int id);
    Task AddProductAsync(ProductDto productDto);
    Task UpdateProductAsync(int id, ProductDto productDto);
    Task DeleteProductAsync(int id);
}

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
    {
        var products = await _productRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<ProductDto>>(products);
    }

    public async Task<ProductDto> GetProductByIdAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null)
        {
            return null;
        }
        return _mapper.Map<ProductDto>(product);
    }

    public async Task AddProductAsync(ProductDto productDto)
    {
        var product = _mapper.Map<Product>(productDto);
        await _productRepository.AddAsync(product);
    }

    public async Task UpdateProductAsync(int id, ProductDto productDto)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null)
        {
            return;
        }

        _mapper.Map(productDto, product);
        await _productRepository.UpdateAsync(product);
    }

    public async Task DeleteProductAsync(int id)
    {
        await _productRepository.DeleteAsync(id);
    }
}
