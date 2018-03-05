using System;
using System.Linq;
using CheckListSL.Models;
using System.Data.Entity.Validation;

namespace CheckListSL.DAL
{
    public class ChecklistRepository : IChecklistRepository
    {
        private ChecklistSLContext _ctx;

        public ChecklistRepository()
        {
            _ctx = new ChecklistSLContext();
        }

        public Checklist DeleteChecklist(int id)
        {
            Checklist checkListToRemove;
            try
            {
                checkListToRemove = _ctx.Checklists.Where(c => c.Id == id).FirstOrDefault();
                return _ctx.Checklists.Remove(checkListToRemove);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<Checklist> GetChecklistById(int Id)
        {
            try
            {
                return _ctx.Checklists.Where(c => c.Id == Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<Checklist> GetChecklists()
        {
            try
            {
                return _ctx.Checklists;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Checklist InsertChecklist(Checklist checklist)
        {
            Checklist newChecklist;
            
            try
            {
                checklist.CreatedOn = DateTime.UtcNow;
                checklist.UpdatedOn = DateTime.UtcNow;
                newChecklist = _ctx.Checklists.Add(checklist);

                if (newChecklist != null)
                {
                    return newChecklist;
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
            catch (DbEntityValidationException ex) {
                throw ex;
            }
            catch (Exception ex)
            {
                //TODO: Log error
                throw ex;
            }
        }

        public Checklist UpdateChecklist(int id, Checklist checklist)
        {
            Checklist newChecklist;

            try
            {
                newChecklist = _ctx.Checklists.Where(c => c.Id == id).FirstOrDefault();

                if (newChecklist != null)
                {
                    newChecklist.Name = checklist.Name;
                    newChecklist.isActive = checklist.isActive;
                    newChecklist.UpdatedOn = DateTime.UtcNow;

                    return newChecklist;
                }

                //TODO Log error
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}