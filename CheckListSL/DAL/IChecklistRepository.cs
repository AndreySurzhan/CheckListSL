using CheckListSL.Models;
using System.Linq;

namespace CheckListSL.DAL
{
    public interface IChecklistRepository
    {
        IQueryable<Checklist> GetChecklists();
        IQueryable<Checklist> GetChecklistById(int id);
        Checklist InsertChecklist(Checklist checklist);
        Checklist UpdateChecklist(int id, Checklist newChecklist);
        Checklist DeleteChecklist(int id);

        bool Save();
    }
}
