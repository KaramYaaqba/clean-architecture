using System;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Personal.Api.Dtos;
using Personal.Api.Entities;

namespace Personal.Api.Mapping;

public static class Mapping
{
    public static Article ToArticleEntity(this ArticleDetailDto articleDto){
        return new Article() {
            Id = articleDto.Id,
            Title = articleDto.Title,
            Body = articleDto.Body,
            AuthorId =  articleDto.AuthorId,
            CreateAt = articleDto.CreatedAt
        };
    }

    public static Article ToArticleEntity(this CreateArticleDto articleDto){
    return new Article() {
        Title = articleDto.Title,
        Body = articleDto.Body,
        AuthorId =  articleDto.AuthorId,
        CreateAt = articleDto.CreatedAt
    };
    }

    public static Article ToArticleEntity(this UpdateArticleDto articleDto, int id){
        return new Article() {
            Id = id,
            Title = articleDto.Title,
            Body = articleDto.Body,
            AuthorId =  articleDto.AuthorId,
            CreateAt = articleDto.CreatedAt
        };
    }


    public static ArticleDetailDto ToArticleDetailDto(this Article article){
        return new ArticleDetailDto(
            article.Id, 
            article.Title, 
            article.Body, 
            article.AuthorId, 
            article.CreateAt);
    }

    public static ArticleSummaryDto ToArticleSummaryDto(this Article article){
        return new ArticleSummaryDto(
            article.Id, 
            article.Title, 
            article.Body, 
            article.Author!.Name, 
            article.CreateAt);
    }
}
