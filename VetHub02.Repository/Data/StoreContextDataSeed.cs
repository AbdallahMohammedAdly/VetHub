using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using VetHub02.Core.Entities;
using VetHub02.Core.Entities.identity;

namespace VetHub02.Repository.Data
{
    public static class StoreContextDataSeed
    {

        public  static async Task SeedAsync(StoreContext context, UserManager<AppUser> userManager)
        {
            //if (!context.Users.Any())
            //{
            //    var UsersData = File.ReadAllText("../VetHub02.Repository/Data/DataSeed/UsersFile.json");
            //    var Users = JsonSerializer.Deserialize<List<User>>(UsersData);

            //    if (Users?.Count() > 0)
            //    {
            //        foreach (var user in Users)
            //        {
            //            var identityUser = new AppUser()
            //            {
            //                DisplayName = user.Name.Trim(),
            //                Email = user.Email,
            //                UserName = user.Email.Trim().Split('@')[0],
            //                PhoneNumber = user.PhoneNumber,
            //            };
            //            var result = await userManager.CreateAsync(identityUser, "Aa@12345678");

            //            if (result.Succeeded)
            //            {
            //                await context.Users.AddAsync(user);
            //            }


            //        }
            //        await context.SaveChangesAsync();

            //    }
            //}
            //if (!context.Articles.Any())
            //{
            //    var ArticlesData = File.ReadAllText("../VetHub02.Repository/Data/DataSeed/Articles.json");
            //    var articles = JsonSerializer.Deserialize<List<Article>>(ArticlesData);

            //    if (articles?.Count() > 0)
            //    {
            //        foreach (var article in articles)
            //        {
            //            var user = await context.Users.FindAsync(article.UserId);
            //            if (user != null)
            //            {

            //                await context.Articles.AddAsync(article);
            //            }

            //            await context.SaveChangesAsync();

            //        }


            //    }
            //}


            //if (!context.Questions.Any())
            //{
            //    var QuestionsData = File.ReadAllText("../VetHub02.Repository/Data/DataSeed/QuestionsFile.json");
            //    var Questions = JsonSerializer.Deserialize<List<Question>>(QuestionsData);

            //    if (Questions?.Count() > 0)
            //    {
            //        foreach (var question in Questions)
            //        {
            //            await context.Questions.AddAsync(question);
            //        }
            //        await context.SaveChangesAsync();

            //    }
            //}

            //if (!context.Comments.Any())
            //{
            //    var CommentsData = File.ReadAllText("../VetHub02.Repository/Data/DataSeed/CommentsFile.json");
            //    var Comments = JsonSerializer.Deserialize<List<Comment>>(CommentsData);

            //    if (Comments?.Count() > 0)
            //    {
            //        foreach (var comment in Comments)
            //        {
            //            await context.Comments.AddAsync(comment);
            //        }
            //        await context.SaveChangesAsync();

            //    }
            //}




            //if (!context.Users.Any())
            //{
            //    var usersFilePath = "../VetHub02.Repository/Data/DataSeed/UsersFile.json";
            //    if (File.Exists(usersFilePath))
            //    {
            //        var usersData = File.ReadAllText(usersFilePath);
            //        var users = JsonSerializer.Deserialize<List<User>>(usersData);

            //        if (users != null && users.Any())
            //        {
            //            foreach (var user in users.Where(u => !string.IsNullOrWhiteSpace(u.Name) && !string.IsNullOrWhiteSpace(u.Email)))
            //            {
            //                // Check if the user's data is valid
            //                if (!string.IsNullOrWhiteSpace(user.Name) && !string.IsNullOrWhiteSpace(user.Email))
            //                {

            //                    if (!userManager.Users.Any())
            //                    {
            //                        var identityUser = new AppUser()
            //                        {
            //                            DisplayName = user.Name.Trim(),
            //                            Email = user.Email,
            //                            UserName = user.Email.Trim().Split('@')[0],
            //                            PhoneNumber = user.PhoneNumber,
            //                        };
            //                        var result = await userManager.CreateAsync(identityUser, "Aa@12345678");

            //                        if (result.Succeeded)
            //                        {
            //                            await context.Users.AddAsync(user);
            //                        }
            //                    }

            //                }
            //            }

            //             context.SaveChanges();
            //        }
            //    }
            //}
            //// Article seeding logic
            //if (!context.Articles.Any())
            //{
            //    var articlesFilePath = "../VetHub02.Repository/Data/DataSeed/Articles.json";
            //    if (File.Exists(articlesFilePath))
            //    {
            //        var articlesData = File.ReadAllText(articlesFilePath);
            //        var articles = JsonSerializer.Deserialize<List<Article>>(articlesData);

            //        if (articles != null && articles.Any())
            //        {
            //            foreach (var article in articles)
            //            {
            //                // Check if the article's UserId corresponds to a valid user ID with non-null data
            //                var user = await context.Users.FindAsync(article.UserId);
            //                if (user != null && !string.IsNullOrWhiteSpace(user.Name) && !string.IsNullOrWhiteSpace(user.Email))
            //                {
            //                    await context.Articles.AddAsync(article);
            //                }
            //            }

            //             context.SaveChanges();
            //        }
            //    }
            //}

            //// Question seeding logic
            //if (!context.Questions.Any())
            //{
            //    var questionsFilePath = "../VetHub02.Repository/Data/DataSeed/QuestionsFile.json";
            //    if (File.Exists(questionsFilePath))
            //    {
            //        var questionsData = File.ReadAllText(questionsFilePath);
            //        var questions = JsonSerializer.Deserialize<List<Question>>(questionsData);

            //        if (questions != null && questions.Any())
            //        {
            //            foreach (var question in questions)
            //            {
            //                // Check if the article's UserId corresponds to a valid user ID with non-null data
            //                var user = await context.Users.FindAsync(question.UserId);
            //                if (user != null && !string.IsNullOrWhiteSpace(user.Name) && !string.IsNullOrWhiteSpace(user.Email))
            //                {
            //                    await context.Questions.AddAsync(question);
            //                }
            //            }

            //             context.SaveChanges();
            //        }
            //    }
            //}

            //// Comment seeding logic
            //if (!context.Comments.Any())
            //{
            //    var commentsFilePath = "../VetHub02.Repository/Data/DataSeed/CommentsFile.json";
            //    if (File.Exists(commentsFilePath))
            //    {
            //        var commentsData = File.ReadAllText(commentsFilePath);
            //        var comments = JsonSerializer.Deserialize<List<Comment>>(commentsData);

            //        if (comments != null && comments.Any())
            //        {
            //            foreach (var comment in comments)
            //            {
            //                // Check if the comment's UserId corresponds to a valid user ID
            //                var user = await context.Users.FindAsync(comment.UserId);

            //                if (user != null && !string.IsNullOrWhiteSpace(user.Name) && !string.IsNullOrWhiteSpace(user.Email))
            //                {
            //                    await context.Comments.AddAsync(comment);
            //                }
            //            }

            //             context.SaveChanges();
            //        }
            //    }
            //}



        }
    }
}
