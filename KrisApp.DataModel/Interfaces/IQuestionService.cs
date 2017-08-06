using KrisApp.DataModel.Questions;
using KrisApp.DataModel.Results;
using System.Collections.Generic;

namespace KrisApp.DataModel.Interfaces
{
    public interface IQuestionService
    {
        List<RekruQuestion> GetQuestions();
        Result AddQuestion(RekruQuestion question);
        RekruQuestion GetQuestion(int id);
        Result AddAnswer(RekruAnswer answer);
        Result EditAnswer(RekruAnswer answer);
        RekruAnswer GetAnswer(int id);
        Result EditQuestion(RekruQuestion question);
        Result DeleteAnswer(int id);
    }
}
