using Microsoft.EntityFrameworkCore;
using WebAPI_PIS_6sem.Entities;

namespace WebAPI_PIS_6sem.Data
{
    public class RuleRepository(RuleDbContext db) : IRuleRepository
    {
        private readonly RuleDbContext _db = db;

        public void Add(Rule rule)
        {
            _db.Rules.Add(rule);
        }

        public Rule? GetById(int id)
        {
            return _db.Rules
                .Include(r => r.Profiles)
                    .ThenInclude(p => p.Properties)
                .Include(r => r.TargetDocuments)
                .Include(r => r.Guidance!)
                    .ThenInclude(g => g.Organizations)
                .FirstOrDefault(r => r.Id == id);
        }
    }
}