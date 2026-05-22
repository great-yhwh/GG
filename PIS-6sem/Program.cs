using PIS_6sem.Data;
using PIS_6sem.Services;

namespace PIS_6sem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var db = new RuleDbContext();
            db.Database.EnsureCreated();

            var uow = new UnitOfWork(db);
            var director = new RuleDirector();
            var serviceRule = new ServiceRule(uow, director);

            Console.WriteLine("=== Создание нового правила ===");

            string ruleName = ReadRequired("Введите название правила: ");

            Console.Write("Введите целевые документы (через ;): ");
            var targetDocs = ReadSemicolonList();

            string guidanceDescription = ReadRequired("Введите описание руководства: ");
            string refusal = ReadOptional("Введите описание отказа: ");

            Console.Write("Введите названия организаций (через ;): ");
            var orgNames = ReadSemicolonList();

            Console.Write("Введите адреса организаций (через ;): ");
            var orgAddresses = ReadSemicolonList();

            if (orgNames.Count != orgAddresses.Count)
            {
                Console.WriteLine("Внимание: количество названий и адресов не совпадает.");
                int min = Math.Min(orgNames.Count, orgAddresses.Count);
                orgNames = orgNames.Take(min).ToList();
                orgAddresses = orgAddresses.Take(min).ToList();
            }

            var daysList = new List<int>();
            var purposeNamesList = new List<List<string>>();
            var citizenshipNamesList = new List<List<string>>();
            var propertyNames = new List<List<string>>();
            var propertyValues = new List<List<string>>();

            Console.WriteLine("\n=== Ввод профилей ===");
            int profileNum = 1;

            do
            {
                Console.WriteLine($"\n--- Профиль #{profileNum} ---");

                daysList.Add(ReadPositiveInt("Введите количество дней: "));

                Console.Write("Введите цели въезда (через запятую): ");
                purposeNamesList.Add(ReadCommaList());

                Console.Write("Введите гражданства (через запятую): ");
                citizenshipNamesList.Add(ReadCommaList());

                var curPropNames = new List<string>();
                var curPropValues = new List<string>();

                while (AskYesNo("Добавить дополнительное свойство профиля?"))
                {
                    curPropNames.Add(ReadRequired("Название свойства: "));
                    curPropValues.Add(ReadRequired("Значение свойства: "));
                }

                propertyNames.Add(curPropNames);
                propertyValues.Add(curPropValues);
                profileNum++;
            }
            while (AskYesNo("Добавить ещё один профиль?"));

            Console.WriteLine("\nСоздаём правило...");

            var rule = serviceRule.CreateRule(
                ruleName, targetDocs,
                guidanceDescription, refusal,
                orgNames, orgAddresses,
                daysList, purposeNamesList, citizenshipNamesList,
                propertyNames, propertyValues);

            PrintRule(rule);

            Console.WriteLine("\n=== Проверка чтения из БД ===");
            using var db2 = new RuleDbContext();
            var uow2 = new UnitOfWork(db2);
            var loaded = uow2.Rules.GetById(rule.Id);

            if (loaded != null)
            {
                Console.WriteLine($"Из БД: {loaded.Name}");
                Console.WriteLine($"Профилей: {loaded.Profiles.Count}");
                Console.WriteLine($"Документов: {loaded.TargetDocuments.Count}");
                Console.WriteLine($"Организаций: {loaded.Guidance?.Organizations.Count}");
            }

            Console.WriteLine("\nГотово! Нажмите любую клавишу...");
            Console.ReadKey();
        }

        static void PrintRule(dynamic rule)
        {
            Console.WriteLine("\n=== Правило создано и сохранено в БД ===");
            Console.WriteLine($"ID: {rule.Id}");
            Console.WriteLine($"Наименование: {rule.Name}");

            Console.WriteLine("\nЦелевые документы:");
            foreach (var doc in rule.TargetDocuments)
                Console.WriteLine($"  - {doc.Name}");

            Console.WriteLine("\nПрофили:");
            int i = 1;
            foreach (var p in rule.Profiles)
            {
                Console.WriteLine($"  Профиль #{i++}");
                Console.WriteLine($"    Дни: {p.Days}");

                var purposes = ((IEnumerable<dynamic>)p.Properties)
                    .Where(x => (string)x.Name == "Цель въезда")
                    .Select(x => (string)x.Value);
                Console.WriteLine($"    Цели: {string.Join(", ", purposes)}");

                var citizenships = ((IEnumerable<dynamic>)p.Properties)
                    .Where(x => (string)x.Name == "Гражданство")
                    .Select(x => (string)x.Value);
                Console.WriteLine($"    Гражданства: {string.Join(", ", citizenships)}");

                var others = ((IEnumerable<dynamic>)p.Properties)
                    .Where(x => (string)x.Name != "Цель въезда"
                             && (string)x.Name != "Гражданство");

                foreach (var prop in others)
                    Console.WriteLine($"    {prop.Name} = {prop.Value}");
            }

            Console.WriteLine("\nРуководство:");
            if (rule.Guidance != null)
            {
                Console.WriteLine($"  Описание: {rule.Guidance.Description}");
                Console.WriteLine($"  Отказ: {rule.Guidance.Refusal}");
                Console.WriteLine("  Организации:");
                foreach (var org in rule.Guidance.Organizations)
                    Console.WriteLine($"    {org.Name} — {org.Address}");
            }
        }

        static List<string> ReadCommaList()
        {
            string input = Console.ReadLine() ?? "";
            return input
                .Split(',', StringSplitOptions.TrimEntries
                    | StringSplitOptions.RemoveEmptyEntries)
                .ToList();
        }

        static List<string> ReadSemicolonList()
        {
            string input = Console.ReadLine() ?? "";
            return input
                .Split(';', StringSplitOptions.TrimEntries
                    | StringSplitOptions.RemoveEmptyEntries)
                .ToList();
        }

        static string ReadRequired(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? val = Console.ReadLine()?.Trim();
                if (!string.IsNullOrWhiteSpace(val)) return val;
                Console.WriteLine("Значение не может быть пустым.");
            }
        }

        static string ReadOptional(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine()?.Trim() ?? "";
        }

        static int ReadPositiveInt(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int val) && val >= 0)
                    return val;
                Console.WriteLine("Введите корректное число.");
            }
        }

        static bool AskYesNo(string prompt)
        {
            while (true)
            {
                Console.Write($"{prompt} (д/н): ");
                string? a = Console.ReadLine()?.Trim().ToLower();
                if (a == "д" || a == "да") return true;
                if (a == "н" || a == "нет") return false;
                Console.WriteLine("Введите 'д' или 'н'.");
            }
        }
    }
}