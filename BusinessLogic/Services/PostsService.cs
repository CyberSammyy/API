using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class PostsService : IPostsService
    {
        public Task<Guid> AddPost(Post user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeletePostById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Post> GetPostById(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Post> GetPosts()
        {
            throw new NotImplementedException();
        }

        public Task<bool> PutPost(Post user)
        {
            throw new NotImplementedException();
        }
    }
}
