using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test_Mot_Chut
{
    public partial class FormVerifyCode : Form
    {
        ConnectionStringSettings conSet = ConfigurationManager.ConnectionStrings["MyDBConnectionString"];
        string verifyCode;
        public static string to;

        public FormVerifyCode()
        {
            InitializeComponent();
        }

        private void btnGui_Click(object sender, EventArgs e)
        {
            string source = conSet.ConnectionString;
            SqlConnection sqlCon = new SqlConnection(source);
            sqlCon.Open();

            while (true)
            {
                int dem_Email = 0;

                SqlCommand cmd_CheckTenDangNhap = new SqlCommand("KiemTraEmail", sqlCon);
                cmd_CheckTenDangNhap.CommandType = CommandType.StoredProcedure;
                cmd_CheckTenDangNhap.Parameters.AddWithValue("@Email", SqlDbType.Char).Value = txtEmail.Text.Trim();
                SqlDataReader dr_CheckTenDangNhap;
                dr_CheckTenDangNhap = cmd_CheckTenDangNhap.ExecuteReader();

                while (dr_CheckTenDangNhap.Read())
                {
                    dem_Email += 1;
                }

                if (dem_Email < 1)
                {
                    errNhapThieu.SetError(txtEmail, "Email không tồn tại");
                    break;
                }

                if (txtEmail.Text == "Email")
                {
                    errNhapThieu.SetError(txtEmail, "Chưa nhập email");
                    break;
                }

                dr_CheckTenDangNhap.Close();

                string from, pass, messageBody;
                Random rand = new Random();
                verifyCode = (rand.Next(999999)).ToString();
                MailMessage message = new MailMessage();
                to = (txtEmail.Text).ToString();
                from = "o868994610@gmail.com";
                pass = "canhsatpoliceman";
                messageBody = "Your verify code is: " + verifyCode;
                message.To.Add(to);
                message.From = new MailAddress(from);
                message.Body = messageBody;
                message.Subject = "password reseting code";
                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                smtp.EnableSsl = true;
                smtp.Port = 587;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential(from, pass);
                try
                {
                    smtp.Send(message);
                    MessageBox.Show("Mã xác nhận gửi thành công");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                break;
            }
            sqlCon.Close();
        }

        private void btnNhap_Click(object sender, EventArgs e)
        {
            if (verifyCode == (txtVerifyCode.Text).ToString())
            {
                to = txtEmail.Text;
                FormChangePassWord cpw = new FormChangePassWord();
                cpw.ShowDialog();
                this.Visible = true;
            }
            else if (txtVerifyCode.Text == "Mã Xác Nhận")
            {
                errNhapThieu.SetError(txtVerifyCode, "Chưa nhập mã xác nhận");
            }
            else
            {
                MessageBox.Show("Nhập sai mã");
            }
        }

        private void btnTat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region Nhấp, rời chuột khỏi các textbox
        private void txtEmail_Leave(object sender, EventArgs e)
        {
            if (txtEmail.Text == "")
            {
                txtEmail.Text = "Email";
            }
        }

        private void txtEmail_Enter(object sender, EventArgs e)
        {
            if (txtEmail.Text == "Email")
            {
                txtEmail.Text = "";
            }
        }

        private void txtVerifyCode_Leave(object sender, EventArgs e)
        {
            if (txtVerifyCode.Text == "")
            {
                txtVerifyCode.Text = "Mã Xác Nhận";
            }
        }

        private void txtVerifyCode_Enter(object sender, EventArgs e)
        {
            if (txtVerifyCode.Text == "Mã Xác Nhận")
            {
                txtVerifyCode.Text = "";
            }
        }
    }
    #endregion
}

