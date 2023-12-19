using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarioAuth.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public string Phone { get; set; }
        public string OrderList { get; set; }
        public string DeliveryAddress { get; set; }

        public string Comment { get; set;}
        public IdentityUser User { get; set; }
    }
}
