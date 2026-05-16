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

namespace Carshering
{
    /// <summary>
    /// Interaction logic for AddUserWindow.xaml
    /// </summary>
    public partial class AddUserWindow : Window
    {
        public AddUserWindow()
        {
            InitializeComponent();
            LoadRoles();
        }

        private void LoadRoles()
        {
            try
            {
                using (var context = Helper.GetContext())
                {
                    
                    var roles = context.roles.ToList();
                    Role.ItemsSource = roles;
                    Role.SelectedValuePath = "id";
                    Role.DisplayMemberPath = "name";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки ролей: {ex.Message}");
            }
        }

        private void btnCanel_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow adminWindow = new AdminWindow();
            adminWindow.Show();
            this.Close();
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var fullname = FullName.Text;
            var loginnew = Login.Text;
            var passwordnew = Password.Text;
            if (string.IsNullOrEmpty(fullname) || string.IsNullOrEmpty(loginnew) || string.IsNullOrEmpty(passwordnew))
            {
                MessageBox.Show("Все поля должны быть заполнены!");
            }

            try
            {
                using (var context = Helper.GetContext())
                {
                  

                    var selectedRole = (roles)Role.SelectedItem;


                    
                    var roleExists = context.roles.Any(r => r.id == selectedRole.id);
                    if (!roleExists)
                    {
                        return;
                    }

                    var newUser = new users
                    {
                        full_name = fullname,
                        login = loginnew,
                        password = passwordnew,
                        is_loced = 0,
                        FailedLoginAttemps = "0",
                        role_id = selectedRole.id
                    };

                    context.users.Add(newUser);
                    context.SaveChanges();
                    
                    MessageBox.Show("Пользователь успешно добавлен!");
                    FullName.Clear();
                    Login.Clear();
                    Password.Clear();
                    AdminWindow adminWindow = new AdminWindow();
                    adminWindow.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }
    }
}
