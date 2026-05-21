namespace PIS_6sem.Entities
{
    public class Rule
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Profile> Profiles { get; set; } = new List<Profile>();
        public List<TargetDocument> TargetDocuments { get; set; } = new List<TargetDocument>();
        public Guidance Guidance { get; set; }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetProfile(Profile profile)
        {
            Profiles.Add(profile);
        }

        public void SetTargetDocument(TargetDocument doc)
        {
            TargetDocuments.Add(doc);
        }

        public void SetGuidance(Guidance guidance)
        {
            Guidance = guidance;
        }
    }
}
