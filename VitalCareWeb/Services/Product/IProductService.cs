namespace VitalCareWeb.Services.Product;
using VitalCareWeb.Entities;
public interface IProductService
{
    Task<bool> IsDublicate(int id, string name);
    Task<Product?> GetById(int id);
    Task<IEnumerable<Product>> GetAll();
    Task<bool> Add(Product product);
    Task<bool> Update(Product product);
    Task<bool> Delete(int id);
}
