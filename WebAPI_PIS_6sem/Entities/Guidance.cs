namespace WebAPI_PIS_6sem.Entities
{
    public class Guidance
    {
        public int Id { get; set; }
        public string Description { get; set; } = "";
        public string Refusal { get; set; } = "";

        public List<Organization> Organizations { get; set; } = new();
    }
}