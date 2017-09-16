using Autofac;
using KrisApp.DataAccess;
using KrisApp.DataAccess.Dictionaries;
using KrisApp.DataModel.Interfaces;
using KrisApp.DataModel.Interfaces.Repositories;
using KrisApp.Services;

namespace KrisApp.AutofacModules
{
    public class AutofacModule : Module
    {
        private readonly string _connStr;

        public AutofacModule(string connStr)
        {
            _connStr = connStr;
        }

        protected override void Load(ContainerBuilder builder)
        {
            #region Repositories

            builder.Register(c => new ArticleRepo(_connStr))
                .As<IArticleRepository>().InstancePerRequest();

            builder.Register(c => new ContactMessageRepo(_connStr))
                .As<IContactMessageRepository>().InstancePerRequest();

            builder.Register(c => new DictionaryRepo(_connStr))
                .As<IDictionaryRepository>().InstancePerRequest();

            builder.Register(c => new AppLogRepo(_connStr))
                .As<ILogRepository>().InstancePerRequest();

            builder.Register(c => new UserRepo(_connStr))
                .As<IUserRepository>().InstancePerRequest();

            builder.Register(c => new UserRequestRepo(_connStr))
                .As<IUserRequestRepository>().InstancePerRequest();

            builder.Register(c => new WorkerRepo(_connStr))
                .As<IWorkerRepository>().InstancePerRequest();

            builder.Register(c => new QuestionRepo(_connStr))
                .As<IQuestionRepository>().InstancePerRequest();

            builder.Register(c => new PageContentRepo(_connStr))
                .As<IPageContentRepository>().InstancePerRequest();

            #endregion

            #region Services

            builder.RegisterType<LogService>().As<IAppLogService>().InstancePerRequest();
            builder.RegisterType<ArticleService>().As<IArticleService>().InstancePerRequest();
            builder.RegisterType<ContactService>().As<IContactService>().InstancePerRequest();
            builder.RegisterType<DictionaryService>().As<IDictionaryService>().InstancePerRequest();
            builder.RegisterType<KrisLogger>().As<ILogger>().InstancePerRequest();
            builder.RegisterType<UserService>().As<IUserService>().InstancePerRequest();
            builder.RegisterType<WorkerService>().As<IWorkerService>().InstancePerRequest();
            builder.RegisterType<QuestionService>().As<IQuestionService>().InstancePerRequest();
            builder.RegisterType<SessionService>().As<ISessionService>().InstancePerRequest();
            builder.RegisterType<PageContentService>().As<IPageContentService>().InstancePerRequest();
            builder.RegisterType<CalcService>().As<ICalcService>().InstancePerRequest();

            #endregion

            base.Load(builder);
        }
    }
}