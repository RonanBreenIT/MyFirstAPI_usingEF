using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyFirstAPI.Models;

namespace MyFirstAPI.Controllers
{

    [RoutePrefix("api/Phonebook")]
    public class PhonebookController : ApiController

    {
        List<Phonebook> myBook = new List<Phonebook>();
        PhonebookContext pCtx = new PhonebookContext();

        public PhonebookController() : base()
        {
            using (PhonebookContext db = new PhonebookContext())
            {
                Phonebook p1 = new Phonebook { Name = "Ronan", Number = "1111", Address = "1 Main Street" };
                Phonebook p2 = new Phonebook { Name = "James", Number = "2222", Address = "2 Main Street" };
                Phonebook p3 = new Phonebook { Name = "Pamela", Number = "3333" };

                myBook.Add(p1);
                myBook.Add(p2);
                myBook.Add(p3);
                foreach (Phonebook item in myBook)
                {
                    db.Phonebooks.Add(item);
                    db.SaveChanges();
                }
            }
        }
        // GET: api/Phonebook
        public IEnumerable<Phonebook> Get()
        {
            using (PhonebookContext db = new PhonebookContext())
            {
                // returns all Phonebook entries
                return db.Phonebooks.ToList();
            }
        }

        // GET: api/Phonebook/1
        [HttpGet]
        [Route("ID/{id}")]
        public IEnumerable<Phonebook> GetByID(int id)
        {
            using (PhonebookContext db = new PhonebookContext())
            {
                // returns all Phonebook entries
                return db.Phonebooks.ToList().Where(p => p.ID == id);
            }
        }

        // Added - basically the same as GetByNumber
        [Route("GetEntry/{number}")]
        public IHttpActionResult GetEntry(string number)
        {
            using (PhonebookContext db = new PhonebookContext())
            {
                var entry = db.Phonebooks.FirstOrDefault(p => p.Number.ToUpper() == number.ToUpper());
                if (entry == null)
                {
                    return NotFound();
                }
                return Ok(entry);
            }
               
        }


        // http://localhost:56097/api/Phonebook?Name=Ronan
        public string GetByName(string name)
        {
            using (PhonebookContext db = new PhonebookContext())
            {
                Phonebook FindName = db.Phonebooks.FirstOrDefault(p => p.Name.ToUpper() == name.ToUpper());
                if (FindName != null)
                {
                    return "Number: " + FindName.Number + ", " + "Address: " + FindName.Address;
                }
                else
                {
                    return "Name not in the API";
                }
            }
        }


        // Added - 1. To add an entry into the Database ** Post Request **
        [Route("AddEntry")]
        public IHttpActionResult AddEntry([FromBody]Phonebook contact)
        {
            using(PhonebookContext db = new PhonebookContext())
            {
                var matchNumber = db.Phonebooks.FirstOrDefault(p => p.Number.ToUpper() == contact.Number.ToUpper());
                var matchName = db.Phonebooks.FirstOrDefault(p => p.Name.ToUpper() == contact.Name.ToUpper());
                if ((matchNumber != null) || (matchName != null))
                {
                    return BadRequest(); // might update but means wont be added to DB - this is a 400
                }
                else
                {
                    db.Phonebooks.Add(contact);
                    db.SaveChanges();
                    return Ok();
                }
            }
        }

        // Added - 2. To update an entry already in the Database ** Post Request **
        [Route("UpdateEntry")]
        public IHttpActionResult UpdateEntry([FromBody]Phonebook contact)
        {
            using (PhonebookContext db = new PhonebookContext())
            {
                var matchNumber = db.Phonebooks.FirstOrDefault(p => p.Number.ToUpper() == contact.Number.ToUpper());
                if ((matchNumber == null)) //|| (matchName == null))
                {
                    return BadRequest("Entry doesn't exist"); // might update but means wont be added to DB - this is a 400
                }
                else //if (matchNumber != null)
                {
                    matchNumber.Number = contact.Number;
                    matchNumber.Name = contact.Name;
                    matchNumber.Address = contact.Address;
                    db.SaveChanges();
                    return Ok();
                }
            }
        }

        // Added - 3. To Delete an entry into the Database ** Post Request **
        [Route("DeleteEntry/{number}")]
        public IHttpActionResult DeleteEntry(string number)
        {
            using (PhonebookContext db = new PhonebookContext())
            {
                var matchNumber = db.Phonebooks.FirstOrDefault(p => p.Number.ToUpper() == number.ToUpper());
                if (matchNumber == null) 
                {
                    return BadRequest("Number not in DB"); // might update but means wont be added to DB - this is a 400
                }
                else
                {
                    db.Phonebooks.Remove(matchNumber);
                    db.SaveChanges();
                    return Ok();
                }
            }
        }

    }

}
