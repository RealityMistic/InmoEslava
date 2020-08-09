using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InmoEslava.Models
{
    public class Property
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagesPath { get; set; }
        public long? Price { get; set; }
        public long? Area { get; set; }
        public string Region { get; set; }
        public string Province { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public bool IsUsersFavourite { get; set; }
        public string Municipality { get; set; }
    }

    public class PropertyData
    { 
        public Property Property { get; set; }
        public string RoomsData { get; set; }
        public string ToiletData { get; set; }
        public string CharactersticData { get; set; }
        public string MaterialData { get; set; }
        public string LocationData { get; set; }
        public string NearToData { get; set; }
    }

    public class PropertyDetailData
    {
        public Property Property { get; set; }
        public int[] RoomsData { get; set; }
        public int[] ToiletData { get; set; }
        public int[] CharactersticData { get; set; }
        public int[] MaterialData { get; set; }
        public int[] LocationData { get; set; }
        public int[] NearToData { get; set; }
    }
}