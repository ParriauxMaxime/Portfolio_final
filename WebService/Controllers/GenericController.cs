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

        // GET api/[controller]
        [HttpGet]
        public IActionResult Get(int page = 0, int pageSize = 50)
        {
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString().ToLower();
            if (pageSize > 200 || pageSize <= 0)
            {
                pageSize = 50;
            }
            if (page > this.Count() / pageSize)
            {
                page = this.Count() / pageSize;
            }
            else if (page < 0)
            {
                page = 0;
            }
            List<TEntity> result = _repository.Get(page, pageSize, null) as List<TEntity>;
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
        public IActionResult Get(int id)
        {
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString().ToLower();
            TEntity result = _repository.GetByID(id);
            var enhancedData = this._repository.Get(0, this._repository.Count(), null) as List<TEntity>;
            if (result == null)
            {
                return NotFound(new Error(_repository.GetType().ToString(), id));
            }
            return Ok(result);
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
            try
            {
                TEntity result = JsonConvert.DeserializeObject<TEntity>(jsonString);
                writableRepository.Update(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            return Ok();
        }

        // POST api/[controller]/{id}
        [HttpPost("{id}")]
        public IActionResult Post(int id, [FromBody]string jsonString)
        {
            GenericWritableRepository<TEntity> writableRepository = this._repository as GenericWritableRepository<TEntity>;
            try
            {
                TEntity result = JsonConvert.DeserializeObject<TEntity>(jsonString);
                result.Id = id;
                writableRepository.Update(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            return Ok();
        }

        // PUT api/[controller]
        [HttpPut]
        public IActionResult Put([FromBody]string jsonString)
        {
            GenericWritableRepository<TEntity> writableRepository = this._repository as GenericWritableRepository<TEntity>;
            try
            {
                TEntity result = JsonConvert.DeserializeObject<TEntity>(jsonString);
                writableRepository.Insert(result);
                return Created("/api/" + this.ControllerContext.RouteData.Values["controller"].ToString().ToLower() + "/" + result, result);
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

