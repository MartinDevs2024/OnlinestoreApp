using EcommerceApp.Interfaces;
using EcommerceApp.Models.Comments;
using EcommerceApp.Models.ViewModels;
using EcommerceApp.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp.Areas.UI.Controllers
{
    [Area("UI")]
    public class PostsController(IPostRepository repo) : Controller
    {
        public IActionResult Index()
        {
            /*var posts = repo.GetAllPost().ToList();
            return View(posts);*/
            return View();
        }

        public IActionResult Detail(int id)
        {
            return View(repo.GetPost(id));
        }

        [HttpPost]
        [Authorize(Roles = SD.Role_Customer)]
        public async Task<IActionResult> Comment(CommentViewModel vm)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Post", new { id = vm.PostId });

            var post = repo.GetPost(vm.PostId);

            if (vm.MainCommentId == 0)
            {
                post.MainComments = post.MainComments ?? new List<MainComment>();

                post.MainComments.Add(new MainComment
                {
                    Message = vm.Message,
                    Created = DateTime.Now
                });
                repo.UpdatePost(post);
            }
            else
            {
                var comment = new SubComment
                {
                    MainCommentId = vm.MainCommentId,
                    Message = vm.Message,
                    Created = DateTime.Now,
                };
                repo.AddSubComment(comment);
            }
            await repo.SaveChangeAsync();
            return RedirectToAction("Detail", new { id = vm.PostId });
        }

        #region
        [HttpGet]
        public IActionResult GetAll()
        {
            var objFromPost = repo.GetAllPost().ToList();
            return Json(new { data = objFromPost });
        }

        #endregion

    }
}
