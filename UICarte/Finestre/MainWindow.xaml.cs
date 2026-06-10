using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;
using UICarte.Models; // La cartela delle classi

namespace UICarte;


public partial class MainWindow : Window // Qua iniziano i metodi per l'UI
{

    private MediaPlayer mediaPlayer = new MediaPlayer();
    private Mano mano = new Mano();
    private Mazzo mazzo = new Mazzo();

    private int _punteggioTotale = 0; // Il punteggio del giocatore è cumulativo
    private int _turno = 1;
    private int _punteggioDaBattere = 100; // Piccolo obbiettivo iniziale iniziale
    public MainWindow()
    {
        InitializeComponent();
        InizializzaAudio();
        RiempiMano();
        StampaMano();
    }

    /* ======== METODI per soundtrack ========
     * (Music track: Cheese by Lukrembo                                   
     * No Copyright Vlog Music for Video) 
     * Source: https://freetouse.com/music */

    private void InizializzaAudio()
    {
        // Apri il file usando un percorso relativo (UriKind)
        mediaPlayer.Open(new Uri("Lukrembo-Cheese (freetouse.com).mp3", UriKind.Relative));
        mediaPlayer.Play();
    }

    private void Megafono_Click(object sender, RoutedEventArgs e)
    {
        if(megafono.Opacity != 1)
        {
            megafono.Opacity = 1;
            mediaPlayer.Play();
        }
        else
        {
            megafono.Opacity = 0.6;
            mediaPlayer.Pause();
        }
    }

    private void Regola_Volume(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        mediaPlayer.Volume = e.NewValue;
    }


    // ======== METODI PER EVENTI BOTTONI (Hover) ========

    private void Btn_Hover_Enter_Red(object sender, MouseEventArgs e)//Scurisco il colore all'Hover
    {
        if (sender is Border border)
        {
            border.Tag = border.Background; //Salviamo il colore originale

            border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#b81414"));
        }
    }
    private void Btn_Hover_Enter_Green(object sender, MouseEventArgs e)//Scurisco il colore all'Hover
    {
        if (sender is Border border)
        {
            border.Tag = border.Background; //Salviamo il colore originale

            border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0b6730"));
        }
    }

    private void Btn_Hover_Enter_Blue(object sender, MouseEventArgs e)//Scurisco il colore all'Hover
    {
        if (sender is Border border)
        {
            border.Tag = border.Background; //Salviamo il colore originale

            border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#5353ec"));
        }
    }

    private void Btn_Hover_Enter_Yellow(object sender, MouseEventArgs e)//Scurisco il colore all'Hover
    {
        if (sender is Border border)
        {
            border.Tag = border.Background; //Salviamo il colore originale

            border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#b87e14"));
        }
    }

    private void Btn_Hover_Leave(object sender, MouseEventArgs e)
    {
        if (sender is Border border)
        {
            if (border.Tag is not null && border.Tag is Brush)
            {
                Brush coloreOriginale = (Brush)border.Tag;
                border.Background = coloreOriginale;
            }

        }
    }

    // ======== METODI PER EVENTI BOTTONI (Click) ========

    private void Btn_Click_Info(object sender, MouseEventArgs e)
    {
        Info nuovaSchermata = new Info(); // Istanza della nuova schermata

        nuovaSchermata.Show();// Mostriamo la finestra
    }

    private void Btn_Impostazioni(object sender, MouseButtonEventArgs e)
    {
        if(sender is Border border && Schermata_Impostazioni.Visibility == Visibility.Hidden)
        {
            Schermata_Impostazioni.Visibility = Visibility.Visible;
            Box_carte.IsHitTestVisible = false;
            BtnInfo.IsHitTestVisible = false;
        }
        else
        {
            Schermata_Impostazioni.Visibility = Visibility.Hidden;
            Box_carte.IsHitTestVisible = true;
            BtnInfo.IsHitTestVisible = true ;
        }
    }

