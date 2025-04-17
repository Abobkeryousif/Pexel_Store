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
            await dbContext.SaveChangesAsync(); // توليد ProductId

            var imagePaths = await _imageService.AddImageAsync(productDto.Photos, product.Name);
        
            var images = imagePaths.Select(path => new Photo
            {
           
                ImageName = path,
                ProductId = product.ProductId
            }).ToList();
             return true;

        }

        public async Task DeleteAsyncs(Productes product)
        {
            var photo = await dbContext.Photo.Where(x => x.ProductId == product.ProductId).ToListAsync();

            foreach (var item in photo)
            {
                _imageService.DeleteImageAsync(item.ImageName);
            }

            dbContext.Products.Remove(product);
            await dbContext.SaveChangesAsync();

        }

        public async Task<bool> UpdateAsync(UpdateProductDto updateProductDto)
        {
            if (updateProductDto is null)
            {
                return false;
            }
            var FindProduct = await dbContext.Products.Include(m => m.Category)
                .Include(m => m.images).FirstOrDefaultAsync(x => x.ProductId == updateProductDto.Id);
            if (FindProduct is null)
            {
                return false;
            }
            _mapper.Map<AddProductDto>(FindProduct);
            var photo = await dbContext.Photo.Where(x => x.ProductId == updateProductDto.Id).ToListAsync();

            foreach (var item in photo)
            {
                _imageService.DeleteImageAsync(item.ImageName);
            }
            dbContext.Photo.RemoveRange(photo);
            var Imagepath = await _imageService.AddImageAsync(updateProductDto.Photos, updateProductDto.Name);
            var ListPhoto = Imagepath.Select(path =>
            new Photo
            {
                ImageName = path,
                ProductId = updateProductDto.Id,
            }).ToList();

            await _imageRepository.AddRange(ListPhoto);
            await dbContext.SaveChangesAsync();
            return true;
        }
    }
}
