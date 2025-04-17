

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pexel.Core.Entities
{
    public class Photo
    {


        [Key]
        public int Id { get; set; }

        public string ImageName { get; set; }

        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Productes Product { get; set; }


    }
}
