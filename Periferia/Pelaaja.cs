﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Periferia
{
    public class Pelaaja : Hahmo, IPiirrettävä
    {
        public event EventHandler NesteMuuttunut;

        private int _nesteytys;
        public int TappoPisteet { get; set; }
        public int Pisteet { get => (TappoPisteet + Moottori.Kartat.Count * 100 + Taso * 100 + HP * 10); }
        public int Nesteytys {
            get {
                return _nesteytys;
            }
            set
            {
                this._nesteytys = value;
                if(Nesteytys <= 0)
                {
                    _nesteytys = 0;
                    //Kuole();
                }
                else if (Nesteytys > 100)
                {
                    _nesteytys = 100;
                }
                
                NesteMuuttunut?.Invoke(this, EventArgs.Empty);
            }
        }

        private int _kokemus;
        public int levutusRaja { get => (int)Math.Floor((decimal)Taso * 1.5m * 10.0m); }

        public int Kokemus {
            get
            {
                return _kokemus;
            }
            set {
                _kokemus = _kokemus + value;
                if(_kokemus > levutusRaja)
                {
                    Taso++;
                    Konsoli.Viestiloki.Lisää($"{this.Nimi} sai LVL:n!", ConsoleColor.DarkYellow);
                    _kokemus = _kokemus - levutusRaja;
                    Voima++;
                    Nopeus++;
                    Onnekkuus++;
                }
            }
        }

        public int MontakoTapettu { get; set; } = 0;

        public ObservableCollection<Tavara> Reppu { get; set ; }

        public override void Kuole()
        {
            // Poistetaan karttaruudulla oleva pelaaja
            Moottori.NykyinenKartta.Ruudut[this.Rivi, this.Sarake].Entiteetti = null;
            Moottori.NykyinenKartta.Ruudut[this.Rivi, this.Sarake].Päivitä();
            // Game over-ruutuun
            Moottori.Pelijatkuu = false;
            
        }
    }
}