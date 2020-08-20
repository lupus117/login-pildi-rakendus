using login_pildirakendus.Models;
using login_pildirakendus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace login_pildirakendus
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogInPage : ContentPage
    {
        public LogInPage()
        {
            InitializeComponent();
        }

        private async void LoginBtn_Clicked(object sender, EventArgs e)
        {
            Output.Text = "";
            string username = UserNameEntry.Text;
            string password = PasswordEntry.Text;

            if ((username == "" || password == ""))
            {
                Output.Text = "Please enter Login data";
                return;
            }

            if (UserExists(username, password))
            {
                var user = App.dbContext.Users_GetUserByNameAndPassword(username, password).Result;
                Output.Text = $"Welcome {user.UserName}";

                await Navigation.PushAsync(new TabsPage() { BindingContext = user });
                
            }
            else
            {
                Output.Text = "User does not exist!";
            }


        }

        private async void SignUpBtn_Clicked(object sender, EventArgs e)
        {
            Output.Text = "";
            string newUsername = UserNameEntry.Text;
            string newPassword = PasswordEntry.Text;

            if (newUsername == "" || newPassword == "")
            {
                Output.Text = "Fields cannot be empty";
                return;
            }

            if (!UserExists(newUsername, newPassword))
            {
                try
                {

                var newUser = new User() { UserName = newUsername, Password = newPassword };
                await App.dbContext.Users_SaveUserAsync(newUser);
                Output.Text = "User successfully registered";
                UserNameEntry.Text = "";
                PasswordEntry.Text = "";
                }
                catch
                {
                Output.Text = "Unexpected error occured.";
                }
            }
            else 
            {
                Output.Text = "User already exists!";
            }
           
        }

        private bool UserExists(string username, string password)
        {
            return App.dbContext.UserExists(username, password);
        }

    }
}