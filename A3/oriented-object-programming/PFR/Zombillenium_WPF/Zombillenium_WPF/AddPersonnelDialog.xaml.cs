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
    /// Logique d'interaction pour AddPersonnelDialog.xaml
    /// </summary>
    public partial class AddPersonnelDialog : Window
    {
        private Administration administration;

        public AddPersonnelDialog(Administration administration)
        {
            InitializeComponent();
            this.administration = administration;
            if (administration != null && administration.Attractions != null)
            {
                foreach (Attraction a in administration.Attractions)
                {
                    AffectationComboBox.Items.Add(a.Nom);
                }
            }
        }

        private void TypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (TypeComboBox.SelectedIndex)
            {
                case 0: // Démon
                    indice.Content = "Force";
                    GradeComboBox.IsEnabled = false;
                    indiceTB.IsEnabled = true;
                    cagnotteTB.IsEnabled = true;
                    AffectationComboBox.IsEnabled = true;
                    CouleurComboBox.IsEnabled = false;
                    break;
                case 1: // Fantome
                    indice.Content = "Pas de carac.";
                    GradeComboBox.IsEnabled = false;
                    indiceTB.IsEnabled = false;
                    cagnotteTB.IsEnabled = true;
                    AffectationComboBox.IsEnabled = true;
                    CouleurComboBox.IsEnabled = false;
                    break;
                case 2: // LoupGarou
                    indice.Content = "Indice de cruauté";
                    GradeComboBox.IsEnabled = false;
                    indiceTB.IsEnabled = true;
                    cagnotteTB.IsEnabled = true;
                    AffectationComboBox.IsEnabled = true;
                    CouleurComboBox.IsEnabled = false;
                    break;
                case 3: // Sorcier
                    indice.Content = "Pas de carac.";
                    GradeComboBox.IsEnabled = true;
                    indiceTB.IsEnabled = false;
                    cagnotteTB.IsEnabled = false;
                    AffectationComboBox.IsEnabled = false;
                    CouleurComboBox.IsEnabled = false;
                    break;
                case 4: // Monstre
                    indice.Content = "Pas de carac.";
                    GradeComboBox.IsEnabled = false;
                    indiceTB.IsEnabled = false;
                    cagnotteTB.IsEnabled = true;
                    AffectationComboBox.IsEnabled = true;
                    CouleurComboBox.IsEnabled = false;
                    break;
                case 5: // Vampire
                    indice.Content = "Indice de luminosité";
                    GradeComboBox.IsEnabled = false;
                    indiceTB.IsEnabled = true;
                    cagnotteTB.IsEnabled = true;
                    AffectationComboBox.IsEnabled = true;
                    CouleurComboBox.IsEnabled = false;
                    break;
                case 6: // Zombie
                    indice.Content = "Degré de décomposition";
                    GradeComboBox.IsEnabled = false;
                    indiceTB.IsEnabled = true;
                    cagnotteTB.IsEnabled = true;
                    AffectationComboBox.IsEnabled = true;
                    CouleurComboBox.IsEnabled = true;
                    break;
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void ValidationButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataIsValid())
            {
                Attraction affectation = GetAffectation();
                
                switch(TypeComboBox.Text)
                {
                    case "Démon":
                        if (affectation != null)
                        {
                            administration.Recruter(new Demon(
                            Int32.Parse(matriculeTB.Text), NameTB.Text, PrenomTB.Text, GetGenre(), fonctionTB.Text, Int32.Parse(cagnotteTB.Text), affectation, Int32.Parse(indiceTB.Text)));
                        } else
                        {
                            administration.Recruter(new Demon(
                            Int32.Parse(matriculeTB.Text), NameTB.Text, PrenomTB.Text, GetGenre(), fonctionTB.Text, Int32.Parse(cagnotteTB.Text), GetAffectationString(), Int32.Parse(indiceTB.Text)));
                        }
                        break;
                    case "Fantôme":
                        if (affectation != null)
                        {
                            administration.Recruter(new Fantome(
                            Int32.Parse(matriculeTB.Text), NameTB.Text, PrenomTB.Text, GetGenre(), fonctionTB.Text, Int32.Parse(cagnotteTB.Text), affectation));
                        }
                        else
                        {
                            administration.Recruter(new Fantome(
                            Int32.Parse(matriculeTB.Text), NameTB.Text, PrenomTB.Text, GetGenre(), fonctionTB.Text, Int32.Parse(cagnotteTB.Text), GetAffectationString()));
                        }
                        break;
                    case "LoupGarou":
                        double.TryParse(indiceTB.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double cruaute);
                        if (affectation != null)
                        {
                            administration.Recruter(new LoupGarou(
                            Int32.Parse(matriculeTB.Text), NameTB.Text, PrenomTB.Text, GetGenre(), fonctionTB.Text, Int32.Parse(cagnotteTB.Text), affectation, cruaute));
                        }
                        else
                        {
                            administration.Recruter(new LoupGarou(
                            Int32.Parse(matriculeTB.Text), NameTB.Text, PrenomTB.Text, GetGenre(), fonctionTB.Text, Int32.Parse(cagnotteTB.Text), GetAffectationString(), cruaute));
                        }
                        break;
                    case "Sorcier":
                        administration.Recruter(new Sorcier(
                            Int32.Parse(matriculeTB.Text), NameTB.Text, PrenomTB.Text, GetGenre(), fonctionTB.Text, GetGrade(), null));
                        break;
                    case "Monstre":
                        if (affectation != null)
                        {
                            administration.Recruter(new Monstre(
                            Int32.Parse(matriculeTB.Text), NameTB.Text, PrenomTB.Text, GetGenre(), fonctionTB.Text, Int32.Parse(cagnotteTB.Text), affectation));
                        }
                        else
                        {
                            administration.Recruter(new Monstre(
                            Int32.Parse(matriculeTB.Text), NameTB.Text, PrenomTB.Text, GetGenre(), fonctionTB.Text, Int32.Parse(cagnotteTB.Text), GetAffectationString()));
                        }
                        break;
                    case "Vampire":
                        double.TryParse(indiceTB.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double luminosite);
                        if (affectation != null)
                        {
                            administration.Recruter(new Vampire(
                            Int32.Parse(matriculeTB.Text), NameTB.Text, PrenomTB.Text, GetGenre(), fonctionTB.Text, Int32.Parse(cagnotteTB.Text), affectation, luminosite));
                        }
                        else
                        {
                            administration.Recruter(new Vampire(
                            Int32.Parse(matriculeTB.Text), NameTB.Text, PrenomTB.Text, GetGenre(), fonctionTB.Text, Int32.Parse(cagnotteTB.Text), GetAffectationString(), luminosite));
                        }
                        break;
                    case "Zombie":
                        if (affectation != null)
                        {
                            administration.Recruter(new Zombie(
                            Int32.Parse(matriculeTB.Text), NameTB.Text, PrenomTB.Text, GetGenre(), fonctionTB.Text, Int32.Parse(cagnotteTB.Text), affectation, GetColor(), Int32.Parse(indiceTB.Text)));
                        }
                        else
                        {
                            administration.Recruter(new Zombie(
                            Int32.Parse(matriculeTB.Text), NameTB.Text, PrenomTB.Text, GetGenre(), fonctionTB.Text, Int32.Parse(cagnotteTB.Text), GetAffectationString(), GetColor(), Int32.Parse(indiceTB.Text)));
                        }
                        break;
                }
                this.Close();
            }
            else
            {
                MaterialMessageBox.ShowError("Merci de vérifier les informations que vous avez entrées.");
            }
        }

        private bool BasicInfoIsValid()
        {
            return (matriculeTB.Text != null && NameTB.Text != null && PrenomTB.Text != null && SexeComboBox.SelectedItem != null && fonctionTB.Text != null);
        }

        private bool AffectationIsValid()
        {
            return (AffectationComboBox.SelectedItem != null);
        }

        private bool DataIsValid()
        {
            if (TypeComboBox.SelectedValue != null)
            {
                string type = TypeComboBox.Text;
                switch (type)
                {
                    case "Démon":
                        if (BasicInfoIsValid() && AffectationIsValid() && indiceTB.Text != null && Int32.TryParse(indiceTB.Text, out int force) && force >= 1 && force <= 10)
                        {
                            return true;
                        }
                        break;
                    case "Fantôme":
                        if (BasicInfoIsValid() && AffectationIsValid())
                        {
                            return true;
                        }
                        break;
                    case "LoupGarou":
                        if (BasicInfoIsValid() && AffectationIsValid() && indiceTB.Text != null && Double.TryParse(indiceTB.Text, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out double cruaute) && cruaute >= 0.1 && cruaute <= 4.0)
                        {
                            return true;
                        }
                        break;
                    case "Sorcier":
                        if (BasicInfoIsValid() && GradeComboBox.SelectedItem != null)
                        {
                            return true;
                        }
                        break;
                    case "Monstre":
                        if (BasicInfoIsValid() && AffectationIsValid())
                        {
                            return true;
                        }
                        break;
                    case "Vampire":
                        if (BasicInfoIsValid() && AffectationIsValid() && indiceTB.Text != null && Double.TryParse(indiceTB.Text, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out double luminosite) && luminosite >= 0.1 && luminosite <= 3.0)
                        {
                            return true;
                        }
                        break;
                    case "Zombie":
                        if (BasicInfoIsValid() && AffectationIsValid() && indiceTB.Text != null && Int32.TryParse(indiceTB.Text, out int decomposition) && decomposition >= 1 && decomposition <= 10)
                        {
                            return true;
                        }
                        break;
                }
            }
            return false;
        }

        private string GetAffectationString()
        {
            if (AffectationIsValid())
            {
                string name = AffectationComboBox.Text;
                switch(name)
                {
                    case "Parc":
                        return "parc";
                    case "Aucune":
                        return "neant";
                }
            }
            return null;
        }

        private Attraction GetAffectation()
        {
            if (AffectationIsValid())
            {
                string name = AffectationComboBox.Text;
                if (name != "Parc" && name != "Aucune")
                {
                    List<Attraction> atts = administration.Attractions.Where(a => a.Nom.Equals(name)).ToList();
                    foreach (Attraction a in atts)
                    {
                        if (a.Nom.Equals(name))
                        {
                            return a;
                        }
                    }
                }
            }
            return null;
        }

        private string GetGrade()
        {
            if (GradeComboBox.SelectedItem != null)
            {
                return GradeComboBox.Text.ToLower();
            }
            else
            {
                return "novice";
            }
        }

        private string GetGenre()
        {
            if (SexeComboBox.SelectedItem != null)
            {
                return SexeComboBox.Text.ToLower();
            }
            else
            {
                return null;
            }
        }

        private string GetColor()
        {
            if (CouleurComboBox.SelectedItem != null)
            {
                return CouleurComboBox.Text.ToLower();
            }
            else
            {
                return "bleuatre";
            }
        }

        private void indiceTB_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+"); //regex that matches disallowed text
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}

