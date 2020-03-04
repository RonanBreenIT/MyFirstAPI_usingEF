using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace MyFirstAPI.Models
{
    /// <summary>
    /// The Phonebook DB
    /// </summary>
    public class PhonebookContext : DbContext
    {
        //public PhonebookContext() : base("DefaultConnection")
        //{
        //    //always drop and re-create the schema
        //    Database.SetInitializer<PhonebookContext>(new DropCreateDatabaseAlways<PhonebookContext>());
        //}

        /// <summary>
        /// The Phonebook Table
        /// </summary>
        public DbSet<Phonebook> Phonebooks { get; set; }
    }
}