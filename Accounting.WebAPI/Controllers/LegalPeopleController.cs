using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure;
using Accounting.WebAPI.Data;
using AutoMapper;
using Accounting.WebAPI.Entities;
using Accounting.Shared.ViewModels.LegalPersonViewModels;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace Accounting.WebAPI.Controllers
{
    public class LegalPeopleController : BaseApiControllerWithDatabase
    {
        public LegalPeopleController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<LegalPeopleController> logger) : base(unitOfWork, mapper)
        {
            _logger = logger;
        }

        private readonly ILogger<LegalPeopleController> _logger;


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllLegalPersonAsync([FromQuery] RequestParams requestParams)
        {
            var legalPeople = await UnitOfWork.PersonRepository.GetAllLegalPeopleUdemyPagingAsync(requestParams);

            var legalPeopleDTO = _mapper.Map<IList<LegalPersonDTO>>(legalPeople);

            return Ok(value: legalPeopleDTO);
        }


        [HttpGet("{id:int}", Name = "GetSingelLegalPersonAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSingelLegalPersonAsync(int id)
        {
            var legalPerson = await UnitOfWork.PersonRepository.GetSingelLegalPersonUdemyAsync(q => q.Id == id);

            if (legalPerson == null)
            {
                _logger.LogError($"LegalPerson with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                var legalPersonDTO = _mapper.Map<LegalPersonDTO>(legalPerson);
                return Ok(legalPersonDTO);
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateLegalPersonAsync([FromBody] LegalPersonCreationDTO legalPersonDTO)
        {
            if (legalPersonDTO == null)
            {
                _logger.LogError("LegalPersonCreationViewModel object sent from client is null.");

                return BadRequest("LegalPersonCreationViewModel object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the LegalPersonCreationDto object");
                return BadRequest(ModelState);
            }

            var legalPerson = _mapper.Map<LegalPerson>(legalPersonDTO);

            await UnitOfWork.PersonRepository.InsertAsync(legalPerson);

            await UnitOfWork.SaveAsync();

            return CreatedAtRoute("GetSingelLegalPersonAsync", new { id = legalPerson.Id }, legalPerson);
        }

        [Authorize]
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateLegalPersonAsync(int id, [FromBody] LegalPersonUpdateDTO legalPersonDTO)
        {
            if (legalPersonDTO == null)
            {
                _logger.LogError("LegalPersonUpdateViewModel object sent from client is null.");
                return BadRequest("LegalPersonUpdateViewModel object is null");
            }

            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError("Invalid model state for the LegalPersonUpdateDto object");
                return BadRequest("");
            }

            var legalPerson = await UnitOfWork.PersonRepository.GetSingelLegalPersonUdemyAsync(q => q.Id == id);

            if (legalPerson == null)
            {
                _logger.LogError($"LegalPerson with id: {id} doesn't exist in the database.");
                return BadRequest("Submitted data is invalid!");
            }

            _mapper.Map(legalPersonDTO, legalPerson);

            UnitOfWork.PersonRepository.Update(legalPerson);

            await UnitOfWork.SaveAsync();

            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteLegalPersonAsync(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"LegalPerson with Id: {id} does not exist in the database!");

                return BadRequest();
            }

            var legalPerson = await UnitOfWork.PersonRepository.GetSingelLegalPersonUdemyAsync(q => q.Id == id);
            if (legalPerson == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteLegalPersonAsync)}");
                return BadRequest("Submitted data is invalid");
            }

            await UnitOfWork.PersonRepository.DeleteByIdAsync(id);
            await UnitOfWork.SaveAsync();

            return NoContent();
        }
    }
}