    // ======== METODI Generali ========
    private void Hover_Opacity_Enter(object sender, MouseEventArgs e)
    {
        if (sender is FrameworkElement elemento) //FrameworkElement è la classe base degli elementi visuali
        {
            elemento.Opacity = 0.7;
        }
    }
    private void Hover_Opacity_Leave(object sender, MouseEventArgs e)
    {
        if (sender is FrameworkElement elemento)
        {
            // In WPF l'opacità è un double non un float l'opacità finale è sempre 1
            elemento.Opacity = 1;
        }
    }

    // ======== METODI PER Carta singola (UI) ========
    public string SimboloSeme(Seme seme) // Converto il seme nel suo rispettivo simbolo 
    {
        switch (seme)
        {
            case Seme.Cuori: return "♥";
            case Seme.Quadri: return "♦";
            case Seme.Fiori: return "♣";
            case Seme.Picche: return "♠";
            default: return "?";
        }
    }

    private void CambiaMazzo(object sender, MouseButtonEventArgs e)
    {
        if (MazzoDiCarte.Source.ToString().Contains("MazzoCinese.png"))
        {
            MazzoDiCarte.Source = new BitmapImage(new Uri("/Foto/Mazzo.png", UriKind.Relative));
            Cambia_mazzo.Source = new BitmapImage(new Uri("/Foto/Mazzo.png", UriKind.Relative));
        }
        else
        {
            MazzoDiCarte.Source = new BitmapImage(new Uri("/Foto/MazzoCinese.png", UriKind.Relative));
            Cambia_mazzo.Source = new BitmapImage(new Uri("/Foto/MazzoCinese.png", UriKind.Relative));
        }

    }
    public string SimboloValore(Valore valore) // Converto il valore nel suo rispettivo simbolo 
    {
        switch (valore)
        {
            case Valore.Asso: return "1";
            case Valore.Due: return "2";
            case Valore.Tre: return "3";
            case Valore.Quattro: return "4";
            case Valore.Cinque: return "5";
            case Valore.Sei: return "6";
            case Valore.Sette: return "7";
            case Valore.Otto: return "8";
            case Valore.Nove: return "9";
            case Valore.Dieci: return "10";
            case Valore.Jack: return "J";
            case Valore.Donna: return "Q";
            case Valore.Re: return "K";
            default: return "?";
        }
    }
    public Brush ColoreCarta(Carta carta) // Converto il seme nel suo rispettivo simbolo 
    {
        if (carta.OttieniSeme() == Seme.Cuori || carta.OttieniSeme() == Seme.Quadri)
        {
            return Brushes.Red;
        }
        else
        {
            return Brushes.Black;
        }
    }
    public Seme RandomSeme()
    {
        Random rnd_Seme = new Random(); // prende un seme rando dall'enum (0 non si scrive il max si mette n +1)

        return (Seme)rnd_Seme.Next(4);
    }
    public Valore RandomValore()
    {
        Random rnd_Valore = new Random(); // prende un Valore rando dall'enum

        return (Valore)rnd_Valore.Next(13);
    }

