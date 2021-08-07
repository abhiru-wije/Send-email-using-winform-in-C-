using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;

namespace WinFormsApp4
{
    public partial class Form1 : Form
    {
        OpenFileDialog ofdAttachment;
        string fileName = " ";
        public Form1()
        {
            InitializeComponent();
        }

        private void Browse_click(object sender, MouseEventArgs e)
        {
            try
            {
                ofdAttachment = new OpenFileDialog();
                ofdAttachment.Filter = "Images(.jpg,.png)|*.png;*.jpg;|Pdf Files|*,pdf";
                if (ofdAttachment.ShowDialog() == DialogResult.OK)
                {
                    fileName = ofdAttachment.FileName;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void sendmail_click(object sender, MouseEventArgs e)
        {
            try
            {
                //smtp client details
                //gmail >> smtp server : smtp.gmail.com, port: 587, ssl required
                //yahoo >> smtp server : smtp.mail.yahoo.com, port:587, ssl reqiured
                SmtpClient clientDetail = new SmtpClient();
                clientDetail.Host = "smtp.gmail.com";
                clientDetail.Port = 587;
                clientDetail.EnableSsl = true;
                //clientDetail.Port = Convert.ToInt32(txtPortNumber.Text.Trim());
                //clientDetail.Host = txtSmtpServer.Text.Trim();
                //clientDetail.EnableSsl = cbxSSL.Checked;
                clientDetail.DeliveryMethod = SmtpDeliveryMethod.Network;
                clientDetail.UseDefaultCredentials = false;
                clientDetail.Credentials = new NetworkCredential(txtSenderEmail.Text.Trim(), txtSenderPassword.Text.Trim());

                //Message Details
                MailMessage mailDetails = new MailMessage();
                mailDetails.From = new MailAddress(txtSenderEmail.Text.Trim());
                mailDetails.To.Add(txtRecipientEmail.Text.Trim());
                //for multiple recients
                //maildetails.To.Add("Another recipient email address");
                //for bcc
                //mailDetails.Bcc.Add("bcc email address")
                mailDetails.Subject = txtSubject.Text.Trim();
                mailDetails.IsBodyHtml = cbxHtmlBody.Checked;
                mailDetails.Body = rtbBody.Text.Trim();

                //file attachments
                if(fileName.Length >0)
                {
                    Attachment attachment = new Attachment(fileName);
                    mailDetails.Attachments.Add(attachment);
                }
                clientDetail.Send(mailDetails);
                MessageBox.Show("Your mail has been sent");
                fileName = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



        }
    }
}
