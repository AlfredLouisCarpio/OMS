using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using OMS.forms;

namespace OMS
{
    public partial class frm_login : Form
    {
        public frm_login()
        {
            InitializeComponent();


        }
        SqlConnection con = new SqlConnection("Data Source=CSLAB450\\SQLEXPRESS;Initial Catalog=OMS;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter adapter = new SqlDataAdapter();

        private void chbkShowPass_CheckedChanged(object sender, EventArgs e)
        {
            if (chbkShowPass.Checked)
            {
                txtPassword.PasswordChar = '\0';
            }
            else
            {
                txtPassword.PasswordChar = '*';
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtUsername.Text = "";
            cmbRole.Text = "";
            txtPassword.Text = "";
            txtUsername.Focus();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            new frmRegister().Show();
            this.Hide();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            con.Open();
            string login = "SELECT * FROM User_Account WHERE username = @Username AND password = @Password AND role = @Role";
            SqlCommand cmd = new SqlCommand(login, con);
            cmd.Parameters.AddWithValue("@Username", txtUsername.Text);
            cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
            cmd.Parameters.AddWithValue("@Role", cmbRole.Text);
            SqlDataReader dr = cmd.ExecuteReader();

            if (txtUsername.Text == "" || txtPassword.Text == "" || cmbRole.Text == "")
            {
                MessageBox.Show("Please fill in all the fields.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (dr.Read())
            {
                string storedUsername = dr["Username"].ToString();
                string storedPassword = dr["Password"].ToString();
                string storedRole = dr["Role"].ToString();

                if (storedUsername != txtUsername.Text && storedPassword != txtPassword.Text && storedRole != cmbRole.Text)
                {
                    MessageBox.Show("Incorrect username, password, and role. Please try again.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (storedUsername != txtUsername.Text && storedPassword != txtPassword.Text)
                {
                    MessageBox.Show("Incorrect username and password. Please try again.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (storedPassword != txtPassword.Text && storedRole != cmbRole.Text)
                {
                    MessageBox.Show("Incorrect password and role. Please try again.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (storedUsername != txtUsername.Text && storedRole != cmbRole.Text)
                {
                    MessageBox.Show("Incorrect username and role. Please try again.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (storedUsername != txtUsername.Text)
                {
                    MessageBox.Show("Incorrect username. Please try again.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (storedPassword != txtPassword.Text)
                {
                    MessageBox.Show("Incorrect password. Please try again.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (storedRole != cmbRole.Text)
                {
                    MessageBox.Show("Incorrect role. Please try again.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (storedRole.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                    {
                        this.Hide();
                        Admin_Dashboard AD = new Admin_Dashboard();
                        AD.Show();
                    }
                    else if (storedRole.Equals("Seller", StringComparison.OrdinalIgnoreCase))
                    {
                        this.Hide();
                        Seller_Dashboard SD = new Seller_Dashboard();
                        SD.Show();
                    }
                    else if (storedRole.Equals("Buyer", StringComparison.OrdinalIgnoreCase))
                    {
                        this.Hide();
                        Buyer_Dashboard BD = new Buyer_Dashboard();
                        BD.Show();
                    }
                }
            }
            else
            {
                MessageBox.Show("Incorrect username, password, or role. Please try again.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            con.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void frm_login_Load(object sender, EventArgs e)
        {
            cmbRole.Items.Add("Admin");
            cmbRole.Items.Add("Seller");
            cmbRole.Items.Add("Buyer");
        }
    }
}
