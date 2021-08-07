using AutoMapper;
using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using DataAccess.Interfaces;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class PostsService : IPostsService
    {
        private readonly IPostsRepository _postsRepository;
        private readonly IMapper _mapper;

        public PostsService(IPostsRepository postsRepository, IMapper mapper)
        {
            _postsRepository = postsRepository;
            _mapper = mapper;
        }

        public async Task<Guid> AddPost(Post post)
        {
            return await _postsRepository.AddPost(_mapper.Map<PostDTO>(post));
        }

        public async Task<bool> DeletePostById(Guid id)
        {
            return await _postsRepository.DeletePostById(id);
        }

        public async Task<Post> GetPostById(Guid id)
        {
            return _mapper.Map<Post>(await _postsRepository.GetPostById(id));
        }

        public IEnumerable<Post> GetPosts()
        {
            return _mapper.Map<IEnumerable<Post>>(_postsRepository.GetPosts());
        }

        public async Task<bool> PutPost(Post post)
        {
            return await _postsRepository.PutPost(_mapper.Map<PostDTO>(post));
        }
    }
}
