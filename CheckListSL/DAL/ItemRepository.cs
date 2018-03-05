using CheckListSL.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace CheckListSL.DAL
{
    public class ItemRepository : IItemRepository

    {
        private ChecklistSLContext _ctx;

        public ItemRepository()
        {
            _ctx = new ChecklistSLContext();
        }

        public IQueryable<Item> GetItemById(int id)
        {
            return _ctx.Items.Where(c => c.Id == id);
        }

        public IQueryable<Item> GetItems(int checklistId)
        {
            return _ctx.Items.Where(c => c.ChecklistId == checklistId);
        }

        public Item InsertItem(Item item)
        {
            Item newItem;

            try
            {
                //Create Item
                item.CreatedOn = DateTime.UtcNow;
                item.UpdatedOn = DateTime.UtcNow;
                newItem = _ctx.Items.Add(item);

                if (newItem != null)
                {
                    return newItem;
                }

                //TODO Log error
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        Item IItemRepository.UpdateItem(int id, Item item)
        {
            Item existingItem;

            try
            {
                existingItem = _ctx.Items.Where(i => i.Id == id)
                    .Include("Translations")
                    .FirstOrDefault();
            
                if (existingItem != null)
                {
                    existingItem.Name = item.Name;
                    existingItem.isChecked = item.isChecked;
                    existingItem.UpdatedOn = DateTime.UtcNow;
                   
                    return existingItem;
                }

                //TODO Log error
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Save()
        {
            try
            {
                return _ctx.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                //TODO: Log error
                throw ex;
            }
        }

        public Item DeleteItem(Item item)
        {
            try
            {
                //_ctx.Entry(item).State = EntityState.Deleted;
                _ctx.Items.Attach(item);
                return _ctx.Items.Remove(item);
            }
            catch (Exception ex)
            {
                //TODO: Log error
                throw ex;
            }
        }
    }
}