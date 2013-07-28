using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HIPSService
{
    public partial class CallWebService : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        private void RunPage()
        {
            string ssn = txtSSN.Value;
            DateTime dob = DateTime.Parse(txtDOB.Value);
           

            
        }

        
    }
}