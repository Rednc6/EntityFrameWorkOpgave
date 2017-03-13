using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Collections;

namespace EntityFrameWorkOpgave
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("1.Create Blog\n2.CreatePost\n3.List All blogs\n4.List specific blog\n5.Edit Blog or Post\n0.End\nPlease enter your choice:");
            int choice = int.Parse(Console.ReadLine());
            Console.Clear();
            while (choice != 0)
            {
                switch (choice)
                {
                    case 1:
                        CreateBlog();
                        break;
                    case 2:
                        CreatePost();
                        break;
                    case 3:
                        ListAllBlogs();
                        break;
                    case 4:
                        ListSpecificBlog();
                        break;
                    case 5:
                        ChangeBlogPost();
                        break;

                }
                Console.WriteLine(
                    "1.Create Blog\n2.Create Post\n3.List All blogs\n4.List specific Blog\n5.Edit Blog or Post\n0.End\nPlease enter your choice:");
                choice = int.Parse(Console.ReadLine());
                Console.Clear();
            }
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();

        }


        private static void CreateBlog()
        {
            using (var context = new BlogContext())
            {
                Blog newBlog = new Blog();

                Console.Clear();
                Console.WriteLine("Enter a name for a new Blog: ");
                newBlog.Name = Console.ReadLine();
                Console.WriteLine("Enter the name of the Blogger: ");
                newBlog.UserName = Console.ReadLine();
                Console.WriteLine("Enter a URL for the new Blog: ");
                newBlog.Url = Console.ReadLine();

                context.Blogs.Add(newBlog);
                // context.Entry(newBlog).State = EntityState.Added;
                context.SaveChanges();
            }

            Console.Clear();
        }

        private static void CreatePost()
        {
            using (var context = new BlogContext())
            {
                Post newPost = new Post();

                Console.Clear();
                Console.WriteLine("Enter a title for a new post: ");
                newPost.Title = Console.ReadLine();
                Console.WriteLine("Enter a Content for the new post: ");
                newPost.Content = Console.ReadLine();
                Console.WriteLine("Enter a Blog ID int for the new Post: ");
                newPost.BlogId = int.Parse(Console.ReadLine());

                context.Posts.Add(newPost);
                // context.Entry(newPost).State = EntityState.Added;
                context.SaveChanges();

            }

            Console.Clear();
        }

        private static void ListAllBlogs()
        {
            using (var context = new BlogContext())
            {
                Console.Clear();
                Console.WriteLine("All Blogs \n");

                var allBlogs = context.Blogs.Include(x => x.Posts).ToList();

                foreach (var blogObj in allBlogs)
                {
                    Console.WriteLine(blogObj.BlogId + "\n" + blogObj.Name + "\n" + blogObj.Url);
                    Console.WriteLine(string.Join("\n", blogObj.Posts));
                }
            }

            Console.ReadLine();
            Console.Clear();
        }

        private static void ListSpecificBlog()
        {
            using (var context = new BlogContext())
            {
                int specificBlog;

                Console.WriteLine("Search DB for specific Blog and post, using BlogID: ");
                specificBlog = int.Parse(Console.ReadLine());

                Console.Clear();
                var specBlog = context.Blogs.Include(x => x.Posts).Where(y => y.BlogId == specificBlog).ToList();

                foreach (var blogObj in specBlog)
                {
                    Console.WriteLine(blogObj.BlogId + "\n" + blogObj.Name + "\n" + blogObj.Url);
                    Console.WriteLine(string.Join("\n", blogObj.Posts));
                }


            }

            Console.ReadLine();
            Console.Clear();
        }

        private static void ChangeBlogPost()
        {
            using (var context = new BlogContext())
            {
                int input;
                int input2;

                var allBlogs = context.Blogs.Include(x => x.Posts).ToList();

                foreach (var blogObj in allBlogs)
                {
                    Console.WriteLine(blogObj.BlogId + "\n" + blogObj.Name + "\n" + blogObj.Url);
                    Console.WriteLine(string.Join("\n", blogObj.Posts));
                }

                Console.WriteLine("\nSelect a Blog using BlogID: ");
                input = int.Parse(Console.ReadLine());

                Console.WriteLine("\nDelete or Edit \n 1. Delete \n 2. Edit");
                input2 = int.Parse(Console.ReadLine());

                switch (input2)
                {
                    case 1:
                        var itemToRemove = context.Blogs.SingleOrDefault(x => x.BlogId == input);
                        context.Blogs.Remove(itemToRemove);
                        break;
                    case 2:
                        Console.WriteLine("Do you want to edit post title or content");
                        
                        break;
                    default:
                        break;
                }

                context.SaveChanges();
            }

            Console.Clear();
        }
        
    }

}

