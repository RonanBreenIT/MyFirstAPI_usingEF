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
        [MaxLength(20)]
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

    }
}