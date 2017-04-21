using KrisApp.DataModel.Interfaces;
using KrisApp.DataModel.Interfaces.Repositories;
using KrisApp.DataModel.Rekru;
using System;
using System.Collections.Generic;
using KrisApp.DataModel.Results;

namespace KrisApp.Services
{
    public class RekruService : AbstractService, IRekruService
    {
        private readonly IRekruRepository _rekruRepo;

        public RekruService(ILogger log, IRekruRepository rekruRepo) : base(log)
        {
            _rekruRepo = rekruRepo;
        }

        public Result AddQuestion(RekruQuestion question)
        {
            Result result = new Result();
            try
            {
                question.AddDate = DateTime.Now;
                _rekruRepo.AddQuestion(question);
                result.IsOK = true;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        public List<RekruQuestion> GetQuestions()
        {
            List<RekruQuestion> questions = _rekruRepo.GetQuestions(false);

            return questions;
        }

    }
}