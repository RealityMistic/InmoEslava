using InmoEslava.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InmoEslava.ViewModel
{
    public class PropertyViewModel
    {
        public Property Property { get; set; }
        public int[] Rooms { get; set; }
        public int[] Location { get; set; }
        public int[] Toilet { get; set; }
        public int[] NearTo { get; set; }
        public int[] Characterstic { get; set; }
        public int[] Material { get; set; }
        public string[] Images { get; set; }
    }
}