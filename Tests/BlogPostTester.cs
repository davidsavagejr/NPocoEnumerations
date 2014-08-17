using System;
using System.Collections.Generic;
using Domain;
using Domain.Models;
using Headspring;
using NPoco;
using NUnit.Framework;
using Should;

namespace Tests
{
    // ReSharper disable InconsistentNaming

    [TestFixture]
    public class BlogPostTester
    {
        [TestFixtureSetUp]
        public void Setup()
        {
            using (var db = Domain.DatabaseFactory.GetDatabase())
            {
                var bookPost = new BlogPost()
                    {
                        Category = Category.Books,
                        Title = "Book Post",
                        Body = "Something written about a book",
                        PostDate = DateTime.Now
                    };

                var videoPost = new BlogPost()
                                    {
                                        Category = Category.Videos,
                                        Title = "Video Post",
                                        Body = "Something written about a video",
                                        PostDate = DateTime.Now
                                    };

                var sitePost = new BlogPost()
                                    {
                                        Category = Category.Websites,
                                        Title = "Website Post",
                                        Body = "Something written about a website",
                                        PostDate = DateTime.Now
                                    };

                db.Insert(bookPost);
                db.Insert(videoPost);
                db.Insert(sitePost);
            }
        }

        [Test]
        public void bookpost_should_be_correct_category()
        {
            using (var db = Domain.DatabaseFactory.GetDatabase())
            {
                var post = db.FirstOrDefault<BlogPost>("WHERE Title = @0", "Book Post");
                post.ShouldNotBeNull();
                post.Category.ShouldNotBeNull();
                post.Category.Value.ShouldEqual(Category.Books.Value);
            }

        }

        [Test]
        public void videopost_should_be_correct_category()
        {
            using (var db = Domain.DatabaseFactory.GetDatabase())
            {
                var post = db.FirstOrDefault<BlogPost>("WHERE Title = @0", "Video Post");
                post.ShouldNotBeNull();
                post.Category.ShouldNotBeNull();
                post.Category.Value.ShouldEqual(Category.Videos.Value);
            }

        }

        [Test]
        public void siteopost_should_be_correct_category()
        {
            using (var db = Domain.DatabaseFactory.GetDatabase())
            {
                var post = db.FirstOrDefault<BlogPost>("WHERE Title = @0", "Website Post");
                post.ShouldNotBeNull();
                post.Category.ShouldNotBeNull();
                post.Category.Value.ShouldEqual(Category.Websites.Value);
            }

        }
    }

    // ReSharper restore InconsistentNaming 
}