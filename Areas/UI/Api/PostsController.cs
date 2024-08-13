using EcommerceApp.Interfaces;
using EcommerceApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp.Areas.UI.Api
{
    public class PostsController(IPostRepository repo) : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
        {
            var posts = repo.GetAllPost();
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var post = repo.GetPost(id);
            return Ok(post);
        }
    }
}
