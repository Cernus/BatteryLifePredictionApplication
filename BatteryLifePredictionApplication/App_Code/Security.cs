using AppFacade;
using AppFacade.Models;
using System;
using System.Web;
using System.Web.Security;

namespace BatteryLifePredictionApplication.App_Code
{
    // Handles authentication of Users and redirects the User to the homepage if they attempt to access a page they do not have permission to view
    public static class Security
    {
        private static readonly int adminId = 1;

        // Signs In the User
        public static void Authenticate(string username, string password)
        {
            Facade facade = new Facade();

            // Get row that has matching username and password
            UserDto user = new UserDto();
            try
            {
                user = facade.GetUser(username, password);
            }
            catch
            {
                HttpContext.Current.Response.Redirect("LogIn?Message=InvalidCredentials");
            }

            // Error if credentials are wrong
            if (user != null)
            {
                // Error if user has been deleted
                if (facade.UserIsActive(user.UserId))
                {
                    FormsAuthentication.RedirectFromLoginPage(user.UserId.ToString(), true);
                }
                else
                {
                    HttpContext.Current.Response.Redirect("LogIn?Message=InactiveUser");
                }
            }
            else
            {
                HttpContext.Current.Response.Redirect("LogIn?Message=InvalidCredentials");
            }
        }

        // Signs Out the User
        public static void SignOut()
        {
            FormsAuthentication.SignOut();
            RedirectToHomePage();
        }

        // Redirects the User to the home page if they are not a guest
        public static void RedirectIfNotGuest()
        {
            bool isLoggedIn = IsLoggedIn();

            if (isLoggedIn && !IsAdmin())
            {
                RedirectToHomePage();
            }
        }

        // Redirects the User to the home page if they are a guest
        public static void RedirectIfIsGuest()
        {
            bool isLoggedIn = IsLoggedIn();

            if (!isLoggedIn)
            {
                RedirectToHomePage();
            }
        }

        // Redirects the User to the home page if they are not the admin
        public static void RedirectIfNotAdmin()
        {
            bool isAdmin = IsAdmin();

            if (!isAdmin)
            {
                RedirectToHomePage();
            }
        }

        // Redirects the User to the home page if the current url does not contain 'id' as a query string
        public static void RedirectIfNoQueryString()
        {
            try
            {
                int feedbackId = Int32.Parse(HttpContext.Current.Request.QueryString["id"]);
            }
            catch
            {
                RedirectToHomePage();
            }
        }

        // Redirects the User to the home page
        public static void RedirectToHomePage()
        {
            HttpContext.Current.Response.Redirect("Default");
        }

        // Redirect to home page if current User's Id does not equal the Id in the query string
        public static void RedirectIfUserIdMismatch()
        {
            if (!IsAdmin())
            {
                int userId = 0;
                try
                {
                    userId = Int32.Parse(HttpContext.Current.Request.QueryString["id"]);
                }
                catch
                {
                    RedirectToHomePage();
                }

                // Get userId of current user
                int userId_Current = Int32.Parse(HttpContext.Current.User.Identity.Name);

                // Redirect is they do not match
                if (userId != userId_Current)
                {
                    RedirectToHomePage();
                }
            }
        }

        // Redirect to home page if current User's Id does not equal the input User id
        public static void RedirectIfUserIdMismatch(int? userId)
        {
            if (!IsAdmin())
            {
                if (userId == null)
                {
                    RedirectToHomePage();
                }

                // Get userId of current user
                int userId_Current = 0;
                try
                {
                    userId_Current = Int32.Parse(HttpContext.Current.User.Identity.Name);
                }
                catch
                {
                    RedirectToHomePage();
                }

                // Redirect is they do not match
                if (userId != userId_Current)
                {
                    RedirectToHomePage();
                }
            }
        }

        // Redirect to home page if the User associated with with UserId is not Active
        public static void RedirectIfUserIsNotActive()
        {
            int userId = Int32.Parse(Security.GetQueryString());

            Facade facade = new Facade();
            UserDto user = facade.GetUser(userId);

            if (user == null)
            {
                RedirectToHomePage();
            }
        }

        // Redirect to home page if the Batch associated with with BatchId is not Active
        public static void RedirectIfBatchIsNotActive()
        {
            int batchId = Int32.Parse(Security.GetQueryString());

            Facade facade = new Facade();
            BatchDto batch = facade.GetBatch(batchId);

            if (batch == null)
            {
                RedirectToHomePage();
            }
        }

        // Get the User Id of the current User
        public static int GetCurrentUserId()
        {
            int userId = Int32.Parse(HttpContext.Current.User.Identity.Name);
            return userId;
        }

        // Get the value of the query string 'id'
        public static string GetQueryString()
        {
            try
            {
                string qs = HttpContext.Current.Request.QueryString["id"];
                return qs;
            }
            catch
            {
                RedirectToHomePage();
            }

            return null;
        }

        // Get the value of the query string passed in as a string
        public static string GetQueryString(string str)
        {
            try
            {
                string qs = HttpContext.Current.Request.QueryString[str];
                return qs;
            }
            catch
            {
                RedirectToHomePage();
            }

            return null;
        }

        // Return true if current User is the Admin
        public static bool IsAdmin()
        {
            try
            {
                int userId = Int32.Parse(HttpContext.Current.User.Identity.Name);
                return userId == adminId;
            }
            catch
            {
                return false;
            }
        }

        // Return true if the current User is logged in
        public static bool IsLoggedIn()
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}