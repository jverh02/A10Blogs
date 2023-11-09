using System;
using EFTutorial.Models;
using System.Linq;

namespace EFTutorial
{
    class Program
    {
        static void Main(string[] args)
        {
            var done = false;
            while (!done) { 
            Console.Write("What would you like to do?\n1. Display Blogs\n2. Add Blog\n3. Display Posts\n4. Add Post\n5. Exit\n>");
            var input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        Console.WriteLine("");

                        // 1. List Posts for Blog #1
                        using (var db = new BlogContext())
                        {
                            var blogs = db.Blogs;
                            foreach (var item in blogs)
                            {
                                Console.WriteLine(item.BlogId + ": " + item.Name);
                            }
                            /*  var blog = db.Blogs.Where(x => x.BlogId == 1).FirstOrDefault();
                              // var blogsList = blog.ToList(); // convert to List from IQueryable

                              System.Console.WriteLine($"Posts for Blog {blog.Name}");

                              foreach (var post in blog.Posts) {
                                  System.Console.WriteLine($"\tPost {post.PostId} {post.Title}");
                              }
                            */
                        }
                        break;
                    case "2":
                        Console.Write("Enter your blog's name.\n>");
                        var name = Console.ReadLine();
                        var blog = new Blog();
                        blog.Name = name;

                        using (var db = new BlogContext())
                        {
                            db.Add(blog);
                            db.SaveChanges();
                        }
                        break;
                    case "3":
                        Console.Write("Which blog do you want to view?\n>");
                        var blogToFind = Console.ReadLine();
                        var isFound = false;
                        using (var db = new BlogContext())
                        {
                            Blog foundBlog = null; //Note to Self: switch cases not having separate scopes is annoying, find alternatives.
                            foreach (var item in db.Blogs)
                            {
                                if (item.Name == blogToFind)
                                {
                                    foundBlog = item;
                                }
                            }
                            if (foundBlog != null)
                            {
                                foreach (var post in foundBlog.Posts)
                                {
                                    Console.WriteLine(post.PostId + ": " + post.Title + " - " + post.Content);
                                }
                            }
                            else
                            {
                                Console.WriteLine("ERROR: Blog with that name not found.");
                            }
                        }
                        break;
                    case "4":
                        Console.Write("Which blog do you want to add a post to?\n>");
                        var blogToAddTo = Console.ReadLine();
                        Blog found = null;
                        using (var db = new BlogContext())
                        {
                            foreach (var item in db.Blogs)
                            {
                                if (item.Name == blogToAddTo)
                                {
                                    found = item;
                                }
                            }
                            if (found != null)
                            {
                                Console.Write("Input your post title.\n>");
                                var postTitle = Console.ReadLine();
                                Console.Write("Input your post content.\n>");
                                var postContent = Console.ReadLine();
                                Post post = new Post();
                                post.Title = postTitle;
                                post.Content = postContent;
                                post.BlogId = found.BlogId;
                                db.Posts.Add(post);
                                db.SaveChanges();
                            }
                            else
                            {
                                Console.WriteLine("ERROR: Blog with that name not found.");
                            }
                        }
                        break;
                    case "5":
                        done = true;
                        break;
                    default:
                        Console.WriteLine("ERROR: That was not a valid option.");
                        break;
                }
                    // 2. Add Post to database
                    // System.Console.WriteLine("Enter your Post title");
                    // var postTitle = Console.ReadLine();

                    // var post = new Post();
                    // post.Title = postTitle;
                    // post.BlogId = 1;

                    // using (var db = new BlogContext())
                    // {
                    //     db.Posts.Add(post);
                    //     db.SaveChanges();
                    // }

                    // 3. Read Blogs from database
                    // using (var db = new BlogContext()) 
                    // {
                    //     System.Console.WriteLine("Here is the list of blogs");
                    //     foreach (var b in db.Blogs) {
                    //         System.Console.WriteLine($"Blog: {b.BlogId}: {b.Name}");
                    //     }
                    // }

                    // 4. Add Blog to Database
                    // System.Console.WriteLine("Enter your Blog name");
                    // var blogName = Console.ReadLine();

                    // // Create new Blog
                    // var blog = new Blog();
                    // blog.Name = blogName;

                    // // Save blog object to database
                    // using (var db = new BlogContext()) 
                    // {
                    //     db.Add(blog);
                    //     db.SaveChanges();
                    // }
            }
        }
    }
}
