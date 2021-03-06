﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace GNAR.Models
{
    public class PetContext : IdentityDbContext<PetUser>
    {
		private IConfigurationRoot _config;

		public PetContext(IConfigurationRoot config, DbContextOptions options) : base(options)
		{
			_config = config;
		}

		public DbSet<Pet> Pets { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);

			optionsBuilder.UseSqlServer(_config["ConnectionStrings:PetsConnection"]);
		}
	}
}
