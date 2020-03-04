using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using MyFirstAPI.Models;

namespace MyFirstAPI.Controllers
{
    /// <summary>
    /// This is an API for Phonebook DB. We can use Get, Post and Delete Entries
    /// </summary>
    [RoutePrefix("api/Phonebook")]

    public class PhonebookController : ApiController

    {
        PhonebookContext pCtx = new PhonebookContext();

        public PhonebookController() : base()
        {
            Phonebook p1 = new Phonebook { Name = "Ronan", Number = "1111", Address = "1 Main Street" };
            Phonebook p2 = new Phonebook { Name = "James", Number = "2222", Address = "2 Main Street" };
            Phonebook p3 = new Phonebook { Name = "Pamela", Number = "3333" };

            pCtx.Phonebooks.Add(p1);
            pCtx.Phonebooks.Add(p2);
            pCtx.Phonebooks.Add(p3);
            pCtx.SaveChanges();
        }

        // GET: api/Phonebook // returns all in the db
        /// <summary>
        /// Returns All Phonebook entries in the DB
        /// </summary>
        public IEnumerable<Phonebook> Get()
        {
            using (PhonebookContext db = new PhonebookContext())
            {
                // returns all Phonebook entries
                return db.Phonebooks.ToList();
            }
        }

        // GET: api/Phonebook/1
        /// <summary>
        /// Returns only one Phonebook Entry where ID is equal to the ID Number you pass in. 
        /// </summary>
        public IEnumerable<Phonebook> GetByID(int id)
        {
            using (PhonebookContext db = new PhonebookContext())
            {
                // returns all Phonebook entries
                return db.Phonebooks.ToList().Where(p => p.ID == id);
            }
        }

        // api/Phonebook?Number=1111
        /// <summary>
        /// Returns only one Phonebook Entry where Phone number is equal to the Phone Number you pass in. 
        /// </summary>
        public string GetByNumber(string number)
        {
            using (PhonebookContext db = new PhonebookContext())
            {
                Phonebook FindNumber = db.Phonebooks.FirstOrDefault(p => p.Number.ToUpper() == number.ToUpper());
                if (FindNumber != null)
                {
                    return "Name: " + FindNumber.Name + ", " + "Address: " + FindNumber.Address;
                }
                else
                {
                    return "Number not in the API";
                }
            }
        }


        // Added - basically the same as GetByNumber /api/Phonebook/GetEntry/1111
        /// <summary>
        /// Returns only one Phonebook Entry where Phone number is equal to the Phone Number you pass in. 
        /// </summary>
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


        // /api/Phonebook?Name=Ronan
        /// <summary>
        /// Returns only one Phonebook Entry where Name is equal to the Name you pass in. 
        /// </summary>
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

        // /api/Phonebook/AddEntry
        /*      {
	                "ID": 3,
	                "Name": "Micahel",
	                "Number": "5555",
	                "Address": "5 Main Street"
                }
         */

        // Added - 1. To add an entry into the Database ** POST Request **
        /// <summary>
        /// Adds a Phonebook Entry based on details you enter in the body
        /// </summary>
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

        // /api/Phonebook/UpdateEntry - POST
        /*  {
	            "ID": 1,
	            "Name": "Trevor",
	            "Number": "1111",
	            "Address": "1 Main Street"
            }
         */

        // Added - 2. To update an entry already in the Database ** Post Request ** Was going to match on both name and number but had issues. May revisit
        /// <summary>
        /// Updates a Phonebook Entry based on details you enter in the body. Matches on Phone Number. 
        /// </summary>
        [Route("UpdateEntry")] 
        public IHttpActionResult UpdateEntry([FromBody]Phonebook contact)
        {
            using (PhonebookContext db = new PhonebookContext())
            {
                var matchNumber = db.Phonebooks.FirstOrDefault(p => p.Number.ToUpper() == contact.Number.ToUpper());
                //var matchName = db.Phonebooks.FirstOrDefault(p => p.Name.ToUpper() == contact.Name.ToUpper());
                if (matchNumber == null) //|| (matchName == null))
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
                //else // (matchName == null))
                //{
                //    matchName.Number = contact.Number;
                //    matchName.Name = contact.Name;
                //    matchName.Address = contact.Address;
                //    db.SaveChanges();
                //    return Ok();
                //}
            }
        }

        // /api/Phonebook/DeleteEntry - DELETE
        /*  {
	            "ID": 1,
	            "Name": "Trevor",
	            "Number": "1111",
	            "Address": "1 Main Street"
            }         
         */

        // Added - 3. To Delete an entry into the Database ** Post Request **
        /// <summary>
        /// Deletes a Phonebook Entry based on details you enter in the body. Matches on Phone Number. 
        /// </summary>
        [Route("DeleteEntry")] // http://localhost:51275/api/Phonebook/DeleteEntry?Number=1111 ** will Delete Ronan
        public IHttpActionResult DeleteEntry([FromBody]Phonebook contact)
        {
            using (PhonebookContext db = new PhonebookContext())
            {
                var matchNumber = db.Phonebooks.FirstOrDefault(p => p.Number.ToUpper() == contact.Number.ToUpper());
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
