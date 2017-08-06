using KrisApp.DataAccess.DbContexts;
using KrisApp.DataModel.Interfaces.Repositories;
using KrisApp.DataModel.Questions;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System;

namespace KrisApp.DataAccess
{
    public class QuestionRepo : BaseDAL, IQuestionRepository
    {
        public QuestionRepo(string cs) : base(cs)
        {
        }

        public void AddAnswer(RekruAnswer answer)
        {
            using (KrisDbContext context = new KrisDbContext(csKris))
            {
                context.Answers.Add(answer);
                context.SaveChanges();
            }
        }

        public void AddQuestion(RekruQuestion question)
        {
            using (KrisDbContext context = new KrisDbContext(csKris))
            {
                context.RekruQuestions.Add(question);
                context.SaveChanges();
            }
        }

        public int DeleteAnswer(int answerID)
        {
            using (KrisDbContext context = new KrisDbContext(csKris))
            {
                context.Entry(new RekruAnswer { ID = answerID }).State = EntityState.Deleted;
                return context.SaveChanges();
            }
        }

        public void EditAnswer(RekruAnswer answer)
        {
            using (KrisDbContext context = new KrisDbContext(csKris))
            {
                context.Entry<RekruAnswer>(answer).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void EditQuestion(RekruQuestion question)
        {
            using (KrisDbContext context = new KrisDbContext(csKris))
            {
                context.Entry<RekruQuestion>(question).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public RekruAnswer GetAnswer(int id)
        {
            RekruAnswer answer = null;
            using (KrisDbContext context = new KrisDbContext(csKris))
            {
                answer = context.Answers.Where(x => x.ID == id).FirstOrDefault();
            }

            return answer;
        }

        public RekruQuestion GetQuestion(int id)
        {
            RekruQuestion question = new RekruQuestion();

            using (KrisDbContext context = new KrisDbContext(csKris))
            {
                question = context.RekruQuestions.AsNoTracking()
                    .Where(x => x.ID == id)
                    .Include(x => x.Answers)
                    .FirstOrDefault();
            }

            return question;
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
