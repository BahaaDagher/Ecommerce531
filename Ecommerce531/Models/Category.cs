

using Ecommerce531.CustomValidations;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce531.Models
{
    public class Category
    {
        public int Id { get; set; }
        //[MinLength(3)]
        //[MaxLength(30)]
        [MinMaxCustomeLength(3,30)]
        public string Name { get; set; } = string.Empty; 
        public string? Description { get; set; }
        public bool Status{ get; set; }
    }
}
