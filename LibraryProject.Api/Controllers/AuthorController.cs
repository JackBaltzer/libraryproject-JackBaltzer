using LibraryProject.Api.DTOs;
using LibraryProject.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace LibraryProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                List<AuthorResponse> authorResponses = _authorService.GetAllAuthors();

                if (authorResponses == null)
                {
                    return Problem("Got no data, not even an empty list, this is unexpected");
                }

                if (authorResponses.Count == 0)
                {
                    return NoContent();
                }

                return Ok(authorResponses);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
