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

namespace Accounting.WebAPI.Controllers
{
    [Route("api/documents")]
    public class DocumentsController : BaseApiControllerWithDatabase
    {
        public DocumentsController(IUnitOfWork unitOfWork, IMapper mapper, ILoggerManager logger) : base(unitOfWork, mapper, logger)
        {
        }

        [HttpGet(template: "people/{personId}/cashes/{cashId}")]
        public async Task<IActionResult> GetAllDocumentsAsync(int personId, int cashId)
        {
            var person = await UnitOfWork.PersonRepository.GetSingelPersonAsync(personId, trackChanges: false);

            if (person == null)
            {
                _logger.LogInfo($"Person with id: {personId} doesn't exist in the database.");

                return NotFound();
            }

            var cash = await UnitOfWork.CashRepository.GetSingelCashAsync(personId, cashId, trackChanges: false);

            if (cash == null)
            {
                _logger.LogInfo($"Cash with id: {cashId} doesn't exist in the database.");

                return NotFound();
            }

            var DocumentsFromDb = await UnitOfWork.DocumentRepository.GetAllDocumentsAsync(personId, cashId, trackChanges: false);

            var documentsDto = _mapper.Map<IEnumerable<DocumentDto>>(DocumentsFromDb);

            return Ok(documentsDto);
        }


        [HttpGet(template: "{id}/people/{personId}/cashes/{cashId}", Name = "GetDocumentAsync")]
        public async Task<IActionResult> GetSingelDocumentAsync(int personId, int cashId, int id)
        {
            var person = await UnitOfWork.PersonRepository.GetSingelPersonAsync(personId, trackChanges: false);

            if (person == null)
            {
                _logger.LogInfo($"Person with id: {personId} doesn't exist in the database.");

                return NotFound();
            }

            var cash = await UnitOfWork.CashRepository.GetByIdAsync(cashId);

            if (cash == null)
            {
                _logger.LogInfo($"Cash with id: {cashId} doesn't exist in the database.");

                return NotFound();
            }

            var documentDb = await UnitOfWork.DocumentRepository.GetSingelDocumentAsync(personId, cashId, id, trackChanges: false);

            if (documentDb == null)
            {
                _logger.LogInfo($"Document with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            var document = _mapper.Map<DocumentDto>(documentDb);

            return Ok(document);
        }


        [HttpPost]
        public async Task<IActionResult> CreateDocumentForPersonAndCashAsync([FromBody] DocumentCreationDto document)
        {
            if (document == null)
            {
                _logger.LogError("DocumentForCreation object sent from client is null.");
                return BadRequest("DocumentForCreation object is null");
            }

            var person = await UnitOfWork.PersonRepository.GetSingelPersonAsync(document.PersonId, trackChanges: false);

            if (person == null)
            {
                _logger.LogInfo($"Company with id: {document.PersonId} doesn't exist in the database.");
                return NotFound();
            }

            var cash = await UnitOfWork.CashRepository.GetByIdAsync(document.CashId);

            if (cash == null)
            {
                _logger.LogInfo($"Cash with id: {document.CashId} doesn't exist in the database.");

                return NotFound();
            }

            var documentEntity = _mapper.Map<Document>(document);

            await UnitOfWork.DocumentRepository.CreateDocumentForPersonAndCashAsync(documentEntity);

            await UnitOfWork.SaveAsync();

            var documentToReturn = _mapper.Map<DocumentDto>(documentEntity);

            return Ok(value: documentToReturn);

            //return CreatedAtRoute("GetDocumentForPersonAndCash", new { document.PersonId, id = documentToReturn.Id }, documentToReturn);
        }


        [HttpDelete(template: "{id}/people/{personId}/cashes/{cashId}")]
        public async Task<IActionResult> DeleteDocumentForPersonAndCashAsync(int personId, int cashId, int id)
        {
            var person = await UnitOfWork.PersonRepository.GetSingelPersonAsync(personId, trackChanges: false);

            if (person == null)
            {
                _logger.LogInfo($"Company with id: {personId} doesn't exist in the database.");
                return NotFound();
            }

            var cash = await UnitOfWork.CashRepository.GetSingelCashAsync(personId, cashId, trackChanges: false);

            if (cash == null)
            {
                _logger.LogInfo($"Cash with id: {cashId} doesn't exist in the database.");

                return NotFound();
            }

            var documentForPersonAndCash = await UnitOfWork.DocumentRepository.GetSingelDocumentAsync(personId, cashId, id, trackChanges: false);

            if (documentForPersonAndCash == null)
            {
                _logger.LogInfo($"Cash with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            await UnitOfWork.DocumentRepository.DeleteDocumentAsync(documentForPersonAndCash);

            await UnitOfWork.SaveAsync();

            return NoContent();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDocumentForPersonAndCashAsync([FromQuery] int id, [FromBody] DocumentUpdateDto document)
        {
            if (document == null)
            {
                _logger.LogError("DocumentForUpdate object sent from client is null.");
                return BadRequest("DocumentForUpdate object is null");
            }

            var person = await UnitOfWork.PersonRepository.GetSingelPersonAsync(document.PersonId, trackChanges: false);

            if (person == null)
            {
                _logger.LogInfo($"Company with id: {document.PersonId} doesn't exist in the database.");
                return NotFound();
            }

            var cash = await UnitOfWork.CashRepository.GetByIdAsync(document.CashId);

            if (cash == null)
            {
                _logger.LogInfo($"Cash with id: {document.CashId} doesn't exist in the database.");

                return NotFound();
            }

            var documentEntity = await UnitOfWork.DocumentRepository.GetByIdAsync(id);

            if (documentEntity == null)
            {
                _logger.LogInfo($"Document with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            _mapper.Map(document, documentEntity);

            await UnitOfWork.SaveAsync();

            return NoContent();
        }
    }
}

