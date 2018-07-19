using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StoneChallange.Models;

namespace StoneChallange.DAL{
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

        public IEnumerable<Transaction> GetTransactions()
            => Context.Transactions.ToList();
        public Transaction GetTransactionsById(int transactionId)
            => Context.Transactions.Find(transactionId);

        public void InsertTransaction(Transaction transacao)
            => Context.Transactions.Add(transacao);

        public void UpdateTransaction(Transaction transacao)
            => Context.Entry(transacao).State = EntityState.Modified;

        public void DeleteTransaction(int transactionId)
            => Context.Transactions.Remove(GetTransactionsById(transactionId));

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