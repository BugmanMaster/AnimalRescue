using AutoMapper;
using GNAR.Models;
using GNAR.Services;
using GNAR.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GNAR.Controllers.Api
{
	[Authorize]
	[Route("api/pets")]
	public class PetsController : Controller
    {
		private ILogger<PetsController> _logger;
		private IGnarRepository _repository;
		private GeoCoordsService _coordService;

		public PetsController(IGnarRepository repository, ILogger<PetsController> logger, GeoCoordsService coordService)
		{
			_repository = repository;
			_logger = logger;
			_coordService = coordService;
		}

		[HttpGet("api/fosters")]
		public IActionResult GetFosters()
		{
			try
			{
				var ttt = User.Identity.Name;
				var pets = _repository.GetAllPets();
				return Ok(Mapper.Map<IEnumerable<FosterViewModel>>(pets));
			}
			catch (Exception ex)
			{
				_logger.LogError("Failed to get All Pets", ex);
				return BadRequest("oops");
			}
		}

		[HttpGet("")]
		public IActionResult Get()
		{
			try
			{
				var ttt = User.Identity.Name;
				var pets = _repository.GetAllPets();
				return Ok(Mapper.Map<IEnumerable<PetViewModel>>(pets));
			}
			catch(Exception ex)
			{
				_logger.LogError("Failed to get All Pets", ex);
				return BadRequest("oops");
			}
		}

		[HttpPost("")]
		public async Task<IActionResult> Post([FromBody]PetViewModel newPet)
		{
			if (ModelState.IsValid)
			{
				var result = await _coordService.GetCoordsAsync("Dallas, TX");
				if (!result.Success)
				{
					_logger.LogError(result.Message);
				}
				var fullPet = Mapper.Map<Pet>(newPet);

				//	Save the data
				_repository.AddPet(fullPet);
				if (await _repository.SaveChangesAsync())
				{
					return Created($"api/pets/{newPet.Name}", Mapper.Map<PetViewModel>(fullPet));
				}
				else
				{
					return BadRequest("Failed to save changes to the database");
				}
			}

			return BadRequest("Bad Data");
		}
	}
}
