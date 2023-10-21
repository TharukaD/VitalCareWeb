namespace VitalCareWeb.Services.Tag;
using VitalCareWeb.Entities;
public interface ITagService
{
    Task<bool> IsDublicate(int id, string name);
    Task<Tag?> GetById(int id);
    Task<IEnumerable<Tag>> GetAll();
    Task<bool> Add(Tag tag);
    Task<bool> Update(Tag tag);
    Task<(bool, string)> Delete(int id);
}
