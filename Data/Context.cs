﻿using Microsoft.EntityFrameworkCore;
using OnosFlow.Models;

namespace OnosFlow.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
