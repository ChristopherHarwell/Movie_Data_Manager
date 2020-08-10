using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Movie_Data_Manager.Models;

namespace Movie_Data_Manager.Data
{
    public class MovieDataContext : DbContext
    {
        public MovieDataContext(DbContextOptions<MovieDataContext> options)
           : base(options)
        {
        }
        public DbSet<Movie> Movie { get; set; }
    }
}
