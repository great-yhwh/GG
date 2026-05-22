using WebAPI_PIS_6sem.Entities;

namespace WebAPI_PIS_6sem.Data
{
    public interface IRuleRepository
    {
        void Add(Rule rule);
        Rule? GetById(int id);
    }
}