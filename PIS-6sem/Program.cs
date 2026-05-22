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

            var uow = new UnitOfWork(db);
            var director = new RuleDirector();
            var serviceRule = new ServiceRule(uow, director);

            Console.WriteLine("Создание нового правила!");

            string ruleName = ReadRequired("Введите название правила: ");

            Console.WriteLine("Введите целевые документы (через запятую): ");
            var targetDocs = ReadCommaSeparatedList();

            string guidanceDescription = ReadRequired("Введите описание руководства: ");
            string refusal = ReadOptional("Введите описание отказа: ");

            Console.WriteLine("Введите названия организаций (через запятую): ");
            var orgNames = ReadCommaSeparatedList();

            Console.WriteLine("Введите адреса организаций (в том же порядке, через запятую): ");
            var orgAddresses = ReadCommaSeparatedList();

            if (orgNames.Count != orgAddresses.Count)
            {
                Console.WriteLine("Внимание: количество названий организаций не совпадает с количеством адресов.");
                int minCount = Math.Min(orgNames.Count, orgAddresses.Count);
                orgNames = orgNames.Take(minCount).ToList();
                orgAddresses = orgAddresses.Take(minCount).ToList();
            }

            var dayOptions = new List<string> { "90", "30", "15", "7" };
            var purposeOptions = new List<string> { "Работа", "Учёба", "Туризм", "Частная", "Иная" };
            var citizenshipOptions = new List<string>
            {
                "Азербайджан", "Таджикистан", "Узбекистан", "Молдова", "Украина",
                "Белоруссия", "Киргизия", "Казахстан", "Армения",
                "Все остальные иностранные граждане"
            };
            var statusOptions = new List<string>
            {
                "Статус отсутствует",
                "Высококвалифицированный специалист или член его(её) семьи",
                "Участник гос программы переселения соотечественников или член его(её) семьи"
            };

            var daysList = new List<int>();
            var purposes = new List<string>();
            var citizenships = new List<string>();
            var propertyNames = new List<List<string>>();
            var propertyValues = new List<List<string>>();

            Console.WriteLine("\nВвод профилей");

            int profileNumber = 1;
            do
            {
                Console.WriteLine($"\n Профиль #{profileNumber}");

                string selectedDays = ChooseFromList("Выберите количество дней:", dayOptions, allowCustom: true);
                int days;
                while (!int.TryParse(selectedDays, out days) || days < 0)
                {
                    Console.WriteLine("Введите корректное положительное число.");
                    Console.Write("Количество дней: ");
                    selectedDays = Console.ReadLine() ?? "";
                }

                string purpose = ChooseFromList("Выберите цель приезда:", purposeOptions, allowCustom: true);
                string citizenship = ChooseFromList("Выберите гражданство:", citizenshipOptions, allowCustom: true);
                string status = ChooseFromList("Выберите статус:", statusOptions, allowCustom: true);

                var currentPropertyNames = new List<string>();
                var currentPropertyValues = new List<string>();

                if (!string.Equals(status, "Статус отсутствует", StringComparison.OrdinalIgnoreCase))
                {
                    currentPropertyNames.Add("Статус");
                    currentPropertyValues.Add(status);
                }

                while (AskYesNo("Добавить ещё свойство профиля?"))
                {
                    string propertyName = ReadRequired("Название свойства: ");
                    string propertyValue = ReadRequired("Значение свойства: ");

                    currentPropertyNames.Add(propertyName);
                    currentPropertyValues.Add(propertyValue);
                }

                daysList.Add(days);
                purposes.Add(purpose);
                citizenships.Add(citizenship);
                propertyNames.Add(currentPropertyNames);
                propertyValues.Add(currentPropertyValues);

                profileNumber++;
            }
            while (AskYesNo("Добавить ещё один профиль?"));

            Console.WriteLine("\nСоздаём правило...");
            var rule = serviceRule.CreateRule(
                ruleName: ruleName,
                targetDocs: targetDocs,
                guidanceDescription: guidanceDescription,
                refusal: refusal,
                orgNames: orgNames,
                orgAddresses: orgAddresses,
                daysList: daysList,
                purposes: purposes,
                citizenships: citizenships,
                propertyNames: propertyNames,
                propertyValues: propertyValues
            );

            Console.WriteLine("\nПравило создано и сохранено в БД!");
            Console.WriteLine($"ID: {rule.Id}");
            Console.WriteLine($"Наименование: {rule.Name}");

            Console.WriteLine("\nПрофили:");
            foreach (var profile in rule.Profiles)
            {
                Console.WriteLine($"  ID: {profile.Id}");
                Console.WriteLine($"  Дни: {profile.Days}");
                Console.WriteLine($"  Цель визита: {profile.Purpose}");
                Console.WriteLine($"  Гражданство: {profile.Citizenship}");
                Console.WriteLine("  Свойства профиля:");

                if (profile.Properties.Count == 0)
                {
                    Console.WriteLine("    нет");
                }
                else
                {
                    foreach (var prop in profile.Properties)
                    {
                        Console.WriteLine($"    {prop.Name} = {prop.Value}");
                    }
                }
            }

            Console.WriteLine("\nЦелевые документы:");
            foreach (var doc in rule.TargetDocuments)
            {
                Console.WriteLine($"  {doc.Name}");
            }

            Console.WriteLine("\nРуководство:");
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

        static string ReadRequired(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? value = Console.ReadLine()?.Trim();

                if (!string.IsNullOrWhiteSpace(value))
                    return value;

                Console.WriteLine("Значение не может быть пустым. Повторите ввод.");
            }
        }

        static string ReadOptional(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine()?.Trim() ?? "";
        }

        static List<string> ReadCommaSeparatedList()
        {
            string input = Console.ReadLine() ?? "";
            return input
                .Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                .ToList();
        }

        static bool AskYesNo(string prompt)
        {
            while (true)
            {
                Console.Write($"{prompt} (д/н): ");
                string? answer = Console.ReadLine()?.Trim().ToLower();

                if (answer == "д" || answer == "да")
                    return true;

                if (answer == "н" || answer == "нет")
                    return false;

                Console.WriteLine("Введите 'д' или 'н'.");
            }
        }

        static string ChooseFromList(string title, List<string> options, bool allowCustom = false)
        {
            Console.WriteLine($"\n{title}");

            for (int i = 0; i < options.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {options[i]}");
            }

            if (allowCustom)
            {
                Console.WriteLine($"{options.Count + 1}. Другое (ввести вручную)");
            }

            while (true)
            {
                Console.Write("Выберите номер: ");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    if (choice >= 1 && choice <= options.Count)
                    {
                        return options[choice - 1];
                    }

                    if (allowCustom && choice == options.Count + 1)
                    {
                        Console.Write("Введите своё значение: ");
                        return Console.ReadLine()?.Trim() ?? "";
                    }
                }

                Console.WriteLine("Некорректный ввод. Повторите.");
            }
        }
    }
}