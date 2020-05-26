﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EParking.Models
{
    public abstract class Korisnik
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }
        [Required]
        public string ImePrezime { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string JMBG { get; set; }
        [Required]
        public string Adresa { get; set; }
        [Required]
        public string BrojMobitela { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
