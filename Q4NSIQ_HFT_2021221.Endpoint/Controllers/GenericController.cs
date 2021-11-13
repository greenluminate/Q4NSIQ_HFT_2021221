using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Q4NSIQ_HFT_2021221.Logic;

namespace Q4NSIQ_HFT_2021221.Endpoint.Controllers
{
    [ApiController]
    public class GenericController<TEntity> : ControllerBase where TEntity : class
    {
        ILogic<TEntity> logic;

        public GenericController(ILogic<TEntity> logic)
        {
            this.logic = logic;
        }

        [HttpGet]
        public IEnumerable<TEntity> Get()
        {
            return logic.ReadAll();
        }

        [HttpGet("{id}")]
        public TEntity Get(int id)
        {
            return logic.Read(id);
        }

        [HttpPost]
        public void Post([FromBody] TEntity entity)
        {
            logic.Create(entity);
        }

        [HttpPut]
        public void Put([FromBody] TEntity entity)
        {
            logic.Update(entity);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            logic.Delete(id);
        }
    }
}
