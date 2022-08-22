#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//your namespace
namespace weding_planer.Models;    //must be the same that is on you program file 
//classnamepublic class Users

//* you need to use
//dotnet ef migrations add FirstMigration
//dotnet ef database update
//* only doit after creating you routes with all the info that you need
public class User{
//this is the primary key
    [Key]
    public int UserId { get; set; }
//change the field as needed
    [Required]
    [MinLength(2,ErrorMessage ="must be at least 2character long ")]
    [Display( Name ="First name")]
    public string F_name { get; set; }

    [Required]
    [MinLength(2,ErrorMessage ="must be at least 2character long ")]
    [Display(Name = "Last name")]
    public string L_name { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [MinLength(8,ErrorMessage ="must be at least 8 character long ")]
    [DataType(DataType.Password)]
    public string Password {get; set;}
    
    [NotMapped]
    [Compare("Password")]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    public string Confirm_password {get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    
// for one to many
    public List<Wedding> createdWeddings {get; set;} = new List<Wedding>();

    public List<Association> attendingWedding {set; get;} = new List<Association>();

    // public string fullName(){
    //     return $"{F_name} {L_name}";
    // }
}