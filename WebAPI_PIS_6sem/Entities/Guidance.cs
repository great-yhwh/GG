namespace WebAPI_PIS_6sem.Entities
{
    public class Guidance
    {
        public int Id { get; set; }
        public string Description { get; set; } = "";
        public string Refusal { get; set; } = "";

        public int RuleId { get; set; }          
        public Rule Rule { get; set; } = null!; 

        public List<Organization> Organizations { get; set; } = [];
    }
}