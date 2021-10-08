using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hi_Tech_Order_Management_System.DataAccess;

namespace Hi_Tech_Order_Management_System.Business
{
    public class UserAccount
    {
        private int userId;
        private string password;
        private int employeeId;

        public int UserId { get => userId; set => userId = value; }
        public string Password { get => password; set => password = value; }
        public int EmployeeId { get => employeeId; set => employeeId = value; }

        public UserAccount SearchUserAccount(int userId, string password)
        {
            return UserAccountDB.SearchRecord(userId, password);
        }
        public UserAccount SearchUserAccount(int userId)
        {
            return UserAccountDB.SearchRecord(userId);
        }
        public UserAccount SearchUserAccountByEmpId(int empId)
        {
            return UserAccountDB.SearchRecordByEmpId(empId);
        }
        public void SaveUser(UserAccount user)
        {
            UserAccountDB.SaveRecord(user);
        }
        public void UpdateUser(UserAccount user)
        {
            UserAccountDB.UpdateRecord(user);
        }
        public void DeleteUser(int userId)
        {
            UserAccountDB.DeleteRecord(userId);
        }
        public List<UserAccount> SearchAllUser()
        {
            return UserAccountDB.GetRecordList();
        }
    }
}
