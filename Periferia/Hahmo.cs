﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Periferia
{
    abstract public class Hahmo
    {
        public int Voima { get; set; }
        public int HP { get; set; }
        public int Sarake { get; set; }
        public int Rivi { get; set; }
        public char Merkki { get; set; }
        public ConsoleColor Väri { get; set; }
        public string Nimi { get; set; }
        public bool OnkoYstävä { get; set; }
        public bool OnkoTekoäly { get; set; }

        public bool LiikuOikealle()
        {
            Karttaruutu ur = Moottori.NykyinenKartta.Ruudut[this.Rivi, this.Sarake+1];
            if (!liiku(ur))
                return false;
            Sarake++;

            return true;
        }

        public bool LiikuVasemmalle()
        {
            Karttaruutu ur = Moottori.NykyinenKartta.Ruudut[this.Rivi, this.Sarake - 1];
            if (!liiku(ur))
                return false;
            Sarake--;

            return true;
        }

        public bool LiikuAlas()
        {
            Karttaruutu ur = Moottori.NykyinenKartta.Ruudut[this.Rivi+1, this.Sarake];
            if (!liiku(ur))
                return false;
            Rivi++;

            return true;
        }

        public bool LiikuYlös()
        {
            Karttaruutu ur = Moottori.NykyinenKartta.Ruudut[this.Rivi - 1, this.Sarake];
            if (!liiku(ur))
                return false;
            Rivi--;

            return true;
        }



        private bool liiku(Karttaruutu ur)
        {
            Karttaruutu vr = Moottori.NykyinenKartta.Ruudut[this.Rivi, this.Sarake];
            if (!ur.Käveltävä)
                return false;
            ur.Entiteetti = vr.Entiteetti;
            vr.Entiteetti = null;

            return true;
        }
    }
}