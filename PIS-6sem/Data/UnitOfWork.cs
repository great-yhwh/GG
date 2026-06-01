using Microsoft.EntityFrameworkCore.Storage;

namespace PIS_6sem.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RuleDbContext _db;

        // Свойство Rules — это и есть тот getter,
        // который вызывается на шаге
        public IRuleRepository Rules { get; private set; }

        public UnitOfWork(RuleDbContext db)
        {
            _db = db;
            // Репозитории создаются один раз в конструкторе
            Rules = new RuleRepository(db);
        }

        //начало транзакции
        public IDbContextTransaction BeginTransaction()
        {
            return _db.Database.BeginTransaction();
        }

        //  сохранение
        public int Save()
        {
            return _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}