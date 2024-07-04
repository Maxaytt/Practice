namespace Domain.Models;
using System.ComponentModel.DataAnnotations;

public class Answer 
{
    public int? Id {get; set;}
    public int? QuestionId {get;set;}
    public Question? Question {get; set;}
    public required string  Text {get;set;}

    public bool IsTrue {get; set;}
}