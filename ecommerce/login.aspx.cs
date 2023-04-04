using Classes_Banco;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Web;

public partial class login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //verifica se foi pedido para lembrar o usuario
        if (Request.Cookies["lembra"] != null)
        {
            txtEmail.Text = Request.Cookies["email"].Value;
            txtSenha.Text = Request.Cookies["senha"].Value;
            btnEntrar_Click(sender, e);
        }
        //verifica se ele tento entrar em outra pagina antes de logar
        if (Request.Cookies["erro"] != null)
        {
            msgErro.Text = Request.Cookies["erro"].Value;
            msgErro.ForeColor = Color.Red;
            Request.Cookies["erro"].Expires = DateTime.Now.AddDays(-1);
        }        
    }
    protected void btnEntrar_Click(object sender, EventArgs e)
    {
        try
        {
            WS.e_commerceSoapClient WSE = new WS.e_commerceSoapClient();
            //verifica se o usuario existe
            if (WSE.verifica_usuario(txtEmail.Text, txtSenha.Text))
            {
                List<Usuario> usuario = new List<Usuario>();
                //cria cookie para uso em outras paginas
                usuario= convert_usuario_to_list(WSE.busca_usuario(txtEmail.Text));
                HttpCookie email = new HttpCookie("email", txtEmail.Text);
                Response.Cookies.Add(email);
                Response.Cookies.Add(new HttpCookie("senha", txtSenha.Text));
                Response.Cookies.Add(new HttpCookie("id_usuario", usuario[0].id_usuario.ToString()));
                if (cbLembre.Checked)
                {
                    Response.Cookies.Add(new HttpCookie("lembra", "lembrar"));
                }
                Session["email"] = txtEmail.Text;
                //manda o usuario para a pagina produtos (pag principal)
                Response.Redirect("produtos.aspx");
            }
            else
            {
                lblErro.Text = "Email ou Senha Incorreto!";
                lblErro.ForeColor = Color.Red;
            }
        }
        catch
        {
            lblErro.Text = "Email ou Senha Incorreto!";
            lblErro.ForeColor = Color.Red;
        }
    }
    private List<Usuario> convert_usuario_to_list(DataSet ds)
    {
        Usuario usuario = new Usuario();
        List<Usuario> pessoa = new List<Usuario>();
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            pessoa.Add(new Usuario
            {
                id_usuario = Convert.ToInt32(dr["id_usuario"]),
                nome = Convert.ToString(dr["nome"]),
                cpf = Convert.ToString(dr["cpf"]),
                email = Convert.ToString(dr["email"]),
                senha = Convert.ToString(dr["senha"]),
                tipo = Convert.ToInt16(dr["tipo"]),
            });
        }
        return pessoa;
    }
}