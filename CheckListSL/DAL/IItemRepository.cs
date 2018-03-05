using CheckListSL.Models;
using System.Linq;

namespace CheckListSL.DAL
{
    interface IItemRepository
    {
        IQueryable<Item> GetItems(int checklistId);
        IQueryable<Item> GetItemById(int id);
        Item InsertItem(Item item);
        Item UpdateItem(int id, Item newItem);
        Item DeleteItem(Item item);

        bool Save();
    }
}
