using DataAccess.Classes;
using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess
{
    public class PostsRepository : IPostsRepository
    {
        private readonly DbContextOptions<UsersDBContext> _options;
        public PostsRepository(DbContextOptions<UsersDBContext> options)
        {
            _options = options;
        }
        public async Task<Guid> AddPost(PostDTO post)
        {
            using (var context = new UsersDBContext(_options))
            {
                try
                {
                    await context.AddAsync(post);
                    await context.SaveChangesAsync();
                    return post.Id;
                }
                catch (Exception)
                {
                    return Guid.Empty;
                }

            }
        }

        public async Task<PostDTO> GetPostById(Guid id)
        {
            using (var context = new UsersDBContext(_options))
            {
                try
                {
                    var post = await context.Posts.FirstOrDefaultAsync(x => x.Id == id);
                    return post;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public IEnumerable<PostDTO> GetPosts()
        {
            using (var context = new UsersDBContext(_options))
            {
                try
                {
                    var posts = context.Posts.ToList();
                    return posts;
                }
#pragma warning disable CS0168 // Variable is declared but never used
                catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
                {
                    return null;
                }
            }
        }

        public async Task<bool> PutPost(PostDTO post)
        {
            using (var context = new UsersDBContext(_options))
            {
                try
                {
                    var foundPosts = await context.Posts.FirstOrDefaultAsync(x => x.Id == post.Id);
                    context.Update(post);
                    await context.SaveChangesAsync();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public async Task<bool> DeletePostById(Guid id)
        {
            using (var context = new UsersDBContext(_options))
            {
                try
                {
                    var postToDelete = await context.Posts.FirstOrDefaultAsync(x => x.Id == id);
                    context.Posts.Remove(postToDelete);
                    await context.SaveChangesAsync();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}
