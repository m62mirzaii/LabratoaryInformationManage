﻿
namespace Models.ViewModel
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; } 
        public string? Password { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Phone { get; set; }
        
        public string? Address { get; set; }
        public string? BankAccountNo { get; set; }
        
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }

    }
}
