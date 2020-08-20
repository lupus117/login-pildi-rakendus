using login_pildirakendus.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace login_pildirakendus.Data
{
    public class ApplicationDatabase
    {
        SQLiteAsyncConnection _dbContext;

        public ApplicationDatabase(string dbPath)
        {
            _dbContext = new SQLiteAsyncConnection(dbPath);
            _dbContext.CreateTableAsync<User>().Wait();
            _dbContext.CreateTableAsync<Comment>().Wait();
            _dbContext.CreateTableAsync<Post>().Wait();

        }



        //USERS 
        public async Task<List<User>> Users_GetUsersAsync()
        {
            return await _dbContext.Table<User>().ToListAsync();
        }

        public bool UserExists(string username, string password)
        {
            var GetUser = Users_GetUserByNameAndPassword(username, password);
            var user = GetUser.Result;

            if (user == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public async Task<User> Users_GetUserByIdAsync(int id)
        {
            return await _dbContext.Table<User>()
                           .Where(x => x.Id == id)
                           .FirstOrDefaultAsync();
        }

        public Task<User> Users_GetUserByNameAndPassword(string username, string password)
        {
            return _dbContext.Table<User>()
                           .Where(x => x.UserName.Equals(username) && x.Password.Equals(password))
                           .FirstOrDefaultAsync();
        }

        public async Task<int> Users_SaveUserAsync(User User)
        {
            if (User.Id != 0)
            {
                return await _dbContext.UpdateAsync(User);
            }
            else
            {
                return await _dbContext.InsertAsync(User);
            }
        }

        public async Task<int> Users_DeleteUserAsync(User User)
        {
            return await _dbContext.DeleteAsync(User);
        }

        //POSTS
        public async Task<List<Post>> Posts_GetPostsAsync()
        {
            return await _dbContext.Table<Post>().ToListAsync();
        }

        public async Task<Post> Posts_GetPostAsync(int id)
        {
            return await _dbContext.Table<Post>()
                           .Where(x => x.Id == id)
                           .FirstOrDefaultAsync();
        }

        public async Task<int> Posts_SavePostAsync(Post Post)
        {
            if (Post.Id != 0)
            {
                return await _dbContext.UpdateAsync(Post);
            }
            else
            {
                return await _dbContext.InsertAsync(Post);
            }
        }

        public async Task<int> Posts_DeletePostAsync(Post Post)
        {
            return await _dbContext.DeleteAsync(Post);
        }


        //Comment 
        public async Task<List<Comment>> Comments_GetCommentsAsync()
        {
            return await _dbContext.Table<Comment>().ToListAsync();
        }

        public async Task<Comment> Comments_GetCommentByCommentIdAsync(int id)
        {
            return await _dbContext.Table<Comment>()
                           .Where(x => x.Id == id)
                           .FirstOrDefaultAsync();
        }

        public async Task<List<Comment>> Comments_GetCommentsListByPostIdAsync(int id)
        {
            return await _dbContext.Table<Comment>()
                           .Where(x => x.PostId == id)
                           .ToListAsync();
        }


        public async Task<int> Comments_SaveCommentAsync(Comment Comment)
        {
            if (Comment.Id != 0)
            {
                return await _dbContext.UpdateAsync(Comment);
            }
            else
            {
                return await _dbContext.InsertAsync(Comment);
            }
        }

        public async Task<int> Comments_DeleteCommentAsync(Comment Comment)
        {
            return await _dbContext.DeleteAsync(Comment);
        }


        public string GetDatabasePath()
        {
            return _dbContext.DatabasePath;
        }
    }
}
