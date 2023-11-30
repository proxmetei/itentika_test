using itentikaproc.Models;
using Microsoft.EntityFrameworkCore;

namespace itentikaproc.Repositories
{
    public class ProcessingRepo
    {
        private readonly ApplicationContext _context;
        public ProcessingRepo(ApplicationContext context)
        {
            _context = context;
        }
        public void addProcess(Incident incident)
        {
            _context.Incidents.Add(incident);
            _context.SaveChanges();
        }
        public List<Incident> GetAllIncidents(bool sortByTime, bool sortByType, int pageNum, int pageSize)
        {
            var result = _context.Incidents.Include("EventsList");
            if(sortByTime)
                result = result.OrderByDescending(x => x.Time);
            if(sortByType)
                result = result.OrderByDescending(x => x.Type);
            if (pageNum > 0 && ((pageNum - 1) * pageSize) < result.Count())
            {
               result = result.Skip((pageNum - 1) * pageSize).Take(pageSize);
            }
            return result.ToList();
        }
    }
}
