using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CheckListSL.Models;
using System.Data.Entity.Validation;
using System.Data.Entity;

namespace CheckListSL.DAL
{
    public class TranslationRepository : ITranslationRepository
    {
        private ChecklistSLContext _ctx;

        public TranslationRepository()
        {
            _ctx = new ChecklistSLContext();
        }

        public Translation DeleteTranslation(Translation translation)
        {
            try
            {
                _ctx.Translations.Attach(translation);
                return _ctx.Translations.Remove(translation);
            }
            catch (Exception ex)
            {
                //TODO: Log error
                throw ex;
            }
        }

        public IQueryable<Translation> GetTranslations(int itemId)
        {
            return _ctx.Translations.Where(c => c.ItemId == itemId);
        }

        public Translation InsertTranslation(Translation translation)
        {
            Translation newTranslation;

            try
            {
                //Create new trasaction
                translation.CreatedOn = DateTime.UtcNow;
                translation.UpdatedOn = DateTime.UtcNow;
                newTranslation = _ctx.Translations.Add(translation);

                if (newTranslation != null)
                {
                    return newTranslation;
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

        public Translation UpdateTranslation(int itemId, Translation transaction)
        {
            Translation newTranslation;

            try
            {
                newTranslation = _ctx.Translations.Where(t => t.ItemId == itemId && t.Language == transaction.Language).FirstOrDefault();

                if (newTranslation != null)
                {
                    newTranslation.Language = transaction.Language;
                    newTranslation.TranslationString = transaction.TranslationString;
                    newTranslation.UpdatedOn = DateTime.UtcNow;

                    return newTranslation;
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