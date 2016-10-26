using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SendMessage
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Send_Click(object sender, EventArgs e)
        {
            string SendAdr = this.SendAdress.Text;
            string SendPer = this.SendPerson.Text;
            string SendCon = this.SendContent.Text;
            string SendTil = this.SendTiltle.Text;
            Boolean flag = true;
            if (string.IsNullOrEmpty(SendAdr.Trim()) || string.IsNullOrEmpty(SendPer.Trim()) || string.IsNullOrEmpty(SendCon.Trim()) || string.IsNullOrEmpty(SendTil.Trim()))
            {
                MessageBox.Show("请完善所有信息！");
            }
            else
            {
                Regex regex = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
                if (!regex.IsMatch(SendPer.Trim()) || !regex.IsMatch(SendAdr.Trim()))
                {
                    MessageBox.Show("Email不合法,请重新输入!");
                }
                else
                {
                    System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
                    client.Host = "";//使用163的SMTP服务器发送邮件
                    client.UseDefaultCredentials = false;
                    client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;//通过网络发送到SMTP服务器
                    client.Credentials = new System.Net.NetworkCredential("", "");//163的SMTP服务器需要用163邮箱的用户名和密码作认证，如果没有需要去163申请个,                                                                         
                    //这里假定你已经拥有了一个163邮箱的账户，用户名为abc，密码为******* 
                    System.Net.Mail.MailMessage Message = new System.Net.Mail.MailMessage();
                    Message.From = new System.Net.Mail.MailAddress(SendPer);//这里需要注意，163似乎有规定发信人的邮箱地址必须是163的，而且发信人的邮箱用户名必须和上面SMTP服务器认证时的用户名相同                                                               
                    //因为上面用的用户名abc作SMTP服务器认证，所以这里发信人的邮箱地址也应该写为abc@163.com
                    Message.To.Add(SendAdr);//将邮件发送给Gmail
                    Message.Subject = SendTil;//邮件主题
                    Message.Body = SendCon;//邮件内容
                    Message.SubjectEncoding = System.Text.Encoding.UTF8;//编码
                    Message.BodyEncoding = System.Text.Encoding.UTF8;
                    Message.Priority = System.Net.Mail.MailPriority.High;//设置邮件优先级
                    Message.IsBodyHtml = true;//设置为HTML格式
                    try
                    {
                        client.Send(Message);

                    }
                    catch
                    {
                        flag = false;

                    }
                    if (flag)
                    {
                        MessageBox.Show("发送成功O(∩_∩)O~");
                    }
                    else
                    {
                        MessageBox.Show("发送失败/(ㄒoㄒ)/~~");
                    }
                }
            }
        }
    }
}
