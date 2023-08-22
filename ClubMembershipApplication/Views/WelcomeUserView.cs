using ClubMembershipApplication.FieldValidators;
using ClubMembershipApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubMembershipApplication.Views
{
    public class WelcomeUserView : IView
    {
        User _user = null;

        public WelcomeUserView(User user)
        {
            _user = user;
        }

        public IFieldValidator FieldValidator => null;

        public void RunView()
        {
            CommonOutputFormat.ChangeFontColor(CommonOutputFormat.FontTheme.Success);
            Console.WriteLine($"Hello {_user.FirstName}!{Environment.NewLine}Welcome to the Cycling club.");
            CommonOutputFormat.ChangeFontColor(CommonOutputFormat.FontTheme.Default);
            Console.ReadKey();
        }
    }
}
