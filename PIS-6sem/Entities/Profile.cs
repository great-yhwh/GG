namespace PIS_6sem.Entities
{
    public class Profile
    {
        public int Id { get; set; }
        public int Days { get; set; }
        public string Purpose { get; set; }
        public string Citizenship { get; set; }

        public List<ProfileProperty> Properties { get; set; } = new List<ProfileProperty>();

        public void SetDays(int days)
        {
            Days = days;
        }

        public void SetPurpose(string purpose)
        {
            Purpose = purpose;
        }

        public void SetCitizenship(string citizenship)
        {
            Citizenship = citizenship;
        }

        public void AddProperty(ProfileProperty property)
        {
            Properties.Add(property);
        }
    }
}