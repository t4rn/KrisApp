using KrisApp.DataAccess;
using KrisApp.DataModel.Work;
using KrisApp.Models.Work;
using KrisApp.Services;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

namespace KrisApp.Controllers
{
    [RoutePrefix("api/workers")]
    public class WorkerApiController : ApiController
    {
        private readonly KrisLogger _log;
        private readonly WorkerService _workerSrv;

        public WorkerApiController()
        {
            _log = new KrisLogger();
            _workerSrv = new WorkerService(_log);
        }

        // GET: api/Workers
        [Route("")]
        public IEnumerable<WorkerModel> GetWorkers()
        {
            List<WorkerModel> workersModel = _workerSrv.GetWorkers();
            return workersModel;
        }

        // GET: api/Workers/5
        [ResponseType(typeof(Worker))]
        [Route("{id:int}")]
        public IHttpActionResult GetWorker(int id)
        {
            WorkerModel worker = _workerSrv.GetWorkerByID(id);
            if (worker == null)
            {
                return NotFound();
            }

            return Ok(worker);
        }

        // GET: api/Workers/5/skills
        [ResponseType(typeof(Worker))]
        [Route("{id:int}/skills")]
        public IHttpActionResult GetWorkerSkills(int id)
        {
            WorkerModel worker = _workerSrv.GetWorkerByID(id);
            if (worker == null)
            {
                return NotFound();
            }

            return Ok(worker.Skills);
        }

        // GET: api/Workers/5/positions
        [ResponseType(typeof(Worker))]
        [Route("{id:int}/positions")]
        public IHttpActionResult GetWorkerPositions(int id)
        {
            WorkerModel worker = _workerSrv.GetWorkerByID(id);
            if (worker == null)
            {
                return NotFound();
            }

            return Ok(worker.Positions);
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