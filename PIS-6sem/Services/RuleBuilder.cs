using PIS_6sem.Entities;

namespace PIS_6sem.Services
{
    public class RuleBuilder
    {
        private string _name;
        private List<string> _targetDocNames = new List<string>();
        private Profile _profile;
        private Guidance _guidance;

        // Шаг 5: сброс состояния
        public void Reset()
        {
            _name = null;
            _targetDocNames.Clear();
            _profile = null;
            _guidance = null;
        }

        // Шаг 6: установка имени
        public void SetName(string name)
        {
            _name = name;
        }

        // Шаг 7: добавление целевого документа
        public void AddTargetDocument(string targetDoc)
        {
            _targetDocNames.Add(targetDoc);
        }

        // Шаг 19: добавление профиля
        public void AddProfile(Profile profile)
        {
            _profile = profile;
        }

        // Шаг 20-29: создание руководства с организациями
        public void AddGuidance(
            string description,
            string refusal,
            List<string> orgNames,
            List<string> orgAddresses)
        {
            // Шаг 21-23: создаем руководство
            var guidance = new Guidance();
            guidance.SetDescription(description);
            guidance.SetRefusal(refusal);

            // Шаг 24-28: создаем организации
            for (int i = 0; i < orgNames.Count; i++)
            {
                var org = new Organization();
                org.SetName(orgNames[i]);
                org.SetAddress(orgAddresses[i]);
                guidance.AddOrganization(org);
            }

            _guidance = guidance;
        }

        // Шаг 30-36: сборка итогового Rule
        public Rule GetResult()
        {
            // Шаг 31: создаем Rule
            var rule = new Rule();
            rule.SetName(_name);

            // Шаг 32: устанавливаем профиль
            if (_profile != null)
            {
                rule.SetProfile(_profile);
            }

            // Шаг 33: устанавливаем целевые документы
            foreach (var docName in _targetDocNames)
            {
                var doc = new TargetDocument();
                doc.SetName(docName);
                rule.SetTargetDocument(doc);
            }

            // Шаг 34: устанавливаем руководство
            if (_guidance != null)
            {
                rule.SetGuidance(_guidance);
            }

            return rule;
        }
    }
}