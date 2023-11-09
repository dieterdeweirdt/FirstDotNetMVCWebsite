using Microsoft.AspNetCore.Mvc;
using DotNet.Models;
using System.Diagnostics;

namespace DotNet.Controllers;

public class ArticlesController : Controller
{

    public IActionResult Index()
    {
        Debug.WriteLine("Index method in ArticlesController");

        ViewData["Articles"] = Article.GetAll();
        return View();
    }

    public IActionResult Detail(int id)
    {
        var article = Article.GetById(id);
        if (article == null)
        {
            return NotFound();
        }
        ViewData["Article"] = article;
        ViewData["Comments"] = Comment.getComments(id);
        return View();
    }
}

    

