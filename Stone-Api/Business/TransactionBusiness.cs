using System;
using System.Collections.Generic;
using System.Linq;
using StoneApi.DAL;
using StoneApi.Models;

namespace StoneApi.Business{
    public interface ITransactionBusiness
    {
        IEnumerable<Transaction> Get();
        Transaction GetById(int id);
        void Insert(Transaction transaction);
        void Update(Transaction transaction);
        void Delete(int id);
        IEnumerable<Transaction> GetWithQueryParameters(
            double[] cnpj = null, 
            DateTime startDate = default(DateTime), 
            DateTime endDate = default(DateTime), 
            string[] brand = null, 
            string[] acquirer = null
        );
    }

    public class TransactionBusiness : ITransactionBusiness {
        public ITransactionRepository Repository {get; set; }
        public TransactionBusiness(ITransactionRepository repository)
        {
            Repository =  repository;
        }

        public IEnumerable<Transaction> Get() => Repository.Get();
        public Transaction GetById(int id) => Repository.GetById(id);
        public void Insert(Transaction transaction) => Repository.Insert(transaction);
        public void Update(Transaction transaction) => Repository.Update(transaction);
        public void Delete(int id) => Repository.Delete(id);

        public IEnumerable<Transaction> GetWithQueryParameters(
            double[] cnpj = null, 
            DateTime startdate = default(DateTime),
            DateTime endDate = default(DateTime), 
            string[] brand = null, 
            string[] acquirer = null
        ) 
        => Repository.Get(
                o =>
                (cnpj != null || startdate != null || endDate != null || brand != null || acquirer != null) &&
                (cnpj != null ? cnpj.Contains(o.MerchantCnpj) : true) &&
                (startdate != null ? o.AcquirerAuthorizationDateTime.Date >= startdate.Date : true) &&
                (endDate != null ? o.AcquirerAuthorizationDateTime.Date <= endDate.Date : true) &&
                (brand != null ? brand.Contains(o.CardBrandName) : true) &&
                (acquirer != null ? acquirer.Contains(o.AcquirerName) : true))??new List<Transaction>();
    }
}