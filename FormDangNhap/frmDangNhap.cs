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

namespace FormDangNhap
{ 
    public partial class frmDangNhap : Form
    {
        SqlConnection conn = null;
        SqlCommand cmd = null;
        DataSet ds = null;
        string strConnectionString = "Data Source = LAPTOP-8K7O0UIN; Database = QuanLyKhachSan; User Id = sa; Password = 123456789";
        public int xacnhan = 0;
        private void Login(string username, string pass)
        {
            try
            {
                conn = new SqlConnection(strConnectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM DangNhap WHERE username ='" + username + "' and password ='" + pass + "'", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        MessageBox.Show("Đăng nhập thành công");
                        xacnhan = 1;
                    }
                }
                
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi truy vấn dữ liệu hoặc kết nối với server thất bại !");
            }
            finally
            {
                conn.Close();
            }
        }
            public frmDangNhap()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void txtUserName_Leave(object sender, EventArgs e)
        {
            if (txtUserName.Text == "")
            {
                txtUserName.Text = "Tên đăng nhập";
                txtUserName.ForeColor = Color.Silver;
            }
        }

        private void txtUserName_Enter(object sender, EventArgs e)
        {
            if (txtUserName.Text == "Tên đăng nhập")
            {
                txtUserName.Text = "";
                txtUserName.ForeColor = Color.Black;
            }
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            if (txtPassword.Text == "Mật khẩu")
            {
                txtPassword.Text = "";
                txtPassword.PasswordChar = '*';
                txtPassword.ForeColor = Color.Black;
            }
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            if (txtPassword.Text == "")
            {
                txtPassword.Text = "Mật khẩu";
                txtPassword.PasswordChar = '\0';
                txtPassword.ForeColor = Color.Silver;
            }
        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Close();
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            Login(txtUserName.Text, txtPassword.Text);
            if (xacnhan != 1)
            {
                MessageBox.Show("Đăng nhập thất bại !");
            }
        }
    }
}
