namespace VitalCareWeb.Services.ArticleCategory;
using VitalCareWeb.Entities;
public interface IArticleCategoryService
{
    Task<bool> IsDublicate(int id, string name);
    Task<ArticleCategory?> GetById(int id);
    Task<IEnumerable<ArticleCategory>> GetAll();
    Task<bool> Add(ArticleCategory articleCategory);
    Task<bool> Update(ArticleCategory articleCategory);
    Task<(bool, string)> Delete(int id);
}
