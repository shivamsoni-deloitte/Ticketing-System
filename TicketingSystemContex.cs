using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using tickeing_system.models;

namespace tickeing_system
{
    public class TicketingSystemContex : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Issue> Issues { get; set; }

        public DbSet<Label> Labels {get; set;}
        
        public TicketingSystemContex(DbContextOptions options):base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)  
        {
            
        }  
        
    }
}