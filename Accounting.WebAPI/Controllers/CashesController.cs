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
using Microsoft.Extensions.Logging;

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
        public async Task<IActionResult> GetAllCashesAsync()
        {
            try
            {
                var cashes = await UnitOfWork.CashRepository.GetAllAsync();
                var cashesDTO = _mapper.Map<IList<CashDTO>>(cashes);
                return Ok(cashesDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(GetAllCashesAsync)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later.");
            }
        }


        [HttpGet("{id:int}", Name = "GetCashForRealPerson")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSingelCashAsync(int id)
        {
            try
            {
                var cash = await UnitOfWork.CashRepository.GetUdemyAsync(q => q.Id == id, new List<string> { "Documents", "Cashier" });

                if (cash == null)
                {
                    _logger.LogError($"Cash with id: {id} doesn't exist in the database.");
                    return NotFound();
                }

                var cashDTO = _mapper.Map<CashDTO>(cash);

                return Ok(cashDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(GetSingelCashAsync)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later.");
            }
        }

        #region * 
        //[HttpPost]
        //public async Task<IActionResult> CreateCashForRealPersonAsync(int realPersonId, [FromBody] CashCreationDTO cash, bool eager = false)
        //{
        //    if (cash == null)
        //    {
        //        _logger.LogError("CashForCreationDto object sent from client is null.");
        //        return BadRequest("CashForCreationDto object is null");
        //    }

        //    if (!ModelState.IsValid)
        //    {
        //        _logger.LogError("Invalid model state for the CashCreationDto object");
        //        return UnprocessableEntity(ModelState);
        //    }

        //    var realPerson = await UnitOfWork.PersonRepository.GetSingelRealPersonAsync(realPersonId, eager: eager);

        //    if (realPerson == null)
        //    {
        //        _logger.LogError($"Company with id: {realPersonId} doesn't exist in the database.");
        //        return NotFound();
        //    }

        //    var cashEntity = _mapper.Map<Cash>(cash);

        //    await UnitOfWork.CashRepository.CreateCashForRealPersonAsync(realPersonId, cashEntity);

        //    await UnitOfWork.SaveAsync();

        //    var cashToReturn = _mapper.Map<CashDTO>(cashEntity);

        //    return CreatedAtRoute("GetCashForRealPerson", new { realPersonId, id = cashToReturn.Id }, cashToReturn);
        //}


        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteCashForRealPersonAsync(int realPersonId, int id, bool eager = false)
        //{
        //    var realPerson = await UnitOfWork.PersonRepository.GetSingelRealPersonAsync(realPersonId, eager: eager);

        //    if (realPerson == null)
        //    {
        //        _logger.LogError($"RealPerson with id: {realPerson} doesn't exist in the database.");
        //        return NotFound();
        //    }

        //    var cashForRealPerson = await UnitOfWork.CashRepository.GetSingelCashAsync(realPersonId, id, trackChanges: false);

        //    if (cashForRealPerson == null)
        //    {
        //        _logger.LogError($"Cash with id: {id} doesn't exist in the database.");
        //        return NotFound();
        //    }

        //    await UnitOfWork.CashRepository.DeleteCashAsync(cashForRealPerson);

        //    await UnitOfWork.SaveAsync();

        //    return NoContent();
        //}


        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateCashForRealPersonAsync(int realPersonId, int id, [FromBody] CashUpdateDto cash, bool eager = false)
        //{
        //    if (cash == null)
        //    {
        //        _logger.LogError("CashForUpdateDto object sent from client is null.");
        //        return BadRequest("CashForUpdateDto object is null");
        //    }

        //    if (!ModelState.IsValid)
        //    {
        //        _logger.LogError("Invalid model state for the CashUpdateDto object");
        //        return UnprocessableEntity(ModelState);
        //    }

        //    var realPerson = await UnitOfWork.PersonRepository.GetSingelRealPersonAsync(realPersonId, eager: eager);

        //    if (realPerson == null)
        //    {
        //        _logger.LogError($"RealPerson with id: {realPersonId} doesn't exist in the database.");
        //        return NotFound();
        //    }

        //    var cashEntity = await UnitOfWork.CashRepository.GetSingelCashAsync(realPersonId, id, trackChanges: true);

        //    if (cashEntity == null)
        //    {
        //        _logger.LogError($"Cash with id: {id} doesn't exist in the database.");
        //        return NotFound();
        //    }

        //    _mapper.Map(cash, cashEntity);

        //    await UnitOfWork.SaveAsync();

        //    return NoContent();
        //}
        #endregion *
    }
}
