using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using Classes_Banco;
using System.Windows.Forms;
using System.Data.SqlClient;

public partial class MasterPage : System.Web.UI.MasterPage
{
    WS.e_commerceSoapClient WSE = new WS.e_commerceSoapClient();
    protected void Page_Load(object sender, EventArgs e)
    {
        //verifica se o usuario esta logado
        //se tiver logado muda o texto e url de login para logout
        if (Session["email"] != null)
        {
            hlLogin.Text = "Logout";
            hlLogin.NavigateUrl = "logout.aspx";
        }
        else
        {
            hlLogin.Text = "Login";
        }
        try
        {
            //adicionas os itens de categoria ao dropdawnlist de categorias
            ddl1.Items.Clear();
            DataSet categoria;
            ddl1.Items.Add("Todos");
            categoria = WSE.listar_categoria();
            foreach (DataRow dr in categoria.Tables[0].Rows)
            {
                ddl1.Items.Add(Convert.ToString(dr["categoria"]));
            }
        }
        catch
        {
            
        }
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        //faz o envio do usuario para a pagina produtos com a url para selecao de um tipo de produto
        try
        {
            DataSet categoria;
            categoria = WSE.listar_categoria();
            foreach (DataRow dr in categoria.Tables[0].Rows)
            {
                if (ddl1.Text == (Convert.ToString(dr["categoria"])))
                {
                    Response.Redirect($"produtos.aspx?categoria={ddl1.Text}");
                }
            }
            if (ddl1.Text == "Todos")
            {
                Response.Redirect($"produtos.aspx?categoria=Categoria");
            }
        }
        catch
        {
            
        }
    }

    //faz envio para a pagina produtos buscando um produto escrito na barra de busca
    protected void SearchButton_Click(object sender, EventArgs e)
    {
        Response.Redirect($"produtos.aspx?produto={SearchBox.Text}");
    }
}
