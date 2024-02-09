using System.ComponentModel.DataAnnotations;

namespace Models.Model;

public class Systems
{
    [Key]
    public int Id { get; set; }
    public string? GroupName { get; set; }
    public string? Name { get; set; }
    public string? NameFa { get; set; }
    public bool IsActive { get; set; }
}
