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

namespace UICarte;


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

public partial class MainWindow : Window // Qua iniziano i metodi per l'UI
{
    public MainWindow()
    {
        InitializeComponent();
        StampaMano();
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

    // ======== METODI Generali ========
    private void Hover_Opacity_Enter(object sender, MouseEventArgs e)
    {
        if (sender is FrameworkElement elemento) //FrameworkElement è la classe base degli elementi visuali
        {
            elemento.Tag = elemento.Opacity; // Salviamo l'opacità originale
            elemento.Opacity = 0.7;
        }
    }
    private void Hover_Opacity_Leave(object sender, MouseEventArgs e)
    {
        if (sender is FrameworkElement elemento)
        {
            if (elemento.Tag is not null && elemento.Tag is Double)
            {
                Double opacitàOriginale = (Double)elemento.Tag; // In WPF l'opacità è un double non un float
                elemento.Opacity = opacitàOriginale;
            }
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

    public void StampaCarta(Carta carta, int riga, int colonna)
    {
        Border border = new Border(); // Creaiamo il contenitore e ne descriviamo le proprietà
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

        // Eventi
        border.MouseEnter += Hover_Opacity_Enter; // += (si possono aggiungere più metodi in un evento) 
        border.MouseLeave += Hover_Opacity_Leave;
        border.MouseDown += Carta_Select;

        Grid.SetColumn(border, colonna);
        Grid.SetRow(border, riga);
        Grid_Carte.Children.Add(border); // aggiunge alla grid che è nominata
    }

    public void StampaMano()
    {
        for (int x = 0; x <= 1; x++) // Righe
        {
            for (int y = 1; y <= 4; y++) // Colonne
            {
                Carta carta = new Carta(RandomValore(), RandomSeme()); /*Passiamo anche la carta perchè sennò creeremo 
                                                                        * solo dei contenitori visivi senza le proprietà di carta */
                StampaCarta(carta, x, y);
            }
        }
    }
    private void Carta_Select(object sender, MouseEventArgs e)
    {
        if (sender is Border border)
        {
            if (border.Tag is not null && border.Margin.Top != -5) // Se non è alzata
            {
                border.Margin = new Thickness(5, -5, 5, 5); // "Alziamo" la carta
                border.Opacity = 1;

            }
            else
            {
                if (border.Tag is not null) // Se è alzata
                {
                    border.Margin = new Thickness(10); // Le carte non selezionate sono sempre margin 10
                }
            }

        }

    }

}