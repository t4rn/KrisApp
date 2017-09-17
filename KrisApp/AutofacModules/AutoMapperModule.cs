using Autofac;
using AutoMapper;
using KrisApp.DataModel.Articles;
using KrisApp.DataModel.Calc;
using KrisApp.DataModel.Contact;
using KrisApp.DataModel.Dictionaries;
using KrisApp.DataModel.Pages;
using KrisApp.DataModel.Questions;
using KrisApp.DataModel.Users;
using KrisApp.DataModel.Work;
using KrisApp.Models;
using KrisApp.Models.Articles;
using KrisApp.Models.Calc;
using KrisApp.Models.Me;
using KrisApp.Models.Pages;
using KrisApp.Models.Questions;
using KrisApp.Models.User;
using KrisApp.Models.Work;
using System.Web.Mvc;

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
                    .ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => src.Type.Name))
                    .ForMember(dest => dest.TypeCode, opt => opt.MapFrom(src => src.Type.Code));

                cfg.CreateMap<PageContent, PageContentModel>();
                cfg.CreateMap<PageContentModel, PageContent>();
                cfg.CreateMap<ArticleType, ArticleTypeModel>();

                cfg.CreateMap<ArticleType, SelectListItem>()
                    .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.ID.ToString()))
                    .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Name));

                cfg.CreateMap<UodSummary, UodSummaryModel>()
                    .ForMember(dest => dest.Average, opt => opt.MapFrom(src => src.Average.ToString("n")))
                    .ForMember(dest => dest.Brutto, opt => opt.MapFrom(src => src.Brutto.ToString("n")))
                    .ForMember(dest => dest.NettoMax, opt => opt.MapFrom(src => src.NettoMax.ToString("n")))
                    .ForMember(dest => dest.NettoMin, opt => opt.MapFrom(src => src.NettoMin.ToString("n")))
                    .ForMember(dest => dest.Sum, opt => opt.MapFrom(src => src.Sum.ToString("n")));

                //foreach (var profile in context.Resolve<IEnumerable<Profile>>())
                //{
                //    cfg.AddProfile(profile);
                //}
            })).AsSelf().SingleInstance();

            //TODO: force validation

            builder.Register(c => c.Resolve<MapperConfiguration>().CreateMapper(c.Resolve))
                .As<IMapper>()
                .InstancePerLifetimeScope();
        }
    }
}