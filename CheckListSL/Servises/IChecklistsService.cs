using CheckListSL.DAL;
using CheckListSL.Models;
using System.Collections.Generic;

namespace CheckListSL.Servises
{
    interface IChecklistsService
    {
        List<Checklist> getAll();

        Checklist getById(int id);

        Checklist insert(Checklist checklist);

        Checklist update(int id, Checklist checklist);

        Checklist delete(int id);
    }
}