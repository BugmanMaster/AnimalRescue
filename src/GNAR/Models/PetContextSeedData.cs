using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GNAR.Models
{
	public class PetContextSeedData
	{
		private PetContext _context;
		private UserManager<PetUser> _userManager;
		private const string masterUserEmail = "ds.fergus@gmail.com";

		public PetContextSeedData(PetContext context, UserManager<PetUser> userManager)
		{
			_context = context;
			_userManager = userManager;
		}

		public async Task EnsureSeedData()
		{
			if (await _userManager.FindByEmailAsync(masterUserEmail) == null)
			{
				var user = new PetUser()
				{
					UserName = "danfergus",
					Email = masterUserEmail,
				};

				await _userManager.CreateAsync(user, "Forest@01");
			}

			if (!_context.Pets.Any())
			{
				var pet = new Pet()
				{
					Name = "Abby",
					Sex = "F",
					Weights = new List<Weight>
					{
					 new Weight()
					 {
						 Pounds = 2,
						 DateWeighed = new DateTime(2015, 11, 12)
					 },
					 new Weight()
					 {
						 Pounds = 3.3,
						 DateWeighed = new DateTime(2015, 12, 11)
					 }
					},
					DateBirth = new DateTime(2015, 9, 9),
					Description = "gray and white medium hair",
					Foster = "Sue McLean",
					Origin = "Found at 1900 Lipscomb",
					DateIntake = new DateTime(2015, 11, 9),
					FleaMeds = "11/10/15 1/2 AM; 1/12/16 AM, 2/20; 4/5; 4/29; 6/6; 7/11 FL",
					DateNeutered = new DateTime(2015, 11, 25),
					Status = PetStatus.Foster,
					Type = "C"
				};

				_context.Pets.Add(pet);


				pet = new Pet()
				{
					Name = "Adelaide",
					Sex = "F",
					Weights = new List<Weight>
					{
					 new Weight()
					 {
						 Pounds = .4,
						 DateWeighed = new DateTime(2016, 5, 10)
					 },
					 new Weight()
					 {
						 Pounds = 2.3,
						 DateWeighed = new DateTime(2016, 7, 29)
					 }
					},
					DateBirth = new DateTime(2016, 5, 7),
					Description = "black",
					Foster = "Kris Savage",
					Origin = "Ridglea Counrty Club",
					DateIntake = new DateTime(2016, 5, 9),
					FleaMeds = "",
					DateNeutered = DateTime.MinValue,
					Status = PetStatus.Foster,
					Type = "C"
				};
				_context.Pets.Add(pet);

				pet = new Pet()
				{
					Name = "Alegria",
					Sex = "F",
					Weights = new List<Weight>
					{
					 new Weight()
					 {
						 Pounds = 2.5,
						 DateWeighed = new DateTime(2015, 8, 5)
					 }
					},
					DateBirth = new DateTime(2015, 6, 5),
					Description = "brown tabby",
					Foster = "Sheven Poole",
					Origin = "1632 Mistletoe",
					DateIntake = new DateTime(2015, 6, 28),
					FleaMeds = "6/28/2015 Revolution kitten",
					DateNeutered = new DateTime(2015, 8, 5),
					Status = PetStatus.Foster,
					Type = "C"
				};
				_context.Pets.Add(pet);

				await _context.SaveChangesAsync();
			}
		}
	}
}
