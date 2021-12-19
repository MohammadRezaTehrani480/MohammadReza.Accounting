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
using Accounting.Shared.ViewModels.CashViewModels;
using Accounting.WebAPI.Entities;

namespace Accounting.WebAPI.Controllers
{
    [Route("api/realPeople/{realPersonId}/cashes")]
    public class CashesController : BaseApiControllerWithDatabase
    {
        public CashesController(IUnitOfWork unitOfWork, IMapper mapper, ILoggerManager logger) : base(unitOfWork, mapper, logger)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCashesForSingelRealPersonAsync(int realPersonId, bool eager = false)
        {
            var realPerson = await UnitOfWork.PersonRepository.GetSingelRealPersonAsync(realPersonId,eager:eager);

            if (realPerson == null)
            {
                _logger.LogInfo($"RealPerson with id: {realPersonId} doesn't exist in the database.");

                return NotFound();
            }

            var cashesFromDb = await UnitOfWork.CashRepository.GetAllCashesAsync(realPersonId, trackChanges: false);

            var CashesDto = _mapper.Map<IEnumerable<CashDto>>(cashesFromDb);

            return Ok(CashesDto);
        }


        [HttpGet("{id}", Name = "GetCashForRealPerson")]
        public async Task<IActionResult> GetSingelCashForSingelRealPersonAsync(int realPersonId, int id, bool eager = false)
        {
            var realPerson = await UnitOfWork.PersonRepository.GetSingelRealPersonAsync(realPersonId,eager:eager);

            if (realPerson == null)
            {
                _logger.LogInfo($"RealPerson with id: {realPersonId} doesn't exist in the database.");
                return NotFound();
            }
            var cashDb = await UnitOfWork.CashRepository.GetSingelCashAsync(realPersonId, id, trackChanges: false);

            if (cashDb == null)
            {
                _logger.LogInfo($"Cash with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            var cash = _mapper.Map<CashDto>(cashDb);

            return Ok(cash);
        }


        [HttpPost]
        public async Task<IActionResult> CreateCashForRealPersonAsync(int realPersonId, [FromBody] CashCreationDto cash, bool eager = false)
        {
            if (cash == null)
            {
                _logger.LogError("CashForCreationDto object sent from client is null.");
                return BadRequest("CashForCreationDto object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the CashCreationDto object");
                return UnprocessableEntity(ModelState);
            }

            var realPerson = await UnitOfWork.PersonRepository.GetSingelRealPersonAsync(realPersonId,eager:eager);

            if (realPerson == null)
            {
                _logger.LogInfo($"Company with id: {realPersonId} doesn't exist in the database.");
                return NotFound();
            }

            var cashEntity = _mapper.Map<Cash>(cash);

            await UnitOfWork.CashRepository.CreateCashForRealPersonAsync(realPersonId, cashEntity);

            await UnitOfWork.SaveAsync();

            var cashToReturn = _mapper.Map<CashDto>(cashEntity);

            return CreatedAtRoute("GetCashForRealPerson", new { realPersonId, id = cashToReturn.Id }, cashToReturn);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCashForRealPersonAsync(int realPersonId, int id, bool eager = false)
        {
            var realPerson = await UnitOfWork.PersonRepository.GetSingelRealPersonAsync(realPersonId,eager:eager);

            if (realPerson == null)
            {
                _logger.LogInfo($"RealPerson with id: {realPerson} doesn't exist in the database.");
                return NotFound();
            }

            var cashForRealPerson = await UnitOfWork.CashRepository.GetSingelCashAsync(realPersonId, id, trackChanges: false);

            if (cashForRealPerson == null)
            {
                _logger.LogInfo($"Cash with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            await UnitOfWork.CashRepository.DeleteCashAsync(cashForRealPerson);

            await UnitOfWork.SaveAsync();

            return NoContent();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCashForRealPersonAsync(int realPersonId, int id, [FromBody] CashUpdateDto cash, bool eager = false)
        {
            if (cash == null)
            {
                _logger.LogError("CashForUpdateDto object sent from client is null.");
                return BadRequest("CashForUpdateDto object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the CashUpdateDto object");
                return UnprocessableEntity(ModelState);
            }

            var realPerson = await UnitOfWork.PersonRepository.GetSingelRealPersonAsync(realPersonId,eager:eager);

            if (realPerson == null)
            {
                _logger.LogInfo($"RealPerson with id: {realPersonId} doesn't exist in the database.");
                return NotFound();
            }

            var cashEntity = await UnitOfWork.CashRepository.GetSingelCashAsync(realPersonId, id, trackChanges: true);

            if (cashEntity == null)
            {
                _logger.LogInfo($"Cash with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            _mapper.Map(cash, cashEntity);

            await UnitOfWork.SaveAsync();

            return NoContent();
        }
    }
}
