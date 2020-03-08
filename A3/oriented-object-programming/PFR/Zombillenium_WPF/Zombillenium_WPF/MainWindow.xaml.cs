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
using System.Text.RegularExpressions;
using System.Windows.Media.Animation;
using MahApps.Metro;
using MaterialDesignThemes;
using BespokeFusion;
using System.Windows.Threading;
using System.Globalization;
using MaterialDesignThemes.Wpf;
using System.Diagnostics;

namespace Zombillenium_WPF
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Administration admin;

        public MainWindow()
        {
            InitializeComponent();

            DispatcherTimer timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                this.dateText.Text = DateTime.Now.ToString("dd MMMM yyyy, HH:mm:ss", new CultureInfo("fr-FR"));
            }, this.Dispatcher);

            admin = new Administration();
            admin.ReadCSV("Listing.csv");

            attractions.ItemsSource = admin.Attractions;

            foreach (Attraction a in admin.Attractions)
            {
                affectationComboBox.Items.Add(a.Nom);
            }

            personnel.ItemsSource = admin.ToutLePersonnel;
            DataContext = admin;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender == addEmployeButton)
            {
                AddPersonnelDialog dialog = new AddPersonnelDialog(admin);
                dialog.Show();
            }
            else if (sender == addAttractionButton)
            {
                AddAttractionDialog dialog = new AddAttractionDialog(admin);
                dialog.Show();
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void ModifEmploye(object sender, RoutedEventArgs e)
        {
            bool problem = false;
            if (admin != null && admin.ToutLePersonnel != null && admin.ToutLePersonnel.Count > 0 && !String.IsNullOrEmpty(matriculeTextBox.Text) && (!String.IsNullOrEmpty(cagnotteTextBox.Text)
                || !String.IsNullOrEmpty(fonctionTextBox.Text) || !String.IsNullOrEmpty(affectationComboBox.Text)))
            {
                string st1 = matriculeTextBox.Text;
                if (!String.IsNullOrEmpty(cagnotteTextBox.Text)) // On modifie la cagnotte
                {
                    string st2 = cagnotteTextBox.Text;
                    if (!admin.ModifierCagnotte(Int32.Parse(matriculeTextBox.Text), Int32.Parse(cagnotteTextBox.Text)))
                    {
                        problem = true;
                    }
                }
                if (!String.IsNullOrEmpty(fonctionTextBox.Text))
                {
                    string st2 = fonctionTextBox.Text;
                    admin.ModifierFonction(Int32.Parse(st1), st2);
                }
                if (!String.IsNullOrEmpty(affectationComboBox.Text))
                {
                    Attraction att = admin.Attractions.Find(a => a.Nom.Contains(affectationComboBox.Text));
                    Personnel perso = admin.ToutLePersonnel.Find(p => p.Matricule == Int32.Parse(matriculeTextBox.Text));
                    if (att != null && !(perso is Sorcier))
                    {
                        admin.ModifierAffectation(Int32.Parse(st1), att);
                    } else
                    {
                        problem = true;
                    }
                }
                if(problem)
                {
                    MaterialMessageBox.ShowError("Une erreur est survenue lors de la modification des informations du personnel n°" + matriculeTextBox.Text + ".");
                } else
                {
                    MaterialMessageBox.Show("Les informations du personnel n°" + matriculeTextBox.Text + " ont bien été modifiées.", "Modification des attributs du personnel n°" + matriculeTextBox.Text);
                }
            }
            else
            {
                MaterialMessageBox.Show("Merci d'entrer un numéro de matricule et un champ minimum.", "Entrée invalide");
            }
        }

        private void refresh_Click(object sender, RoutedEventArgs e)
        {
            if (sender == refreshAttractions)
            {
                attractions.ItemsSource = null;
                if (admin.Attractions != null)
                {
                    attractions.ItemsSource = admin.Attractions;
                }

                var ease = new PowerEase { EasingMode = EasingMode.EaseOut };

                //DoubleAnimation(FromValue. ToValue, Duration)
                DoubleAnimation myanimation = new DoubleAnimation
                        (0, 360, new Duration(TimeSpan.FromSeconds(0.8)));

                //Adding Power ease to the animation
                myanimation.EasingFunction = ease;

                RotateTransform rt = new RotateTransform();

                //  "img" is Image added in XAML
                refreshAttractions.RenderTransform = rt;
                refreshAttractions.RenderTransformOrigin = new Point(0.5, 0.5);
                rt.BeginAnimation(RotateTransform.AngleProperty, myanimation);

            }
            else if (sender == refreshPersonnel)
            {
                personnel.ItemsSource = null;
                if (admin.ToutLePersonnel != null)
                {
                    personnel.ItemsSource = admin.ToutLePersonnel;
                }
                var ease = new PowerEase { EasingMode = EasingMode.EaseOut };

                //DoubleAnimation(FromValue. ToValue, Duration)
                DoubleAnimation myanimation = new DoubleAnimation
                        (0, 360, new Duration(TimeSpan.FromSeconds(0.8)));

                //Adding Power ease to the animation
                myanimation.EasingFunction = ease;

                RotateTransform rt = new RotateTransform();

                //  "img" is Image added in XAML
                refreshPersonnel.RenderTransform = rt;
                refreshPersonnel.RenderTransformOrigin = new Point(0.5, 0.5);
                rt.BeginAnimation(RotateTransform.AngleProperty, myanimation);
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            natureMtnceTextBox.IsEnabled = true;
            dureeMtnceTextBox.IsEnabled = true;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            natureMtnceTextBox.IsEnabled = false;
            dureeMtnceTextBox.IsEnabled = false;
            dureeMtnceTextBox.Text = "";
            natureMtnceTextBox.Text = "";
        }

        private void ModifAttraction(object sender, RoutedEventArgs e)
        {
            if (identifiantTextBox.Text != null && maintenanceCheckBox.IsChecked == true && natureMtnceTextBox.Text != null && dureeMtnceTextBox.Text != null)
            {
                try
                {
                    admin.ModifierMaintenance(Int32.Parse(identifiantTextBox.Text), natureMtnceTextBox.Text, Double.Parse(dureeMtnceTextBox.Text));
                }
                catch (Exception)
                {
                    MaterialMessageBox.ShowError("Une erreur est survenue.");
                }
            }
            else if (identifiantTextBox.Text != null && maintenanceCheckBox.IsChecked == false)
            {
                try
                {
                    admin.ModifierMaintenance(Int32.Parse(identifiantTextBox.Text), "", 0.0);
                }
                catch (Exception)
                {
                    MaterialMessageBox.ShowError("Une erreur est survenue.");
                }
            }
            else
            {
                MaterialMessageBox.Show("Merci de vérifier vos informations.", "Entrée invalide");
            }
        }

        private void exportCSV_Click(object sender, RoutedEventArgs e)
        {
            //DialogHost.Show(ExportCSV);
            ExportCSV.IsOpen = true;
        }

        private void Sample1_DialogHost_OnDialogClosing(object sender, DialogClosingEventArgs eventArgs)
        {
            Console.WriteLine("SAMPLE 1: Closing dialog with parameter: " + (eventArgs.Parameter ?? ""));

            //you can cancel the dialog close:
            //eventArgs.Cancel();

            if (!Equals(eventArgs.Parameter, true)) return;

            if (!string.IsNullOrWhiteSpace(FruitTextBox.Text))
            {
                admin.ExtractListToCSV(FruitTextBox.Text + ".csv", admin.ToutLePersonnel, admin.Attractions);
                try
                {
                    FileInfo f = new FileInfo(FruitTextBox.Text + ".csv");
                    string fullname = f.FullName;
                    Process.Start(fullname);
                }
                catch (Exception)
                {

                }
            }
        }
    }
}
