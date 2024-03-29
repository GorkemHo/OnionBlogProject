using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnionBlogProject.Application.Models.Dtos.AuthorDto;
using OnionBlogProject.Application.Models.Dtos.GenreDto;
using OnionBlogProject.Application.Services.AuthorService;
using OnionBlogProject.Application.Services.GenreService;

namespace OnionBlogProject.UI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class GenreController : Controller
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        public async Task<IActionResult> Index()
        {
            var genreList = await _genreService.GetGenres();

            return View(genreList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGenreDto model)
        {
            if (ModelState.IsValid)
            {
                bool result = await _genreService.IsGenreExist(model.Name);

                if (!result)
                {
                    TempData["Success"] = $"{model.Name} Başrıyla Oluşturuldu.";
                    await _genreService.Create(model);
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Warning"] = $"{model.Name} Zaten var.";
                    return View(model);
                }
            }
            else
            {
                TempData["Error"] = $"{model.Name} Eklenemedi.";
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var genre = await _genreService.GetById(id);
            return View(genre);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateGenreDto model)
        {
            if (ModelState.IsValid)
            {
                await _genreService.Update(model);
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Error"] = $"{model.Name} Güncellenemedi.";
                return View(model);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _genreService.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
