using login_pildirakendus.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
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
	public partial class AddPostPage : ContentPage
	{
		public AddPostPage()
		{
			InitializeComponent ();
		}

        private async void TakePictureBtn_Clicked(object sender, EventArgs e)
        {
            ErrorLabel.Text = "";
            
            try
            {

                var photo = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions() { });
                if (photo != null)
                    PhotoImage.Source = photo.Path;
            }
            catch
            {
              
            }


        }
        private async void UploadPictureBtn_Clicked(object sender, EventArgs e)
        {
            ErrorLabel.Text = "";

            try
            {

                var photo = await CrossMedia.Current.PickPhotoAsync();
                if (photo != null)
                    PhotoImage.Source = photo.Path;
            }
            catch
            {
                
            }


        }

        private async void SavePostBtn_Clicked(object sender, EventArgs e)
        {
            ErrorLabel.Text = "";

            if ((PostTitleEntry.Text == "" && PhotoImage.Source.ToString() == "File: ") ||
                (PostTitleEntry.Text == "" || PhotoImage.Source.ToString() == "File: "))
            {
                ErrorLabel.Text = "Title and Picture are required!";
            }
            else
            {

                //var post = (Post)BindingContext;
                var post = new Post();
                var user = (User)BindingContext;
                post.Title = PostTitleEntry.Text;
                string currentPath = PhotoImage.Source.ToString();
                string formattedPath = currentPath.Substring(6);
                post.ImgPath = formattedPath;
                post.Date = DateTime.Now;
                post.UserPhotoPath = user.ProfilePhotoPath;
                post.UserName = user.UserName;

                await App.dbContext.Posts_SavePostAsync(post);
                await Navigation.PopAsync();
            }

        }
    }
}