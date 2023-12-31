﻿using ElevenNoteWebApp_2.Server.Services.Category;
using ElevenNoteWebApp_2.Shared.Models.Category;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElevenNoteWebApp_2.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService; 
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreate model)
        {
            if (model == null || !ModelState.IsValid) return BadRequest();

            bool wasSuccessful = await _categoryService.CreateCategoryAsync(model);

            if (wasSuccessful) return Ok();
            return UnprocessableEntity();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Category(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);

            if (category == null) return NotFound();
            return Ok(category);
        }



    }
}
