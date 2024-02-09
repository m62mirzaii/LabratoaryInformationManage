using System.ComponentModel.DataAnnotations;

namespace Models.Model
{
    public class RequestUser
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; } 

    }
}
