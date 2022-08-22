#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
//your namespace
namespace weding_planer.Models;    //must be the same that is on you program file 
//classname
public class Association
{
//* you need to use
//dotnet ef migrations add FirstMigration
//dotnet ef database update
//* only doit after creating you routes with all the info that you need
//this is the primary key
    [Key]
    public int AssociationId { get; set; }
//change the field as needed

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public int UserId { get; set; }
    public User? user {get; set; }
    public int WeddingId { get; set; } 
    public Wedding? wedding {get; set;}



}