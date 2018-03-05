using CheckListSL.DAL;
using CheckListSL.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CheckListSL.Servises
{
    public class ItemsService : IItemsService
    {
        private IChecklistRepository _checklistRepo;
        private IItemRepository _itemRepo;
        private ITranslationRepository _translationRepo;

        public ItemsService()
        {
            _itemRepo = new ItemRepository();
            _checklistRepo = new ChecklistRepository();
            _translationRepo = new TranslationRepository();
        }

        public List<Item> getAll(int checklistId)
        {
            return _itemRepo.GetItems(checklistId)
                .Include("Translations")
                .ToList();
        }

        public Item insert(int checklistId, Item item)
        {
            //Find checklist
            Checklist checklist = _checklistRepo.GetChecklistById(checklistId)
                .Include("Items")
                .ToList()
                .FirstOrDefault();

            item.ChecklistId = checklistId;

            foreach (var translation in item.Translations.ToList())
            {
                _translationRepo.InsertTranslation(translation);
            }

            _itemRepo.InsertItem(item);

            checklist.Items.Add(item);
            _checklistRepo.UpdateChecklist(checklistId, checklist);

            if (_checklistRepo.Save())
            {
                return item;
            }

            return null;
        }

        public Item update(int id, Item item) {
            List<Translation> updatedTranslations = new List<Translation>();

            foreach (var translation in item.Translations.ToList())
            {
                updatedTranslations.Add(_translationRepo.UpdateTranslation(id, translation));
            }

            bool isTranslationsSaved = _translationRepo.Save();

            Item itemToUpdate = _itemRepo.UpdateItem(id, item);

            if (isTranslationsSaved && (itemToUpdate != null) && _itemRepo.Save())
            {
                return itemToUpdate;
            }

            return null;
        }

        public Item delete(int id)
        {
            Item itemToDelete = _itemRepo.GetItemById(id).FirstOrDefault();
            List<Translation> translationsToDelete = _translationRepo.GetTranslations(id).ToList();

            bool isItemDeleted = true;
            bool isTranslationsDeleted = true;

            foreach (var translationToDelete in translationsToDelete)
            {
                if (translationToDelete != null)
                {
                    _translationRepo.DeleteTranslation(translationToDelete);
                }
            }

            isTranslationsDeleted = _translationRepo.Save();

            if (itemToDelete != null)
            {
                _itemRepo.DeleteItem(itemToDelete);
                isItemDeleted = _itemRepo.Save();
            }

            if (isItemDeleted && isTranslationsDeleted)
            {
                return itemToDelete;
            }

            return null;
        }
    }
}