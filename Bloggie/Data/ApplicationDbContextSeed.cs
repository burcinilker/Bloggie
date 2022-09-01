﻿namespace Bloggie.Data
{
    public static class ApplicationDbContextSeed
    {
        public async static Task SeedAsync(ApplicationDbContext db,UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager) 
        {
            await db.Database.MigrateAsync();

            var adminEmail = "admin@example.com";
            var adminPass = "P@ssword1";
            var adminRole = "Administrator";

            var cat1 = new Category() { Name="Sample Category 1",Slug="sample-cagory-1"};
            var cat2 = new Category() { Name="Sample Category 2",Slug="sample-category-2"};

            var post1 = new Post()
            {
                Title="Sample Post 1",
                Content= "<p>Am I a good boy? The reason I ask is because someone told me that people say this to all dogs, even if they aren't good...</p>",
                
                Category =cat1
            };


            var post2 = new Post()
            {
                Title = "Sample Post 2",
                Content = "<p>Am I a good boy? The reason I ask is because someone told me that people say this to all dogs, even if they aren't good...</p>",
                
                Category = cat2
            };


            var post3 = new Post()
            {
                Title = "Sample Post 3",
                Content = "<p>Am I a good boy? The reason I ask is because someone told me that people say this to all dogs, even if they aren't good...</p>",
               
                Category = cat1
            };


            var post4 = new Post()
            {
                Title = "Sample Post 4",
                Content = "<p>Last month's report looks great, I am very happy with the progress so far, keep up the good work!</p>",
               
                Category = cat1
            };

            if (!await roleManager.RoleExistsAsync(adminRole))
            {
                await roleManager.CreateAsync(new IdentityRole(adminRole));
            }

            if (!userManager.Users.Any(x=>x.UserName == adminEmail))
            {
                var adminUser = new ApplicationUser()
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    DisplayName = "Admin User",
                    EmailConfirmed = true,
                    Posts = new List<Post>() { post1,post2,post3,post4}
                };
                await userManager.CreateAsync(adminUser,adminPass);
                await userManager.AddToRoleAsync(adminUser,adminRole);
            }

        }
            public async static Task SeedDataAsync(this WebApplication app)
            {
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                await SeedAsync(db, userManager, roleManager);
            }
            }
    }
}
