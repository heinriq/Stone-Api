using System;
using System.Collections.Generic;
using StoneApi.Models;

namespace StoneApi.DAL
{
    public interface ITransactionRepository : IDisposable
    {
        IEnumerable<Transaction> GetTransactions();
        Transaction GetTransactionsById(int transactionId);
        void InsertTransaction(Transaction transacao);
        void DeleteTransaction(int transactionId);
        void UpdateTransaction(Transaction transacao);
        void Save();
    }
}