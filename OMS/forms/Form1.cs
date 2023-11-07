using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OMS
{
    public partial class frmRegister : Form
    {
        public frmRegister()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Data Source=CSLAB450\\SQLEXPRESS;Initial Catalog=OMS;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter adapter = new SqlDataAdapter();

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtPassword_Click(object sender, EventArgs e)
        {

        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text == "" || cmbRole.Text == "" || txtPassword.Text == "" || txtConPass.Text == "")
            {
                MessageBox.Show("Username, Role, or Password fields are empty", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!char.IsUpper(txtUsername.Text, 0))
            {
                MessageBox.Show("Username must start with a capital letter", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (txtPassword.Text == txtConPass.Text)
            {
                con.Open();
                string register = "INSERT INTO User_Account VALUES (@Username,@Password, @Role)";
                SqlCommand cmd = new SqlCommand(register, con);
                cmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
                cmd.Parameters.AddWithValue("@Role", cmbRole.Text);
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Your Account has been Successfully Created", "Registration Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Password does not match, Please Re-enter", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Text = "";
                txtConPass.Text = "";
                txtPassword.Focus();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtUsername.Text = "";
            cmbRole.Text = "";
            txtPassword.Text = "";
            txtConPass.Text = "";
            txtUsername.Focus();

        }

        private void label7_Click(object sender, EventArgs e)
        {
            new frm_login().Show();
            this.Hide();
        }

        private void cmbRole_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void frmRegister_Load(object sender, EventArgs e)
        {
            cmbRole.Items.Add("Admin");
            cmbRole.Items.Add("Seller");
            cmbRole.Items.Add("Buyer");
        }

        private void chbkShowPass_CheckedChanged(object sender, EventArgs e)
        {
            if (chbkShowPass.Checked)
            {
                txtPassword.PasswordChar = '\0';
                txtConPass.PasswordChar = '\0';
            }
            else
            {
                txtPassword.PasswordChar = '*';
                txtConPass.PasswordChar = '*';
            }
        }
    }
}
