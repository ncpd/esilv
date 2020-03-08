using BespokeFusion;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Zombillenium_WPF
{
    /// <summary>
    /// Logique d'interaction pour AddAttractionDialog.xaml
    /// </summary>
    public partial class AddAttractionDialog : Window
    {
        private Administration administration;
        private List<string> horairesList;

        public AddAttractionDialog(Administration administration)
        {
            InitializeComponent();
            this.administration = administration;
            horairesList = new List<string>();
        }

        private void TypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (TypeComboBox.SelectedIndex)
            {
                case 0: // Boutique
                    TypeBoutiqueComboBox.IsEnabled = true;
                    dureeTB.IsEnabled = false;
                    vehiculeCheckBox.IsEnabled = false;
                    CategorieComboBox.IsEnabled = false;
                    ageTB.IsEnabled = false;
                    tailleTB.IsEnabled = false;
                    salleTB.IsEnabled = false;
                    placesTB.IsEnabled = false;
                    horairesComboBox.IsEnabled = false;
                    horaireHourTB.IsEnabled = false;
                    horaireMinutesTB.IsEnabled = false;
                    addHoraire.IsEnabled = false;
                    break;
                case 1: // DarkRide
                    TypeBoutiqueComboBox.IsEnabled = false;
                    dureeTB.IsEnabled = true;
                    vehiculeCheckBox.IsEnabled = true;
                    CategorieComboBox.IsEnabled = false;
                    ageTB.IsEnabled = false;
                    tailleTB.IsEnabled = false;
                    salleTB.IsEnabled = false;
                    placesTB.IsEnabled = false;
                    horairesComboBox.IsEnabled = false;
                    horaireHourTB.IsEnabled = false;
                    horaireMinutesTB.IsEnabled = false;
                    addHoraire.IsEnabled = false;
                    break;
                case 2: // RollerCoaster
                    TypeBoutiqueComboBox.IsEnabled = false;
                    dureeTB.IsEnabled = false;
                    vehiculeCheckBox.IsEnabled = false;
                    CategorieComboBox.IsEnabled = true;
                    ageTB.IsEnabled = true;
                    tailleTB.IsEnabled = true;
                    salleTB.IsEnabled = false;
                    placesTB.IsEnabled = false;
                    horairesComboBox.IsEnabled = false;
                    horaireHourTB.IsEnabled = false;
                    horaireMinutesTB.IsEnabled = false;
                    addHoraire.IsEnabled = false;
                    break;
                case 3: // Spectacle
                    TypeBoutiqueComboBox.IsEnabled = false;
                    dureeTB.IsEnabled = false;
                    vehiculeCheckBox.IsEnabled = false;
                    CategorieComboBox.IsEnabled = false;
                    ageTB.IsEnabled = false;
                    tailleTB.IsEnabled = false;
                    salleTB.IsEnabled = true;
                    placesTB.IsEnabled = true;
                    horairesComboBox.IsEnabled = true;
                    horaireHourTB.IsEnabled = true;
                    horaireMinutesTB.IsEnabled = true;
                    addHoraire.IsEnabled = true;
                    break;
            }
        }

        private void idTB_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void ValidationButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataIsValid())
            {
                switch (TypeComboBox.Text)
                {
                    case "Boutique":
                        administration.AjouterAttraction(new Boutique(Int32.Parse(idTB.Text), NameTB.Text, Int32.Parse(nbMinMonstresTB.Text),
                            checkboxBesoin.IsChecked.GetValueOrDefault(), typebesoinTB.Text, TypeBoutiqueComboBox.Text));
                        break;
                    case "DarkRide":
                        administration.AjouterAttraction(new DarkRide(Int32.Parse(idTB.Text), NameTB.Text, Int32.Parse(nbMinMonstresTB.Text),
                            checkboxBesoin.IsChecked.GetValueOrDefault(), typebesoinTB.Text, dureeTB.Text, vehiculeCheckBox.IsChecked.GetValueOrDefault()));
                        break;
                    case "RollerCoaster":
                        double.TryParse(tailleTB.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double invariantCultureResult);
                        administration.AjouterAttraction(new RollerCoaster(Int32.Parse(idTB.Text), NameTB.Text, Int32.Parse(nbMinMonstresTB.Text),
                            checkboxBesoin.IsChecked.GetValueOrDefault(), typebesoinTB.Text, CategorieComboBox.Text, Int32.Parse(ageTB.Text), invariantCultureResult));
                        break;
                    case "Spectacle":
                        StringBuilder horairesConcat = new StringBuilder();
                        foreach (string s in horairesList)
                        {
                            horairesConcat.Append(s).Append(' ');
                        }
                        horairesConcat.Length--;
                        administration.AjouterAttraction(new Spectacle(Int32.Parse(idTB.Text), NameTB.Text, Int32.Parse(nbMinMonstresTB.Text),
                            checkboxBesoin.IsChecked.GetValueOrDefault(), typebesoinTB.Text, salleTB.Text, Int32.Parse(placesTB.Text), horairesConcat.ToString()));
                        break;
                }
                this.Close();
            }
            else
            {
                MaterialMessageBox.ShowError("Merci de vérifier les informations que vous avez entrées.");
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            typebesoinTB.IsEnabled = true;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            typebesoinTB.IsEnabled = false;
            typebesoinTB.Text = String.Empty;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (horaireHourTB != null && horaireMinutesTB != null)
            {
                if (DateTime.TryParse(horaireHourTB.Text + ":" + horaireMinutesTB.Text, out DateTime time))
                {
                    horairesList.Add(time.ToShortTimeString());
                    horairesComboBox.ItemsSource = null;
                    horairesComboBox.ItemsSource = horairesList;
                    horaireHourTB.Text = "";
                    horaireMinutesTB.Text = "";
                } else
                {
                    MaterialMessageBox.ShowError("Merci d'entrer un format d\'heure correct");
                }
            }
        }

        private bool DataIsValid()
        {
            if (TypeComboBox.SelectedValue != null)
            {
                string type = TypeComboBox.Text;
                switch (type)
                {
                    case "Boutique":
                        if (BasicInfoIsValid() && BesoinIsValid() && TypeBoutiqueComboBox.SelectedItem != null)
                        {
                            return true;
                        }
                        break;
                    case "DarkRide":
                        if (BasicInfoIsValid() && BesoinIsValid() && dureeTB.Text != null)
                        {
                            return true;
                        }
                        break;
                    case "RollerCoaster":
                        if (BasicInfoIsValid() && BesoinIsValid() && CategorieComboBox.SelectedItem != null && ageTB.Text != null && tailleTB.Text != null && Double.TryParse(tailleTB.Text, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out double taille))
                        {
                            return true;
                        }
                        break;
                    case "Spectacle":
                        if (BasicInfoIsValid() && BesoinIsValid() && salleTB.Text != null && placesTB.Text != null && horairesList.Count > 0)
                        {
                            return true;
                        }
                        break;
                }
            }
            return false;
        }

        private bool BasicInfoIsValid()
        {
            return (idTB.Text != null && NameTB.Text != null && nbMinMonstresTB.Text != null);
        }

        private bool BesoinIsValid()
        {
            if (checkboxBesoin.IsChecked == true && typebesoinTB.Text != null && typebesoinTB.Text != "")
            {
                return true;
            } else if(checkboxBesoin.IsChecked == false && (typebesoinTB.Text == null || typebesoinTB.Text == ""))
            {
                return true;
            } else
            {
                return false;
            }
        }

        private void HoraireHourTB_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex checktime = new Regex(@"^(?:0?[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$");
            e.Handled = checktime.IsMatch(e.Text);
        }

        private void tailleTB_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+"); //regex that matches disallowed text
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
