using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pexel.Application.Contracts.Interfaces;
using Pexel.Application.Contracts.Services;
using Pexel.Core.DTOs.Product;
using Pexel.Core.DTOs.Products;
using Pexel.Core.Entities;

namespace Pexel.Infrastructrue.Implementation
{
    public class ProductRepository : GenericRepository<Productes>, IProductRepository
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;
        private readonly IImageRepository _imageRepository;
        public ProductRepository(IImageRepository imageRepository,IMapper mapper,IImageService imageService,ApplicationDbContext context) : base(context)
        {
            _mapper = mapper;
            _imageService = imageService;
            dbContext = context;
            _imageRepository = imageRepository;
        }

        public async Task<bool> AddAsync(AddProductDto productDto)
        {

            if (productDto == null) return false;

            var product = _mapper.Map<Productes>(productDto);
            await dbContext.Products.AddAsync(product);
            await dbContext.SaveChangesAsync(); 

            var imagePaths = await _imageService.AddImageAsync(productDto.Photos, product.Name);
        
            var images = imagePaths.Select(path => new Photo
            {
           
                ImageName = path,
                ProductId = product.ProductId
            }).ToList();
             return true;

        }

        
        }
    }

