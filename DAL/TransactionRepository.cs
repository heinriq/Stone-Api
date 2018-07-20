using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using StoneApi.Models;

namespace StoneApi.DAL{
    public class TransactionRepository : ITransactionRepository, IDisposable
    {
        public TransacaoContext Context { get; set; }
        bool Disposed { get; set; }
        public TransactionRepository(TransacaoContext context)
        {
            Context = context;
        }
        
        public void Save()
            => Context.SaveChanges();

        public IEnumerable<Transaction> Get()
            => Context.Transactions.ToList();
        public IEnumerable<Transaction> Get(Func<Transaction, bool> func)
            => Context.Transactions.Where(func);

        public Transaction GetById(int transactionId)
            => Context.Transactions.Find(transactionId);

        public void Insert(Transaction transacao)
            => Context.Transactions.Add(transacao);

        public void Update(Transaction transacao)
            => Context.Entry(transacao).State = EntityState.Modified;

        public void Delete(int transactionId)
            => Context.Transactions.Remove(GetById(transactionId));

        public virtual void Dispose(bool disposing)
        {
            if(!Disposed){
                if(disposing)
                    Context.Dispose();
                Disposed = true;
            }
        }

        public void Dispose(){
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}