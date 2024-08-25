namespace Personal.Api.Dtos;

public record class ArticleSummaryDto(int Id, string Title, string Body, string Author, DateOnly? CreatedAt);
