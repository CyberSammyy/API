using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IPostsRepository
    {
        public Task<Guid> AddPost(PostDTO user);
        public Task<bool> DeletePostById(Guid id);
        public Task<bool> PutPost(PostDTO user);
        public Task<PostDTO> GetPostById(Guid id);
        public IEnumerable<PostDTO> GetPosts();
    }
}
