using PIS_6sem.Entities;

namespace PIS_6sem.Services
{
    public class RuleDirector
    {
        public Rule Construct(
            string ruleName,
            List<string> targetDocs,
            string guidanceDescription,
            string refusal,
            List<string> orgNames,
            List<string> orgAddresses,
            int days,
            string purpose,
            string citizenship,
            RuleBuilder ruleBuilder,
            ProfileFactory profileFactory)
        {
            // Шаг 5: reset
            ruleBuilder.Reset();

            // Шаг 6: setName
            ruleBuilder.SetName(ruleName);

            // Шаг 7: добавление целевых документов
            foreach (var doc in targetDocs)
            {
                ruleBuilder.AddTargetDocument(doc);
            }

            // Шаг 8-18: создание профиля через фабрику
            var profile = profileFactory.CreateProfile(
                days,
                purpose,
                citizenship,
                new List<string> { "Срок", "Цель", "Гражданство" },
                new List<string> { days.ToString(), purpose, citizenship }
            );

            // Шаг 19: добавление профиля в builder
            ruleBuilder.AddProfile(profile);

            // Шаг 20: добавление руководства
            ruleBuilder.AddGuidance(guidanceDescription, refusal, orgNames, orgAddresses);

            // Шаг 30: получение результата
            var rule = ruleBuilder.GetResult();

            // Шаг 36-37: возврат
            return rule;
        }
    }
}