using CheckListSL.DAL;
using CheckListSL.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CheckListSL.Servises
{
    public class ChecklistsService : IChecklistsService
    {
        private IChecklistRepository _checklistRepo;
        private IItemRepository _itemRepo;
        private ITranslationRepository _translationRepo;

        public ChecklistsService()
        {
            _checklistRepo = new ChecklistRepository();
            _itemRepo = new ItemRepository();
            _translationRepo = new TranslationRepository();
        }

        public List<Checklist> getAll()
        {
            return _checklistRepo.GetChecklists()
                .Include(c => c.Items.Select(i => i.Translations))
                .ToList();
        }

        public Checklist getById(int id)
        {
            return _checklistRepo.GetChecklistById(id)
                .Include(c => c.Items.Select(i => i.Translations))
                .ToList()
                .FirstOrDefault();
        }

        public Checklist insert(Checklist checklist)
        {
            Checklist newChecklist = _checklistRepo.InsertChecklist(checklist);
            bool isSaved = _checklistRepo.Save();

            if ((newChecklist != null) && isSaved)
            {
                return newChecklist;
            }

            return null;
        }

        public Checklist update(int id, Checklist checklist)
        {
            Checklist updatedChecklist = _checklistRepo.UpdateChecklist(id, checklist);
            bool isSaved = _checklistRepo.Save();

            if ((updatedChecklist != null) && isSaved)
            {
                return updatedChecklist;
            }

            return null;
        }

        public Checklist delete(int id)
        {
            bool isCheckListDeleted = true;
            bool isItemsDeleted = true;
            bool isTranslationsDeleted = true;

            Checklist checklistToDelete = _checklistRepo.GetChecklistById(id)
                .Include("Items")
                .Single();

            var itemsToDelete = checklistToDelete.Items.ToList();

            Checklist deletedChecklist = _checklistRepo.DeleteChecklist(id);

            if (!itemsToDelete.Any() || itemsToDelete == null)
            {
                isCheckListDeleted = _checklistRepo.Save();

                if (!isCheckListDeleted)
                {
                    deletedChecklist = null;
                }

                return deletedChecklist;
            }
            else
            {
                //Delete all Items and corresponding translations
                foreach (var item in itemsToDelete)
                {
                    Item itemToDelete = _itemRepo.GetItemById(item.Id).FirstOrDefault();

                    List<Translation> translationsToDelete = _translationRepo.GetTranslations(item.Id).ToList();

                    foreach (var translationToDelete in translationsToDelete)
                    {
                        if (translationToDelete != null)
                        {
                            _translationRepo.DeleteTranslation(translationToDelete);
                        }
                    }

                    if (itemToDelete != null)
                    {
                        _itemRepo.DeleteItem(itemToDelete);
                    }
                }

                isCheckListDeleted = _checklistRepo.Save();
                isTranslationsDeleted = _translationRepo.Save();
                isItemsDeleted = _itemRepo.Save();

                if (!isTranslationsDeleted || !isTranslationsDeleted && !isTranslationsDeleted)
                {
                    deletedChecklist = null;
                }

                return deletedChecklist;
            }
        }
    }
}