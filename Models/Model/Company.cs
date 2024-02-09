using System.ComponentModel.DataAnnotations;

namespace Models.Model
{
    public class Company
    {
        [Key]
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; } 
    }
}
