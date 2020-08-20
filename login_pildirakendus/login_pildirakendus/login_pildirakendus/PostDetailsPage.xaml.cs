using login_pildirakendus.Models;
using login_pildirakendus.ViewModels;
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
    public partial class PostDetailsPage : ContentPage
    {
        public PostDetailsPage()
        {
            InitializeComponent();
           
        }

        
        protected override void OnAppearing()
        {
            base.OnAppearing();
            var bc = this.Comments_ListView;
            int postid = (this.BindingContext as Post).Id;
            (bc.BindingContext as CommentsViewModel)?.RefreshList(postid);
        }
        

        private async void DeletePostBtn_Clicked(object sender, EventArgs e)
        {

            var post = (Post)BindingContext;
            await App.dbContext.Posts_DeletePostAsync(post);
            await Navigation.PopAsync();
        }
    }
}