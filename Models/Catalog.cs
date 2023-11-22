using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarioAuth.Models
{
    public class Catalog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[]? Image { get; set; }

        public override string ToString()
        {
            return Name;
        }

    }
}
