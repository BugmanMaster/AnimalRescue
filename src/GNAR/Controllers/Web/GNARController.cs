using GNAR.Models;
using GNAR.Services;
using GNAR.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace GNAR.Controllers
{
	public class GNARController : Controller
    {
		private IMailService _mailService;
		private IConfigurationRoot _config;
		private ILogger<GNARController> _logger;
		private IGnarRepository _repository;

		public GNARController(IMailService mailService, 
			IConfigurationRoot config,
			IGnarRepository repository,
			ILogger<GNARController> logger)
		{
			_config = config;
			_mailService = mailService;
			_logger = logger;
			_repository = repository;
		}

		public IActionResult Index()
		{
			return View();
		}

		[Authorize]
		public IActionResult Pets()
		{
			try
			{
				var data = _repository.GetAllPets();
				return View(data);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to do anything in Index page : {ex.Message}");
				return Redirect("");
			}
		}

		[Authorize]
		public IActionResult Fosters()
		{
			try
			{
				var data = _repository.GetAllPets();
				return View(data);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to do anything in Index page : {ex.Message}");
				return Redirect("");
			}
		}

		[HttpPost]
		public IActionResult Contact(ContactViewModel model)
		{
			if (ModelState.IsValid)
			{
				_mailService.SendMail("", "", "", "");
			}

			return View();
		}

		public IActionResult About()
		{
			ViewData["Message"] = "Your application description page.";

			return View();
		}

		public IActionResult Contact()
		{
			ViewData["Message"] = "GNAR Animal Rescue";

			return View();
		}

		public IActionResult Donate()
		{
			ViewData["Message"] = "GNAR Animal Rescue";

			return View();
		}
		public IActionResult Available()
		{
			ViewData["Message"] = "GNAR Animal Rescue";

			return View();
		}
		public IActionResult Adopt()
		{
			ViewData["Message"] = "GNAR Animal Rescue";

			return View();
		}

		public IActionResult Volunteer()
		{
			ViewData["Message"] = "GNAR Animal Rescue";

			return View();
		}

		public IActionResult Error()
		{
			return View();
		}

	}
}
