using KrisApp.DataModel.Rekru;
using System.Collections.Generic;
using KrisApp.DataModel.Results;

namespace KrisApp.DataModel.Interfaces
{
    public interface IRekruService
    {
        List<RekruQuestion> GetQuestions();
        Result AddQuestion(RekruQuestion question);
    }
}
