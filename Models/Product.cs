using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarioAuth.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public byte[]? Image { get; set; }
        public string Price { get; set; }

        [Display(AutoGenerateField = false)]
        public int CatalogSection{  get; set; }

        [ForeignKey("CatalogSection")]
        public Catalog Catalog { get; set; }
    }
}
