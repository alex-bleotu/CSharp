using JustTests.TestDataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustTests {
    internal class Database {
        private UsersTableAdapter usersTableAdapter = new UsersTableAdapter();
        private TestDataSet db = new TestDataSet();

        public bool Register(string username, string password) {
            usersTableAdapter.Insert(username, password);
            usersTableAdapter.Fill(db.Users);

            return db.Users.Any(u => u.username.Trim() == username && u.password.Trim() == password);
        }

        public int GetUsersCount() {
            usersTableAdapter.Fill(db.Users);
            return db.Users.Count;
        }

        public bool CheckIfUserExists(string username) {
            usersTableAdapter.Fill(db.Users);
            return db.Users.Any(u => u.username.Trim() == username);
        }

        public int Login(string username, string password) {
            usersTableAdapter.Fill(db.Users);

            var userRow = db.Users.FirstOrDefault(u => u.username.Trim() == username && u.password.Trim() == password);

            if (userRow != null)
                return userRow.id;
            return -1;
        }
    }
}