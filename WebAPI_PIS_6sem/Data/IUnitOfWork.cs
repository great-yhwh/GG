using Microsoft.EntityFrameworkCore.Storage;

namespace WebAPI_PIS_6sem.Data
{
    public interface IUnitOfWork : IDisposable
    {
        // Свойства-геттеры для доступа к репозиториям
        IRuleRepository Rules { get; }

        // Управление транзакцией
        IDbContextTransaction BeginTransaction();

        // Сохранение изменений
        int Save();
    }
}