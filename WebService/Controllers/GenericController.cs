using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataAccessLayer;
using WebService.Models;
using DataAccessLayer.Repository;
using Newtonsoft.Json;

namespace WebService.Controllers
{
    [FormatFilter]
    [Route("api/[controller]")]

    public abstract class GenericReadableController<TEntity> : Controller where TEntity : GenericModel
    {
        protected readonly IDataService _dataService;
        protected readonly GenericRepository<TEntity> _repository;
        public GenericReadableController(IDataService dataService, GenericReadableRepository<TEntity> repository)
        {
            this._dataService = dataService;
            this._repository = repository as GenericReadableRepository<TEntity>;
        }

        public class EncapsulateReference {
            public readonly TEntity Reference;
            public readonly string Prev;
            public readonly string Current;
            public readonly string Next;
            public EncapsulateReference(string url, List<TEntity> repository, TEntity refere) {
                this.Reference = refere;
                var r = repository;
                Nullable<Int32> nextId;
                try {
                    nextId = r.Find(e => e.Id > this.Reference.Id).Id as Nullable<Int32>;
                }
                catch (Exception) {
                    nextId = null;
                } 
                this.Next = this.createUrl(url, nextId);
                
                r.Reverse();

                Nullable<Int32> prevId;
                try {
                    prevId = r.Find(e => e.Id < this.Reference.Id).Id as Nullable<Int32>;
                }
                catch (Exception) {
                    prevId = null;
                }
                this.Prev = this.createUrl(url, prevId);
                this.Current = this.createUrl(url, this.Reference.Id);
            }

            private string createUrl(string controller, int? id = null) {
                if (id == null) {
                    return "";
                }
                return "/api/" + controller + "/" + id;
            }
        }

        // GET api/[controller]
        [HttpGet]
        public IActionResult Get(int page = 0, int pageSize = 50)
        {
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString().ToLower();
            if (pageSize > 200 || pageSize <= 0) {
                pageSize = 50;
            }
            if (page > this.Count()/pageSize) {
                page = this.Count()/pageSize;
            }
            else if (page < 0) {
                page = 0;
            }
            List<TEntity> result = _repository.Get(page, pageSize, null) as List<TEntity>;
            IList<EncapsulateReference> reference = new List<EncapsulateReference>();
            var enhancedData = this._repository.Get(page > 0 ? page - 1: page, pageSize * 2 + 1, null) as List<TEntity>;            
            result.ForEach(e => {
                reference.Add(new EncapsulateReference(controllerName, enhancedData, e));
            });
            return Ok(reference);
        }

        private class Error {
            public readonly string Description;
            public readonly string Repository; 
            public readonly int Id;
            public Error(string repository, int id) {
                this.Id = id;
                this.Description = "Cannot be found";
                this.Repository = repository;
            }
        }
        // GET api/[controller]/5
        [HttpGet("{id}.{format?}")]
        public IActionResult Get(int id)
        {
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString().ToLower();            
            TEntity result = _repository.GetByID(id);
            var enhancedData = this._repository.Get(0, this._repository.Count(), null) as List<TEntity>;            
            if (result == null) {
                return NotFound(new Error(_repository.GetType().ToString(), id));
            }
            return Ok(new EncapsulateReference(controllerName, enhancedData, result));
        }

        public int Count()
        {
            return _repository.Count();
        }
    }

    public abstract class GenericCRUDController<TEntity> : GenericReadableController<TEntity> where TEntity : GenericModel
    {
        public GenericCRUDController(IDataService dataService, GenericWritableRepository<TEntity> repository) : base(dataService, repository)
        { 
        }
        // POST api/[controller]
        [HttpPost]
        public void Post([FromBody]string jsonString)
        {
            GenericWritableRepository<TEntity> writableRepository = this._repository as GenericWritableRepository<TEntity>;
            try {
                TEntity result = JsonConvert.DeserializeObject<TEntity>(jsonString);
                writableRepository.Update(result);
            }
            catch (Exception) {
            }            
        }

        // POST api/[controller]/{id}
        [HttpPost("{id}")]
        public string Post(int id, [FromBody]string jsonString)
        {
            GenericWritableRepository<TEntity> writableRepository = this._repository as GenericWritableRepository<TEntity>;
            TEntity result = JsonConvert.DeserializeObject<TEntity>(jsonString);
            result.Id = id;
            writableRepository.Update(result);
            return "null";
        }

        // PUT api/[controller]
        [HttpPut]
        public void Put([FromBody]string jsonString)
        {
            GenericWritableRepository<TEntity> writableRepository = this._repository as GenericWritableRepository<TEntity>;
            TEntity result = JsonConvert.DeserializeObject<TEntity>(jsonString);
            writableRepository.Insert(result);
        }

        // DELETE api/[controller]/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            GenericWritableRepository<TEntity> writableRepository = this._repository as GenericWritableRepository<TEntity>;
            TEntity result = writableRepository.GetByID(id);
            writableRepository.Delete(result);
        }
    }
}

