namespace PIS_6sem.Entities
{
    public class Organization
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetAddress(string address)
        {
            Address = address;
        }
    }
}
