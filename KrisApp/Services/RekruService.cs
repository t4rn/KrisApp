using KrisApp.DataModel.Interfaces;
using KrisApp.DataModel.Interfaces.Repositories;
using KrisApp.DataModel.Rekru;
using KrisApp.DataModel.Results;
using KrisApp.DataModel.Users;
using System;
using System.Collections.Generic;

namespace KrisApp.Services
{
    public class RekruService : AbstractService, IRekruService
    {
        private readonly IRekruRepository _rekruRepo;
        private readonly User _user;
        private readonly ISessionService _sessionSrv;

        public RekruService(ILogger log, IRekruRepository rekruRepo, ISessionService sessionSrv) : base(log)
        {
            _rekruRepo = rekruRepo;
            _sessionSrv = sessionSrv;
            _user = _sessionSrv.GetFromSession<User>(SessionItem.User);
        }

        public Result AddAnswer(RekruAnswer answer)
        {
            Result result = new Result();

            try
            {
                answer.AddDate = DateTime.Now;
                answer.Author = _user?.Login;
                _rekruRepo.AddAnswer(answer);
                result.IsOK = true;
                _log.Debug("[AddAnswer] User '{0}' dodal odpowiedz na pytanie o ID = '{1}'",
                    _user?.Login, answer.QuestionID);

            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                ExceptionLog("AddAnswer", ex);
            }

            return result;
        }

        public Result AddQuestion(RekruQuestion question)
        {
            Result result = new Result();
            try
            {
                question.AddDate = DateTime.Now;
                question.Author = _user?.Login ?? question.Author;
                _rekruRepo.AddQuestion(question);
                result.IsOK = true;

                _log.Debug("[AddQuestion] User '{0}' dodal pytanie o tresci = '{1}'. Otrzymalo ID = '{2}'",
                    _user?.Login, question.Question, question.ID);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                ExceptionLog("AddQuestion", ex);
            }

            return result;
        }

        public List<RekruQuestion> GetQuestions()
        {
            List<RekruQuestion> questions = _rekruRepo.GetQuestions(false);

            return questions;
        }

        public RekruQuestion GetQuestion(int id)
        {
            RekruQuestion question = _rekruRepo.GetQuestion(id);

            return question;
        }

        public Result EditAnswer(RekruAnswer answer)
        {
            Result result = new Result();

            try
            {
                _rekruRepo.EditAnswer(answer);
                result.IsOK = true;
                _log.Debug("[EditAnswer] User '{0}' edytowal odpowiedz o ID = '{1}'",
                    _user?.Login, answer.ID);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                ExceptionLog("EditAnswer", ex);
            }

            return result;
        }

        public RekruAnswer GetAnswer(int id)
        {
            RekruAnswer answer = _rekruRepo.GetAnswer(id);

            return answer;
        }

        public Result EditQuestion(RekruQuestion question)
        {
            Result result = new Result();

            try
            {
                _rekruRepo.EditQuestion(question);
                result.IsOK = true;
                _log.Debug("[EditQuestion] User '{0}' edytowal pytanie o ID = '{1}'",
                    _user?.Login, question.ID);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                ExceptionLog("[EditQuestion]", ex);
            }

            return result;
        }
    }
}