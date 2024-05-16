using EcommerceApp.Interfaces;
using EcommerceApp.Models.Comments;
using EcommerceApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp.Areas.UI.Controllers
{
    [Area("UI")]
    public class PostsController : Controller
    {
        private readonly IPostRepository _repo;

        public PostsController(IPostRepository repo)
        {
            _repo = repo;
        }
        public IActionResult Index()
        {
            var posts = _repo.GetAllPost().ToList();
            return View(posts);
        }

        public IActionResult Detail(int id)
        {
            return View(_repo.GetPost(id));
        }

        [HttpPost]
        public async Task<IActionResult> Comment(CommentViewModel vm)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Post", new { id = vm.PostId });

            var post = _repo.GetPost(vm.PostId);

            if (vm.MainCommentId == 0)
            {
                post.MainComments = post.MainComments ?? new List<MainComment>();

                post.MainComments.Add(new MainComment
                {
                    Message = vm.Message,
                    Created = DateTime.Now
                });
                _repo.UpdatePost(post);
            }
            else
            {
                var comment = new SubComment
                {
                    MainCommentId = vm.MainCommentId,
                    Message = vm.Message,
                    Created = DateTime.Now,
                };
                _repo.AddSubComment(comment);
            }
            await _repo.SaveChangeAsync();
            return RedirectToAction("Detail", new { id = vm.PostId });
        }

        #region
        [HttpGet]
        public IActionResult GetAll()
        {
            var objFromPost = _repo.GetAllPost().ToList();
            return Json(new { data = objFromPost });
        }

        #endregion

    }
}
