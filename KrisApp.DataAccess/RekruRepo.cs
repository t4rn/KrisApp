using KrisApp.DataAccess.DbContexts;
using KrisApp.DataModel.Interfaces.Repositories;
using KrisApp.DataModel.Rekru;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KrisApp.DataAccess
{
    public class RekruRepo : BaseDAL, IRekruRepository
    {
        public RekruRepo(string cs) : base(cs)
        {
        }

        public void AddQuestion(RekruQuestion question)
        {
            using (KrisDbContext context = new KrisDbContext(csKris))
            {
                context.RekruQuestions.Add(question);
                context.SaveChanges();
            }
        }

        public RekruQuestion GetQuestion(int id)
        {
            throw new NotImplementedException();
        }

        public List<RekruQuestion> GetQuestions(bool includeGhosts)
        {
            List<RekruQuestion> questions = null;
            using (KrisDbContext context = new KrisDbContext(csKris))
            {
                IQueryable<RekruQuestion> query = context.RekruQuestions.AsNoTracking();

                if (includeGhosts == false)
                {
                    query = query.Where(x => x.Ghost == false);
                }

                questions = query.ToList();
            }

            return questions;
        }
    }
}
