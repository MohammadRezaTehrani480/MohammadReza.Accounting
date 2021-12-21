using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure;
using Accounting.WebAPI.Data;
using AutoMapper;
using Accounting.WebAPI.Contracts;
using Accounting.Shared.ViewModels.DocumentsViewModels;
using Accounting.WebAPI.Entities;
using Microsoft.Extensions.Logging;

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
        public async Task<IActionResult> GetAllDocumentsAsync()
        {
            try
            {
                var documents = await UnitOfWork.DocumentRepository.GetAllUdemyAsync();

                var documentsDTO = _mapper.Map<IList<DocumentDTO>>(documents);

                return Ok(documentsDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(GetAllDocumentsAsync)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later.");
            }
        }


        [HttpGet(template: "{id:int}", Name = "GetDocumentAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSingelDocumentAsync(int id)
        {
            try
            {
                var document = await UnitOfWork.DocumentRepository.GetUdemyAsync(q => q.Id == id, new List<string> { "Cash", "AccountSide", "DocType" });

                if (document == null)
                {
                    _logger.LogError($"Document with id: {id} doesn't exist in the database.");
                    return NotFound();
                }

                var documentDTO = _mapper.Map<DocumentDTO>(document);

                return Ok(documentDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(GetSingelDocumentAsync)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later.");
            }
        }

        #region *
        //[HttpPost]
        //public async Task<IActionResult> CreateDocumentForPersonAndCashAsync([FromBody] DocumentCreationDto document)
        //{
        //    if (document == null)
        //    {
        //        _logger.LogError("DocumentForCreation object sent from client is null.");
        //        return BadRequest("DocumentForCreation object is null");
        //    }

        //    var person = await UnitOfWork.PersonRepository.GetSingelPersonAsync(document.PersonId, trackChanges: false);

        //    if (person == null)
        //    {
        //        _logger.LogError($"Company with id: {document.PersonId} doesn't exist in the database.");
        //        return NotFound();
        //    }

        //    var cash = await UnitOfWork.CashRepository.GetByIdAsync(document.CashId);

        //    if (cash == null)
        //    {
        //        _logger.LogError($"Cash with id: {document.CashId} doesn't exist in the database.");

        //        return NotFound();
        //    }

        //    var documentEntity = _mapper.Map<Document>(document);

        //    await UnitOfWork.DocumentRepository.CreateDocumentForPersonAndCashAsync(documentEntity);

        //    await UnitOfWork.SaveAsync();

        //    var documentToReturn = _mapper.Map<DocumentDTO>(documentEntity);

        //    return Ok(value: documentToReturn);

        //    //return CreatedAtRoute("GetDocumentForPersonAndCash", new { document.PersonId, id = documentToReturn.Id }, documentToReturn);
        //}


        //[HttpDelete(template: "{id}/people/{personId}/cashes/{cashId}")]
        //public async Task<IActionResult> DeleteDocumentForPersonAndCashAsync(int personId, int cashId, int id)
        //{
        //    var person = await UnitOfWork.PersonRepository.GetSingelPersonAsync(personId, trackChanges: false);

        //    if (person == null)
        //    {
        //        _logger.LogError($"Company with id: {personId} doesn't exist in the database.");
        //        return NotFound();
        //    }

        //    var cash = await UnitOfWork.CashRepository.GetSingelCashAsync(personId, cashId, trackChanges: false);

        //    if (cash == null)
        //    {
        //        _logger.LogError($"Cash with id: {cashId} doesn't exist in the database.");

        //        return NotFound();
        //    }

        //    var documentForPersonAndCash = await UnitOfWork.DocumentRepository.GetSingelDocumentAsync(personId, cashId, id, trackChanges: false);

        //    if (documentForPersonAndCash == null)
        //    {
        //        _logger.LogError($"Cash with id: {id} doesn't exist in the database.");
        //        return NotFound();
        //    }

        //    await UnitOfWork.DocumentRepository.DeleteDocumentAsync(documentForPersonAndCash);

        //    await UnitOfWork.SaveAsync();

        //    return NoContent();
        //}


        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateDocumentForPersonAndCashAsync([FromQuery] int id, [FromBody] DocumentUpdateDto document)
        //{
        //    if (document == null)
        //    {
        //        _logger.LogError("DocumentForUpdate object sent from client is null.");
        //        return BadRequest("DocumentForUpdate object is null");
        //    }

        //    var person = await UnitOfWork.PersonRepository.GetSingelPersonAsync(document.PersonId, trackChanges: false);

        //    if (person == null)
        //    {
        //        _logger.LogError($"Company with id: {document.PersonId} doesn't exist in the database.");
        //        return NotFound();
        //    }

        //    var cash = await UnitOfWork.CashRepository.GetByIdAsync(document.CashId);

        //    if (cash == null)
        //    {
        //        _logger.LogError($"Cash with id: {document.CashId} doesn't exist in the database.");

        //        return NotFound();
        //    }

        //    var documentEntity = await UnitOfWork.DocumentRepository.GetByIdAsync(id);

        //    if (documentEntity == null)
        //    {
        //        _logger.LogError($"Document with id: {id} doesn't exist in the database.");
        //        return NotFound();
        //    }

        //    _mapper.Map(document, documentEntity);

        //    await UnitOfWork.SaveAsync();

        //    return NoContent();
        //}
        #endregion *
    }
}

