using System.ComponentModel.DataAnnotations;

namespace Personal.Api.Dtos;

public record class UpdateArticleDto(    
    [Required]
    [StringLength(100)]
    string Title, 
    [Required]
    [StringLength(500)]
    string Body,
    int AuthorId,
    DateOnly? CreatedAt);
