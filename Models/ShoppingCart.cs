using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
namespace MarioAuth.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }


        [ForeignKey("User")]
        public string UserId { get; set; }

        public int Quantity { get; set; }

        public System.DateTime DateCreated { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
        public IdentityUser User { get; set; }
    }
}
