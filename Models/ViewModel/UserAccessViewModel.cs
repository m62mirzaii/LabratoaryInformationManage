namespace Models.ViewModel
{
    public class UserAccessViewModel
    {
        public int Id { get; set; }
        public int? SystemId { get; set; }
        public string GroupName { get; set; }
        public string Name { get; set; }
        public string NameFa { get; set; }
        public int UserId { get; set; }
        public bool IsAdmin { get; set; }
        public string FullNameUser { get; set; }
    }
}
