using Accounting.Shared.ViewModels;
using Accounting.Shared.ViewModels.RealPersonViewModels;
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
using Microsoft.AspNetCore.Http;
using Accounting.WebAPI.Services;
using Microsoft.AspNetCore.Authorization;

namespace Accounting.WebAPI.Controllers
{
    /*We have to mention that this cache rule will apply to all
     * the actions inside the controller except the ones that already
     * have the ResponseCache atribute applied.*/
    //[ResponseCache(CacheProfileName = "120SecondsDuration")]
    public class RealPeopleController : BaseApiControllerWithDatabase
    {
        public RealPeopleController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<RealPeopleController> logger, IAuthManager authManager) : base(unitOfWork, mapper)
        {
            _logger = logger;
            _authManager = authManager;
        }

        private readonly ILogger<RealPeopleController> _logger;
        private readonly IAuthManager _authManager;


        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllRealPeoplePersonAsync([FromQuery] RequestParams requestParams)
        {
            var realPeople = await UnitOfWork.PersonRepository.GetAllRealPeopleUdemyPagingAsync(requestParams);

            var realPeopleDTO = _mapper.Map<IList<RealPersonDTO>>(realPeople);

            return Ok(value: realPeopleDTO);
        }

        [HttpGet("{id:int}", Name = "GetSingelRealPersonAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSingelRealPersonAsync(int id)
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

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateRealPersonAsync([FromBody] RealPersonCreationDTO realPersonDTO)
        {
            if (realPersonDTO == null)
            {
                _logger.LogError("RealPersonCreationViewModel object sent from client is null.");

                return BadRequest("RealPersonCreationViewModel object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid model state for the {nameof(CreateRealPersonAsync)} object");
                return BadRequest(ModelState);
            }

            var realPerson = _mapper.Map<RealPerson>(realPersonDTO);

            await UnitOfWork.PersonRepository.InsertAsync(realPerson);

            await UnitOfWork.SaveAsync();

            /*The advantage of this method is in postman and in headers tap we can see the location field of this
              record which we can use it to fetch this record*/
            return CreatedAtRoute("GetSingelRealPersonAsync", new { id = realPerson.Id }, realPerson);

        }

        [Authorize]
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateRealPersonAsync(int id, [FromBody] RealPersonUpdateDTO realPersonDTO)
        {
            if (realPersonDTO == null)
            {
                _logger.LogError("RealPersonUpdateViewModel object sent from client is null.");
                return BadRequest("RealPersonUpdateViewModel object is null");
            }


            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError("Invalid model state for the RealPersonUpdateDTO object");
                return BadRequest(ModelState);
            }

            var realPerson = await UnitOfWork.PersonRepository.GetSingelRealPersonUdemyAsync(q => q.Id == id);
            if (realPerson == null)
            {
                _logger.LogError("Invalid model state for the RealPersonUpdateDto object");
                return BadRequest("Submitted data is invalid!");
            }

            _mapper.Map(realPersonDTO, realPerson);

            UnitOfWork.PersonRepository.Update(realPerson);

            await UnitOfWork.SaveAsync();

            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteRealPersonAsync(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"RealPerson with Id: {id} does not exist in the database!");

                return BadRequest();
            }

            var realPerson = await UnitOfWork.PersonRepository.GetSingelRealPersonUdemyAsync(q => q.Id == id);
            if (realPerson == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteRealPersonAsync)}");
                return BadRequest("Submitted data is invalid");
            }

            await UnitOfWork.PersonRepository.DeleteByIdAsync(id);
            await UnitOfWork.SaveAsync();

            return NoContent();
        }
    }
}

