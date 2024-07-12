namespace Domain.Models;
using System.ComponentModel.DataAnnotations;

public class Question 
{
    
    public int Id  {get; set;}
    public string Text {get; set;}

    
    public  ICollection<Answer> Answers {get; set;}
}