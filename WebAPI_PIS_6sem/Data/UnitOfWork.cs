using Microsoft.EntityFrameworkCore.Storage;

namespace WebAPI_PIS_6sem.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RuleDbContext _db;

        // Свойство Rules — это и есть тот getter,
        // который вызывается на шаге 5 диаграммы
        public IRuleRepository Rules { get; private set; }

        public UnitOfWork(RuleDbContext db)
        {
            _db = db;
            // Репозитории создаются один раз в конструкторе
            Rules = new RuleRepository(db);
        }

        // Шаг 1-2 диаграммы: начало транзакции
        public IDbContextTransaction BeginTransaction()
        {
            return _db.Database.BeginTransaction();
        }

        // Шаг 11-12 диаграммы: сохранение
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