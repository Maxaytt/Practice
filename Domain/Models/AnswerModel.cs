namespace Domain.Models;
using System.ComponentModel.DataAnnotations;

public class Answer 
{
    public Guid Id {get; set;}
    public Guid QuestionId {get;set;}
    public Question Question {get; set;}
    public  string  Text {get;set;}

    public bool IsTrue {get; set;}
}