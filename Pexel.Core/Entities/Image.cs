

namespace Pexel.Core.Entities
{
    public class Image
    {
        public int ImageId { get; set; }
        public string ImageName { get; set; }
        public int ProductId { get; set; }

        public Productes Product { get; set; }
    }
}
