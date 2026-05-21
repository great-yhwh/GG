using PIS_6sem.Data;
using PIS_6sem.Entities;

namespace PIS_6sem.Services
{
    public class ServiceRule
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly RuleDirector _director;

        public ServiceRule(IUnitOfWork unitOfWork, RuleDirector director)
        {
            _unitOfWork = unitOfWork;
            _director = director;
        }

        public Rule CreateRule(
            string ruleName,
            List<string> targetDocs,
            string guidanceDescription,
            string refusal,
            List<string> orgNames,
            List<string> orgAddresses,
            int days,
            string purpose,
            string citizenship)
        {
            // ===== ФАЗА 1: Создание объекта в памяти =====
            // (Диаграмма последовательности операций, шаги 2-37)

            // Шаг 2: создаем builder
            var ruleBuilder = new RuleBuilder();

            // Шаг 3: создаем factory
            var profileFactory = new ProfileFactory();

            // Шаг 4-37: director строит Rule
            var rule = _director.Construct(
                ruleName, targetDocs,
                guidanceDescription, refusal,
                orgNames, orgAddresses,
                days, purpose, citizenship,
                ruleBuilder, profileFactory);

            // ===== ФАЗА 2: Сохранение в БД =====
            // (Диаграмма последовательности сохранения, шаги 1-30)

            // Шаг 1-4: открываем транзакцию
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                // Шаг 5-6: получаем репозиторий через getter свойства Rules
                // Шаг 7: добавляем rule в репозиторий
                _unitOfWork.Rules.Add(rule);

                // Шаг 11-27: сохраняем все изменения в БД
                _unitOfWork.Save();

                // Шаг 28: фиксируем транзакцию
                transaction.Commit();

            } // Шаг 29: Dispose() вызывается автоматически

            // Шаг 30: возвращаем rule
            return rule;
        }
    }
}