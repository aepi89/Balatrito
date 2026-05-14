using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace UICarte.Models
{
    public class Mano
    {
        private List<Carta> _mano = new List<Carta>();
        private int _cartePresenti;

        public Mano() // Costruttore
        {
            _mano = new List<Carta>(8); // Le mani sono sempre da 8 carte
            _cartePresenti = 0;
        }

        public bool AggiungiCarta(Carta carta)
        {
            if (_cartePresenti >= 8) //Se la mano è piena
            {
                return false;
            }
            else
            {
                _mano.Add(carta);
                _cartePresenti++;
                OrdinaMano();
                return true;
            }
        }

        public void OrdinaMano()
        {
            Carta temp = null!;

            for (int i = 0; i < _cartePresenti - 1; i++)
            {
                for (int j = 0; j < _cartePresenti - 1 -i; j++)//( j < _cartePresenti - 1 -i) nel bubble sort gli ultimi elementi sono già sortati dopo un passggio
                {
                    if (!_mano[j].VienePrimaDi(_mano[j + 1]))
                    {
                        temp = _mano[j + 1];
                        _mano[j + 1] = _mano[j];
                        _mano[j] = temp;
                    }
                }
            }
        }

        public Carta OttieniCarta(int indice) // ritorna una carta ad un indice specificato
        {
            if (indice < 0 || _mano.Count == 0) return null!;

            return _mano[indice];
        }

        public List<string> MostraMano()
        {
            List<string> manoScoperta = new List<string>(8);

            if (_cartePresenti == 0) return new List<string>();

            for (int i = 0; i < _cartePresenti; i++)
            {
                manoScoperta[i] = _mano[i].OttieniDescrizione();
            }
            return manoScoperta;
        }

        public bool Contiene(Carta carta) // Controlla che una carta data sia nel mazzo
        {
            for (int i = 0; i < _cartePresenti; i++)
            {
                if (_mano[i].UgualeA(carta))
                {
                    return true;
                }
            }
            return false;
        }
        public bool Coppia() // Vede se è presenta una coppia
        {
            for (int i = 0; i < _cartePresenti - 1; i++)
            {
                if (_mano[i].OttieniValore() == _mano[i + 1].OttieniValore())
                {
                    return true;
                }
            }
            return false;
        }

        public List<string> Mescola()
        {
            List<string> ordinate = MostraMano();
            int n = _cartePresenti;

            for (int i = n - 1; i > 0; i--)
            {
                int casuale = Random.Shared.Next(i + 1);

                string temp = ordinate[i];
                ordinate[i] = ordinate[casuale];
                ordinate[casuale] = temp;
            }
            return ordinate;
        }
        public bool Scarta(Carta carta)
        {
            if(_cartePresenti == 0)
            {
                return false;
            }

            for (int i = 0; i < _cartePresenti; i++)
            {
                if (_mano[i].UgualeA(carta))
                {
                    _mano.RemoveAt(i);
                    _cartePresenti--;
                }
            }
            return true;
        }

        public bool Scala()
        {
            int contatore = 1; // Conta che la scala raggiunga il minimo di 5 carte
            for (int i = 0; i < _cartePresenti - 1; i++)
            {
                if ((int)_mano[i].OttieniValore() == (int)_mano[i + 1].OttieniValore() - 1)
                {
                    contatore++;

                    if (contatore == 5)
                    {
                        break;
                    }
                }
                else {contatore = 1;} // Se le condizioni per una scala non avvengono 5 volte 
            }

            if (contatore < 5)
            {
                return false;
            }
            return true;
        }

        public bool ScalaReale()
        {
            int contatore = 1;

            if (Scala())// Prima di avere una scala reale serve una scala
            {
                for (int i = 0; i < _cartePresenti - 1; i++)
                {
                    if (_mano[i].OttieniSeme() == _mano[i + 1].OttieniSeme() && 
                        (int)_mano[i].OttieniValore() == (int)_mano[i + 1].OttieniValore() - 1)
                    {
                        contatore++;

                        if (contatore == 5)
                        {
                            break;
                        }
                    }
                    else { contatore = 1; } // Se le condizioni per una scala reale non avvengono 5 volte 
                }

                if (contatore < 5)
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        public bool Tris() // Vede se è presenta una coppia
        {
            int contatore = 1;
            if (Coppia())
            {
                for (int i = 0; i < _cartePresenti - 1; i++)
                {
                    if (_mano[i].OttieniValore() == _mano[i + 1].OttieniValore())
                    {
                        contatore++;

                        if (contatore == 3)
                        {
                            break;
                        }
                    }
                    else
                    {
                        contatore = 1;
                    }
                }
                if (contatore < 3)
                {
                    return false;
                }
                return true;
            }
            return false;
        }
        public bool DoppiaCoppia() // Vede se è presenta una coppia
        {
            bool PrimaCoppiaTrovata = false;
            Valore ValorePrimaCoppia = Valore.Donna;

            if (Coppia())
            {
                for (int i = 0; i < _cartePresenti - 1; i++)
                {
                    if (_mano[i].OttieniValore() == _mano[i + 1].OttieniValore())
                    {
                        if (!PrimaCoppiaTrovata) // Ci siamo imbattutti nella prima coppia
                        {
                            PrimaCoppiaTrovata = true;
                            ValorePrimaCoppia = _mano[i].OttieniValore();

                            i++; // Saltiamo la seconda carta della prima coppia per evitare controlli inutili
                        }
                        else if (PrimaCoppiaTrovata && ValorePrimaCoppia != _mano[i].OttieniValore())
                        {
                            return true; // SEconda coppi differente
                        }
                    }
                }
                return false;
            }
            return false;
        }
        public bool Colore()
        {
            int contaCuori = 0; // Facciamo un contatore per ogni seme
            int contaQuadri = 0;
            int contaPicche = 0;
            int contaFiori = 0;

            for (int i = 0; i < _cartePresenti; i++)
            {
                switch (_mano[i].OttieniSeme())
                {
                    case Seme.Cuori: 
                        contaCuori++;
                        break;
                    case Seme.Quadri: 
                        contaQuadri++;
                        break;
                    case Seme.Fiori:  
                        contaFiori++;
                        break;
                    case Seme.Picche: 
                        contaPicche++;
                        break;
                }
            }
            if (contaCuori >= 5 || contaQuadri >= 5 || contaPicche >= 5 || contaFiori >= 5)
            {
                return true;
            }
            return false;
        }

        public bool FullHouse() // Un tris + coppia diversa
        {
            int contatore = 1;
            Valore PrimoTris = Valore.Asso; // Questa ci serve per controllare il valore del tris

            for (int i = 0; i < _cartePresenti - 1; i++)
            {
                if (_mano[i].OttieniValore() == _mano[i + 1].OttieniValore())
                {
                    contatore++;

                    if (contatore == 3)
                    {
                        PrimoTris = _mano[i].OttieniValore();
                        break;
                    }
                }
                else
                {
                    contatore = 1;
                }
            }
            if (contatore < 3)
            {
                return false;
            }

            for (int i = 0; i < _cartePresenti - 1; i++)
            {
                if (_mano[i].OttieniValore() == _mano[i + 1].OttieniValore()
                    && _mano[i].OttieniValore() != PrimoTris)
                {
                    return true;
                }
            }
            return false;
        }
    }   
}
