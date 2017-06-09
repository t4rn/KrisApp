using Autofac;
using AutoMapper;
using KrisApp.DataModel.Articles;
using KrisApp.DataModel.Contact;
using KrisApp.DataModel.Dictionaries;
using KrisApp.DataModel.Pages;
using KrisApp.DataModel.Questions;
using KrisApp.DataModel.Users;
using KrisApp.DataModel.Work;
using KrisApp.Models;
using KrisApp.Models.Articles;
using KrisApp.Models.Me;
using KrisApp.Models.Pages;
using KrisApp.Models.Questions;
using KrisApp.Models.User;
using KrisApp.Models.Work;

namespace KrisApp.AutofacModules
{
    public class AutoMapperModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // rejestrujemy wszystkie klasy dziedziczące po Profile w danym Assembly
            builder.RegisterAssemblyTypes(typeof(AutoMapperModule).Assembly).As<Profile>();

            builder.Register(context => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Worker, WorkerModel>();
                cfg.CreateMap<WorkerSkill, WorkerSkillModel>();
                cfg.CreateMap<WorkerPosition, WorkerPositionModel>();
                cfg.CreateMap<DictionaryItem, DictionaryItemModel>();
                cfg.CreateMap<UserRegisterModel, UserRequest>();
                cfg.CreateMap<ContactModel, ContactMessage>();

                cfg.CreateMap<RekruQuestion, QuestionModel>();
                cfg.CreateMap<QuestionModel, RekruQuestion>();
                cfg.CreateMap<RekruAnswer, AnswerModel>();
                cfg.CreateMap<AnswerModel, RekruAnswer>();
                cfg.CreateMap<ArticleModel, Article>();
                cfg.CreateMap<Article, ArticleModel>();
                cfg.CreateMap<Article, ArticleDetailsModel>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.Name));
                cfg.CreateMap<PageContent, PageContentModel>();
                cfg.CreateMap<PageContentModel, PageContent>();
                cfg.CreateMap<ArticleType, ArticleTypeModel>();

                //foreach (var profile in context.Resolve<IEnumerable<Profile>>())
                //{
                //    cfg.AddProfile(profile);
                //}
            })).AsSelf().SingleInstance();

            //TODO: wymuszenie walidacji

            builder.Register(c => c.Resolve<MapperConfiguration>().CreateMapper(c.Resolve))
                .As<IMapper>()
                .InstancePerLifetimeScope();
        }
    }
}