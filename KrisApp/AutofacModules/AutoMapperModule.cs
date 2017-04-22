using Autofac;
using AutoMapper;
using KrisApp.DataModel.Contact;
using KrisApp.DataModel.Dictionaries;
using KrisApp.DataModel.Rekru;
using KrisApp.DataModel.Users;
using KrisApp.DataModel.Work;
using KrisApp.Models;
using KrisApp.Models.Me;
using KrisApp.Models.Rekru;
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

                //foreach (var profile in context.Resolve<IEnumerable<Profile>>())
                //{
                //    cfg.AddProfile(profile);
                //}
            })).AsSelf().SingleInstance();

            builder.Register(c => c.Resolve<MapperConfiguration>().CreateMapper(c.Resolve))
                .As<IMapper>()
                .InstancePerLifetimeScope();
        }
    }
}