    public void ScrittaCombinazione(Mano manoDaGiocare)
    {
        int ficheAssegnate, multAssegnato;

        string risultato;
        switch (true)
        {
            case true when manoDaGiocare.ScalaReale(): 
                risultato = "Scala reale";
                Text_Combinazione.FontSize = 20;
                ficheAssegnate = 100;
                multAssegnato = 8;
                break;

            case true when manoDaGiocare.FullHouse(): 
                risultato = "Full House";
                Text_Combinazione.FontSize = 25;
                ficheAssegnate = 40;
                multAssegnato = 4;
                break;

            case true when manoDaGiocare.Colore(): 
                risultato = "Colore";
                Text_Combinazione.FontSize = 30;
                ficheAssegnate = 35;
                multAssegnato = 4;
                break;

            case true when manoDaGiocare.Scala():
                risultato = "Scala";
                Text_Combinazione.FontSize = 30;
                ficheAssegnate = 30;
                multAssegnato = 4;
                break;

            case true when manoDaGiocare.DoppiaCoppia(): 
                risultato = "Doppia coppia";
                Text_Combinazione.FontSize = 20;
                ficheAssegnate = 30;
                multAssegnato = 3;
                break;

            case true when manoDaGiocare.Tris(): 
                risultato = "Tris";
                Text_Combinazione.FontSize = 30;
                ficheAssegnate = 20;
                multAssegnato = 2;
                break;

            case true when manoDaGiocare.Coppia(): 
                risultato = "Coppia";
                Text_Combinazione.FontSize = 30;
                ficheAssegnate = 10;
                multAssegnato = 2;
                break;

            default:
                risultato = "Carta alta";
                Text_Combinazione.FontSize = 20;
                ficheAssegnate = 5;
                multAssegnato = 1;
                break;
        }
        Text_Combinazione.Text = risultato; // Ad ogni lancio il testo si aggiorna

        Punteggio punteggio = new Punteggio(ficheAssegnate, multAssegnato);// Stampa le fiche e il moltiplicatore della combinazione
        text_Fiche.Text = $"{punteggio.Ottienifiche()}"; // i punteggio sono int
        text_Multiplier.Text = $"{punteggio.OttieniMultiplier()}";

        _punteggioTotale += punteggio.CalcoloPunteggio();
        Text_Punteggio.Text = $"{_punteggioTotale}";
        Text_turno.Text = $"turno: {_turno}";
        Text_Da_Superare.Text = $"{_punteggioDaBattere}";

    }

    public bool Turnosuperato() // Controlla se il punteggio da superare è stato superato
    {
        if(_punteggioTotale >= _punteggioDaBattere)
        {
            _punteggioTotale = 0; // Azzeriamo il punteggio in vista del nuovo turno
            _turno++;

            switch (_turno % 3) // Ciclo dei turni ( 1%3 = 1 2%3 = 2 3%3 = 0)
            {
                case 1: // Obbiettivo Piccolo
                    _punteggioDaBattere = 100;
                    break;
                case 2: // Obbiettivo Grande 
                    _punteggioDaBattere = 150;
                    break;
                case 0: // Obbiettivo Boss
                    _punteggioDaBattere = 200;
                    break;
            }

            return true;
        }
        return false;
    }

    public void StampaCarta(Carta carta, int riga, int colonna)
    {
        Border border = new Border(); // Creaiamo il contenitore e ne descriviamo le proprietà
        border.Tag = carta; // Salvaiamo la carta per altri usi
        border.CornerRadius = new CornerRadius(8);
        border.Background = Brushes.White; // Brushes sono i colori solidi
        border.Margin = new Thickness(10);
        border.Cursor = Cursors.Hand;
        border.ToolTip = carta.OttieniDescrizione();

        StackPanel stackPanel = new StackPanel(); // Lo stackPanel non ha proprietà da definire

        TextBlock textBlockValore = new TextBlock();// testo valore
        textBlockValore.Text = SimboloValore(carta.OttieniValore());
        textBlockValore.FontSize = 30;
        textBlockValore.FontWeight = FontWeights.Bold;
        textBlockValore.Foreground = Brushes.Black;
        textBlockValore.HorizontalAlignment = HorizontalAlignment.Center;

        TextBlock textBlockSeme = new TextBlock();// testo seme
        textBlockSeme.Text = SimboloSeme(carta.OttieniSeme()); // Il valore dei semi viene dopo dato da random
        textBlockSeme.FontSize = 30;
        textBlockSeme.Foreground = ColoreCarta(carta);
        textBlockSeme.HorizontalAlignment = HorizontalAlignment.Center;
        textBlockSeme.VerticalAlignment = VerticalAlignment.Bottom;

        border.Child = stackPanel; // stackPanel dentro il border
        stackPanel.Children.Add(textBlockValore);
        stackPanel.Children.Add(textBlockSeme);

        MazzoDiCarte.ToolTip = $"Carte rimanenti: {mazzo.CarteRimanenti()}";

        // Eventi
        border.MouseEnter += Hover_Opacity_Enter; // += (si possono aggiungere più metodi in un evento) 
        border.MouseLeave += Hover_Opacity_Leave;
        border.MouseDown += Carta_Select;

        Grid.SetColumn(border, colonna);
        Grid.SetRow(border, riga);
        Grid_Carte.Children.Add(border); // aggiunge alla grid che è nominata
    }

