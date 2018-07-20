using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using StoneApi.Business;
using StoneApi.DAL;
using StoneApi.Models;

namespace StoneApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        ITransactionBusiness Business { get; }        
        public TransactionsController(ITransactionRepository repository, ITransactionBusiness business) 
            => Business = business;

        // GET api/transactions
        [HttpGet]
        public JToken Get() 
            => JToken.FromObject(new { results = Business.Get() });

        // GET api/values/5
        [HttpGet("{id}")]
        public JToken Get(int id) 
            => JToken.FromObject(new {results = Business.GetById(id) });

        [HttpGet("query")]
        public JToken Get(
            [FromQuery]string cnpj = default(string), 
            [FromQuery]string startDate = null,
            [FromQuery]string endDate = null, 
            [FromQuery]string brand = null, 
            [FromQuery]string acquirer = null
        )
            => JToken.FromObject(
                new {
                    results = Business.GetWithQueryParameters(
                        cnpj: cnpj?.Split(',').Select(o => {return Convert.ToDouble(o); }).ToArray(),
                        startDate: Convert.ToDateTime(startDate),
                        endDate: Convert.ToDateTime(endDate),
                        brand: brand?.Split(','),
                        acquirer: acquirer?.Split(',')
                    )
                }
            );

        // POST api/transactions
        [HttpPost]
        public JToken Post([FromBody] Transaction value) 
            => /*Business.Insert(value);*/ 
                JToken.FromObject( new{ message = "Metodo de inserção não implementado" });

        // PUT api/transactions/5
        [HttpPut("{id}")]
        public JToken Put(int id, [FromBody] Transaction value) 
            => /*Business.Update(value);*/
                JToken.FromObject( new { message = "Metodo de atualização não implementado" });

        // DELETE api/transactions/5
        [HttpDelete("{id}")]
        public JToken Delete(int id) 
            => /*Business.Delete(id);*/
                JToken.FromObject( new{ message = "Metodo de remoção não implementado" });
    }
}
