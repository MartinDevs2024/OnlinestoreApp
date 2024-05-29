namespace EcommerceApp.Data.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        IProductRepository Product { get; }
        IProductImageRepository ProductImage { get; }
        ICompanyRepository Company { get; }
        void Save();
    }
}
