using System;
using System.IO;
using System.Threading;

namespace Verolaskuri
{
    class Verolaskuri
    {
        private decimal tulot;
        private decimal veroprosentti;

        public Verolaskuri(decimal tulot, decimal veroprosentti)
        {
            this.tulot = tulot;
            this.veroprosentti = veroprosentti;
        }

        public decimal LaskeVerot()
        {
            decimal verot = tulot * (veroprosentti / 100);
            return verot;
        }
    }

    class Program
    {
        // Tallennusfunktio
        private static void TallennaLaskutiedot(decimal tulot, decimal veroprosentti, decimal verot)
        {
            Console.WriteLine("Haluatko tallentaa laskutiedot tiedostoon (kyllä/ei)?");
            string tallennaVastaus = Console.ReadLine();

            if (tallennaVastaus.ToLower() == "kyllä")
            {
                Console.WriteLine("Syötä tiedoston nimi: ");
                string tiedostoNimi = Console.ReadLine();

                using (StreamWriter tiedosto = new StreamWriter(tiedostoNimi))
                {
                    tiedosto.WriteLine("Tulot: " + tulot);
                    tiedosto.WriteLine("Veroprosentti: " + veroprosentti);
                    tiedosto.WriteLine("Verot: " + verot);
                    tiedosto.WriteLine("----------------------------------");
                }

                Console.WriteLine("Laskutiedot tallennettu tiedostoon " + tiedostoNimi);
            }
            else
            {
                Console.WriteLine("Laskutietoja ei tallennettu.");
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Tervetuloa verolaskuriin!");

            while (true)
            {
                Console.Write("Syötä vuositulosi: ");
                decimal tulot;
                while (!decimal.TryParse(Console.ReadLine(), out tulot))
                {
                    Console.WriteLine("Virheellinen syöte. Syötä kelvollinen numero.");
                    Console.Write("Syötä vuositulosi: ");
                }

                if (tulot <= 0)
                {
                    Console.WriteLine("Ikävää, että teitte tappiota.");
                }
                else
                {
                    Console.Write("Syötä veroprosentti: ");
                    decimal veroprosentti;
                    while (!decimal.TryParse(Console.ReadLine(), out veroprosentti))
                    {
                        Console.WriteLine("Virheellinen syöte. Syötä kelvollinen numero.");
                        Console.Write("Syötä veroprosentti: ");
                    }

                    Verolaskuri laskuri = new Verolaskuri(tulot, veroprosentti);
                    decimal verot = laskuri.LaskeVerot();

                    Console.WriteLine("Verosi ovat: " + verot);
                    Console.WriteLine("Verojen jälkeen sinulle jää " + (tulot - verot) + " euroa");

                    TallennaLaskutiedot(tulot, veroprosentti, verot);
                }

                string vastaus = "";
                while (vastaus.ToLower() != "kyllä" && vastaus.ToLower() != "ei")
                {
                    Console.WriteLine("Haluatko sammuttaa laskurin (kyllä/ei)?");
                    vastaus = Console.ReadLine();

                    if (vastaus.ToLower() != "kyllä" && vastaus.ToLower() != "ei")
                    {
                        Console.WriteLine("Virheellinen vastaus. Vastaa kyllä tai ei.");
                    }
                }

                if (vastaus.ToLower() == "kyllä")
                {
                    DateTime sulkemisaika = DateTime.Now;
                    Console.WriteLine("Suljetaan laskuri. Aika: " + sulkemisaika);

                    // Tallenna sulkemisaika tai tee muuta haluamaasi toiminnallisuutta tässä

                    break;
                }
            }

            int countdown = 10;

            while (countdown > 0)
            {
                Console.WriteLine("Laskuri sammuu automaattisesti " + countdown + " sekunnin kuluttua.");
                Thread.Sleep(1000); // Odota yksi sekunti
                countdown--;
            }

            Console.WriteLine("Laskuri sammuu.");

            Console.ReadLine();
        }
    }
}