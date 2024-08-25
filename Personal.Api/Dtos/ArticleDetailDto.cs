namespace Personal.Api.Dtos;

public record class ArticleDetailDto(int Id, string Title, string Body, int AuthorId, DateOnly? CreatedAt);