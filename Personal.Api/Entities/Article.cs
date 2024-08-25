using System;

namespace Personal.Api.Entities;

public class Article
{
    public int Id { set; get;}
    public required string Title { set; get; }
    public required string Body { set; get; }
    public int AuthorId { set; get;}
    public Author? Author { set; get; }
    public DateOnly? CreateAt { set; get; }
}
