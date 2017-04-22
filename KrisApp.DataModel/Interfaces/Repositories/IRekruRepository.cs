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

        /// <summary>
        /// Zwraca pytanie o danym ID wraz z odpowiedziami
        /// </summary>
        RekruQuestion GetQuestion(int id);
        void AddQuestion(RekruQuestion question);
        /// <summary>
        /// Dodaje odpowiedź
        /// </summary>
        void AddAnswer(RekruAnswer answer);
        void EditAnswer(RekruAnswer answer);
        RekruAnswer GetAnswer(int id);
        void EditQuestion(RekruQuestion question);
    }
}
