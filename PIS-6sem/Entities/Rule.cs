namespace PIS_6sem.Entities
{
    public class Rule
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";

        public List<Profile> Profiles { get; set; } = new();
        public List<TargetDocument> TargetDocuments { get; set; } = new();
        public Guidance? Guidance { get; set; }
    }
}