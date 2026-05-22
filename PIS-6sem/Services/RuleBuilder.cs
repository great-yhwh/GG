using PIS_6sem.Entities;

namespace PIS_6sem.Services
{
    public class RuleBuilder
    {
        private string _name = string.Empty;
        private readonly List<string> _targetDocNames = new();
        private readonly List<Profile> _profiles = new();
        private Guidance? _guidance;

        public void Reset()
        {
            _name = string.Empty;
            _targetDocNames.Clear();
            _profiles.Clear();
            _guidance = null;
        }

        public void SetName(string name)
        {
            _name = name;
        }

        public void AddTargetDocument(string targetDoc)
        {
            _targetDocNames.Add(targetDoc);
        }

        public void AddProfile(Profile profile)
        {
            _profiles.Add(profile);
        }

        public void AddGuidance(
            string description,
            string refusal,
            List<string> orgNames,
            List<string> orgAddresses)
        {
            var guidance = new Guidance();
            guidance.SetDescription(description);
            guidance.SetRefusal(refusal);

            int count = Math.Min(orgNames.Count, orgAddresses.Count);

            for (int i = 0; i < count; i++)
            {
                var org = new Organization();
                org.SetName(orgNames[i]);
                org.SetAddress(orgAddresses[i]);
                guidance.AddOrganization(org);
            }

            _guidance = guidance;
        }

        public Rule GetResult()
        {
            var rule = new Rule();
            rule.SetName(_name);

            foreach (var profile in _profiles)
            {
                rule.SetProfile(profile);
            }

            foreach (var docName in _targetDocNames)
            {
                var doc = new TargetDocument();
                doc.SetName(docName);
                rule.SetTargetDocument(doc);
            }

            if (_guidance != null)
            {
                rule.SetGuidance(_guidance);
            }

            return rule;
        }
    }
}