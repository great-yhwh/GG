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
            List<int> daysList,
            List<string> purposes,
            List<string> citizenships,
            List<List<string>> propertyNames,
            List<List<string>> propertyValues)
        {
            var ruleBuilder = new RuleBuilder();
            var profileFactory = new ProfileFactory();

            var rule = _director.Construct(
                ruleName,
                targetDocs,
                guidanceDescription,
                refusal,
                orgNames,
                orgAddresses,
                daysList,
                purposes,
                citizenships,
                propertyNames,
                propertyValues,
                ruleBuilder,
                profileFactory);

            using (var transaction = _unitOfWork.BeginTransaction())
            {
                _unitOfWork.Rules.Add(rule);
                _unitOfWork.Save();
                transaction.Commit();
            }

            return rule;
        }
    }
}