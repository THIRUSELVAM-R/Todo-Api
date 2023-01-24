using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ToDo.Data;
using ToDo.Models;

namespace ToDo.Controllers
{
    public class TodosController : ApiController
    {
        private ToDoContext db = new ToDoContext();

        // GET: api/Todos
        public IQueryable<Todos> GetTodos()
        {
            return db.Todos;
        }

        // GET: api/Todos/5
        [ResponseType(typeof(Todos))]
        public IHttpActionResult GetTodos(int id)
        {
            Todos todos = db.Todos.Find(id);
            if (todos == null)
            {
                return NotFound();
            }

            return Ok(todos);
        }

        // PUT: api/Todos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTodos(int id, Todos todos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != todos.Id)
            {
                return BadRequest();
            }

            db.Entry(todos).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodosExists(id))
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

        // POST: api/Todos
        [ResponseType(typeof(Todos))]
        public IHttpActionResult PostTodos(Todos todos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Todos.Add(todos);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = todos.Id }, todos);
        }

        // DELETE: api/Todos/5
        [ResponseType(typeof(Todos))]
        public IHttpActionResult DeleteTodos(int id)
        {
            Todos todos = db.Todos.Find(id);
            if (todos == null)
            {
                return NotFound();
            }

            db.Todos.Remove(todos);
            db.SaveChanges();

            return Ok(todos);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TodosExists(int id)
        {
            return db.Todos.Count(e => e.Id == id) > 0;
        }
    }
}