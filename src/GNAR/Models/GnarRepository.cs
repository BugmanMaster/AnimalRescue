using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GNAR.Models
{
	public class GnarRepository : IGnarRepository
	{
		private PetContext _context;

		public GnarRepository(PetContext context)
		{
			_context = context;
		}

		public IEnumerable<Pet> GetAllPets()
		{
			return _context.Pets.ToList();
		}

		public IEnumerable<Pet> GetAllCats()
		{
			return (from cats in _context.Pets where cats.Type == "C" select cats).ToList() ;
		}

		public IEnumerable<Pet> GetAllDogs()
		{
			return (from dogs in _context.Pets where dogs.Type == "D" select dogs).ToList();
		}

		public void AddPet(Pet newPet)
		{
			_context.Add(newPet);
		}

		public async Task<bool> SaveChangesAsync()
		{
			return (await _context.SaveChangesAsync()) > 0;
		}
	}
}
