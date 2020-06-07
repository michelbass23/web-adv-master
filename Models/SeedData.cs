using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using EscAdv.Data;
using EscAdv.Models;
using System;
using System.Linq;

namespace EscAdv.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new EscAdvContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<EscAdvContext>>()))
            {
                // Look for any movies.
                if (context.Process.Any())
                {
                    return;   // DB has been seeded
                }

                context.Process.AddRange(
                  new Process{
                    title = "Felipe",
                    type = "Criminal",
                    petition = "1547854",
                    created = DateTime.Parse("2018-1-11")
                  },
                  new Process{
                    title = "Edmar",
                    type = "Administrativo",
                    petition = "5472014",
                    created = DateTime.Parse("2019-1-11")
                  },
                  new Process{
                    title = "Giovanna",
                    type = "Previdênciário",
                    petition = "7540325",
                    created = DateTime.Parse("2020-1-11")
                  }
                );
                context.SaveChanges();
            }
        }
    }
}