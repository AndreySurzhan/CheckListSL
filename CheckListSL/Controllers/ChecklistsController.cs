using CheckListSL.Models;
using CheckListSL.Servises;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CheckListSL.Controllers
{
    [RoutePrefix("api/v1/checklists")]
    public class ChecklistsController : ApiController
    {
        private ChecklistsService _checklistsService;

        public ChecklistsController()
        {
            _checklistsService = new ChecklistsService();
        }

        [Route("")]
        [HttpGet]
        public IEnumerable<Checklist> Get()
        {
            return _checklistsService.getAll();
        }

        [Route("{id}")]
        [HttpGet]
        public Checklist Get(int id)
        {
            return _checklistsService.getById(id);
        }

        [Route("")]
        [HttpPost]
        public HttpResponseMessage Post([FromBody]Checklist checklist)
        {
            Checklist newChecklist = _checklistsService.insert(checklist);

            if (newChecklist != null)
            {
                return Request.CreateResponse(HttpStatusCode.Created, newChecklist);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        [Route("{id}")]
        [HttpPatch]
        public HttpResponseMessage Put(int id, [FromBody]Checklist checklist)
        {
            Checklist updatedChecklist = _checklistsService.update(id, checklist);

            if (updatedChecklist != null)
            {
                return Request.CreateResponse(HttpStatusCode.Created, updatedChecklist);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        [Route("{id}")]
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            Checklist deletedChecklist = _checklistsService.delete(id);

            if (deletedChecklist != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, deletedChecklist);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}