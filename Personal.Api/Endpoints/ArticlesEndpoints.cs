using System;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Personal.Api.Data;
using Personal.Api.Dtos;
using Personal.Api.Entities;
using Personal.Api.Mapping;

namespace Personal.Api.Endpoints;

public static class ArticlesEndpoints
{
    const string CreateArticleEndpointName = "CreateArticle";
    public static RouteGroupBuilder MapArticleEndpoints(this WebApplication app){

        var articleGroup = app.MapGroup("articles").WithParameterValidation();
        // GET articles
        articleGroup.MapGet("/", (BlogDBContext dbContext) => {
            return Results.Ok(dbContext.Articles.Include(article => article.Author).Select(article => article.ToArticleSummaryDto()).AsNoTracking());
        });

        // GET articles/1
        articleGroup.MapGet("/{id}", (int id, BlogDBContext dbContext) => {
            var article = dbContext.Articles.Find(id);
            return article is null ? Results.NotFound() : Results.Ok(article.ToArticleDetailDto());
            })
            
        .WithName(CreateArticleEndpointName);

        // POST article
        articleGroup.MapPost("/", (CreateArticleDto newArticle, BlogDBContext dbConext) => {
            Article article = newArticle.ToArticleEntity();
            article.Author = dbConext.Authors.Find(newArticle.AuthorId);
            dbConext.Articles.Add(article);
            dbConext.SaveChanges();
            return Results.CreatedAtRoute(CreateArticleEndpointName, new {Id = article.Id}, article.ToArticleDetailDto());
        })
        .WithParameterValidation();

        // PUT articles/1
        articleGroup.MapPut("/{id}", (int id, UpdateArticleDto updatedArticle, BlogDBContext dbContext) => {
            var article = dbContext.Articles.Find(id);
            if(article is null) return Results.NotFound();
            dbContext.Entry(article).CurrentValues.SetValues(updatedArticle.ToArticleEntity(id));
            dbContext.SaveChanges();
            return Results.NoContent();
        });

        // DELETE articles/1
        articleGroup.MapDelete("/{id}", (int id, BlogDBContext dbContext) => {
            dbContext.Articles.Where(article => article.Id == id).ExecuteDelete();
            return Results.NoContent();
        });
        return articleGroup;
    }
}
