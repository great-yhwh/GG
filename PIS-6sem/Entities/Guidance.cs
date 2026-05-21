namespace PIS_6sem.Entities
{
    public class Guidance
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Refusal { get; set; }

        public List<Organization> Organizations { get; set; } = new List<Organization>();

        public void SetDescription(string description)
        {
            Description = description;
        }

        public void SetRefusal(string refusal)
        {
            Refusal = refusal;
        }

        public void AddOrganization(Organization org)
        {
            Organizations.Add(org);
        }
    }
}
