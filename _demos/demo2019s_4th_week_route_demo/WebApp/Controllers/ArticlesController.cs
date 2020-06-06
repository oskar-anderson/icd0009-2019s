using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class ArticlesController : Controller
    {
        public string Index()
        {
            return "Articles Index";
        }

        public string Show(string author, string? article = "default")
        {
            return "Articles Show" + "author " + author + " - " + "article " + article;
        }
        
        public string ShowInt(string author, string? article = "default")
        {
            return "Articles Showint" + "author " + author + " - " + "article " + article;
        }
        
        public string ShowStr(string author, string? article = "default")
        {
            return "Articles Showstr " + "author " + author + " - " + "article " + article;
        }
    }
}