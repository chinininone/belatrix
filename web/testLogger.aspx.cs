using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogic;

namespace web
{
    public partial class testLogger : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void btnSend_Click(object sender, EventArgs e )
        {
            lblError.Text = "";
            try
            {
                new JobLogger(chkFile.Checked, chkConsole.Checked, chkDataBase.Checked);
                JobLogger.LogMessage(txtMessage.Text, chkMessage.Checked, chkWarning.Checked, chkError.Checked);
            }
            catch(Exception ex) {
                lblError.Text = "There were errors: " + ex.Message;
            }
        }
    }
}