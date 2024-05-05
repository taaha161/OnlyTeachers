using Microsoft.EntityFrameworkCore;

using MvcMovie.Data;

namespace MvcMovie.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new MvcMovieContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<MvcMovieContext>>()))
        {
            // Look for any movies.
            if (context.Post.Any())
            {
                return;   // DB has been seeded
            }
            context.Post.AddRange(
                new Post
                {
                    Title = "This is My first post",
                   PostDate = DateTime.Now,
                    content = "Horny teachers nearby",
                    Price = 7.99M,
                    subscribers = 0
                },
                new Post
                {
                Title = "This is My Second post",
                   PostDate = DateTime.Now,
                    content = "Pay to get exclusive content",
                    Price = 7.99M,
                    subscribers = 0
                },
                new Post
                {
                    Title = "This is My Third post",
                   PostDate = DateTime.Now,
                    content = "Physics lectures",
                  Price = 7.99M,
                    subscribers = 0
                },
                new Post
                {
                      Title = "This is My Fourth post",
                   PostDate = DateTime.Now,
                    content = "DB Course by Ishaq Raza",
                   Price = 7.99M,
                    subscribers = 0
                }
            );
            context.SaveChanges();
        }
    }
}