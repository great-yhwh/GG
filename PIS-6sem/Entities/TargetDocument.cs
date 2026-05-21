namespace PIS_6sem.Entities
{
    public class TargetDocument
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public void SetName(string name)
        {
            Name = name;
        }
    }
}
