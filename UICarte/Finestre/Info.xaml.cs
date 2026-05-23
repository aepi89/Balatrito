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

namespace UICarte;

public partial class Info : Window  
{
    public Info()
    {
        InitializeComponent();
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

}