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

    public IActionResult Detail(int id)
    {
        var article = Article.getArticleById(id);
        if (article == null)
        {
            return NotFound();
        }
        ViewData["Article"] = article;
        ViewData["Comments"] = Comment.getComments(id);
        return View();
    }
}

    

