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
using Microsoft.Extensions.Logging;

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
        public async Task<IActionResult> GetAllLegalPersonAsync()
        {
            try
            {
                var legalPeople = await UnitOfWork.PersonRepository.GetAllLegalPeopleUdemyAsync();

                var legalPeopleDTO = _mapper.Map<IList<LegalPersonDTO>>(legalPeople);

                return Ok(value: legalPeopleDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetAllLegalPersonAsync)} action {ex}");

                return StatusCode(500, "Internal server error!");
            }
        }


        [HttpGet("{id:int}", Name = "LegalPersonById")]
        public async Task<IActionResult> GetSingelLegalPersonAsync(int id)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetSingelLegalPersonAsync)} action {ex}");

                return StatusCode(500, "Internal server error!");
            }
        }

        #region *
        //[HttpPost]
        //public async Task<IActionResult> CreateLegalPersonAsync([FromBody] LegalPersonCreationDTO legalPerson)
        //{
        //    if (legalPerson == null)
        //    {
        //        _logger.LogError("LegalPersonCreationViewModel object sent from client is null.");

        //        return BadRequest("LegalPersonCreationViewModel object is null");
        //    }

        //    if (!ModelState.IsValid)
        //    {
        //        _logger.LogError("Invalid model state for the LegalPersonCreationDto object");
        //        return UnprocessableEntity(ModelState);
        //    }

        //    var legalPersonResult = _mapper.Map<LegalPerson>(legalPerson);

        //    await UnitOfWork.PersonRepository.InsertAsync(legalPersonResult);

        //    await UnitOfWork.SaveAsync();

        //    var legalPersonToReturn = _mapper.Map<LegalPersonDTO>(legalPersonResult);

        //    return CreatedAtRoute("LegalPersonById", new { id = legalPersonToReturn.Id }, legalPersonToReturn);
        //}


        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteLegalPersonAsync(int id)
        //{
        //    var legalPerson = await UnitOfWork.PersonRepository.GetSingelLegalPersonAsync(id);

        //    if (legalPerson == null)
        //    {
        //        _logger.LogError($"LegalPerson with id: {id} doesn't exist in the database.");
        //        return NotFound();
        //    }
        //    await UnitOfWork.PersonRepository.DeleteLegalPersonAsync(legalPerson);

        //    await UnitOfWork.SaveAsync();

        //    return NoContent();
        //}


        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateLegalPersonAsync(int id, [FromBody] LegalPersonUpdateDto legalPerson)
        //{
        //    if (legalPerson == null)
        //    {
        //        _logger.LogError("LegalPersonUpdateViewModel object sent from client is null.");
        //        return BadRequest("LegalPersonUpdateViewModel object is null");
        //    }

        //    if (!ModelState.IsValid)
        //    {
        //        _logger.LogError("Invalid model state for the LegalPersonUpdateDto object");
        //        return UnprocessableEntity(ModelState);
        //    }

        //    var legalPersonEntity = await UnitOfWork.PersonRepository.GetSingelLegalPersonAsync(id);

        //    if (legalPersonEntity == null)
        //    {
        //        _logger.LogError($"LegalPerson with id: {id} doesn't exist in the database.");
        //        return NotFound();
        //    }

        //    _mapper.Map(legalPerson, legalPersonEntity);

        //    await UnitOfWork.SaveAsync();

        //    return NoContent();
        //}
        #endregion *
    }
}

