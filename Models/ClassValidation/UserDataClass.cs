using TUNIWEB.Models.Enums;

namespace TUNIWEB.Models.ClassValidation
{
    public class UserDataClass
    {
        public string UserID { get; }

        public string UserType { get; }
        public UserDataClass(string UserID, string UserType) 
        {
            this.UserID = UserID;
            this.UserType = UserType;  
        }
    }
}
