using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class PostsController
    {
        private readonly ILogger<PostsController> _logger;
        private readonly IPostsService _service;

        public PostsController(ILogger<PostsController> logger, IPostsService service)
        {
            _logger = logger;
            _service = service;
        }

        [AllowAnonymous]
        [HttpGet]
        public IEnumerable<Post> GetPosts()
        {
            return _service.GetPosts();
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<Post> GetPostById(Guid id)
        {
            return await _service.GetPostById(id);
        }

        [Authorize(Roles = "Writer, Admin, Moderator")]
        [HttpPost]
        public async Task<Guid> PostPost(Post Post)
        {
            return await _service.AddPost(Post);
        }

        [Authorize(Roles = "Writer, Admin, Moderator")]
        [HttpDelete("{id}")]
        public async Task<bool> DeleteById(Guid id)
        {
            return await _service.DeletePostById(id);
        }

        [Authorize(Roles = "Writer, Admin, Moderator")]
        [HttpPut]
        public async Task<bool> PutPost(Post Post)
        {
            return await _service.PutPost(Post);
        }
    }
}
