namespace VitalCareWeb.Services.Article;
using VitalCareWeb.Entities;
public interface IArticleService
{
    Task<Article?> GetById(int id);
    Task<IEnumerable<Article>> GetAll();
    Task<IEnumerable<Article>> GetRandomArticles();
    Task<bool> Add(Article article);
    Task<bool> Update(Article article);
    Task<(bool, string)> Delete(int id);
    Task<bool> IsDublicate(int id, string name);
    Task<bool> AssignTags(int articleId, List<int> tagIds);
}
