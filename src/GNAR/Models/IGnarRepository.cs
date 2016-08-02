using System.Collections.Generic;
using System.Threading.Tasks;

namespace GNAR.Models
{
	public interface IGnarRepository
	{
		IEnumerable<Pet> GetAllCats();
		IEnumerable<Pet> GetAllDogs();
		IEnumerable<Pet> GetAllPets();

		void AddPet(Pet newPet);

		Task<bool> SaveChangesAsync();
	}
}