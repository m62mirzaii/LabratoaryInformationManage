using System.ComponentModel.DataAnnotations;

namespace Models.Model
{
    public class RequestUnit
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; } 

    }
}
