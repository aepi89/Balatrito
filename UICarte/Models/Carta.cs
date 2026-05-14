using System;
using System.Collections.Generic;
using System.Text;

namespace UICarte.Models
{
    // ======== METODI per carta singola (logica) ========

    public enum Seme
    {
        Cuori,   // 0
        Quadri,  // 1
        Fiori,   // 2
        Picche   // 3
    }

    public enum Valore
    {
        Asso,
        Due,
        Tre,
        Quattro,
        Cinque,
        Sei,
        Sette,
        Otto,
        Nove,
        Dieci,
        Jack,
        Donna,
        Re
    }

    public class Carta
    {
        private Seme _seme;
        private Valore _valore;
        public Carta(Valore valore, Seme seme)
        {
            _valore = valore;
            _seme = seme;
        }

        public Seme OttieniSeme()
        {
            return _seme;
        }

        public Valore OttieniValore()
        {
            return _valore;
        }

        public bool UgualeA(Carta altra)
        {
            if (altra == null) return false;

            return _valore == altra._valore && _seme == altra._seme;
        }
        public bool ValoreDiversoDa(Carta altra)
        {
            if (altra == null) return false;

            return _valore != altra._valore;
        }

        public bool VienePrimaDi(Carta altra)
        {
            if (altra == null) return false;

            // Ordiniamo prima per Valore
            if (_valore != altra._valore)
            {
                return _valore < altra._valore;
            }

            // A parità di Valore, ordiniamo per Seme
            return _seme < altra._seme;
        }

        public string OttieniDescrizione()
        {
            return $"{_valore} di {_seme}";
        }
    }

}
