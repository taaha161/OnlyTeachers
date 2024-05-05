using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
namespace MvcMovie.Models;

public class Post
{
    // user ID from AspNetUser table.
    public string? OwnerID { get; set; }
    public int Id { get; set; }
    public string? Title { get; set; }
    
    [Display(Name = "Post Date")]
    [DataType(DataType.Date)]  
      public DateTime PostDate { get; set; }
    public string? content { get; set; }
     [Column(TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }
    [Display(Name = "Subscribers")]
    public int subscribers {get; set;}
     public ContactStatus Status { get; set; }
}

public enum ContactStatus
{
    Submitted,
    Approved,
    Rejected
}
public class AppRole : IdentityRole
{
    public AppRole() : base() { }
    public AppRole(string name) : base(name) { }
    // extra properties here 
}
public class AppUser : IdentityUser
{
    //add your custom properties which have not included in IdentityUser before
    public string role { get; set; }  
}