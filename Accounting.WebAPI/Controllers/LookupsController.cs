using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounting.WebAPI.Entities;
using Accounting.WebAPI.Data;
using Accounting.WebAPI.Enum;
using Infrastructure;
using AutoMapper;
using Accounting.Shared.ViewModels;
using Accounting.WebAPI.Contracts;

namespace Accounting.WebAPI.Controllers
{

    [Route("api/lookups")]
    public class LookupsController : BaseApiControllerWithDatabase
    {
        public LookupsController(IUnitOfWork unitOfWork, IMapper mapper, ILoggerManager logger) : base(unitOfWork, mapper, logger)
        {
        }

        [HttpGet]
        public async Task<ActionResult> GetLookupsAsync()
        {
            try
            {
                var lookups = await UnitOfWork.LookupRepository.GetAllLookupsAsync(trackChanges: false);

                var lookupsDto = _mapper.Map<IEnumerable<LookupDTO>>(lookups);

                return Ok(value: lookupsDto.AsQueryable());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetLookupsAsync)} action {ex}");

                return StatusCode(500, "Internal server error!");
            }
        }


        [HttpGet("{id}", Name = "LookupById")]
        public async Task<IActionResult> GetLookupAsync(int id)
        {
            var lookup = await UnitOfWork.LookupRepository.GetLookupAsync(id, trackChanges: false);

            if (lookup == null)
            {
                _logger.LogInfo($"Lookup with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                var lookupDto = _mapper.Map<LookupDTO>(lookup);
                return Ok(lookupDto);
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateLookupAsync([FromBody] LookupCreationDto lookup)
        {
            if (lookup == null)
            {
                _logger.LogError("LookupCreationViewModel object sent from client is null.");

                return BadRequest("LookupCreationViewModel object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the LookupCreationDto object");
                return UnprocessableEntity(ModelState);
            }

            var lookupResult = _mapper.Map<Lookup>(lookup);

            await UnitOfWork.LookupRepository.InsertAsync(lookupResult);

            await UnitOfWork.SaveAsync();

            var lookupToReturn = _mapper.Map<LookupDTO>(lookupResult);

            return CreatedAtRoute("LookupById", new { id = lookupToReturn.Id }, lookupToReturn);
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteLookupAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogError($"Lookup with Id: {id} does not exist in the database!");

                return NotFound();
            }

            await UnitOfWork.LookupRepository.DeleteByIdAsync(id);

            await UnitOfWork.SaveAsync();

            return NoContent();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLookupAsync(int id, [FromBody] LookupUpdateDto lookup)
        {
            if (lookup == null)
            {
                _logger.LogError("LookupUpdateViewModel object sent from client is null.");
                return BadRequest("LookupUpdateViewModel object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the LookupUpdateDto object");
                return UnprocessableEntity(ModelState);
            }

            var lookupEntity = await UnitOfWork.LookupRepository.GetLookupAsync(id, trackChanges: true);

            if (lookupEntity == null)
            {
                _logger.LogInfo($"Lookup with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            _mapper.Map(lookup, lookupEntity);

            await UnitOfWork.SaveAsync();

            return NoContent();
        }
    }
}


