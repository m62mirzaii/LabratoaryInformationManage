
using Models.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModel
{
    public class UserAccess
    {
        [Key]
        public int Id { get; set; }    
        public int UserId { get; set; }        

        [Column("SystemId")]
        public int? SystemsId { get; set; } 

        public Systems Systems { get; set; }
        
    }
}
