using CheckListSL.Models;
using CheckListSL.Servises;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CheckListSL.Controllers
{
    [RoutePrefix("api/v1/checklists/{checklistId}/items")]
    public class ItemsController : ApiController
    {
        private ItemsService _itemsService;

        public ItemsController()
        {
            _itemsService = new ItemsService();
        }

        [Route("")]
        [HttpGet]
        public HttpResponseMessage Get(int checklistId)
        {
            List<Item> items = _itemsService.getAll(checklistId);

            if (items != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, items);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        [Route("")]
        [HttpPost]
        public HttpResponseMessage Post(int checklistId, [FromBody]Item item)
        {
            //Find checklist
            Item newAddedItem = _itemsService.insert(checklistId, item);

            if (newAddedItem != null)
            {
                return Request.CreateResponse(HttpStatusCode.Created, newAddedItem);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        [Route("{id}")]
        [HttpPatch]
        public HttpResponseMessage UpdateItem(int id, [FromBody]Item item)
        {
            Item updatedItem = _itemsService.update(id, item);

            if (updatedItem != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, updatedItem);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        [Route("{id}")]
        [HttpDelete]
        public HttpResponseMessage DeleteItem(int id)
        {
            Item deleteItem = _itemsService.delete(id);

            if (deleteItem != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, deleteItem);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}