using System;
using System.Collections.Generic;
using System.Text;

namespace UICarte.Models
{
    class Punteggio
    {
        private int _fiche;
        private int _multiplier;
        private bool _testovisibile = false;

        public Punteggio(int fiche, int multiplier)
        {
            _fiche = fiche;
            _multiplier = multiplier;
        }

        public bool IstestoVisibile()
        {
            return _testovisibile;
        }

        public bool IstestoVisibile(bool visibilità)
        {
            _testovisibile = visibilità;
            return _testovisibile;
        }

        public int Ottienifiche()
        {
            return _fiche;
        }

        public int OttieniMultiplier()
        {
            return _multiplier;
        }

        public int CalcoloPunteggio()
        {
            int punteggio = _fiche * _multiplier;
            return punteggio;
        }
    }
}
