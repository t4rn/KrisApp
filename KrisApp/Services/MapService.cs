using AutoMapper;
using KrisApp.DataModel.Work;
using KrisApp.Models.Work;
using System.Collections.Generic;
using System;
using KrisApp.DataModel.Dictionaries;
using KrisApp.Models;

namespace KrisApp.Services
{
    public static class MapService
    {
        static MapService()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Worker, WorkerModel>();
                cfg.CreateMap<WorkerSkill, WorkerSkillModel>();
                cfg.CreateMap<WorkerPosition, WorkerPositionModel>();
                cfg.CreateMap<DictionaryItem, DictionaryItemModel>();
            });

            //var config = new MapperConfiguration(cfg => cfg.CreateMap<Worker, WorkerModel>());
        }

        internal static List<WorkerModel> MapWorkersToModel(List<Worker> workers)
        {
            List<WorkerModel> workersModel = new List<WorkerModel>();

            workersModel = Mapper.Map<List<WorkerModel>>(workers);

            return workersModel;
        }

        internal static WorkerModel MapWorkerToModel(Worker worker)
        {
            WorkerModel workerModel = Mapper.Map<WorkerModel>(worker);
            return workerModel;
        }
    }
}