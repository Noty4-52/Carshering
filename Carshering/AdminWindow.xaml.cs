using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Data.Entity;

namespace Carshering
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
            Usersdt.ItemsSource = Helper.GetContext().users.ToList();
            LoadUsers();
        }
        private void LoadUsers()
        {
            try
            {
                using (var context = Helper.GetContext())
                {
                 
                    var usersList = context.users
                                           .Include(u => u.roles) 
                                           .ToList();

                    Usersdt.ItemsSource = usersList;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки: {ex.Message}");
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddUserWindow addUserWindow = new AddUserWindow();
            addUserWindow.Show();
            this.Close();
        }
        
        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            if(Usersdt.SelectedItem == null)
    {
                MessageBox.Show("Выберите пользователя для удаления!");
                return;
            }

            var selectedUser = (users)Usersdt.SelectedItem;

          
            var result = MessageBox.Show($"Удалить пользователя {selectedUser.full_name}?",
                                          "Подтверждение",
                                          MessageBoxButton.YesNo,
                                          MessageBoxImage.Warning);

            if (result != MessageBoxResult.Yes)
                return;

            try
            {
                using (var context = Helper.GetContext())
                {
                    var user = context.users.Find(selectedUser.id);
                    if (user != null)
                    {
                        context.users.Remove(user);
                        context.SaveChanges();
                        LoadUsers();
                        MessageBox.Show("Пользователь удалён!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }

        }

        private void btnBlock_unblock_Click(object sender, RoutedEventArgs e)
        {
            if (Usersdt.SelectedItem == null)
            {
                MessageBox.Show("Выберите пользователя!");
                return;
            }

            var selectedUser = (users)Usersdt.SelectedItem;

            using (var context = Helper.GetContext())
            {
                var user = context.users.Find(selectedUser.id);

                if (user != null)
                {
                   
                    user.is_loced = user.is_loced == 1 ? (byte)0 : (byte)1;

                    context.SaveChanges();
                    LoadUsers();

                   
                    string status = user.is_loced == 1 ? "заблокирован" : "разблокирован";
                    MessageBox.Show($"Пользователь {user.full_name} {status}!");
                }
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
