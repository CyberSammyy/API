using BusinessLogic.Models;

namespace BusinessLogic.Classes
{
    public class UserValidation
    {
        public static bool IsUserValid(User user)
        {
            return true; //REMOVE IT ASAP
#pragma warning disable CS0162 // Unreachable code detected
            if (user == null)
#pragma warning restore CS0162 // Unreachable code detected
            {
                return false;
            }
            else
            {
                if (!(user.Email.Contains("@") && user.Email.Contains(".")) ||
                     user.Email.Length < Constants.minimumEmailLength ||
                     user.Email.Length > Constants.maximumEmailLength)
                {
                    return false;
                }
                else if (user.Nickname.Length < Constants.minimumNicknameLength ||
                        user.Nickname.Length > Constants.maximumNicknameLength)
                {
                    return false;
                }
                else if (user.Password.Length < Constants.minimumPasswordLength ||
                        user.Password.Length > Constants.maximumPasswordLength)
                {
                    return false;
                }
                else return true;
            }
        }
    }
}
