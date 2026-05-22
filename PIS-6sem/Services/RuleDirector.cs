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
            List<int> daysList,
            List<string> purposes,
            List<string> citizenships,
            List<List<string>> propertyNames,
            List<List<string>> propertyValues,
            RuleBuilder ruleBuilder,
            ProfileFactory profileFactory)
        {
            if (daysList.Count != purposes.Count ||
                daysList.Count != citizenships.Count ||
                daysList.Count != propertyNames.Count ||
                daysList.Count != propertyValues.Count)
            {
                throw new ArgumentException("Количество элементов в списках профилей не совпадает.");
            }

            ruleBuilder.Reset();
            ruleBuilder.SetName(ruleName);

            foreach (var doc in targetDocs)
            {
                ruleBuilder.AddTargetDocument(doc);
            }

            for (int i = 0; i < daysList.Count; i++)
            {
                var profile = profileFactory.CreateProfile(
                    daysList[i],
                    purposes[i],
                    citizenships[i],
                    propertyNames[i],
                    propertyValues[i]);

                ruleBuilder.AddProfile(profile);
            }

            ruleBuilder.AddGuidance(guidanceDescription, refusal, orgNames, orgAddresses);

            return ruleBuilder.GetResult();
        }
    }
}