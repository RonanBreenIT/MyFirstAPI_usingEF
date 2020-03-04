using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyFirstAPI.Models
{
    /// <summary>
    /// Holds individual details for Phonebook Entries. 
    /// </summary>
    public class Phonebook
    {
        /// <summary>
        /// The Primary Key ID for Phonebook Entry
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// The Name for Phonebook Entry. Cannot be null. 
        /// </summary>
        [Required]
        public String Name { get; set; }

        /// <summary>
        /// The Number for Phonebook Entry. Nullable. 1 - 20 charachters.
        /// </summary>
        [Required]
        [MinLength(1)]
        [MaxLength(20)]
        public String Number { get; set; }

        /// <summary>
        /// The Number for Phonebook Entry. Nullable. 
        /// </summary>
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