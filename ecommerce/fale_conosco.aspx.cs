using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;
using System.Drawing;

public partial class fale_conosco : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnEnviarEmail_Click(object sender, EventArgs e)
    {
        try
        {
            SmtpClient mySmtpClient = new SmtpClient("my.smtp.exampleserver.net");
            mySmtpClient.UseDefaultCredentials = false;
            NetworkCredential basicAuthenticationInfo = new NetworkCredential("username", "password");
            mySmtpClient.Credentials = basicAuthenticationInfo;

            //email que vai mandar e que vai receber
            MailAddress from = new MailAddress(txtEmail.Text, txtNome.Text);
            MailAddress to = new MailAddress("vitorfreireparaty@gmail.com", "Vitor");
            MailMessage myMail = new MailMessage(from, to);

            //assunto do email
            myMail.Subject = txtAssunto.Text;
            myMail.SubjectEncoding = System.Text.Encoding.UTF8;

            //corpo do email
            myMail.Body = txtCorpo.Text;
            myMail.BodyEncoding = System.Text.Encoding.UTF8;
            myMail.IsBodyHtml = true;

            mySmtpClient.Send(myMail);
            email.Text = "Email Enviado com Sucesso!";
            email.ForeColor = Color.Green;
        }
        catch
        {
            email.Text = "Não foi possivel enviar o email!";
            email.ForeColor = Color.Red;
        }
        
    }
}