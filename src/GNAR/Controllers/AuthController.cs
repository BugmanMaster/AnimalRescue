﻿using GNAR.Models;
using GNAR.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GNAR.Controllers
{
	public class AuthController : Controller
	{
		private SignInManager<PetUser> _signInManager;

		public AuthController(SignInManager<PetUser> signInManager)
		{
			_signInManager = signInManager;
		}

		public IActionResult Login()
		{
			if (User.Identity.IsAuthenticated)
			{
				return RedirectToAction("Pets", "GNAR");
			}

			return View();
		}

		[HttpPost]
		public async Task<ActionResult> Login(LoginViewModel vm, string returnUrl)
		{
			if (ModelState.IsValid)
			{
				var signInResult = await _signInManager.PasswordSignInAsync(vm.Username, vm.Password, true, false);

				if (signInResult.Succeeded)
				{
					if (string.IsNullOrWhiteSpace(returnUrl))
					{
						return RedirectToAction("Pets", "GNAR");
					}
					else
					{
						return Redirect(returnUrl);
					}
				}
				else
				{
					ModelState.AddModelError("", "Username or password incorrect");
				}
			}

			return View();
		}

		public async Task<ActionResult> Logout()
		{
			if (User.Identity.IsAuthenticated)
			{
				await _signInManager.SignOutAsync();
			}

			return RedirectToAction("Index", "GNAR");
		}
	}
}
