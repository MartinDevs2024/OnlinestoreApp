using System.ComponentModel.DataAnnotations;

namespace EcommerceApp.Models
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
