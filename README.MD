# MVC .NET Website

Installeer [.NET SDK](https://dotnet.microsoft.com/en-us/download)

Installeer in VS Code de plugins
- C# Dev Kit
- NuGet Package Manager 

## Start een nieuw project

```
mkdir mywebsite
cd mywebsite
dotnet new mvc
```
Build en run nu het project. In de terminal krijg je de link naar je opgestarte server. bv http://localhost:5244/

```
dotnet run
```
Bij iedere wijziging moet je de server stoppen en starten.

Dus in development gebruik je beter de dotnet watch methode.

```
dotnet watch
```

## Connecteren met MySQL en MVC in .Net

Installeer MySql.Data.dll via NuGet package manager.

Hieronder een voorbeeld model dat mySql gebruikt.

### ArticleModel.cs
```
using MySql.Data.MySqlClient;

namespace DotNet.Models;

public class Article
{
    public int Id { get; set; }
    public string Slug { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    private static string connectionString = "Server=localhost;Database=3306;Uid=root;Pwd=secret;Database=gardenblog;";

    public static List<Article> getArticles()
    {
        List<Article> articles = new List<Article>();

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            string sql = "SELECT * FROM articles";

            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Article article = new Article
                        {
                            Id = reader.GetInt32("id"),
                            Slug = reader.GetString("slug"),
                            Title = reader.GetString("title"),
                            Content = reader.GetString("content")
                        };

                        articles.Add(article);
                    }
                }
            }
            connection.Close();
        }
        return articles;
    }
}
```
### ArticleController.cs
```
using Microsoft.AspNetCore.Mvc;
using DotNet.Models;

namespace DotNet.Controllers;

public class ArticlesController : Controller
{

    public IActionResult Index()
    {
        ViewData["Articles"] = Article.getArticles();
        return View();
    }
}
```

### ./Views/Articles/Index.cshtml

.Net maakt gebruik van Razor als templating engine (.cshtml). [Razor syntax reference for ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/mvc/views/razor?view=aspnetcore-7.0)

```
@using DotNet.Models

@{
    ViewData["Title"] = "Articles";
    var myarticles = ViewData["articles"] as List<Article>;
}

<h1 >Tuinblog</h1>
@foreach (var article in myarticles)
{
    <li><a href="/articles/detail/@article.Id">@article.Title</a></li>
}
```
