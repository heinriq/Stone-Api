using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StoneChallange.DAL;
using StoneChallange.Models;

namespace core_webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        ITransactionRepository repository;        
        public ValuesController(ITransactionRepository repository)
        {
            this.repository = repository;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<Transaction> Get()
        {
            return repository.GetTransactions();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Transaction Get(int id)
        {
            return repository.GetTransactionsById(id);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] Transaction value)
        {
            repository.InsertTransaction(value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Transaction value)
        {
            repository.UpdateTransaction(value);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            repository.DeleteTransaction(id);
        }
    }
}
