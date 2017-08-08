using AutoMapper;
using KrisApp.DataModel.Interfaces;
using KrisApp.DataModel.Work;
using KrisApp.Models.Work;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

namespace KrisApp.Controllers
{
    [RoutePrefix("api/workers")]
    public class WorkerApiController : ApiController
    {
        private readonly ILogger _log;
        private readonly IWorkerService _workerSrv;
        private readonly IMapper _mapper;

        public WorkerApiController(ILogger log, IWorkerService workerSrv, IMapper mapper)
        {
            _log = log;
            _workerSrv = workerSrv;
            _mapper = mapper;
        }

        // GET: api/Workers
        [Route("")]
        public IEnumerable<WorkerModel> GetWorkers()
        {
            List<WorkerModel> workersModel = new List<WorkerModel>();

            // pobieramy dane z bazy
            List<Worker> workers = _workerSrv.GetWorkers();

            // mapujemy encje z bazy na modele
            workersModel = _mapper.Map<List<WorkerModel>>(workers);

            return workersModel;
        }

        // GET: api/Workers/5
        [ResponseType(typeof(Worker))]
        [Route("{id:int}")]
        public IHttpActionResult GetWorker(int id)
        {
            Worker worker = _workerSrv.GetWorkerByID(id);

            WorkerModel workerModel = new WorkerModel();

            if (worker != null)
            {
                workerModel = _mapper.Map<WorkerModel>(worker);
            }

            if (worker == null)
            {
                return NotFound();
            }

            return Ok(workerModel);
        }

        // GET: api/Workers/5/skills
        [ResponseType(typeof(Worker))]
        [Route("{id:int}/skills")]
        public IHttpActionResult GetWorkerSkills(int id)
        {
            Worker worker = _workerSrv.GetWorkerByID(id);

            WorkerModel workerModel = _mapper.Map<WorkerModel>(worker);
            if (workerModel == null)
            {
                return NotFound();
            }

            return Ok(workerModel.Skills);
        }

        // GET: api/Workers/5/positions
        [ResponseType(typeof(Worker))]
        [Route("{id:int}/positions")]
        public IHttpActionResult GetWorkerPositions(int id)
        {
            Worker worker = _workerSrv.GetWorkerByID(id);

            WorkerModel workerModel = _mapper.Map<WorkerModel>(worker);
            if (workerModel == null)
            {
                return NotFound();
            }

            return Ok(workerModel.Positions);
        }

        [Route("skills")]
        public IHttpActionResult GetSkillTypes()
        {
            return Ok(_workerSrv.GetSkillTypes());
        }

        [Route("positions")]
        public IHttpActionResult GetPositionTypes()
        {
            return Ok(_workerSrv.GetPositionTypes());
        }

        /*

        // PUT: api/WorkerApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutWorker(int id, Worker worker)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != worker.ID)
            {
                return BadRequest();
            }

            db.Entry(worker).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/WorkerApi
        [ResponseType(typeof(Worker))]
        public IHttpActionResult PostWorker(Worker worker)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Workers.Add(worker);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = worker.ID }, worker);
        }

        // DELETE: api/WorkerApi/5
        [ResponseType(typeof(Worker))]
        public IHttpActionResult DeleteWorker(int id)
        {
            Worker worker = db.Workers.Find(id);
            if (worker == null)
            {
                return NotFound();
            }

            db.Workers.Remove(worker);
            db.SaveChanges();

            return Ok(worker);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WorkerExists(int id)
        {
            return db.Workers.Count(e => e.ID == id) > 0;
        }

    */
    }
}