using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Movie_Data_Manager.Data;
using System;
using System.Linq;

namespace Movie_Data_Manager.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MovieDataContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MovieDataContext>>()))
            {
                // Look for any movies.
                if (context.Movie.Any())
                {
                    return;   // DB has been seeded
                }

                context.Movie.AddRange(
                    new Movie
                    {
                        Name = "When Harry Met Sally",
                        ReleaseYear = 1989,
                        Description = "Romantic Comedy"
                    },

                    new Movie
                    {
                        Name = "Ghostbusters ",
                        ReleaseYear = 1984,
                        Description = "Comedy"
                    },

                    new Movie
                    {
                        Name = "Ghostbusters 2",
                        ReleaseYear = 1986,
                        Description = "Comedy"
                    },

                    new Movie
                    {
                        Name = "Rio Bravo",
                        ReleaseYear = 1959,
                        Description = "Western"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}