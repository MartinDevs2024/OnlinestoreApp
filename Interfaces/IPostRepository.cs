using EcommerceApp.Models;
using EcommerceApp.Models.Comments;
using EcommerceApp.Models.ViewModels;

namespace EcommerceApp.Interfaces
{
    public interface IPostRepository
    {
        Post GetPost(int id);
        IndexViewModel GetAllPosts(int pageNumber, string category, string search);
        List<Post> GetAllPost();
        void AddPost(Post post);
        void UpdatePost(Post post);
        void RemovePost(int id);
        void AddSubComment(SubComment comment);
        Task<bool> SaveChangeAsync();
    }
}
