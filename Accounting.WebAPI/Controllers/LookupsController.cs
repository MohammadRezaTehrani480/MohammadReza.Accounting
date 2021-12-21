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
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace Accounting.WebAPI.Controllers
{
    public class LookupsController : BaseApiControllerWithDatabase
    {
        public LookupsController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<LookupsController> logger) : base(unitOfWork, mapper)
        {
            _logger = logger;
        }

        private readonly ILogger<LookupsController> _logger;

        [HttpGet]
        public async Task<IActionResult> GetAllLookupsAsync()
        {
            try
            {
                var lookups = await UnitOfWork.LookupRepository.GetAllUdemyAsync();

                var lookupsDTO = _mapper.Map<IEnumerable<LookupDTO>>(lookups);

                return Ok(value: lookupsDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetAllLookupsAsync)} action {ex}");

                return StatusCode(500, "Internal server error!");
            }
        }


        [HttpGet("{id}", Name = "LookupById")]
        public async Task<IActionResult> GetSingelLookupAsync(int id)
        {
            try
            {
                var lookup = await UnitOfWork.LookupRepository.GetUdemyAsync(q => q.Id == id);

                if (lookup == null)
                {
                    _logger.LogError($"Lookup with id: {id} doesn't exist in the database.");
                    return NotFound();
                }
                else
                {
                    var lookupDTO = _mapper.Map<LookupDTO>(lookup);
                    return Ok(lookupDTO);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetSingelLookupAsync)} action {ex}");

                return StatusCode(500, "Internal server error!");
            }
        }

        #region *
        //[HttpPost]
        //public async Task<IActionResult> CreateLookupAsync([FromBody] LookupCreationDTO lookup)
        //{
        //    if (lookup == null)
        //    {
        //        _logger.LogError("LookupCreationViewModel object sent from client is null.");

        //        return BadRequest("LookupCreationViewModel object is null");
        //    }

        //    if (!ModelState.IsValid)
        //    {
        //        _logger.LogError("Invalid model state for the LookupCreationDto object");
        //        return UnprocessableEntity(ModelState);
        //    }

        //    var lookupResult = _mapper.Map<Lookup>(lookup);

        //    await UnitOfWork.LookupRepository.InsertAsync(lookupResult);

        //    await UnitOfWork.SaveAsync();

        //    var lookupToReturn = _mapper.Map<LookupDTO>(lookupResult);

        //    return CreatedAtRoute("LookupById", new { id = lookupToReturn.Id }, lookupToReturn);
        //}


        //[HttpDelete]
        //public async Task<IActionResult> DeleteLookupAsync(int id)
        //{
        //    if (id <= 0)
        //    {
        //        _logger.LogError($"Lookup with Id: {id} does not exist in the database!");

        //        return NotFound();
        //    }

        //    await UnitOfWork.LookupRepository.DeleteByIdAsync(id);

        //    await UnitOfWork.SaveAsync();

        //    return NoContent();
        //}


        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateLookupAsync(int id, [FromBody] LookupUpdateDto lookup)
        //{
        //    if (lookup == null)
        //    {
        //        _logger.LogError("LookupUpdateViewModel object sent from client is null.");
        //        return BadRequest("LookupUpdateViewModel object is null");
        //    }

        //    if (!ModelState.IsValid)
        //    {
        //        _logger.LogError("Invalid model state for the LookupUpdateDto object");
        //        return UnprocessableEntity(ModelState);
        //    }

        //    var lookupEntity = await UnitOfWork.LookupRepository.GetLookupAsync(id, trackChanges: true);

        //    if (lookupEntity == null)
        //    {
        //        _logger.LogError($"Lookup with id: {id} doesn't exist in the database.");
        //        return NotFound();
        //    }

        //    _mapper.Map(lookup, lookupEntity);

        //    await UnitOfWork.SaveAsync();

        //    return NoContent();
        //}
        #endregion *
    }
}

