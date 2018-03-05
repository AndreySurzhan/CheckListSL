using CheckListSL.Models;
using System.Linq;

namespace CheckListSL.DAL
{
    interface ITranslationRepository
    {
        IQueryable<Translation> GetTranslations(int itemId);
        Translation InsertTranslation(Translation translation);
        Translation UpdateTranslation(int itemId, Translation newTranslation);
        Translation DeleteTranslation(Translation translation);

        bool Save();
    }
}
