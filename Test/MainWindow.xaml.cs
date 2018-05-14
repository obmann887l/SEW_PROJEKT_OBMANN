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
using System.IO;
using System.Collections.ObjectModel;

namespace Test
{
    public partial class MainWindow : Window
    {

        ObservableCollection<string> KlassenCollection { get; set; }
        public MainWindow()
        {
            InitializeComponent();

            for (int i = 0; i < 5; i++)
            {
                cb_Zahlen.Items.Add(i+1);
            }


            KlassenLesen();
           
            DataContext = this;
            
            
        }


        private void btn_add_Click(object sender, RoutedEventArgs e)
        {

            try
            {

                if (tb_ID.Text == "ID" && cb_Klasse.Text == "" && tb_Vorname.Text == "Vorname" && tb_Nachname.Text == "Nachname" && tb_Noten.Text == "Notendurchschnitt")
                    throw new Exception();
                else
                {
                    lb_Informationen.Items.Add("ID: " + tb_ID.Text + " | " + " Klasse: " + cb_Zahlen.Text + cb_Klasse.Text + " | " + " Vorname: " + tb_Vorname.Text + " | " + " Nachname: " + tb_Nachname.Text + " | " + " Note: " + tb_Noten.Text);
                }
            }
            catch (Exception eing)
            {
                MessageBox.Show(eing.Message, "Füllen Sie bitte Alle Felder aus", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btn_rmf_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                lb_Informationen.Items.RemoveAt(lb_Informationen.Items.IndexOf(lb_Informationen.SelectedItem));
            }
            catch (Exception loe)
            {
                MessageBox.Show(loe.Message, "Wählen Sie bitte ein Element aus, das gelöscht werden soll", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btn_datei_Click(object sender, RoutedEventArgs e)
        {
            var path = @"\Users\Lukas\Desktop\Schulverwaltung.txt";
            if (lb_Informationen.Items.Count > 0)
            {
                using (TextWriter tw = new StreamWriter(path))
                {
                    foreach (string itemText in lb_Informationen.Items)
                    {
                        tw.WriteLine(itemText);
                    }
                }

            }
        }

        private void KlassenLesen()
        {
            try
            {

                KlassenCollection = new ObservableCollection<string>();
                var path = @"\Users\Lukas\Desktop\Klasse.txt";
                using (StreamReader sr = new StreamReader(path))
                {
                    string line = sr.ReadToEnd();
                    string[] arr = line.Split(';');
                    
                    foreach (var item in arr)
                    {
                        KlassenCollection.Add(item);
                    }
                    cb_Klasse.ItemsSource = KlassenCollection;
                }
            }
            catch (Exception fail)
            {
                MessageBox.Show(fail.Message, "Datei konnte nicht gelesen werden", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void btn_Lesen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var path = @"\Users\Lukas\Desktop\Schulverwaltung.txt";
                using (StreamReader sr = new StreamReader(path))
                {
                    string line = sr.ReadToEnd();
                    lb_Informationen.Items.Add(line);
                }
            }
            catch (Exception fail)
            {
                MessageBox.Show(fail.Message, "Datei konnte nicht gelesen werden", MessageBoxButton.OK, MessageBoxImage.Error,);
            }
        }

        private void btn_change_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string select = lb_Informationen.SelectedItem.ToString();
                tb_bearbeiten.Text = select;
                if (select == null)
                    throw new Exception();
            }
            catch (Exception auswahl)
            {
                MessageBox.Show(auswahl.Message, "Wählen Sie ein Element zur Bearbeitung aus", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void btn_Update_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                lb_Informationen.Items.RemoveAt(lb_Informationen.Items.IndexOf(lb_Informationen.SelectedItem));
                lb_Informationen.Items.Add(tb_bearbeiten.Text);
                if (tb_bearbeiten.Text == "")
                    throw new Exception();
            }
            catch (Exception inhalt)
            {
                MessageBox.Show(inhalt.Message, "Es wurde kein Element bearbeitet", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
