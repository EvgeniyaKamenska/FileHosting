using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FileHosting.ServiceReference1;

namespace FileHosting
{
    public partial class LogonForm : Form
    {
        public User currUser;
        ServiceReference1.FileHostingServiceClient client = new ServiceReference1.FileHostingServiceClient();
        
        public LogonForm()
        {
            InitializeComponent();
        }

        private async void  btnLogOn_Click(object sender, EventArgs e)
        {
            btnLogOn.Enabled = false;
            currUser = new User();
            currUser.Email = this.txtEmail.Text;
            currUser.Login = this.txtUsername.Text;
            currUser.Password = this.txtPassword.Text;
            if (chBNewUser.Checked)
            {
                currUser = await client.RegistrNewUserAsync(currUser);
                if (currUser == null)
                {
                    btnLogOn.Enabled = true;
                    this.labelError.Text = "Username exist";
                }
                else
                {
                    MainForm mf = this.Owner as MainForm;
                    mf.currentUser = currUser;
                    mf.Update();
                    this.Close();
                }
                
            }
            else
            {
                currUser = await client.CheckEnterUserPasswordAsync(this.txtUsername.Text, this.txtPassword.Text);
                if (currUser != null)
                {
                    MainForm mf = this.Owner as MainForm;
                    mf.currentUser = currUser;
                    mf.Update();
                   this.Close();
                }
                else 
                {
                    btnLogOn.Enabled = true;
                    this.labelError.Text = "User name not found or incorrect password";
                }
            }

            btnLogOn.Enabled = true;
        }
    }
}
