using KrisApp.DataModel.Rekru;
using System.Collections.Generic;

namespace KrisApp.DataModel.Interfaces.Repositories
{
    public interface IRekruRepository
    {
        /// <summary>
        /// Zwraca wszystkie pytania rekrutacyjne
        /// </summary>
        List<RekruQuestion> GetQuestions(bool includeGhosts);

        RekruQuestion GetQuestion(int id);
        void AddQuestion(RekruQuestion question);
    }
}
