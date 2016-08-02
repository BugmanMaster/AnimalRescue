using System;
using System.Collections.Generic;

namespace GNAR.Models
{
	public class Pet
    {
		public int Id { get; set; }
		public string Name { get; set; }
		public string Sex { get; set; }
		public string Type { get; set; }
		public string Description { get; set; }
		public DateTime DateIntake { get; set; }
		public DateTime DateBirth { get; set; }
		public string Foster { get; set; }
		public string Origin { get; set; }
		public string Notes { get; set; }
		public string FleaMeds { get; set; }
		public DateTime DateNeutered { get; set; }
		public DateTime DateAdopted { get; set; }
		public string Adopter { get; set; }
		public PetStatus Status { get; set; }
		public List<Weight> Weights { get; set; }
	}
}
