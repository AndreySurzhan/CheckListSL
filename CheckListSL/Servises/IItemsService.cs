using CheckListSL.Models;
using System.Collections.Generic;

namespace CheckListSL.Servises
{
    interface IItemsService
    {
        List<Item> getAll(int checklistId);

        Item insert(int checklistId, Item item);

        Item update(int id, Item item);

        Item delete(int id);
    }
}