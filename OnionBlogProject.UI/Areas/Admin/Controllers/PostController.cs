using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnionBlogProject.Application.Models.Dtos.GenreDto;
using OnionBlogProject.Application.Models.Dtos.PostDto;
using OnionBlogProject.Application.Services.GenreService;
using OnionBlogProject.Application.Services.PostService;

namespace OnionBlogProject.UI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class PostController : Controller
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        public async Task<IActionResult> Index()
        {
            var postList = await _postService.GetPosts();

            return View(postList);
        }

        public async Task<IActionResult> Create()
        {
            var vm = await _postService.CreatePost();
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreatePostDto model)
        {
            if (ModelState.IsValid)
            {
                await _postService.Create(model);
                TempData["Success"] = $"{model.Title} Başarıyla Oluşturuldu.";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Error"] = $"HATA! {model.Title} Eklenemedi.";
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var post = await _postService.GetById(id);
            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdatePostDto model)
        {
            if (ModelState.IsValid)
            {
                await _postService.Update(model);
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Error"] = $"{model.Title} Güncellenemedi.";
                return View(model);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _postService.Delete(id);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            return View(await _postService.GetPostDetailsVm(id));
        }
    }
}
