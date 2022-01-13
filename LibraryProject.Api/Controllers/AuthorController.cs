using LibraryProject.Api.DTOs;
using LibraryProject.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<AuthorResponse> authorResponses = await _authorService.GetAllAuthors();

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

        [HttpGet("{authorId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById([FromRoute] int authorId)
        {
            try
            {
                AuthorResponse authorResponse = await _authorService.GetAuthorById(authorId);

                if (authorResponse == null)
                {
                    return NotFound();
                }

                return Ok(authorResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] AuthorRequest newAuthor)
        {
            try
            {
                AuthorResponse authorResponse = await _authorService.CreateAuthor(newAuthor);

                if (authorResponse == null)
                {
                    return Problem("Author was NOT created, something went wrong");
                }

                return Ok(authorResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut("{authorId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromRoute] int authorId, [FromBody] AuthorRequest updateAuthor)
        {
            try
            {
                AuthorResponse authorResponse = await _authorService.UpdateAuthor(authorId, updateAuthor);

                if (authorResponse == null)
                {
                    return NotFound();
                }

                return Ok(authorResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete("{authorId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] int authorId)
        {
            try
            {
                AuthorResponse authorResponse = await _authorService.DeleteAuthor(authorId);

                if (authorResponse == null)
                {
                    return NotFound();
                }

                return Ok(authorResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

    }
}
