using KrisApp.DataModel.Rekru;
using System.Collections.Generic;
using KrisApp.DataModel.Results;

namespace KrisApp.DataModel.Interfaces
{
    public interface IRekruService
    {
        List<RekruQuestion> GetQuestions();
        Result AddQuestion(RekruQuestion question);
        RekruQuestion GetQuestion(int id);
        Result AddAnswer(RekruAnswer answer);
        Result EditAnswer(RekruAnswer answer);
        RekruAnswer GetAnswer(int id);
        Result EditQuestion(RekruQuestion question);
    }
}
