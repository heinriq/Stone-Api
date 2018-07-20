using System;
using System.Collections.Generic;
using StoneApi.Models;

namespace StoneApi.DAL
{
    public interface ITransactionRepository : IDisposable
    {
        IEnumerable<Transaction> Get();
        IEnumerable<Transaction> Get(Func<Transaction, bool> func);
        Transaction GetById(int transactionId);
        void Insert(Transaction transacao);
        void Delete(int transactionId);
        void Update(Transaction transacao);
        void Save();
    }
}