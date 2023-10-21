using Microsoft.EntityFrameworkCore;
using VitalCareWeb.Data;

namespace VitalCareWeb.Services.ArticleCategory
{
    public class ArticleCategoryService : IArticleCategoryService
    {
        private ApplicationDbContext _context;
        public ArticleCategoryService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Entities.ArticleCategory?> GetById(int id)
        {
            return await _context.FindAsync<Entities.ArticleCategory>(id);
        }

        public async Task<IEnumerable<Entities.ArticleCategory>> GetAll()
        {
            return await _context.ArticleCategories.ToListAsync();
        }

        public async Task<bool> Add(Entities.ArticleCategory ArticleCategory)
        {
            _context.ArticleCategories.Add(ArticleCategory);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<(bool, string)> Delete(int id)
        {
            var ArticleCategory = await _context.ArticleCategories.Include(r => r.Articles).SingleOrDefaultAsync(r => r.Id == id);
            if (ArticleCategory != null)
            {
                if (ArticleCategory.Articles.Any())
                {
                    return (false, "ArticleCategory already assigned with article");
                }

                _context.ArticleCategories.Remove(ArticleCategory);
                await _context.SaveChangesAsync();
                return (true, "Success");
            }
            return (false, "ArticleCategory not found");
        }

        public async Task<bool> IsDublicate(int id, string name)
        {
            if (id == 0)
            {
                if (await _context.ArticleCategories.FirstOrDefaultAsync(r => r.Name == name) != null)
                    return true;
            }
            else
            {
                if (await _context.ArticleCategories.FirstOrDefaultAsync(r => r.Name == name && r.Id != id) != null)
                    return true;
            }
            return false;
        }

        public async Task<bool> Update(Entities.ArticleCategory ArticleCategory)
        {
            _context.Update(ArticleCategory);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
