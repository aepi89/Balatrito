using System;
using System.Collections.Generic;
using System.Text;

namespace UICarte.Models
{
    internal class Mazzo
    {
        private List<Carta> _mazzo = new List<Carta>(52);

        public Mazzo()
        {
            CompilaMazzo();
        }

        public void CompilaMazzo()
        {
            _mazzo.Clear(); // Protezione nel caso cercassimo di compilare un mazzo non vuoto

            // Creiamo due list con valori e semi
            List<Seme> semi = Enum.GetValues(typeof(Seme)).Cast<Seme>().ToList();
            List<Valore> valori = Enum.GetValues(typeof(Valore)).Cast<Valore>().ToList();

            foreach (Seme semePresente in semi)// Scorriamo ogni valore per ogni seme
            {
                foreach (Valore valorePresente in valori)
                {
                    _mazzo.Add(new Carta(valorePresente, semePresente)); // Compilazione mazzo
                }
            }
            Mescola(); // Mescoliamo il mazzo perchè le liste di valori e semi sono sempre uguali
        }
        public int CarteRimanenti()
        {
            return _mazzo.Count;
        }
        public void Mescola()
        {

            for (int i = _mazzo.Count - 1; i > 0; i--)
            {
                int casuale = Random.Shared.Next(i + 1);

                Carta temp = _mazzo[i];
                _mazzo[i] = _mazzo[casuale];
                _mazzo[casuale] = temp;
            }
        }

        public Carta Distribuisci()
        {
            if (_mazzo.Count == 0)// Se il mazzo si svuota
            {
                CompilaMazzo();
            }

            Carta carta = _mazzo[0];
            _mazzo.RemoveAt(0);

            return carta;
        }

    }
}
