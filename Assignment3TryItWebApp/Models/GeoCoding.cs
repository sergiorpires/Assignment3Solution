using System.ComponentModel.DataAnnotations;

namespace Assignment3TryItWebApp.Models
{
    public class GeoCoding
    {
        [Required]
        public string Address { get; set; } = "";
    }
}