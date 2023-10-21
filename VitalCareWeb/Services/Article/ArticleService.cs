using Microsoft.EntityFrameworkCore;
using VitalCareWeb.Data;

namespace VitalCareWeb.Services.Article;
using VitalCareWeb.Entities;
public class ArticleService : IArticleService
{
    private ApplicationDbContext _context;
    public ArticleService(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Article?> GetById(int id)
    {
        return await _context.Articles
            .Include(r => r.ArticleCategory)
            .Include(r => r.ArticleTags)
                .ThenInclude(r => r.Tag)
            .SingleOrDefaultAsync(r => r.Id == id);
    }

    public async Task<IEnumerable<Article>> GetAll()
    {
        return await _context.Articles
            .Include(r => r.ArticleCategory)
            .Include(r => r.ArticleTags)
                .ThenInclude(r => r.Tag)
            .ToListAsync();
    }

    public async Task<bool> Add(Article Article)
    {
        _context.Articles.Add(Article);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<(bool, string)> Delete(int id)
    {
        var Article = await _context.Articles.SingleOrDefaultAsync(r => r.Id == id);
        if (Article != null)
        {
            _context.Articles.Remove(Article);
            await _context.SaveChangesAsync();
            return (true, "Success");
        }
        return (false, "Article not found");
    }

    public async Task<bool> IsDublicate(int id, string name)
    {
        if (id == 0)
        {
            if (await _context.Articles.FirstOrDefaultAsync(r => r.Title == name) != null)
                return true;
        }
        else
        {
            if (await _context.Articles.FirstOrDefaultAsync(r => r.Title == name && r.Id != id) != null)
                return true;
        }
        return false;
    }

    public async Task<bool> Update(Article Article)
    {
        _context.Update(Article);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> AssignTags(int articleId, List<int> tagIds)
    {
        var Article = await _context.Articles.SingleOrDefaultAsync(r => r.Id == articleId);
        if (Article != null && tagIds.Count > 0)
        {
            var existTags = await _context.ArticleTags.Where(r => r.ArticleId == articleId).ToListAsync();
            if (existTags.Any())
            {
                _context.ArticleTags.RemoveRange(existTags);
                await _context.SaveChangesAsync();
            }

            var articleTags = new List<ArticleTag>();
            foreach (var tagId in tagIds)
            {
                var articleTag = new ArticleTag()
                {
                    ArticleId = articleId,
                    TagId = tagId,
                };
                articleTags.Add(articleTag);
            }
            _context.ArticleTags.AddRange(articleTags);
            await _context.SaveChangesAsync();
            return true;
        }
        return true;
    }
}
