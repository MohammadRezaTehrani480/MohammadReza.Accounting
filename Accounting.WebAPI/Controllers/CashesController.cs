using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure;
using Accounting.WebAPI.Data;
using AutoMapper;
using Accounting.Shared.ViewModels.CashViewModels;
using Accounting.WebAPI.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace Accounting.WebAPI.Controllers
{
    public class CashesController : BaseApiControllerWithDatabase
    {
        public CashesController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CashesController> logger) : base(unitOfWork, mapper)
        {
            _logger = logger;
        }

        private readonly ILogger<CashesController> _logger;


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCashesAsync([FromQuery] RequestParams requestParams)
        {
            var cashes = await UnitOfWork.CashRepository.GetAllUdemyPagingAsync(requestParams);
            var cashesDTO = _mapper.Map<IList<CashDTO>>(cashes);
            return Ok(cashesDTO);
        }


        [HttpGet("{id:int}", Name = "GetSingelCashAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSingelCashAsync(int id)
        {
            var cash = await UnitOfWork.CashRepository.GetUdemyAsync(q => q.Id == id, new List<string> { "Documents", "RealPerson" });

            if (cash == null)
            {
                _logger.LogError($"Cash with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            var cashDTO = _mapper.Map<CashDTO>(cash);

            return Ok(cashDTO);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCashAsync([FromBody] CashCreationDTO cashDTO)
        {
            if (cashDTO == null)
            {
                _logger.LogError("CashForCreationDto object sent from client is null.");
                return BadRequest("CashForCreationDto object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the CashCreationDto object");
                return BadRequest(ModelState);
            }

            var cash = _mapper.Map<Cash>(cashDTO);

            await UnitOfWork.CashRepository.InsertAsync(cash);

            await UnitOfWork.SaveAsync();

            return CreatedAtRoute("GetSingelCashAsync", new { id = cash.Id }, cash);
        }

        [Authorize]
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCashAsync(int id, [FromBody] CashUpdateDTO cashDTO)
        {
            if (cashDTO == null)
            {
                _logger.LogError("CashForUpdateDto object sent from client is null.");
                return BadRequest("CashForUpdateDto object is null");
            }

            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError("Invalid model state for the CashUpdateDto object");
                return BadRequest(ModelState);
            }

            var cash = await UnitOfWork.CashRepository.GetUdemyAsync(q => q.Id == id);

            if (cash == null)
            {
                _logger.LogError($"Cash with id: {id} doesn't exist in the database.");
                return BadRequest("Submitted data is invalid!");
            }

            _mapper.Map(cashDTO, cash);

            UnitOfWork.CashRepository.Update(cash);

            await UnitOfWork.SaveAsync();

            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCashAsync(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"Cash with Id: {id} does not exist in the database!");

                return BadRequest();
            }

            var cash = await UnitOfWork.CashRepository.GetUdemyAsync(q => q.Id == id);
            if (cash == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteCashAsync)}");
                return BadRequest("Submitted data is invalid");
            }

            await UnitOfWork.CashRepository.DeleteByIdAsync(id);
            await UnitOfWork.SaveAsync();

            return NoContent();
        }
    }
}
