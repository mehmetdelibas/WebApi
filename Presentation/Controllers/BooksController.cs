using Entities.DataTransferObject;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public BooksController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooksAsync()
        {

            var books = await _manager.BookServices.GetAllBookAsync(false);
            return Ok(books);

        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOneBookAsync([FromRoute(Name = "id")] int id)
        {



            var book = await _manager
            .BookServices
            .GetOneBookByIdAsync(id, false);

            return Ok(book);



        }

        [HttpPost]
        public async Task<IActionResult> CreateOneBookAsync([FromBody] BookDtoForInsertion bookDto)
        {


            if (bookDto is null)
                return BadRequest();

            if(!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            var book = await _manager.BookServices.CreatOneBookAsync(bookDto);

            return StatusCode(201, book); 
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateOneBookAsync([FromRoute(Name = "id")] int id,
            [FromBody] BookDtoForUpdate bookDto)
        {

            if (bookDto is null)
                return BadRequest();

            if(!ModelState.IsValid)
                return UnprocessableEntity(ModelState);


            await _manager.BookServices.UpdateOneBookAsync(id, bookDto, false);
            return NoContent();

        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOneBookAsync([FromRoute(Name = "id")] int id)
        {
            await _manager.BookServices.DeleteOneBookAsync(id, false);
            return NoContent();

        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> PartiallyUpdateOneBookAsync([FromRoute(Name = "id")] int id,
            [FromBody] JsonPatchDocument<BookDtoForUpdate> bookPatch)
        {
            if (bookPatch is null)
                return BadRequest();

            var result = await _manager.BookServices.GetOneBookForPatchAsync(id, false);

            bookPatch.ApplyTo(result.bookDtoForUpdate, ModelState);

            TryValidateModel(result.bookDtoForUpdate);

            if(!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            await _manager.BookServices.SaveChangesForPatchesAsync(result.bookDtoForUpdate, result.book);

            return NoContent();

        }

    }
}
