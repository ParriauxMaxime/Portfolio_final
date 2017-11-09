using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataAccessLayer;
using Models;
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
        private int count {get; set;}
        public class Encapsulation {
            public string Url {get; set;}
            public TEntity Data {get; set;}
        }

        public class ListEncapsulation {
            public int Total {get; set;} 
            public int Pages {get;set;}
            public int Page {get;set;}
            public string Prev {get;set;}
            public string Url {get; set;}
            public string Next {get;set;}
            public List<Encapsulation> Data {get;set;}
        }

        public GenericReadableController(IDataService dataService, GenericReadableRepository<TEntity> repository)
        {
            this._dataService = dataService;
            this._repository = repository as GenericReadableRepository<TEntity>;
            this.count = this._repository.Count();
        }

        protected string createUrl(int? id = null, int? page = null, int? pageSize = null) {
            var host = "";
            if (Request == null) // It's null while Unit Testing...
            {
                host = "localhost:5001";
            } else
            {
                host = Request.Host.ToUriComponent();
            }
            var controller = ControllerContext.RouteData?.Values["controller"].ToString().ToLower();
            var url = "http://" + host + "/api/" + controller;
            if (id == null) {
                if (page < 0) {
                    return "";
                }
                else if (page > (count / pageSize)) {
                    return "";
                }
                else 
                    return url + "?page=" + page + "&pageSize=" + pageSize;
            }
            else 
                return url + "/" + id;
        }

        // GET api/[controller]
        [HttpGet]
        public IActionResult Get(int page = 0, int pageSize = 50)
        {
            if (pageSize > 200 || pageSize <= 0)
            {
                pageSize = 50;
            }
            if (page > count / pageSize)
            {
                page = count / pageSize;
            }
            else if (page < 0)
            {
                page = 0;
            }
            List<TEntity> data = _repository.Get(page, pageSize, null) as List<TEntity>;
            List<Encapsulation> tmp = new List<Encapsulation>();
            data.ForEach(e => {
                tmp.Add(new Encapsulation {Url = createUrl(e.Id), Data = e});
            });
            var result = new ListEncapsulation {
                Total = count,
                Pages = count / pageSize != 0 ? count / pageSize : 1,
                Page = page,
                Prev = createUrl(null, page - 1, pageSize),
                Next = createUrl(null, page + 1, pageSize),
                Url = createUrl(null, page, pageSize),
                Data = tmp
            };
            return Ok(result);
        }

        private class Error
        {
            public readonly string Description;
            public readonly string Repository;
            public readonly int Id;
            public Error(string repository, int id)
            {
                this.Id = id;
                this.Description = "Cannot be found";
                this.Repository = repository;
            }
        }
        // GET api/[controller]/5
        [HttpGet("{id}.{format?}")]
        public IActionResult GetByID(int id)
        {
            string url = this.ControllerContext.RouteData?.Values["controller"].ToString();
            TEntity result = _repository.GetByID(id);
            if (result == null)
            {
                return NotFound(new Error(_repository.GetType().ToString(), id));
            }
            return Ok(new Encapsulation{Url = createUrl(result.Id), Data = result});
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
        public IActionResult Post([FromBody]string jsonString)
        {
            GenericWritableRepository<TEntity> writableRepository = this._repository as GenericWritableRepository<TEntity>;
            int code;
            TEntity result;
            try
            {
                result = JsonConvert.DeserializeObject<TEntity>(jsonString);
                code = writableRepository.Update(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            if (code == 200)
                return Ok();
            else {
                var url = createUrl(result.Id);
                return Created(url, new Encapsulation { Data = result, Url = url});
            }
        }

        // POST api/[controller]/{id}
        [HttpPost("{id}")]
        public IActionResult Post(int id, [FromBody]string jsonString)
        {
            GenericWritableRepository<TEntity> writableRepository = this._repository as GenericWritableRepository<TEntity>;
            int code;
            TEntity result = null;
            try
            {
                result = JsonConvert.DeserializeObject<TEntity>(jsonString);
                result.Id = id;
                code = writableRepository.Update(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            if (code == 200)
                return Ok();
            else {
                var url = createUrl(result.Id);
                return Created(url, new Encapsulation { Data = result, Url = url});
            }
        }

        // PUT api/[controller]
        [HttpPut]
        public IActionResult Put([FromBody]string jsonString)
        {
            GenericWritableRepository<TEntity> writableRepository = this._repository as GenericWritableRepository<TEntity>;
            try
            {
                TEntity result = JsonConvert.DeserializeObject<TEntity>(jsonString);
                int id = writableRepository.Insert(result);
                var url = createUrl(id);
                return Created(url, new Encapsulation { Data = result, Url = url} );
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // DELETE api/[controller]/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            GenericWritableRepository<TEntity> writableRepository = this._repository as GenericWritableRepository<TEntity>;
            try
            {
                TEntity result = writableRepository.GetByID(id);
                writableRepository.Delete(result);
                return Ok();
            }
            catch (Exception) {
                return NotFound();
            } 
        }
    }
}

