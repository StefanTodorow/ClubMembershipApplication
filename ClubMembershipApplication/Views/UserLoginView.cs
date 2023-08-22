using ClubMembershipApplication.Data;
using ClubMembershipApplication.FieldValidators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubMembershipApplication.Views
{
    public class UserLoginView : IView
    {
        ILogin _loginUser = null;
        public IFieldValidator FieldValidator => null;

        public UserLoginView(ILogin login) 
        {
            _loginUser = login;
        }

        public void RunView()
        {
            CommonOutputText.WriteMainHeading();
            CommonOutputText.WriteLoginHeading();

            Console.WriteLine("Please enter your email address");
            string emailAddress = Console.ReadLine();

            Console.WriteLine("Please enter your password");
            string password = Console.ReadLine();

            var user = _loginUser.Login(emailAddress, password);

            if (user != null)
            {
                IView welcomeUserView = new WelcomeUserView(user);
                welcomeUserView.RunView();
            }
            else
            {
                Console.Clear();

                CommonOutputFormat.ChangeFontColor(CommonOutputFormat.FontTheme.Danger);
                Console.WriteLine("The entered credentials do not match our records");
                CommonOutputFormat.ChangeFontColor(CommonOutputFormat.FontTheme.Default);

                Console.ReadKey();
            }
        }
    }
}
