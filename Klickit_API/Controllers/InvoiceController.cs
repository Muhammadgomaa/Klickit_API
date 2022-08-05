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
using Klickit_API.Models;

namespace Klickit_API.Controllers
{
    public class InvoiceController : ApiController
    {
        private DB_Context db = new DB_Context();

        // GET: api/Invoice
        public IQueryable<Invoice> GetInvoices()
        {
            return db.Invoices;
        }

        // GET: api/Invoice/5
        [ResponseType(typeof(Invoice))]
        public IHttpActionResult GetInvoice(long id)
        {
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return NotFound();
            }

            return Ok(invoice);
        }

        // PUT: api/Invoice/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutInvoice(long id, Invoice invoice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != invoice.Invo_ID)
            {
                return BadRequest();
            }

            db.Entry(invoice).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceExists(id))
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

        // POST: api/Invoice
        [ResponseType(typeof(Invoice))]
        public IHttpActionResult PostInvoice(Invoice invoice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Invoices.Add(invoice);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = invoice.Invo_ID }, invoice);
        }

        // DELETE: api/Invoice/5
        [ResponseType(typeof(Invoice))]
        public IHttpActionResult DeleteInvoice(long id)
        {
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return NotFound();
            }

            db.Invoices.Remove(invoice);
            db.SaveChanges();

            return Ok(invoice);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InvoiceExists(long id)
        {
            return db.Invoices.Count(e => e.Invo_ID == id) > 0;
        }
    }
}