    public void RiempiMano()// riempe la mano logicamente
    {
        for (int i = 0; i < 8; i++)
        {
            Carta carta = mazzo.Distribuisci();
            mano.AggiungiCarta(carta);
        }
    }
    public void StampaMano()
    {
        int numCarte = 0;
        for (int x = 0; x <= 1; x++) // Righe
        {
            for (int y = 1; y <= 4; y++) // Colonne
            {
                StampaCarta(mano.OttieniCarta(numCarte), x, y);
                numCarte++;
            }
        }
    }
    private void Carta_Select(object sender, MouseEventArgs e)
    {
        if (sender is Border border)
        {
            if (border.Margin.Top != -5) // Se non è alzata
            {
                border.Margin = new Thickness(5, -5, 5, 5); // "Alziamo" la carta
                border.Opacity = 1;
            }
            else
            {

                border.Margin = new Thickness(10); // Le carte non selezionate sono sempre margin 10

            }

        }

    }

    // ======== Scarta Carta ========
    private void Btn_Scarta_Carta(object sender, MouseEventArgs e)
    {

        List<Border> daScartare = new List<Border>();// Salviamo le carte da scartere in una lista e poi le scartiamo con un for

        foreach (UIElement elemento in Grid_Carte.Children)
        {
            if (elemento is Border border && border.Margin.Top == -5) // Se la carta è selezionata ed è un border
            {
                daScartare.Add(border); // Aggiungi i border delle carte da scartare alla lista con le loro proprietà
            }
        }

        for (int i = 0; i < daScartare.Count; i++)
        {
            // Scartiamo la vecchia carta
            mano.Scarta((Carta)daScartare[i].Tag);
            Grid_Carte.Children.Remove(daScartare[i]); // Cancelliamo l'elemento childen border

            // Riposizioniamo la nuova carta
            Carta cartaNuova = mazzo.Distribuisci();
            mano.AggiungiCarta(cartaNuova);

            int riga = Grid.GetRow(daScartare[i]);
            int colonna = Grid.GetColumn(daScartare[i]);

            StampaCarta(cartaNuova, riga, colonna);
        }

    }

    // ======== Gioca Carta ========
    private void Btn_Gioca_Carta(object sender, MouseButtonEventArgs e)
    {
        List<Border> daGiocare = new List<Border>();// Stessa logica del foreach dello scarta

        foreach (UIElement elemento in Grid_Carte.Children)
        {
            if (elemento is Border border && border.Margin.Top == -5)
            {
                daGiocare.Add(border);
            }
        }

        if (daGiocare.Count == 0) return;

        Mano manoGiocata = new Mano();// creiamo una mano con dentro le carte selezionate
        for (int i = 0; i < daGiocare.Count; i++)
        {
            manoGiocata.AggiungiCarta((Carta)daGiocare[i].Tag);

            // Rimuoviamo la carta dalla mano di 8 carte
            mano.Scarta((Carta)daGiocare[i].Tag);
            Grid_Carte.Children.Remove(daGiocare[i]);

            // Riposizioniamo la nuova carta
            Carta cartaNuova = mazzo.Distribuisci();
            mano.AggiungiCarta(cartaNuova);

            int riga = Grid.GetRow(daGiocare[i]);
            int colonna = Grid.GetColumn(daGiocare[i]);

            StampaCarta(cartaNuova, riga, colonna);
        }
        // Calcoliamo il punteggio
        ScrittaCombinazione(manoGiocata);

        if (Turnosuperato())
        {
            Text_Da_Superare.Text = $"{_punteggioDaBattere}";
            mazzo.CompilaMazzo(); // Riempiamo di nuovo il mazzo per il nuovo turno
        }
    }
}