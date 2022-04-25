using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Q4NSIQ_HFT_2021221.Logic;
using Microsoft.AspNetCore.SignalR;
using Q4NSIQ_HFT_2021221.Endpoint.Services;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection;
using System.ComponentModel.DataAnnotations.Schema;

namespace Q4NSIQ_HFT_2021221.Endpoint.Controllers
{
    [ApiController]
    public class GenericController<TEntity> : ControllerBase where TEntity : class
    {
        ILogic<TEntity> logic;
        IHubContext<SignalRHub> hub;

        public GenericController(ILogic<TEntity> logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
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

            List<string> connectedTabelsNames = typeof(TEntity).GetProperties().Where(prop => prop.PropertyType.AssemblyQualifiedName.Contains("ICollection")).Select(prop => prop.Name != "Seats" ? prop.Name.TrimEnd('s') : prop.Name).ToList();
            this.hub.Clients.All.SendAsync($"{typeof(TEntity).Name}Created", entity);
            connectedTabelsNames.ForEach(className => this.hub.Clients.All.SendAsync($"{className}Created", entity));
        }

        [HttpPut]
        public void Put([FromBody] TEntity entity)
        {
            logic.Update(entity);

            List<string> connectedTabelsNames = typeof(TEntity).GetProperties().Where(prop => prop.PropertyType.AssemblyQualifiedName.Contains("ICollection")).Select(prop => prop.Name != "Seats" ? prop.Name.TrimEnd('s') : prop.Name).ToList();
            
            this.hub.Clients.All.SendAsync($"{typeof(TEntity).Name}Updated", entity);
            connectedTabelsNames.ForEach(className => this.hub.Clients.All.SendAsync($"{className}Updated", entity));
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var entityToDelete = this.logic.Read(id);
            logic.Delete(id);

            List<string> connectedTabelsNames = typeof(TEntity).GetProperties().Where(prop => prop.PropertyType.AssemblyQualifiedName.Contains("ICollection")).Select(prop => prop.Name != "Seats" ? prop.Name.TrimEnd('s') : prop.Name).ToList();

            this.hub.Clients.All.SendAsync($"{typeof(TEntity).Name}Deleted", entityToDelete);
            connectedTabelsNames.ForEach(className => this.hub.Clients.All.SendAsync($"{className}Deleted", entityToDelete));
        }
    }
}
