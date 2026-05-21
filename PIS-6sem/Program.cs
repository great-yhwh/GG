using PIS_6sem.Data;
using PIS_6sem.Services;

namespace PIS_6sem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Инициализация
            var db = new RuleDbContext();
            db.Database.EnsureCreated();

            var unitOfWork = new UnitOfWork(db);
            var director = new RuleDirector();
            var serviceRule = new ServiceRule(unitOfWork, director);

            // Ввод данных
            Console.WriteLine("Создание нового правила!");

            Console.Write("Введите название правила (например, Рабочая виза): ");
            string ruleName = Console.ReadLine()?.Trim();
            while (string.IsNullOrWhiteSpace(ruleName))
            {
                Console.Write("Название не может быть пустым. Повторите ввод: ");
                ruleName = Console.ReadLine()?.Trim();
            }

            Console.WriteLine("Введите целевые документы (через запятую, например: " +
                "Разрешение на работу, Рабочая виза): ");
            string docsInput = Console.ReadLine() ?? "";
            var targetDocs = docsInput.Split(',', StringSplitOptions.TrimEntries 
                | StringSplitOptions.RemoveEmptyEntries).ToList();

            Console.Write("Введите описание руководства (например: Обратитесь в миграционную службу): ");
            string guidanceDescription = Console.ReadLine()?.Trim() ?? "";

            Console.Write("Введите описание отказа (например: Отказ при неполном пакете документов): ");
            string refusal = Console.ReadLine()?.Trim() ?? "";

            Console.WriteLine("Введите названия организаций (через запятую, например: МВД, МФЦ): ");
            string orgNamesInput = Console.ReadLine() ?? "";
            var orgNames = orgNamesInput.Split(',', StringSplitOptions.TrimEntries 
                | StringSplitOptions.RemoveEmptyEntries).ToList();

            Console.WriteLine("Введите адреса организаций (в том же порядке, через запятую, " +
                "например: ул. Ленина, 1, ул. Мира, 5): ");
            string orgAddressesInput = Console.ReadLine() ?? "";
            var orgAddresses = orgAddressesInput.Split(',', StringSplitOptions.TrimEntries 
                | StringSplitOptions.RemoveEmptyEntries).ToList();

            // Проверка, что количество названий и адресов совпадает
            if (orgNames.Count != orgAddresses.Count)
            {
                Console.WriteLine("Внимание: количество названий организаций" +
                    " не совпадает с количеством адресов. Будет использовано минимальное число.");
                int minCount = Math.Min(orgNames.Count, orgAddresses.Count);
                orgNames = orgNames.Take(minCount).ToList();
                orgAddresses = orgAddresses.Take(minCount).ToList();
            }

            Console.Write("Введите количество дней (целое число, например 90): ");
            if (!int.TryParse(Console.ReadLine(), out int days))
            {
                Console.WriteLine("Некорректный ввод, установлено значение 0");
                days = 0;
            }

            Console.Write("Введите цель (например, Работа): ");
            string purpose = Console.ReadLine()?.Trim() ?? "";

            Console.Write("Введите гражданство (например, Узбекистан): ");
            string citizenship = Console.ReadLine()?.Trim() ?? "";

            // Создание правила через сервис
            Console.WriteLine("\nСоздаём правило...");
            var rule = serviceRule.CreateRule(
                ruleName: ruleName,
                targetDocs: targetDocs,
                guidanceDescription: guidanceDescription,
                refusal: refusal,
                orgNames: orgNames,
                orgAddresses: orgAddresses,
                days: days,
                purpose: purpose,
                citizenship: citizenship
            );

            // результата
            Console.WriteLine("\nПравило создано и сохранено в БД!");
            Console.WriteLine($"ID: {rule.Id}");
            Console.WriteLine($"Наименование: {rule.Name}");

            Console.WriteLine("\nПрофили");
            foreach (var profile in rule.Profiles)
            {
                Console.WriteLine($"  ID: {profile.Id}");
                Console.WriteLine($"  Дни: {profile.Days}");
                Console.WriteLine($"  Цель визита: {profile.Purpose}");
                Console.WriteLine($"  Гражданство: {profile.Citizenship}");
                Console.WriteLine("  Доп сведения:");
                foreach (var prop in profile.Properties)
                {
                    Console.WriteLine($"    {prop.Name} = {prop.Value}");
                }
            }

            Console.WriteLine("\nЦелевые документы");
            foreach (var doc in rule.TargetDocuments)
            {
                Console.WriteLine($"  {doc.Name}");
            }

            Console.WriteLine("\nРуководство");
            if (rule.Guidance != null)
            {
                Console.WriteLine($"  Описание: {rule.Guidance.Description}");
                Console.WriteLine($"  Отказ: {rule.Guidance.Refusal}");
                Console.WriteLine("  Организации:");
                foreach (var org in rule.Guidance.Organizations)
                {
                    Console.WriteLine($"    {org.Name} — {org.Address}");
                }
            }

            // --- Проверка чтения из БД ---
            Console.WriteLine("\nПроверка: читаем из БД заново");
            using var db2 = new RuleDbContext();
            var uow2 = new UnitOfWork(db2);
            var loadedRule = uow2.Rules.GetById(rule.Id);

            if (loadedRule != null)
            {
                Console.WriteLine($"Из БД: {loadedRule.Name}");
                Console.WriteLine($"Профилей: {loadedRule.Profiles.Count}");
                Console.WriteLine($"Документов: {loadedRule.TargetDocuments.Count}");
                Console.WriteLine($"Организаций: {loadedRule.Guidance?.Organizations.Count}");
            }

            Console.WriteLine("\nГотово! Нажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}