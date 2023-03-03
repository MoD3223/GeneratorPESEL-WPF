using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GeneratorPESELWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        string PESELCaly;
        Byte[] PESELByte = new Byte[11];

        int rok;
        int miesiac;
        int dzien;
        Random rnd = new Random();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (WybranaData.SelectedDate == null)
            {
                PESEL.Foreground = new SolidColorBrush(Colors.Red);
                PESEL.Text = "Blad! Nie wybrano daty";
            }
            else if (MezczyznaPrzycisk.IsChecked == false & KobietaPrzycisk.IsChecked == false)
            {
                PESEL.Foreground = new SolidColorBrush(Colors.Red);
                PESEL.Text = "Blad! Nie wybrano plci";
            }
            else
            {
                rok = WybranaData.SelectedDate.Value.Year;

                PESELByte[0] = (byte)((rok % 100)/10);
                PESELByte[1] = (byte)(rok % 10);
                if (WybranaData.SelectedDate.Value.Year < 1900 & WybranaData.SelectedDate.Value.Year > 1800)
                {
                    miesiac += 80;
                }
                else if (WybranaData.SelectedDate.Value.Year > 2000)
                {
                    miesiac += 20;
                }
                miesiac += WybranaData.SelectedDate.Value.Month;
                if (miesiac < 10)
                {
                    PESELByte[2] = 0;
                    PESELByte[3] = (byte)miesiac;
                }
                else
                {
                    PESELByte[2] = (byte)(miesiac /10);
                    PESELByte[3] = (byte)(miesiac % 10);
                }
                dzien = WybranaData.SelectedDate.Value.Day;
                if (dzien < 10)
                {
                    PESELByte[4] = 0;
                    PESELByte[5] = (byte)dzien;
                }
                else
                {
                    PESELByte[4] = (byte)(dzien / 10);
                    PESELByte[5] = (byte)(dzien % 10);
                }
                PESELByte[6] = (byte)rnd.Next(10);
                PESELByte[7] = (byte)rnd.Next(10);
                PESELByte[8] = (byte)rnd.Next(10);



                if (MezczyznaPrzycisk.IsChecked == true)
                {
                    int rand = rnd.Next(10);
                    if (rand % 2 == 0)
                    {
                        rand++;
                    }
                    PESELByte[9] = (byte)rand;
                }
                else
                {
                    int rand = rnd.Next(10);
                    if (rand % 2 == 1)
                    {
                        rand++;
                    }
                    PESELByte[9] = (byte)rand;
                }

                int sumaKontrolna = 1 * PESELByte[0] + 3 * PESELByte[1] + 7 * PESELByte[2] + 9 * PESELByte[3] + 1 * PESELByte[4] + 3 * PESELByte[5] + 7 * PESELByte[6] + 9 * PESELByte[7] + 1 * PESELByte[8] + 3 * PESELByte[9];
                sumaKontrolna %= 10;
                sumaKontrolna = 10 - sumaKontrolna;
                PESELByte[10] = (byte)sumaKontrolna;

                PESELCaly = "";
                for (int i = 0; i < PESELByte.Length; i++)
                {
                    PESELCaly += PESELByte[i];
                }
                PESEL.Foreground = new SolidColorBrush(Colors.Green);
                PESEL.Text = PESELCaly;

                Clipboard.SetText(PESELCaly);
            }
        }
    }
}
