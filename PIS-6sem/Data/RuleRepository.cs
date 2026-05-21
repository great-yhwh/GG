using Microsoft.EntityFrameworkCore;
using PIS_6sem.Entities;

namespace PIS_6sem.Data
{
    public class RuleRepository : IRuleRepository
    {
        private readonly RuleDbContext _db;

        public RuleRepository(RuleDbContext db)
        {
            _db = db;
        }

        public void Add(Rule rule)
        {
            // Шаг 8 диаграммы: Rules.Add(rule)
            // DbContext начинает отслеживать весь граф объектов
            _db.Rules.Add(rule);
        }

        public Rule GetById(int id) => _db.Rules
                .Include(r => r.Profiles)
                    .ThenInclude(p => p.Properties)
                .Include(r => r.TargetDocuments)
                .Include(r => r.Guidance)
                    .ThenInclude(g => g.Organizations)
                .FirstOrDefault(r => r.Id == id);
    }
}