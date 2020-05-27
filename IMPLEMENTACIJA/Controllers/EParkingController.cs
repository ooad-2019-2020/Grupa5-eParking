﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EParking.Models;
using Microsoft.AspNetCore.Mvc;

namespace EParking.Controllers
{
    public class EParkingController : Controller
    {
        private readonly EParkingContext _context;
        public EParkingController(EParkingContext context)
        {
            _context = context;
        }
        public IActionResult Map()
        {
            EParkingFacade.Instance.Parkinzi = _context.ParkingLokacija.ToList();
            List<Cjenovnik> Cjenovnici = _context.Cjenovnik.ToList();
            foreach (var p in EParkingFacade.Instance.Parkinzi)
            {
                foreach (var c in Cjenovnici)
                {
                    if (p.CjenovnikId == c.ID)
                    {
                        p.Cjenovnik = c;
                    }
                }
            }
            string markeri = "[";
            int vel = 0;
            foreach (ParkingLokacija parking in EParkingFacade.Instance.Parkinzi)
            {
                markeri += "{";
                double lat = parking.Lat;
                double lng = parking.Long;
                markeri += String.Format("'lat': '{0}',", lat.ToString(System.Globalization.CultureInfo.InvariantCulture));
                markeri += String.Format("'lng': '{0}',", lng.ToString(System.Globalization.CultureInfo.InvariantCulture));
                //cijena po satu nije kako treba spasena u parking lokaciji u bazi pa zato ovo
                //ISPRAVI TO KASNIJE!!!
                //double cijenaSat = 1.5;
                markeri += string.Format("'naziv': '{0}',", parking.Naziv);
                markeri += string.Format("'adresa': '{0}',", parking.Adresa);
                markeri += string.Format("'cijena': '{0}',", parking.Cjenovnik.DnevnaCijenaSat);
                markeri += string.Format("'slobodnaMjesta': '{0}',", parking.BrojSlobodnihMjesta);
                markeri += string.Format("'kapacitet': '{0}'", parking.Kapacitet);
                //markeri += "},";
                if (vel < EParkingFacade.Instance.Parkinzi.Count - 1)
                {
                    markeri += "},";
                }
                else
                {
                    markeri += "}";
                }
            }
            markeri += "];";
            ViewBag.Markeri = markeri;
            return View(EParkingFacade.Instance);
        }
    }
}