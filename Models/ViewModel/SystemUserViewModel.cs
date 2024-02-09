using System.ComponentModel.DataAnnotations;

namespace Models.ViewModel
{
    public class SystemUserViewModel
    { 
        public int Id { get; set; }
        public string SystemName { get; set; }
        public string SystemNameFa { get; set; }
        public int UserId { get; set; }
        public bool IsAdmin { get; set; }
        public string FullNameUser { get; set; }         
    }
}
