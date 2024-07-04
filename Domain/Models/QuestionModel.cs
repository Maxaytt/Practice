namespace Domain.Models;
using System.ComponentModel.DataAnnotations;

public class Question 
{
    [Required]
    public int Id  {get; set;}
    public required string Text {get; set;}

    
    public required ICollection<Answer> Answers {get; set;}
}