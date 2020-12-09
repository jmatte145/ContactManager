using ContactManager.Windows;
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

namespace ContactManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var DBContact = ContactDB.CInstance;
            DBContact.LoadDB();
            List<Contact> contacts = DBContact.getList();
            contactList.ItemsSource = contacts;
        }

        private void NewContact_Click(object sender, RoutedEventArgs e) 
        {
            AddContact ac = new AddContact();
            ac.ShowDialog();

            contactList.ItemsSource = null;
            contactList.Items.Clear();
            contactList.Items.Refresh();

            var DBContact = ContactDB.CInstance;
            if(ac.fNameInput.Text != "" && ac.lNameInput.Text != "" && ac.fNameInput.Text != "" && ac.fNameInput.Text != "")
            {
                DBContact.AddContact(ac.fNameInput.Text, ac.lNameInput.Text, ac.PhoneInput.Text, ac.EmailInput.Text);
            }
            DBContact.UpdateDB();
            List<Contact> contacts = new List<Contact>();
            contacts = DBContact.getList();
            contactList.ItemsSource = contacts;
        }

        private void EditContact_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ViewContact_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteContact_Click(object sender, RoutedEventArgs e)
        {
            int idSelect = 0;
            string SelectedItem = contactList.SelectedItem.ToString();  
            string[] splitSelected = SelectedItem.Split(' ');


            int.TryParse(splitSelected[0], out idSelect);

            contactList.ItemsSource = null;
            contactList.Items.Clear();
            contactList.Items.Refresh();

            var DBContact = ContactDB.CInstance;
            if(idSelect != 0)
            {
                DBContact.DeleteContact(idSelect);
            }
            DBContact.UpdateDB();
            List<Contact> contacts = new List<Contact>();
            contacts = DBContact.getList();
            contactList.ItemsSource = contacts;
        }

        private void contactList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ImportContact_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ExportContact_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
