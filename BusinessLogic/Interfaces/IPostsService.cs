using BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IPostsService
    {
        public Task<Guid> AddPost(Post user);
        public Task<bool> DeletePostById(Guid id);
        public Task<Post> GetPostById(Guid id);
        public IEnumerable<Post> GetPosts();
        public Task<bool> PutPost(Post user);
    }
}
