using System;
using System.Collections.Generic;
using System.Text;

namespace Prayug.Module.Core.Models
{
    public class fnd_user
    {
        public fnd_user()
        {

        }
        public int user_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string user_name { get; set; }
        public string user_role { get; set; }
        public string user_type { get; set; }
        public string web_role_access { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string user_image { get; set; }
        public string password { get; set; }

        public fnd_user(int user_id, string first_name, string last_name, string user_name, string user_role, string user_type, string email, string mobile, string city, string state, 
            string password)
        {
            this.user_id = user_id;
            this.first_name = first_name;
            this.last_name = last_name;
            this.user_name = user_name;
            this.user_role = user_role;
            this.user_type = user_type;
            this.email = email;
            this.mobile = mobile;
            this.city = city;
            this.state = state;
            this.password = password;
        }
    }
}
