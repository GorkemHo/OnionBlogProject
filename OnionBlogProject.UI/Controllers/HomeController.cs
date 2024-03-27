using Microsoft.AspNetCore.Mvc;
using OnionBlogProject.Application.Services.PostService;
using OnionBlogProject.UI.Models;
using System.Diagnostics;

namespace OnionBlogProject.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostService _postService;

        public HomeController(IPostService postService)
        {
            _postService = postService;
        }

        public async Task<IActionResult> Index()
        {
            var posts = await _postService.GetPostsForMembers();
            return View(posts);
        }


    }
}
