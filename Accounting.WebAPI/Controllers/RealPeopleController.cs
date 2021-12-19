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


namespace Accounting.WebAPI.Controllers
{
    [Route("api/realPeople")]
    public class RealPeopleController : BaseApiControllerWithDatabase
    {
        public RealPeopleController(IUnitOfWork unitOfWork, IMapper mapper, ILoggerManager logger) : base(unitOfWork, mapper, logger)
        {
        }

        // #region Examinate
        //*******************************************************************************************
        //[HttpGet]
        //public async Task<ActionResult> GetRealPeopleExamineAsync([FromQuery] RealPersonParameters realPersonParameters)
        //{
        //    try
        //    {
        //        if (!realPersonParameters.ValidAgeRange)
        //        {
        //            return BadRequest("Max age can't be less than min age.");
        //        }

        //        var realPeople = await UnitOfWork.RealPersonRepository.GetAllRealPeopleConditionalAsync(realPersonParameters: realPersonParameters, trackChanges: false);

        //        var realPeopleDto = _mapper.Map<IEnumerable<RealPersonDTO>>(realPeople);

        //        return Ok(value: realPeopleDto);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Something went wrong in the {nameof(GetRealPersonAsync)} action {ex}");

        //        return StatusCode(500, "Internal server error!");
        //    }
        //}
        //*******************************************************************************************

        //[HttpPost]
        //[Route("filter")]
        //public async Task<ActionResult<IQueryable<RealPerson>>> GetRealPeopleAsync(FilterDTO filter)
        //{
        //    try
        //     {
        //        //var filter = options.GetFilter();

        //        var realPeople = await UnitOfWork.RealPersonRepository.GetAllRealPeopleAsync(filter, trackChanges: false);

        //        return Ok(value: realPeople);

        //        //var realPeopleDto = _mapper.Map<IEnumerable<RealPersonDTO>>(realPeople);

        //        //var realPeopleDto = realPeople.AsQueryable();


        //        ////var appliedQueryOptions = AllowedQueryOptions.Skip | AllowedQueryOptions.Select | AllowedQueryOptions.Expand;

        //        //var appliedQueryOptions = AllowedQueryOptions.Filter;

        //        //var realPeopleResult = options.ApplyTo(realPeople.AsQueryable(), appliedQueryOptions) as IQueryable<RealPerson>;

        //        //return Ok(value: realPeopleResult.AsQueryable());
        //    }
        //    catch (Exception ex) 
        //    {
        //        _logger.LogError($"Something went wrong in the {nameof(GetRealPersonAsync)} action {ex}");

        //        return StatusCode(500, "Internal server error!");
        //    }
        //}
        //#endregion Examinate


        [HttpGet]
        public async Task<IActionResult> GetAllRealPeoplePersonAsync(bool eager = false)
        {
            try
            {
                var realPeople = await UnitOfWork.PersonRepository.GetAllRealPeopleAsync(eager);

                var realPeopleDto = _mapper.Map<IEnumerable<RealPersonDTO>>(realPeople);

                return Ok(value: realPeopleDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetAllRealPeoplePersonAsync)} action {ex}");

                return StatusCode(500, "Internal server error!");
            }
        }


        [HttpGet("{id}", Name = "realpersonbyid")]
        public async Task<IActionResult> GetSingelRealPersonAsync(int id, bool eager = false)
        {
            var realperson = await UnitOfWork.PersonRepository.GetSingelRealPersonAsync(id, eager);

            if (realperson == null)
            {
                _logger.LogInfo($"realperson with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                var realpersondto = _mapper.Map<RealPersonDTO>(realperson);
                return Ok(realpersondto);
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateRealPersonAsync([FromBody] RealPersonCreationDto realPerson)
        {
            if (realPerson == null)
            {
                _logger.LogError("RealPersonCreationViewModel object sent from client is null.");

                return BadRequest("RealPersonCreationViewModel object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the EmployeeForCreationDto object");
                return UnprocessableEntity(ModelState);
            }

            var realPersonResult = _mapper.Map<RealPerson>(realPerson);

            await UnitOfWork.PersonRepository.InsertAsync(realPersonResult);

            await UnitOfWork.SaveAsync();

            var realPersonToReturn = _mapper.Map<RealPersonDTO>(realPersonResult);

            return CreatedAtRoute("RealPersonById", new { id = realPersonToReturn.Id }, realPersonToReturn);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRealPersonAsync(int id, bool eager = false)
        {
            var realPerson = await UnitOfWork.PersonRepository.GetSingelRealPersonAsync(id, eager: eager);

            if (realPerson == null)
            {
                _logger.LogInfo($"RealPerson with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            await UnitOfWork.PersonRepository.DeleteRealPersonAsync(realPerson);

            await UnitOfWork.SaveAsync();

            return NoContent();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRealPersonAsync(int id, [FromBody] RealPersonUpdateDto realPerson, bool eager = false)
        {
            if (realPerson == null)
            {
                _logger.LogError("RealPersonUpdateViewModel object sent from client is null.");
                return BadRequest("RealPersonUpdateViewModel object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the RealPersonUpdateDto object");
                return UnprocessableEntity(ModelState);
            }

            var realPersonEntity = await UnitOfWork.PersonRepository.GetSingelRealPersonAsync(id, eager: eager);

            if (realPersonEntity == null)
            {
                _logger.LogInfo($"RealPerson with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            _mapper.Map(realPerson, realPersonEntity);

            await UnitOfWork.SaveAsync();

            return NoContent();
        }
    }
}

