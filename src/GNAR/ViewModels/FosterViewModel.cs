using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GNAR.ViewModels
{
    public class FosterViewModel
    {
		[Required]
		[StringLength(25, MinimumLength = 3)]
		public string Name { get; set; }

		[Required]
		[StringLength(1)]
		public string Sex { get; set; }

		[Required]
		[StringLength(1)]
		public string Type { get; set; }

		[Required]
		[StringLength(512, MinimumLength = 10)]
		public string Description { get; set; }

		[Required, DataType(DataType.DateTime)]
		public DateTime DateIntake { get; set; }

		[Required, DataType(DataType.DateTime)]
		public DateTime DateBirth { get; set; }

		[Required]
		[StringLength(128, MinimumLength = 3)]
		public string Foster { get; set; }
	}
}
