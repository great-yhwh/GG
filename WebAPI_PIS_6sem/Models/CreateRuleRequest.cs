namespace WebAPI_PIS_6sem.Models
{
    public class CreateRuleRequest
    {
        public string RuleName { get; set; } = "";
        public List<string> TargetDocs { get; set; } = new();
        public string GuidanceDescription { get; set; } = "";
        public string Refusal { get; set; } = "";
        public List<string> OrgNames { get; set; } = new();
        public List<string> OrgAddresses { get; set; } = new();
        public List<int> DaysList { get; set; } = new();
        public List<List<string>> PurposeNamesList { get; set; } = new();
        public List<List<string>> CitizenshipNamesList { get; set; } = new();
        public List<List<string>> PropertyNames { get; set; } = new();
        public List<List<string>> PropertyValues { get; set; } = new();
    }
}