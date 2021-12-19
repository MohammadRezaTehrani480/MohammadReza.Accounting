using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure;
using Accounting.WebAPI.Data;
using AutoMapper;
using Accounting.WebAPI.Contracts;
using Accounting.WebAPI.Entities;
using Accounting.Shared.ViewModels.LegalPersonViewModels;

namespace Accounting.WebAPI.Controllers
{
    [Route("api/legalPeople")]
    public class LegalPeopleController : BaseApiControllerWithDatabase
    {
        public LegalPeopleController(IUnitOfWork unitOfWork, IMapper mapper, ILoggerManager logger) : base(unitOfWork, mapper, logger)
        {
        }


        [HttpGet]
        public async Task<ActionResult> GetAllLegalPersonAsync()
        {
            try
            {
                var legalPeople = await UnitOfWork.PersonRepository.GetAllLegalPeopleAsync();

                var legalPeopleDto = _mapper.Map<IEnumerable<LegalPersonDTO>>(legalPeople);

                return Ok(value: legalPeopleDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetLegalPersonAsync)} action {ex}");

                return StatusCode(500, "Internal server error!");
            }
        }


        [HttpGet("{id}", Name = "LegalPersonById")]
        public async Task<IActionResult> GetLegalPersonAsync(int id)
        {
            var legalPerson = await UnitOfWork.PersonRepository.GetSingelLegalPersonAsync(id);

            if (legalPerson == null)
            {
                _logger.LogInfo($"LegalPerson with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                var legalPersonDto = _mapper.Map<LegalPersonDTO>(legalPerson);
                return Ok(legalPersonDto);
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateLegalPersonAsync([FromBody] LegalPersonCreationDto legalPerson)
        {
            if (legalPerson == null)
            {
                _logger.LogError("LegalPersonCreationViewModel object sent from client is null.");

                return BadRequest("LegalPersonCreationViewModel object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the LegalPersonCreationDto object");
                return UnprocessableEntity(ModelState);
            }

            var legalPersonResult = _mapper.Map<LegalPerson>(legalPerson);

            await UnitOfWork.PersonRepository.InsertAsync(legalPersonResult);

            await UnitOfWork.SaveAsync();

            var legalPersonToReturn = _mapper.Map<LegalPersonDTO>(legalPersonResult);

            return CreatedAtRoute("LegalPersonById", new { id = legalPersonToReturn.Id }, legalPersonToReturn);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLegalPersonAsync(int id)
        {
            var legalPerson = await UnitOfWork.PersonRepository.GetSingelLegalPersonAsync(id);

            if (legalPerson == null)
            {
                _logger.LogInfo($"LegalPerson with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            await UnitOfWork.PersonRepository.DeleteLegalPersonAsync(legalPerson);

            await UnitOfWork.SaveAsync();

            return NoContent();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLegalPersonAsync(int id, [FromBody] LegalPersonUpdateDto legalPerson)
        {
            if (legalPerson == null)
            {
                _logger.LogError("LegalPersonUpdateViewModel object sent from client is null.");
                return BadRequest("LegalPersonUpdateViewModel object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the LegalPersonUpdateDto object");
                return UnprocessableEntity(ModelState);
            }

            var legalPersonEntity = await UnitOfWork.PersonRepository.GetSingelLegalPersonAsync(id);

            if (legalPersonEntity == null)
            {
                _logger.LogInfo($"LegalPerson with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            _mapper.Map(legalPerson, legalPersonEntity);

            await UnitOfWork.SaveAsync();

            return NoContent();
        }
    }
}

