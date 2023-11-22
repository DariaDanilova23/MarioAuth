using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarioAuth.Models
{
    public class News
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public byte[]? Image { get; set; }
        public string Title { get; set; }

        [DataType(DataType.MultilineText)] 
        public string Description { get; set; }
    }
}
