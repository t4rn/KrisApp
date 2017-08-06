using KrisApp.DataModel.Interfaces;
using KrisApp.DataModel.Interfaces.Repositories;
using KrisApp.DataModel.Questions;
using KrisApp.DataModel.Results;
using KrisApp.DataModel.Users;
using System;
using System.Collections.Generic;

namespace KrisApp.Services
{
    public class QuestionService : AbstractService, IQuestionService
    {
        private readonly IQuestionRepository _questionRepo;
        private readonly User _user;
        private readonly ISessionService _sessionSrv;

        public QuestionService(ILogger log, IQuestionRepository questionRepo, ISessionService sessionSrv) : base(log)
        {
            _questionRepo = questionRepo;
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
                _questionRepo.AddAnswer(answer);
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
                _questionRepo.AddQuestion(question);
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
            List<RekruQuestion> questions = _questionRepo.GetQuestions(false);

            return questions;
        }

        public RekruQuestion GetQuestion(int id)
        {
            RekruQuestion question = _questionRepo.GetQuestion(id);

            return question;
        }

        public Result EditAnswer(RekruAnswer answer)
        {
            Result result = new Result();

            try
            {
                _questionRepo.EditAnswer(answer);
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
            RekruAnswer answer = _questionRepo.GetAnswer(id);

            return answer;
        }

        public Result EditQuestion(RekruQuestion question)
        {
            Result result = new Result();

            try
            {
                _questionRepo.EditQuestion(question);
                result.IsOK = true;
                _log.Debug("[EditQuestion] User '{0}' edytowal pytanie o ID = '{1}'",
                    _user?.Login, question.ID);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                ExceptionLog("EditQuestion", ex);
            }

            return result;
        }

        public Result DeleteAnswer(int answerID)
        {
            Result result = new Result();
            try
            {
                int rowsAffected = _questionRepo.DeleteAnswer(answerID);

                if (rowsAffected > 0)
                {
                    _log.Debug("[DeleteAnswer] User '{0}' usunal odpowiedz o ID = '{1}'",
                        _user?.Login, answerID);
                    result.IsOK = true;
                }
                else
                {
                    result.Message = $"Nie usunięto odpowiedzi o ID '{answerID}'";
                }
            }
            catch (Exception ex)
            {
                ExceptionLog("DeleteAnswer", ex);
                result.Message = ex.Message;
            }

            return result;
        }
    }
}