using Microsoft.AspNetCore.Identity;

namespace TicketShopAdminApp.Models
{
    public class TicketShopAppUser
    {
        public  string UserName
        {
            get;
            set;
        }

       
        public  string NormalizedUserName
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }

        public  string NormalizedEmail
        {
            get;
            set;
        }

    }
}
