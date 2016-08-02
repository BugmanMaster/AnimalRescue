using System;
using System.ComponentModel.DataAnnotations;

namespace GNAR.ViewModels
{
	public class PetViewModel
    {
		[Required]
		[StringLength(25, MinimumLength =3)]
		public string Name { get; set; }

		[Required]
		[StringLength(1)]
		public string Sex { get; set; }

		[Required]
		[StringLength(1)]
		public string Type { get; set; }

		public DateTime DateIntake { get; set; }
	}
}
