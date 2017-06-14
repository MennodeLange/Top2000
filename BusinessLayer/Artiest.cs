using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Artiest
    {
        private string bio;

        public string Bio
        {
            get { return bio; }
            set { bio = value; }
        }
        private string naam;

        public string Naam
        {
            get { return naam; }
            set { naam = value; }
        }

        private string geboorteDatum;

        public string GeboorteDatum
        {
            get { return geboorteDatum; }
            set { geboorteDatum = value; }
        }

        private string foto;

        public string Foto
        {
            get { return foto; }
            set { foto = value; }
        }
        public Artiest()
        {

        }

        public Artiest(string _naam, string _geboorteDatum, string _foto, string _bio)
        {
            this.naam = _naam;
            this.geboorteDatum = _geboorteDatum;
            this.foto = _foto;
            this.bio = _bio;

        }
    }
}
