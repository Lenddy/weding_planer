#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
//your namespace
namespace weding_planer.Models;    //must be the same that is on you program file 
//classname
public class Wedding
{
//* you need to use
//dotnet ef migrations add FirstMigration
//dotnet ef database update
//* only doit after creating you routes with all the info that you need
//this is the primary key
    [Key]
    public int WeddingId { get; set; }
//change the field as needed
    [Required]
    [MinLength(2)]
    [Display(Name = "Wedder one")]
    public string person1 { get; set; }

    [Required]
    [MinLength(2)]
    [Display(Name = "Wedder two")] 
    public string person2 { get; set; }

    [Required]
    [DataType(DataType.Date)]
    // custom validation 
    [FutureDate]
    public  DateTime? Date{ get; set; }
    [Required]
    [MinLength(8)]
    [Display(Name = "Wedding address")] 
    public string Address { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    // for one to many
    public int UserId {get; set;}
    public User? weddingCreator {get; set;}

    public List<Association> Guest {get; set;} = new List<Association>();

}