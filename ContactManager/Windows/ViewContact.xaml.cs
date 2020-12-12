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
using System.Windows.Shapes;

namespace ContactManager.Windows
{
    /// <summary>
    /// Interaction logic for ViewContact.xaml
    /// </summary>
    public partial class ViewContact : Window
    {
        public ViewContact(Contact c)
        {
            InitializeComponent();

            fName.Text = c.FirstName;
            lName.Text = c.LastName;
            phone.Text = c.Phone;
            email.Text = c.Email;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
