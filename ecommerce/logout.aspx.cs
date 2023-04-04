using System;

public partial class logout : System.Web.UI.Page
{
    //quebra os cookie :) KKKKK
    //fecha a sessao do usuario
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cookies["email"].Expires = DateTime.Now.AddDays(-1);
        Response.Cookies["senha"].Expires = DateTime.Now.AddDays(-1);
        Response.Cookies["lembra"].Expires = DateTime.Now.AddDays(-1);
        Response.Cookies["id_usuario"].Expires = DateTime.Now.AddDays(-1);
        Request.Cookies["erro"].Expires = DateTime.Now.AddDays(-1);
        Session.Abandon();
        Response.Redirect("~/login.aspx");
    }
}