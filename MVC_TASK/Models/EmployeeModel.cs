using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_TASK.Models
{
    public class EmployeeModel
    {
        public int Employee_ID { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public DateTime Birth_Date { get; set; }
        public string Email_Address { get; set; }
        public string User_Password { get; set;}
        public string Confirm_Password { get; set; }
        

    }
}