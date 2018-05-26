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
        ObservableCollection<Schueler> alle_schueler = new ObservableCollection<Schueler>();
        ObservableCollection<Lehrer> alle_lehrer = new ObservableCollection<Lehrer>();

        public MainWindow()
        {
            
            InitializeComponent();
            
            lb_Informationen.ItemsSource = alle_schueler;
            

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

                if (tb_ID.Text == "ID" || cb_Klasse.Text == "" || tb_Vorname.Text == "Vorname" || tb_Nachname.Text == "Nachname")
                    throw new Exception();
                else
                {
                    if (tb_kuerzel.Text == "Lehrerkürzel"|| tb_kuerzel.Text== "")
                    {
                        if (tb_Noten.Text == "Notendurchschnitt")
                        {
                            throw new Exception("Es ist kein Notendurchschnitt angegeben");
                        }
                        Schueler schueler = new Schueler(Convert.ToInt32(tb_ID.Text), cb_Zahlen.Text + cb_Klasse.Text, tb_Vorname.Text, tb_Nachname.Text, Convert.ToDouble(tb_Noten.Text));
                        alle_schueler.Add(schueler);
                        alle_schueler.ToList().Sort();
                        lb_Informationen.ItemsSource = alle_schueler;
                    }
                       
                    else
                    {
                        Lehrer lehrer = new Lehrer(Convert.ToInt32(tb_ID.Text), cb_Zahlen.Text + cb_Klasse.Text, tb_Vorname.Text, tb_Nachname.Text, tb_kuerzel.Text);
                        alle_lehrer.Add(lehrer);
                        lb_Informationen.ItemsSource = alle_lehrer;
                        alle_lehrer.ToList().Sort();
                    }
                   
                }
            }
            catch (Exception eing)
            {
                MessageBox.Show("Füllen Sie bitte Alle Felder aus."+ eing.Message, "Füllen Sie bitte Alle Felder aus", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btn_rmf_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string typ = (((lb_Informationen.ItemsSource.ToString()).Split('[')[1]).Split('.')[1]).Replace("]", String.Empty);
                if (typ == "Schueler")
                {
                    alle_schueler.RemoveAt(lb_Informationen.SelectedIndex);
                    alle_schueler.ToList().Sort();
                }
                else
                {
                    alle_lehrer.RemoveAt(lb_Informationen.SelectedIndex);
                    alle_lehrer.ToList().Sort();
                }
            }
            catch (Exception loe)
            {
                MessageBox.Show(loe.Message, "Wählen Sie bitte ein Element aus, das gelöscht werden soll", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btn_datei_Click(object sender, RoutedEventArgs e)
        {
            string typ = (((lb_Informationen.ItemsSource.ToString()).Split('[')[1]).Split('.')[1]).Replace("]", String.Empty);
            string path;
            if (lb_Informationen.Items.Count > 0)
            {
                string alles = String.Empty;
                if (typ == "Schueler")
                {
                    path = "Schulverwaltung_Schueler.txt";
                    foreach (var item in alle_schueler)
                    {
                        alles += item + Environment.NewLine;
                    }
                }
                else
                {
                    path = "Schulverwaltung_Lehrer.txt";
                    foreach (var item in alle_lehrer)
                    {
                        alles += item + Environment.NewLine;
                    }
                }
                File.WriteAllText(path, alles);
            }
        }

        private void KlassenLesen()
        {
            try
            {

                KlassenCollection = new ObservableCollection<string>();
                var path = "Klasse.txt";
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
                string path;
                string line;
                if (rb_Schueler.IsChecked == true)
                    {
                    path = "Schulverwaltung_Schueler.txt";
                    StreamReader file = new StreamReader(path);
                    alle_schueler.Clear();
                    while ((line = file.ReadLine()) != null && line != String.Empty)
                    {
                        alle_schueler.Add(new Schueler(Convert.ToInt32(line.Split('|')[0].Split(':')[1].Replace(" ", String.Empty)), line.Split('|')[1].Split(':')[1].Replace(" ", String.Empty), line.Split('|')[2].Split(':')[1].Replace(" ", String.Empty), line.Split('|')[3].Split(':')[1].Replace(" ", String.Empty), Convert.ToDouble(line.Split('|')[4].Split(':')[1].Replace(" ", String.Empty))));
                    }
                    lb_Informationen.ItemsSource = alle_schueler;
                }
                else if (rb_Lehrer.IsChecked == true)
                {
                    path = "Schulverwaltung_Lehrer.txt";
                    StreamReader file = new StreamReader(path);
                    while ((line = file.ReadLine()) != null && line != String.Empty)
                    {
                        alle_lehrer.Add(new Lehrer(Convert.ToInt32(line.Split('|')[0].Split(':')[1].Replace(" ", String.Empty)), line.Split('|')[1].Split(':')[1].Replace(" ", String.Empty), line.Split('|')[2].Split(':')[1].Replace(" ", String.Empty), line.Split('|')[3].Split(':')[1].Replace(" ", String.Empty), line.Split('|')[4].Split(':')[1].Replace(" ", String.Empty)));
                    }
                    lb_Informationen.ItemsSource = alle_lehrer;

                }
            }
            catch (Exception fail)
            {
                MessageBox.Show(fail.Message, "Datei konnte nicht gelesen werden", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btn_change_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int index = lb_Informationen.SelectedIndex;
                string typ = (((lb_Informationen.ItemsSource.ToString()).Split('[')[1]).Split('.')[1]).Replace("]", String.Empty);
                if (typ == "Schueler")
                {

                    tb_Vorname.Text = alle_schueler[index].Vorname;
                    tb_Nachname.Text = alle_schueler[index].Nachname;
                    tb_ID.Text = alle_schueler[index].SID.ToString();
                    tb_Noten.Text = alle_schueler[index].Noten.ToString();
                    cb_Zahlen.SelectedItem = Convert.ToInt32(alle_schueler[index].Klasse.Substring(0, 1));
                    cb_Klasse.SelectedItem = alle_schueler[index].Klasse.Substring(1);
                }
                else
                {
                    tb_Vorname.Text = alle_lehrer[index].Vorname;
                    tb_Nachname.Text = alle_lehrer[index].Nachname;
                    tb_ID.Text = alle_lehrer[index].LID.ToString();
                    tb_kuerzel.Text = alle_lehrer[index].Lehrerkürzel;
                    cb_Zahlen.SelectedItem = Convert.ToInt32(alle_lehrer[index].Klasse.Substring(0, 1));
                    cb_Klasse.SelectedItem = alle_lehrer[index].Klasse.Substring(1);

                }
                



                //if (select == null)
                //  throw new Exception();

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
                //alle_schueler.RemoveAt(lb_Informationen.SelectedIndex);
                string typ = (((lb_Informationen.ItemsSource.ToString()).Split('[')[1]).Split('.')[1]).Replace("]", String.Empty);
                if (typ == "Schueler")
                {


                    Schueler schueler = new Schueler(Convert.ToInt32(tb_ID.Text), cb_Zahlen.Text + cb_Klasse.Text, tb_Vorname.Text, tb_Nachname.Text, Convert.ToDouble(tb_Noten.Text));
                    int index = lb_Informationen.SelectedIndex;
                    if (index < 0)
                        throw new Exception();
                    alle_schueler[index] = schueler;
                    alle_schueler.ToList().Sort();
                    
                }
                else
                {
                    Lehrer lehrer = new Lehrer(Convert.ToInt32(tb_ID.Text), cb_Zahlen.Text + cb_Klasse.Text, tb_Vorname.Text, tb_Nachname.Text, tb_kuerzel.Text);
                    int index = lb_Informationen.SelectedIndex;
                    if (index < 0)
                        throw new Exception();
                    alle_lehrer[index] = lehrer;
                    alle_lehrer.ToList().Sort();
                }
            }
            catch (Exception inhalt)
            {
                MessageBox.Show(inhalt.Message, "Es wurde kein Element bearbeitet", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
