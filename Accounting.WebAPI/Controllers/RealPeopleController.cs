using Accounting.Shared.ViewModels;
using Accounting.Shared.ViewModels.RealPersonViewModels;
using Accounting.WebAPI.Contracts;
using Accounting.WebAPI.Data;
using Accounting.WebAPI.Entities;
using AutoMapper;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounting.WebAPI.RequestFeatures;
using Accounting.WebAPI.Extensions;
using System.Linq.Expressions;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace Accounting.WebAPI.Controllers
{
    public class RealPeopleController : BaseApiControllerWithDatabase
    {
        public RealPeopleController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<RealPeopleController> logger) : base(unitOfWork, mapper)
        {
            _logger = logger;
        }

        private readonly ILogger<RealPeopleController> _logger;

        [HttpGet]
        public async Task<IActionResult> GetAllRealPeoplePersonAsync()
        {
            try
            {
                var realPeople = await UnitOfWork.PersonRepository.GetAllRealPeopleUdemyAsync();

                var realPeopleDTO = _mapper.Map<IList<RealPersonDTO>>(realPeople);

                return Ok(value: realPeopleDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetAllRealPeoplePersonAsync)} action {ex}");

                return StatusCode(500, "Internal Server Error. Please Try Again Later!");
            }
        }


        [HttpGet("{id:int}", Name = "realpersonbyid")]
        public async Task<IActionResult> GetSingelRealPersonAsync(int id)
        {
            try
            {
                var realPerson = await UnitOfWork.PersonRepository.GetSingelRealPersonUdemyAsync(q => q.Id == id, new List<string> { "Cashes", "BirthPlace", "Nationality" });

                if (realPerson == null)
                {
                    _logger.LogError($"realperson with id: {id} doesn't exist in the database.");
                    return NotFound();
                }
                else
                {
                    var realPersonDTO = _mapper.Map<RealPersonDTO>(realPerson);
                    return Ok(realPersonDTO);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetSingelRealPersonAsync)} action {ex}");

                return StatusCode(500, "Internal Server Error. Please Try Again Later!");
            }
        }

        #region *
        //[HttpPost]
        //public async Task<IActionResult> CreateRealPersonAsync([FromBody] RealPersonCreationDTO realPerson)
        //{
        //    if (realPerson == null)
        //    {
        //        _logger.LogError("RealPersonCreationViewModel object sent from client is null.");

        //        return BadRequest("RealPersonCreationViewModel object is null");
        //    }

        //    if (!ModelState.IsValid)
        //    {
        //        _logger.LogError("Invalid model state for the EmployeeForCreationDto object");
        //        return UnprocessableEntity(ModelState);
        //    }

        //    var realPersonResult = _mapper.Map<RealPerson>(realPerson);

        //    await UnitOfWork.PersonRepository.InsertAsync(realPersonResult);

        //    await UnitOfWork.SaveAsync();

        //    var realPersonToReturn = _mapper.Map<RealPersonDTO>(realPersonResult);

        //    return CreatedAtRoute("RealPersonById", new { id = realPersonToReturn.Id }, realPersonToReturn);
        //}


        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteRealPersonAsync(int id, bool eager = false)
        //{
        //    var realPerson = await UnitOfWork.PersonRepository.GetSingelRealPersonAsync(id, eager: eager);

        //    if (realPerson == null)
        //    {
        //        _logger.LogError($"RealPerson with id: {id} doesn't exist in the database.");
        //        return NotFound();
        //    }
        //    await UnitOfWork.PersonRepository.DeleteRealPersonAsync(realPerson);

        //    await UnitOfWork.SaveAsync();

        //    return NoContent();
        //}


        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateRealPersonAsync(int id, [FromBody] RealPersonUpdateDto realPerson, bool eager = false)
        //{
        //    if (realPerson == null)
        //    {
        //        _logger.LogError("RealPersonUpdateViewModel object sent from client is null.");
        //        return BadRequest("RealPersonUpdateViewModel object is null");
        //    }

        //    if (!ModelState.IsValid)
        //    {
        //        _logger.LogError("Invalid model state for the RealPersonUpdateDto object");
        //        return UnprocessableEntity(ModelState);
        //    }

        //    var realPersonEntity = await UnitOfWork.PersonRepository.GetSingelRealPersonAsync(id, eager: eager);

        //    if (realPersonEntity == null)
        //    {
        //        _logger.LogError($"RealPerson with id: {id} doesn't exist in the database.");
        //        return NotFound();
        //    }

        //    _mapper.Map(realPerson, realPersonEntity);

        //    await UnitOfWork.SaveAsync();

        //    return NoContent();
        //}
        #endregion *
    }
}

