namespace FashionEcommerceMVC
{
    public interface IHomeRepository
    {
        Task<IEnumerable<Product>> GetProducts(string sTerm = "", int categotryId = 0);
        Task<IEnumerable<Category>> Categories();
    }
}