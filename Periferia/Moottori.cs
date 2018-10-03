﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Periferia
{
    static class Moottori
    {
        static public List<Kartta> Kartat;
        static public Kartta NykyinenKartta;
        static public List<VihollisMalli> VihollisMallit = new List<VihollisMalli>();
        static public Pelaaja Pelaaja = new Pelaaja()
        {
            Väri = ConsoleColor.Cyan,
            Merkki = '@',
            HP = 49,
            Taso = 4,
            Nesteytys = 100,
            Nimi = "Pekka",
            Voima = 50,
            Reppu = new ObservableCollection<Tavara>(),
            Sarake = 2,
            Rivi = 2
        };


        static public void Peli()
        {
            VihollisMallit.Add(new VihollisMalli("Karhu", 'K') {Voima=3, Nopeus=2, HP=60, Hyökkäys="raapaisee"});
            VihollisMallit.Add(new VihollisMalli("Susi", 'S') {Voima=2, Nopeus=3, HP=30, Hyökkäys="puraisee"});
            VihollisMallit.Add(new VihollisMalli("Goblin", 'G', ConsoleColor.DarkGreen) {Voima=1, Nopeus=1, HP=15, Hyökkäys="lyö"});
            VihollisMallit.Add(new VihollisMalli("Arska", 'A', ConsoleColor.DarkYellow) {Voima=1, Nopeus=1, HP=15, Hyökkäys="lyö"});


            Konsoli k = new Konsoli();
            Konsoli.AlustaKonsoli();
            Moottori.NykyinenKartta = Kartta.LuoKartta();
            //Pelaaja.Reppu.Add(new Tavara("kirves"));
            //Pelaaja.Reppu.Add(new Tavara("leka"));


            k.PiirräKartta(Moottori.NykyinenKartta);

            Konsoli.Viestiloki.Lisää("Peli alkaa!");

            bool pelijatkuu = true;
            while (pelijatkuu)
            {
                k.PiirräHahmoRuutu();
                //Konsoli.TyhjennäKonsoli();
                k.PiirräLoki();

                //Konsoli.PiirräReunat();


                pelaajanVuoro();
                Konsoli.Viestiloki.Lisää("Vihollisen vuoro, paina space");

                vihollistenVuoro();

            }
        }

        static void pelaajanVuoro()
        {
            ConsoleKeyInfo näppäin = Console.ReadKey();
            switch (näppäin.Key)
            {
                case ConsoleKey.RightArrow:
                    Moottori.Pelaaja.LiikuOikealle();
                    Pelaaja.Nesteytys--;
                    break;
                case ConsoleKey.LeftArrow:
                    Moottori.Pelaaja.LiikuVasemmalle();
                    Pelaaja.Nesteytys--;
                    break;
                case ConsoleKey.UpArrow:
                    Moottori.Pelaaja.LiikuYlös();
                    Pelaaja.Nesteytys--;
                    break;
                case ConsoleKey.DownArrow:
                    Moottori.Pelaaja.LiikuAlas();
                    Pelaaja.Nesteytys--;
                    break;

            }
        }

        static void vihollistenVuoro()
        {
            foreach (Vihollinen v in NykyinenKartta.Entiteetit.Where(v => v is Vihollinen))
            {
                if (!v.OnkoTekoäly)
                    continue; // älytön tyyppi, mennään seuraavaan

                v.Tekoäly();
            }

        }
    }
}
