using Microsoft.EntityFrameworkCore.Storage;

namespace PIS_6sem.Data
{
    public interface IUnitOfWork :
    {
        // доступ к репозиторию
        IRuleRepository Rules { get; }

        // управление транзакцией
        IDbContextTransaction BeginTransaction();

        // сохранение
        int Save();
    }
}