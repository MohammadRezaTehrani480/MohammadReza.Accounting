using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure;
using Accounting.WebAPI.Data;
using AutoMapper;
using Accounting.Shared.ViewModels.DocumentsViewModels;
using Accounting.WebAPI.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace Accounting.WebAPI.Controllers
{
    public class DocumentsController : BaseApiControllerWithDatabase
    {
        public DocumentsController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<DocumentsController> logger) : base(unitOfWork, mapper)
        {
            _logger = logger;
        }

        private readonly ILogger<DocumentsController> _logger;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllDocumentsAsync([FromQuery] RequestParams requestParams)
        {
            var documents = await UnitOfWork.DocumentRepository.GetAllUdemyPagingAsync(requestParams);

            var documentsDTO = _mapper.Map<IList<DocumentDTO>>(documents);

            return Ok(documentsDTO);
        }


        [HttpGet(template: "{id:int}", Name = "GetSingelDocumentAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSingelDocumentAsync(int id)
        {
            var document = await UnitOfWork.DocumentRepository.GetUdemyAsync(q => q.Id == id, new List<string> { "Cash", "Person", "DocType" });

            if (document == null)
            {
                _logger.LogError($"Document with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            var documentDTO = _mapper.Map<DocumentDTO>(document);

            return Ok(documentDTO);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateDocument([FromBody] DocumentCreationDTO documentDTO)
        {
            if (documentDTO == null)
            {
                _logger.LogError("DocumentForCreation object sent from client is null.");
                return BadRequest("DocumentForCreation object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the DocumentCreationDto object");
                return BadRequest(ModelState);
            }

            var document = _mapper.Map<Document>(documentDTO);

            await UnitOfWork.DocumentRepository.InsertAsync(document);

            await UnitOfWork.SaveAsync();

            return CreatedAtRoute("GetSingelDocumentAsync", new { id = document.Id }, document);
        }

        [Authorize]
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateDocumentAsync(int id, [FromBody] DocumentUpdateDTO documentDTO)
        {
            if (documentDTO == null)
            {
                _logger.LogError("DocumentForUpdate object sent from client is null.");
                return BadRequest("DocumentForUpdate object is null");
            }

            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError("Invalid model state for the DocumentUpdateDto object");
                return BadRequest(ModelState);
            }

            var document = await UnitOfWork.DocumentRepository.GetUdemyAsync(q => q.Id == id);

            if (document == null)
            {
                _logger.LogError($"Document with id: {id} doesn't exist in the database.");
                return BadRequest("Submitted data is invalid!");
            }

            _mapper.Map(documentDTO, document);

            UnitOfWork.DocumentRepository.Update(document);

            await UnitOfWork.SaveAsync();

            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteDocumentAsync(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"Document with Id: {id} does not exist in the database!");

                return BadRequest();
            }

            var document = await UnitOfWork.DocumentRepository.GetUdemyAsync(q => q.Id == id);
            if (document == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteDocumentAsync)}");
                return BadRequest("Submitted data is invalid");
            }

            await UnitOfWork.DocumentRepository.DeleteByIdAsync(id);
            await UnitOfWork.SaveAsync();

            return NoContent();
        }
    }
}

