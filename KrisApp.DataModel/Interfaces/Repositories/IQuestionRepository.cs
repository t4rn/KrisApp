using KrisApp.DataModel.Questions;
using System.Collections.Generic;

namespace KrisApp.DataModel.Interfaces.Repositories
{
    public interface IQuestionRepository
    {
        /// <summary>
        /// Returns all technical questions
        /// </summary>
        List<RekruQuestion> GetQuestions(bool includeGhosts);

        /// <summary>
        /// Returns a question of the given ID (with answers)
        /// </summary>
        RekruQuestion GetQuestion(int id);
        void AddQuestion(RekruQuestion question);
        /// <summary>
        /// Adds a question
        /// </summary>
        void AddAnswer(RekruAnswer answer);
        void EditAnswer(RekruAnswer answer);
        RekruAnswer GetAnswer(int id);
        void EditQuestion(RekruQuestion question);
    }
}
