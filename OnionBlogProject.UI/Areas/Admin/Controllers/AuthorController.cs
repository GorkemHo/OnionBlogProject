using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnionBlogProject.Application.Models.Dtos.AuthorDto;
using OnionBlogProject.Application.Services.AuthorService;

namespace OnionBlogProject.UI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        public async Task<IActionResult> Index()
        {
            var authorList = await _authorService.GetAuthors();

            return View(authorList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAuthorDto createAuthorDto)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Girdiğiniz verileri kontrol ediniz.";
                return View(createAuthorDto);
            }

            bool result = await _authorService.IsAuthorExist(createAuthorDto.FirstName, createAuthorDto.LastName);

            if (!result)
            {
                await _authorService.Create(createAuthorDto);
                TempData["Success"] = $"{createAuthorDto.FirstName} {createAuthorDto.LastName} Oluşturuldu.";
            }
            else
            {
                TempData["Error"] = "Bu isim soyisim de kayıtlı bir yazar var.";
            }



            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            var author = await _authorService.GetById(id);
            return View(author);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var author = await _authorService.GetById(id);
            return View(author);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateAuthorDto updateAuthorDto)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Bilgilerinizi Kontrol ediniz.";
                return View(updateAuthorDto);
            }

           await _authorService.Update(updateAuthorDto);
            TempData["Success"] = $"{updateAuthorDto.FirstName} {updateAuthorDto.LastName} için güncelleme işlemi başarılı.";

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Delete(int id)
        {
            await _authorService.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
