using Classes_Banco;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class produtos : System.Web.UI.Page
{
    WS.e_commerceSoapClient WSE = new WS.e_commerceSoapClient();
    protected void Page_Load(object sender, EventArgs e)
    {
        List<Produte> produtos = new List<Produte>();

        //Definindo a cetegoria escolhida para busca de produtos:
        var categoria_busca = Request.QueryString["categoria"];
        var produto_busca = Request.QueryString["produto"];
        if (categoria_busca != null && categoria_busca != "Categoria" && categoria_busca != "ERRO")
        {
            //busca uma categoria especifica
            produtos = convert_list(WSE.listar_produtos_categoria(categoria_busca.ToString()));
        }
        else if (categoria_busca == "ERRO")
        {
            //possivel erro de busca
            lblERRO.Text = "ERRO NA BUSCA DE CATEGORIA!";
            Panel2.Controls.Add(new LiteralControl("<br>"));
            lblERRO.ForeColor = Color.Red;
        }
        else if (produto_busca != null)
        {
            //busca feito pelo txtbox
            produtos = convert_list(WSE.busca_produto_nome(produto_busca.ToString()));
        }
        else if (produto_busca == null)
        {
            //todos os produtos
            produtos = convert_list(WSE.listar_produtos());
        }

        //BOTOES DE CATEGORIAS PARA BUSCA
        DataSet categoria;
        categoria = WSE.listar_categoria();
        Panel2.Controls.Add(new LiteralControl("<div class=\"container text-center\">"));
        Panel2.Controls.Add(new LiteralControl("<div class=\"row justify-content-start\">"));
        Panel2.Controls.Add(new LiteralControl("<div class=\"col\">"));
        Button btn1 = new Button();
        btn1.ID = "Categoria";
        btn1.CssClass = "btn btn-secondary";
        btn1.Text = "Todos";
        btn1.Click += new EventHandler(btnCategoria_Click);
        Panel2.Controls.Add(btn1);
        Panel2.Controls.Add(new LiteralControl("</div>"));
        foreach (DataRow dr in categoria.Tables[0].Rows)
        {
            String nome_categoria = Convert.ToString(dr["categoria"]);
            Panel2.Controls.Add(new LiteralControl("<div class=\"col\">"));
            Button btn = new Button();
            btn.ID = nome_categoria;
            btn.CssClass = "btn btn-secondary";
            btn.Text = nome_categoria;
            btn.Click += new EventHandler(btnCategoria_Click);
            Panel2.Controls.Add(btn);
            Panel2.Controls.Add(new LiteralControl("</div>"));
        }
        Panel2.Controls.Add(new LiteralControl("</div>"));
        //Panel2.Controls.Add(new LiteralControl("</div>"));

        //Cards de Produtos:
        int i = 0;
        int x = 0;
        Panel1.Controls.Add(new LiteralControl("<div class=\"row\">"));
        Panel1.Controls.Add(new LiteralControl("<br></div>"));
        Panel1.Controls.Add(new LiteralControl("<div class=\"row justify-content-md-center\">"));
        foreach (Produte produto in produtos)
        {
            if (i == 3)
            {
                Panel1.Controls.Add(new LiteralControl("</div>" +
                    "<br>" +
                    "<div class=\"row justify-content-md-center\">"));
                i = 0;
            }
            Panel1.Controls.Add(new LiteralControl("<div class=\"col-md-auto\">" +
                "<div class=\"card\" style=\"width: 18rem;\">" +
                "<img class=\"card-img-top\" style=\"height:10rem\" src=\"" + produto.caminho_imagm + "\" alt=\"Imagem do " + produto.nome_produto + "\">" +
                "<div class=\"card - body\">" +
                "<h5 class=\"card-title\">" + produto.nome_produto + "</h5>" +
                "<p class=\"card-text\">" + produto.descricao + "</p>" +
                "<h5 class=\"card-title\">" + produto.preco_unitario.ToString("C") + "</h5>"));
            //Verifica se existe estoque
            if (produto.quantidade > 0)
            {
                Button btn = new Button();
                btn.ID = produto.id_produto.ToString();
                btn.CssClass = "btn btn-primary";
                btn.Text = "Adicionar ao Carrinho";
                btn.Click += new EventHandler(btnAddCarrinho_Click);
                btn.Attributes.Add("data-bs-toggle", "modal");
                btn.Attributes.Add("data-value", produto.id_produto.ToString());
                btn.Attributes.Add("data-bs-target", "#exampleModal");
                btn.Attributes.Add("href", "#");
                Panel1.Controls.Add(btn);
            }
            else
            {
                Panel1.Controls.Add(new LiteralControl("<h5 class=\"card-title\"> Produto Indisponivel </h5>"));
            }


            Panel1.Controls.Add(new LiteralControl("</div></div></div>"));

            i++;
            x++;
        }
        if (produtos.Count <= 0)
        {
            lblERRO.Text = "Sem Produtos dessa Categoria!";
            lblERRO.ForeColor = Color.Red;
        }
    }

    //Gera um Modal Personalizado
    protected void btnAddCarrinho_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        int id_produto = Convert.ToInt16(btn.Attributes["data-value"]);

        List<Produte> produtos = convert_list(WSE.busca_produto_id(id_produto));
        List<Endereco> enderecos = convert_list_endereco(WSE.busca_localidade(id_usuario()));

        Label1.Text = produtos[0].nome_produto.ToString();
        Label2.Text = produtos[0].descricao.ToString();
        Label3.Text = produtos[0].preco_unitario.ToString();

        btnConfirmar.Attributes["data-value"] = id_produto.ToString();
        ddlQuant.Items.Clear();
        ddlLocal.Items.Clear();
        foreach (Endereco endereco in enderecos)
        {
            ddlLocal.Items.Add(endereco.rua);
        }

        for (int i = 0; i <= produtos[0].quantidade; i++)
        {
            ddlQuant.Items.Add(i.ToString());
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal1();", true);
    }

    //Pegar o Id do usuario pelo cookie
    private int id_usuario()
    {
        return Convert.ToInt32(Server.HtmlEncode(Request.Cookies["id_usuario"].Value));
    }

    //Botao para fechar modal
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModal1();", true);
    }

    //Botao do Modal de Confirmacao de Carrinho
    protected void btnConfirma_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        int id_produto = Convert.ToInt16(btn.Attributes["data-value"]);
        List<Endereco> enderecos = convert_list_endereco(WSE.busca_localidade_rua(ddlLocal.SelectedValue, id_usuario()));
        WSE.insert_carrinho(enderecos[0].id_local, Convert.ToInt32(btn.Attributes["data-value"]), Convert.ToInt32(ddlQuant.SelectedValue));
        btnCancelar_Click(sender, e);
        lblERRO.Text = "Produto Inserido no Carrinho!";
        lblERRO.ForeColor = Color.Green;
    }

    //Botao de Escolha de Categoria
    protected void btnCategoria_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        Response.Redirect($"produtos.aspx?categoria={btn.ID}");
    }

    //Conversao de DataSet:
    private List<Produte> convert_list(DataSet banco)
    {
        List<Produte> produtos = new List<Produte>();
        foreach (DataRow dr in banco.Tables[0].Rows)
        {
            produtos.Add(new Produte
            {
                id_produto = Convert.ToInt32(dr["id_produto"]),
                nome_produto = Convert.ToString(dr["nome_produto"]),
                descricao = Convert.ToString(dr["descricao"]),
                categoria = Convert.ToInt32(dr["categoria"]),
                preco_unitario = Convert.ToDecimal(dr["preco_unitario"]),
                marca = Convert.ToString(dr["marca"]),
                data_lancamento = Convert.ToString(dr["data_lancamento"]),
                data_cadastro = Convert.ToString(dr["data_cadastro"]),
                quantidade = Convert.ToInt32(dr["quantidade"]),
                peso_gramas = Convert.ToSingle(dr["peso_gramas"]),
                caminho_imagm = Convert.ToString(dr["caminho_imagem"])
            });
        }
        return produtos;
    }
    private List<Endereco> convert_list_endereco(DataSet banco)
    {
        List<Endereco> enderecos = new List<Endereco>();
        foreach (DataRow dr in banco.Tables[0].Rows)
        {
            enderecos.Add(new Endereco
            {
                id_local = Convert.ToInt32(dr["id_local"]),
                id_usuario = Convert.ToInt32(dr["id_usuario"]),
                cep = Convert.ToString(dr["cep"]),
                pais = Convert.ToString(dr["pais"]),
                estado = Convert.ToString(dr["estado"]),
                cidade = Convert.ToString(dr["cidade"]),
                bairro = Convert.ToString(dr["bairro"]),
                rua = Convert.ToString(dr["rua"]),
                numero_residencia = Convert.ToInt32(dr["numero_residencia"]),
                complemento = Convert.ToString(dr["complemento"]),
            });
        }
        return enderecos;
    }
}