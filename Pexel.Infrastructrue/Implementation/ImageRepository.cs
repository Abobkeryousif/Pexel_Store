using Pexel.Application.Contracts.Interfaces;
using Pexel.Core.Entities;


namespace Pexel.Infrastructrue.Implementation
{
    public class ImageRepository : GenericRepository<Photo>, IImageRepository
    {
        public ImageRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
