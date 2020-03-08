using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PFR
{
    class Appartement
    {
        private long host_id;
        private int room_id;
        private string room_type;
        private int arrondissement;
        private string neighborhood;
        private int reviews;
        private int accommodates;
        private int nbChambres;
        private int price;
        private int minStay;
        private double evaluation;
        private string week;
        private string availability;
        private bool disponible;

        //Appartements construits par Désérialization
        public Appartement()
        {

        }

        #region Accesseurs

        [JsonProperty("week")]
        public string Week { get => week; set => week = value; }

        [JsonProperty("﻿host_id")]
        public long HostId { get => host_id; set => host_id = value; }

        [JsonProperty("room_id")]
        public int RoomId { get => room_id; set => room_id = value; }

        [JsonProperty("borough")]
        public int Arrondissement { get => arrondissement; set => arrondissement = value; }

        [JsonProperty("room_type")]
        public string Room_type { get => room_type; set => room_type = value; }

        [JsonProperty("bedrooms")]
        public int NbChambres { get => nbChambres; set => nbChambres = value; }

        [JsonProperty("overall_satisfaction")]
        public double Evaluation { get => evaluation; set => evaluation = value; }

        [JsonProperty("availability")]
        public string Availability
        {
            get => availability;
            set
            {
                availability = value;
                if(value.Equals("yes"))
                {
                    disponible = true;
                } else
                {
                    disponible = false;
                }
            }
        }

        [JsonProperty("accommodates")]
        public int Accommodates { get => accommodates; set => accommodates = value; }

        [JsonProperty("price")]
        public int Price { get => price; set => price = value; }

        [JsonProperty("neighborhood")]
        public string Neighborhood { get => neighborhood; set => neighborhood = value; }

        [JsonProperty("reviews")]
        public int Reviews { get => reviews; set => reviews = value; }

        [JsonProperty("minstay")]
        public int MinStay { get => minStay; set => minStay = value; }
        public bool Disponible { get => disponible; set => disponible = value; }

        #endregion

        public override string ToString()
        {
            string plural = (nbChambres > 1) ? "s" : "";
            string type_chambre = "Appartement";
            if(room_type.Equals("Chambre partagée"))
            {

            } else if(room_type.Equals("Private room"))
            {
                type_chambre = "Chambre privée";
            }
            string note = (reviews > 0) ? "noté " + evaluation.ToString() + " sur 5" : "non noté";
            return type_chambre + " pour " + accommodates + " personnes dans le " + arrondissement + "ème arrondissement, près de " + neighborhood + ", possèdant " + nbChambres + " chambre" + plural + ", " + note
                + ". Son prix est de " + price + " EUR, sa durée de séjour minimum est de " + minStay + " jour(s). " + "Votre hôte est le n°" + HostId + " et le logement le n°" + RoomId;
        }
    }
}
