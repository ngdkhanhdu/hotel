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
    public partial class frmDoiMatKhau : Form
    {
        SqlConnection conn = null;
        SqlCommand cmd = null;
        string strConnectionString = "Data Source = LAPTOP-8K7O0UIN; Database = QuanLyKhachSan; User Id = sa; Password = 123456789";
        void DoiMau()
        {
            txtUserName.BackColor = Color.LightGray;
            txtUserName.ForeColor = Color.Gray;
            txtUserName.ReadOnly = true;

            txtDisplay_Name.BackColor = Color.LightGray;
            txtDisplay_Name.ForeColor = Color.Gray;
            txtDisplay_Name.ReadOnly = true;
        }
        public frmDoiMatKhau()
        {
            InitializeComponent();
        }
        private void frmDoiMatKhau_Load(object sender, EventArgs e)
        {
            DoiMau();            
        }
        private void txtOld_Password_Leave(object sender, EventArgs e)
        {
            if (txtOld_Password.Text == "")
            {
                txtOld_Password.Text = "Mật khẩu cũ";
                txtOld_Password.PasswordChar = '\0';
                txtOld_Password.ForeColor = Color.Silver;
            }
        }
        private void txtOld_Password_Enter(object sender, EventArgs e)
        {
            if (txtOld_Password.Text == "Mật khẩu cũ")
            {
                txtOld_Password.Text = "";
                txtOld_Password.PasswordChar = '*';
                txtOld_Password.ForeColor = Color.Black;
            }
        }
        private void txtNew_Password_Leave(object sender, EventArgs e)
        {
            if (txtNew_Password.Text == "")
            {
                txtNew_Password.Text = "Mật khẩu mới";
                txtNew_Password.PasswordChar = '\0';
                txtNew_Password.ForeColor = Color.Silver;
                picBoxError_NewPass.Visible = true;
            }
        }
        private void txtNew_Password_Enter(object sender, EventArgs e)
        {
            if (txtNew_Password.Text == "Mật khẩu mới")
            {
                txtNew_Password.Text = "";
                txtNew_Password.PasswordChar = '*';
                txtNew_Password.ForeColor = Color.Black;
            }
        }
        private void txtConfirm_Password_Leave(object sender, EventArgs e)
        {
            if (txtConfirm_Password.Text == "")
            {
                txtConfirm_Password.Text = "Nhập lại mật khẩu mới";
                txtConfirm_Password.PasswordChar = '\0';
                txtConfirm_Password.ForeColor = Color.Silver;
                picBoxCheck_ConfirmPass.Visible = false;
                picBoxError_ConfirmPass.Visible = true;
            }
        }
        private void txtConfirm_Password_Enter(object sender, EventArgs e)
        {
            if (txtConfirm_Password.Text == "Nhập lại mật khẩu mới")
            {
                txtConfirm_Password.Text = "";
                txtConfirm_Password.PasswordChar = '*';
                txtConfirm_Password.ForeColor = Color.Black;
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void txtConfirm_Password_TextChanged(object sender, EventArgs e)
        {
            if(txtConfirm_Password.Text != "")
            {
                if (txtConfirm_Password.Text != txtNew_Password.Text)
                {
                    picBoxError_ConfirmPass.Visible = true;
                    picBoxCheck_ConfirmPass.Visible = false;
                }
                else
                {
                    if (txtConfirm_Password.Text == txtNew_Password.Text)
                    {
                        picBoxCheck_ConfirmPass.Visible = true;
                        picBoxError_ConfirmPass.Visible = false;
                    }                    
                }
            }
            else picBoxCheck_ConfirmPass.Visible = false;
        }
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            Error_XacNhan();
            DoiMatKhau();
        }
        void DoiMatKhau()
        {
            if (picBoxCheck_ConfirmPass.Visible == true && picBoxCheck_OldPass.Visible == true && picBoxError_NewPass.Visible == false)
            {
                cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "Update DangNhap set password = '" + txtNew_Password.Text + "' where username = '" + txtUserName.Text + "'";
                cmd.CommandType = CommandType.Text;
                conn.Open();
                int kq = cmd.ExecuteNonQuery();
                conn.Close();
                if (kq == 1)
                {
                    MessageBox.Show("Cập nhật mật khẩu thành công !");
                }
                else
                {
                    MessageBox.Show("Cập nhật KHÔNG thành công !");
                }
            }
        }
        void Error_XacNhan()
        {
            string st1 = "Bạn chưa xác nhận mật khẩu mới !",
                   st2 = "Bạn chưa nhập mật khẩu cũ !",
                   st3 = "Bạn nhập sai xác nhận mật khẩu mới !",
                   st4 = "Bạn nhập sai mật khẩu cũ !";
            if (picBoxError_ConfirmPass.Visible == true && picBoxError_OldPass.Visible == false && picBoxError_NewPass.Visible == false)
            {
                if (txtConfirm_Password.Text == "Nhập lại mật khẩu mới")
                {
                    MessageBox.Show(st1);
                }
                else MessageBox.Show(st3);

            }
            else if (picBoxError_ConfirmPass.Visible == false && picBoxError_OldPass.Visible == true && picBoxError_NewPass.Visible == false)
            {
                if (txtOld_Password.Text == "Mật khẩu cũ")
                {
                    MessageBox.Show(st2);
                }
                else MessageBox.Show(st4);
            }
            else if (picBoxError_ConfirmPass.Visible == true && picBoxError_OldPass.Visible == true && picBoxError_NewPass.Visible == false)
            {
                if (txtConfirm_Password.Text == "Nhập lại mật khẩu mới" && txtOld_Password.Text == "Mật khẩu cũ")
                {
                    MessageBox.Show(st2 + "\n" + st1);
                }
                else MessageBox.Show(st4 + "\n" + st3);
            }
            else if (picBoxError_NewPass.Visible == true)
            {
                if (txtNew_Password.Text == "" || txtNew_Password.Text == "Mật khẩu mới")
                {
                    MessageBox.Show("Bạn chưa nhập mật khẩu mới !");
                }
                else MessageBox.Show("Mật khẩu mới bị trùng !");
            }
            else if (txtNew_Password.Text == "Mật khẩu mới" && txtConfirm_Password.Text == "Nhập lại mật khẩu mới" && txtOld_Password.Text == "Mật khẩu cũ")
            {
                MessageBox.Show("Vui lòng điền thông tin vào form !");
                picBoxError_ConfirmPass.Visible = true;
                picBoxError_OldPass.Visible = true;
                picBoxError_NewPass.Visible = true;
            }
            
        }
            private void txtOld_Password_TextChanged(object sender, EventArgs e)
            { 
            if (txtOld_Password.Text != "")
            {
                conn = new SqlConnection(strConnectionString);
                cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "Select password From DangNhap Where username = '"+ txtUserName.Text +"'";
                cmd.CommandType = CommandType.Text;
                conn.Open();
                SqlDataReader reader1 = cmd.ExecuteReader();
                string checkPass = "";
                while (reader1.Read())
                {
                    checkPass = reader1.GetString(0);
                }
                conn.Close();
                checkPass = checkPass.Trim();
                if (checkPass == txtOld_Password.Text)
                {
                    picBoxCheck_OldPass.Visible = true;
                    picBoxError_OldPass.Visible = false;
                }
                else
                {
                    picBoxCheck_OldPass.Visible = false;
                    picBoxError_OldPass.Visible = true;
                }
            }
            else
            {
                picBoxCheck_OldPass.Visible = false;
                picBoxError_OldPass.Visible = false;
            }
        }

        private void txtNew_Password_TextChanged(object sender, EventArgs e)
        {
            if (txtNew_Password.Text != "")
            {
                if (txtNew_Password.Text == txtOld_Password.Text)
                {
                    picBoxError_NewPass.Visible = true;
                }
                else picBoxError_NewPass.Visible = false;
            }
            else picBoxError_NewPass.Visible = false;
        }
    }
}
