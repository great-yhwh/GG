namespace WebAPI_PIS_6sem.Models
{
    public class CreateRuleRequest
    {
        public string RuleName { get; set; } = "";
        public List<string> TargetDocs { get; set; } = [];
        public string GuidanceDescription { get; set; } = "";
        public string Refusal { get; set; } = "";
        public List<string> OrgNames { get; set; } = [];
        public List<string> OrgAddresses { get; set; } = [];
        public List<int> DaysList { get; set; } = [];
        public List<List<string>> PurposeNamesList { get; set; } = [];
        public List<List<string>> CitizenshipNamesList { get; set; } = [];
        public List<List<string>> PropertyNames { get; set; } = [];
        public List<List<string>> PropertyValues { get; set; } = [];
    }
}