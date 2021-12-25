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
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Accounting.Shared.ViewModels.LookupViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Accounting.WebAPI.Controllers
{
    [ApiExplorerSettings(GroupName = "v1")]
    public class LookupsController : BaseApiControllerWithDatabase
    {
        public LookupsController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<LookupsController> logger) : base(unitOfWork, mapper)
        {
            _logger = logger;
        }

        private readonly ILogger<LookupsController> _logger;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllLookupsAsync([FromQuery] RequestParams requestParams)
        {
            var lookups = await UnitOfWork.LookupRepository.GetAllUdemyPagingAsync(requestParams);

            var lookupsDTO = _mapper.Map<IEnumerable<LookupDTO>>(lookups);

            return Ok(value: lookupsDTO);
        }

        [HttpGet("{id}", Name = "GetSingelLookupAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSingelLookupAsync(int id)
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

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateLookupAsync([FromBody] LookupCreationDTO lookupDTO)
        {
            if (lookupDTO == null)
            {
                _logger.LogError("LookupCreationViewModel object sent from client is null.");

                return BadRequest("LookupCreationViewModel object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the LookupCreationDto object");
                return BadRequest(ModelState);
            }

            var lookup = _mapper.Map<Lookup>(lookupDTO);

            await UnitOfWork.LookupRepository.InsertAsync(lookup);

            await UnitOfWork.SaveAsync();

            return CreatedAtRoute("GetSingelLookupAsync", new { id = lookup.Id }, lookup);
        }

        [Authorize]
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateLookupAsync(int id, [FromBody] LookupUpdateDTO lookupUpdateDTO)
        {
            if (lookupUpdateDTO == null)
            {
                _logger.LogError("LookupUpdateViewModel object sent from client is null.");
                return BadRequest("LookupUpdateViewModel object is null");
            }

            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError("Invalid model state for the LookupUpdateViewModel object");
                return BadRequest(ModelState);
            }

            var lookup = await UnitOfWork.LookupRepository.GetUdemyAsync(q => q.Id == id);
            if (lookup == null)
            {
                _logger.LogError("Invalid model state for the LookupPersonUpdateDto object");
                return BadRequest("Submitted data is invalid!");
            }

            _mapper.Map(lookupUpdateDTO, lookup);

            UnitOfWork.LookupRepository.Update(lookup);

            await UnitOfWork.SaveAsync();

            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteLookupAsync(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"Lookup with Id: {id} does not exist in the database!");

                return BadRequest();
            }


            var lookup = await UnitOfWork.LookupRepository.GetUdemyAsync(q => q.Id == id);
            if (lookup == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteLookupAsync)}");
                return BadRequest("Submitted data is invalid");
            }

            await UnitOfWork.LookupRepository.DeleteByIdAsync(id);
            await UnitOfWork.SaveAsync();

            return NoContent();
        }
    }
}

