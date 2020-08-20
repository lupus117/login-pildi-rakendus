using login_pildirakendus.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace login_pildirakendus.ViewModels
{
    public class CommentsViewModel : INotifyPropertyChanged
    {

        //MAYBE WILL BE USED AT SOME POINT
        public CommentsViewModel()
        {
            Comments_ = new ObservableCollection<Comment>();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        //public ObservableCollection<Post> Comments { get; set; }
        private ObservableCollection<Comment> _comments;
        public ObservableCollection<Comment> Comments_
        {
            get
            {
                return _comments;
            }

            set
            {
                if (_comments != value)
                {
                    _comments = value;
                    OnPropertyChanged(nameof(Comments_));
                }
            }

        }

        public void RefreshList(int i)
        {

            Comments_.Clear();

            List<Comment> commentList = Task.Run(async () => await App.dbContext.Comments_GetCommentsAsync()).Result;
            
      

            foreach (Comment comment in commentList)
            {
                if(comment.PostId == i)
                Comments_.Add(comment);
            }
        }
    }
}
