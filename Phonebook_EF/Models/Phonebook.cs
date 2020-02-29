using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyFirstAPI.Models
{
    public class Phonebook
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public String Name { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(140)]
        public String Number { get; set; }

        
        public String Address { get; set; }

        public Phonebook()
        {

        }

        public Phonebook(string _name, string _number, string _address = null)
        {
            this.Name = _name;
            this.Number = _number;
            this.Address = _address;
        }

        //public void AddPhoneBookEntry (Phonebook contact)
        //{
        //    using(PhonebookContext db = new PhonebookContext())
        //    {
        //        var match = db.Phonebooks.FirstOrDefault(p => p.Name.ToUpper() == contact.Name.ToUpper());
        //        if (match != null)
        //        {
        //            throw new ArgumentException("Contact Already Exists");
        //        }
        //        else
        //        {
        //            db.Phonebooks.Add(contact);
        //            db.SaveChanges();
        //        }

        //    }
        //}

    }
}