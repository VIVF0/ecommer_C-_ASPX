using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using WS;
using Classes_Banco;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Data.Common;

public partial class admin : System.Web.UI.Page
{
    WS.e_commerceSoapClient WSE = new WS.e_commerceSoapClient();
    protected void Page_Load(object sender, EventArgs e)
    {
        //Verifica se o usuario tem permissao para entrar nessa pagina:
        MultiView1.ActiveViewIndex = 4;
        if (Session["email"] == null)
        {
            Response.Cookies.Add(new HttpCookie("erro", "Deve ser feito o login!"));
            Response.Redirect("login.aspx");
        }
        if (WSE.verifica_permissao(Request.Cookies["email"].Value) != true)
        {
            Response.Cookies.Add(new HttpCookie("erro", "Você não tem permissão suficiente!"));
            Response.Redirect("login.aspx");
        }
    }
    protected void btnEvent_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        //botao para a aba de cadastro de novo produto
        if (btn.ID == "btnCadProduto")
        {
            MultiView1.ActiveViewIndex = 0;
            DataSet categoria;
            categoria = WSE.listar_categoria();
            foreach (DataRow dr in categoria.Tables[0].Rows)
            {
                ddlProduto.Items.Add(Convert.ToString(dr["subcategoria"]));
            }
        }
        //botao para aba de cadastro de nova subcategoria
        else if (btn.ID == "btnCadCategoria")
        {
            MultiView1.ActiveViewIndex = 1;
        }
        //botao para aba para visualizar os usuarios do site
        else if (btn.ID == "btnUsuarios")
        {
            MultiView1.ActiveViewIndex = 2;
            gvResultadoUsuarios.DataSource = WSE.total_usuarios();
            gvResultadoUsuarios.DataBind();
            if (gvResultadoUsuarios.Rows.Count == 0)
            {
                lblusuario.Text = "Sem Usuarios no banco!";
                lblusuario.ForeColor = Color.Blue;
            }
        }
        //aba que mostrar a quantidade de venda por produto
        else if (btn.ID == "btnVendas")
        {
            MultiView1.ActiveViewIndex = 3;
            gvResultadoVendas.DataSource = WSE.total_vendas();
            gvResultadoVendas.DataBind();
            if (gvResultadoVendas.Rows.Count == 0)
            {
                lblVendas.Text = "Sem Vendas Realizadas!";
                lblVendas.ForeColor = Color.Blue;
            }
        }
        //aba de produtos cadaastrados
        else if (btn.ID == "btnProds")
        {
            MultiView1.ActiveViewIndex = 4;
            /*gvResultadoProds.DataSource = WSE.listar_produtos();
            gvResultadoProds.DataBind();
            if (gvResultadoProds.Rows.Count == 0)
            {
                lblProds.Text = "Sem Produtos Cadastrados!";
                lblProds.ForeColor = Color.Red;
            }*/
        }
        else
        {
            MultiView1.ActiveViewIndex = 5;
        }
    }

    //Nova Subcategoria
    protected void btnSubCategoriaEnviar_Click(object sender, EventArgs e)
    {
        string subcategoria = txtSubcategoria.Text.ToLower();
        MultiView1.ActiveViewIndex = 1;
        if (subcategoria == "" && txtCategoria.Text == "")
        {
            lblErroSubC.Text = "TODOS OS CAMPOS DEVEM SER PREENCHIDOS!";
            lblErroSubC.ForeColor = Color.Red;
        }
        else
        {
            if (WSE.verifica_subcategoria(subcategoria))
            {
                WSE.cadastra_subcategoria(subcategoria, txtCategoria.Text.ToLower());
                lblErroSubC.Text = "Cadastro feito com sucesso!";
                lblErroSubC.ForeColor = Color.Green;
            }
            else
            {
                lblErroSubC.Text = "ESSA SUBCATEGORIA JÁ EXISTE!";
                lblErroSubC.ForeColor = Color.Red;
            }
        }

    }

    //Novo Produto
    protected void btnCadastrarPRod_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 0;
        if (verificaCadastroProd())
        {
            int id_subcategoria = id_subcategoria_funci(ddlProduto.Text);
            WSE.cadastra_produto(txtNomeProd.Text,txtDescricaoProd.Text,id_subcategoria,
                Convert.ToDecimal(txtPrecoProd.Text),txtMarcaProd.Text,txtDataLancProd.Text,Convert.ToInt16(txtQuantProd.Text),
                Convert.ToSingle(txtPeso.Text),txtURLProd.Text);
        }else if(verificaCadastroProd()==false)
        {
            lblCadProd.Text = "Todos os Campos Devem ser Preenchidos";
            lblCadProd.ForeColor = Color.Red;
        }
    }

    //Conversao de dataset pra lista
    public List<Subcategoria> convert_categoria_to_list(DataSet ds)
    {
        List<Subcategoria> subcategoria = new List<Subcategoria>();
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            subcategoria.Add(new Subcategoria
            {
                id_subcategoria = Convert.ToInt32(dr["id_subcategoria"]),
                subcategoria = Convert.ToString(dr["subcategoria"]),
                categoria = Convert.ToString(dr["categoria"]),
            });
        }
        return subcategoria;
    }
    public int id_subcategoria_funci(String subcategoria)
    {
        List<Subcategoria> subcategorias = convert_categoria_to_list(WSE.listar_categoria());
        int id_subcategoria = new int();
        foreach (Subcategoria id_categoria in subcategorias)
        {
            if (id_categoria.subcategoria == subcategoria)
            {
                id_subcategoria = id_categoria.id_subcategoria;
            }
        }
        return id_subcategoria;
    }

    //verifica se todos os campos de cadastro foram preenchidos
    private Boolean verificaCadastroProd()
    {
        if (txtNomeProd.Text == "")
        {
            return false;
        }
        else if(txtNomeProd.Text == "")
        {
            return false;
        }
        else if (txtPrecoProd.Text == "")
        {
            return false;
        }
        else if(ddlProduto.SelectedItem== null)
        {
            return false;
        }
        else if (txtMarcaProd.Text == "")
        {
            return false;
        }
        else if (txtDataLancProd.Text == "")
        {
            return false;
        }
        else if (txtQuantProd.Text == "")
        {
            return false;
        }
        else if (txtPeso.Text == "")
        {
            return false;
        }
        else if (txtURLProd.Text == "")
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public void SqlDataSource1_Deleting(Object source, SqlDataSourceCommandEventArgs e)
    {
        MultiView1.ActiveViewIndex = 4;
    }
    public void SqlDataSource1_Updating(Object source, SqlDataSourceCommandEventArgs e)
    {
        MultiView1.ActiveViewIndex = 4;
    }
    protected void OnSqlDeleted(object sender, SqlDataSourceStatusEventArgs e)
    {
        MultiView1.ActiveViewIndex = 4;
    }
    public void OnSqlUpdated(Object source, SqlDataSourceStatusEventArgs e)
    {
        MultiView1.ActiveViewIndex = 4;
    }
}