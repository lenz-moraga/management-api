using BorrowingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BorrowingSystem.Context
{
    public class DbSeeder
    {
        public static async Task SeedAsync(BorrowingContext context)
        {
            // Fixed GUIDs for seeding
            var adminRoleId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var userRoleId = Guid.Parse("22222222-2222-2222-2222-222222222222");
            var seededUserId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

            Console.WriteLine($">>>>>>>>>>>>>>>>>>>>>> {context.Roles} <<<<<<<<<<<<<<<<<");

            if (!await context.Roles.AnyAsync())
            {
                var roles = new List<Role>
                {
                    new() {
                        Id = adminRoleId,
                        Name = "Admin",
                        CreatedAt = DateTime.UtcNow,
                        CreatedBy = Guid.Empty
                    },
                    new() {
                        Id = userRoleId,
                        Name = "Regular",
                        CreatedAt = DateTime.UtcNow,
                        CreatedBy = Guid.Empty
                    }
                };

                await context.Roles.AddRangeAsync(roles);
                await context.SaveChangesAsync();
            }

            // Seed User
            var seedUser = await context.Users
                .Include(u => u.Roles) // Include roles to avoid re-fetching later
                .FirstOrDefaultAsync(u => u.Id == seededUserId);
            if (seedUser == null)
            {
                seedUser = new User
                {
                    Id = seededUserId,
                    FullName = "Seeded User",
                    Email = "seeded@example.com",
                    PasswordHash = "hashedpassword",
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = Guid.Empty,
                    Roles = []
                };

                await context.Users.AddAsync(seedUser);
                await context.SaveChangesAsync(); // Save first so it's tracked
            }

            //Seed UserRoles(many-to - many)
            var adminRole = context.Roles.Local.FirstOrDefault(r => r.Id == adminRoleId) ?? context.Roles.Find(adminRoleId);
            var userRole = context.Roles.Local.FirstOrDefault(r => r.Id == userRoleId) ?? context.Roles.Find(userRoleId);

            if (adminRole != null && !seedUser.Roles.Any(r => r.Id == adminRole.Id))
                seedUser.Roles.Add(adminRole);
            if (userRole != null && !seedUser.Roles.Any(r => r.Id == userRole.Id))
                seedUser.Roles.Add(userRole);

            await context.SaveChangesAsync();
        }
    }
}
