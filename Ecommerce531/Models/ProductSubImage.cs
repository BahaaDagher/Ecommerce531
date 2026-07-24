using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

using System.Runtime.InteropServices;

namespace Ecommerce531.Models
{
    [PrimaryKey(nameof(ProductId) , nameof(Img))]
    public class ProductSubImage
    {
        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }
        public string Img { get; set; }
    }
}
