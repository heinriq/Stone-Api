using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using StoneApi.DAL;
using StoneApi.Models;

namespace StoneApi.Controllers
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
            => repository.GetTransactions();

        // GET api/values/5
        [HttpGet("{id}")]
        public Transaction Get(int id) 
            => repository.GetTransactionsById(id);

        // POST api/values
        [HttpPost]
        public void Post([FromBody] Transaction value) 
            => repository.InsertTransaction(value);

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Transaction value) 
            => repository.UpdateTransaction(value);

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id) 
            => repository.DeleteTransaction(id);
    }
}
