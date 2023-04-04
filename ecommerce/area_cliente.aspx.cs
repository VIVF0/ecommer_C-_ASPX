using Classes_Banco;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;

public partial class area_cliente : System.Web.UI.Page
{
    WS.e_commerceSoapClient WSE = new WS.e_commerceSoapClient();
    //lista de UFs do brasil
    string[] ufs = new string[] { "AC", "AL", "AP", "AM", "BA", "CE", "DF", "ES", "GO", "MA", "MT", "MS", "MG", "PA", "PB", "PR", "PE", "PI", "RJ", "RN", "RS", "RO", "RR", "SC", "SP", "SE", "TO" };
    protected void Page_Load(object sender, EventArgs e)
    {
        MT1.ActiveViewIndex = 3;
        if (Session["email"] == null)
        {
            Response.Cookies.Add(new HttpCookie("erro", "Deve ser feito o login!"));
            Response.Redirect("login.aspx");
        }
    }
    //pega o id do usuario pelo cookie
    private int id_usuario()
    {
        return Convert.ToInt32(Server.HtmlEncode(Request.Cookies["id_usuario"].Value));
    }

    protected void btnEvent_Click(object sender, EventArgs e)
    {
        int id = id_usuario();
        Button btn = (Button)sender;
        //mostra os pedidos do usuario:
        if (btn.ID == "btnPedidos")
        {
            MT1.ActiveViewIndex = 0;
            gvPedidos.DataSource = WSE.pedido_usuario(id);
            gvPedidos.DataBind();
            if (gvPedidos.Rows.Count == 0)
            {
                lblErroPedidos.Text = "Sem Pedidos Feitos!";
                lblErroPedidos.ForeColor = Color.Blue;
            }

        }
        //mostra a aba de endereco:
        else if (btn.ID == "btnEndereco")
        {
            MT1.ActiveViewIndex = 1;
        }
        //mostra o carrinho do usuario
        else if (btn.ID == "btnCarrinho")
        {
            MT1.ActiveViewIndex = 2;
            gvCarrinho.DataSource = WSE.carrinho_usuario(id);
            gvCarrinho.DataBind();
            if (gvCarrinho.Rows.Count == 0)
            {
                lblCarrinho.Text = "Sem Itens no Carrinho!";
                lblCarrinho.ForeColor = Color.Blue;
                btnCarrinhoCancelado.Visible = false;
                btnCarrinhoFechado.Visible = false;
            }
        }
        else
        {
            MT1.ActiveViewIndex = 3;
        }
    }
    //abas de endereco
    protected void btnEvent_Click02(object sender, EventArgs e)
    {
        MT1.ActiveViewIndex = 1;
        MVEndereco.ActiveViewIndex = 0;
        Button btn = (Button)sender;
        //novo endereco
        if (btn.ID == "btnNovEnd")
        {
            MVEndereco.ActiveViewIndex = 0;
            foreach (String uf in ufs)
            {
                ddlEstados.Items.Add(uf);
            }
        }
        //enderecos registrados
        else if (btn.ID == "btnEnderecos")
        {
            MVEndereco.ActiveViewIndex = 1;
            gvEndereco.DataSource = WSE.busca_localidade(id_usuario());
            gvEndereco.DataBind();
            if (gvEndereco.Rows.Count == 0)
            {
                lblEndereco.Text = "Sem Endereços Cadastrados!";
                lblEndereco.ForeColor = Color.Blue;
            }
        }
    }
    //botao de cadastro de enderecos
    protected void btnCadEndereco_Click(object sender, EventArgs e)
    {
        MT1.ActiveViewIndex = 1;
        MVEndereco.ActiveViewIndex = 0;
        int id = id_usuario();
        if (verifica_cad_endereco())
        {
            if (txtComplemento.Text == "")
            {
                WSE.cadastra_localidade(id, txtCEP.Text, txtPais.Text, ddlEstados.SelectedValue,
                txtCidade.Text, txtBairro.Text, txtRua.Text, Convert.ToInt16(txtNumero_residencia.Text), null);
            }
            else
            {
                WSE.cadastra_localidade(id, txtCEP.Text, txtPais.Text, ddlEstados.SelectedValue,
                txtCidade.Text, txtBairro.Text, txtRua.Text, Convert.ToInt16(txtNumero_residencia.Text), txtComplemento.Text);
            }
            lblErroCadEnd.Text = "Endereço Cadastrado";
            lblErroCadEnd.ForeColor = Color.Green;
            txtBairro.Text = ""; txtCEP.Text = ""; txtCidade.Text = ""; txtComplemento.Text = "";
            txtNumero_residencia.Text = ""; txtPais.Text = ""; txtRua.Text = "";
        }
        else
        {
            lblErroCadEnd.Text = "Todos os Campos devem ser preenchidos **(menos o Complemento que é opcional)**";
            lblErroCadEnd.ForeColor = Color.Red;
        }
    }

    //pega as celulas da tabela carrinho
    protected string rua_grid(GridViewRow row)
    {
        string rua = row.Cells[0].Text;
        return rua;
    }
    protected string produto_grid(GridViewRow row)
    {
        string prod = row.Cells[2].Text;
        return prod;
    }
    protected int qtd_grid(GridViewRow row)
    {
        string qtd = row.Cells[3].Text;
        return Convert.ToInt16(qtd);
    }

    //Finaliza a compra do usuario
    protected void btnCarrinhoFechado_Click(object sender, EventArgs e)
    {
        MT1.ActiveViewIndex = 2;
        List<Endereco> enderecos = convert_list_endereco(WSE.busca_localidade_rua(rua_grid(gvCarrinho.Rows[0]), id_usuario()));
        int id = WSE.insere_vendas(enderecos[0].id_local, "amor");
        for (int i = 0; i < gvCarrinho.Rows.Count; i++)
        {
            enderecos = convert_list_endereco(WSE.busca_localidade_rua(rua_grid(gvCarrinho.Rows[i]), id_usuario()));
            List<Produte> produto = convert_list(WSE.busca_produto_nome(produto_grid(gvCarrinho.Rows[i])));
            WSE.insere_tabela_itens(enderecos[0].id_local, produto[0].id_produto, qtd_grid(gvCarrinho.Rows[i]), id);
        }
        Response.Redirect("area_cliente.aspx");
    }

    //Apaga o carrinho do usuario
    protected void btnCarrinhoCancelado_Click(object sender, EventArgs e)
    {
        MT1.ActiveViewIndex = 2;
        for (int i = 0; i < gvCarrinho.Rows.Count; i++)
        {
            List<Endereco> enderecos = convert_list_endereco(WSE.busca_localidade_rua(rua_grid(gvCarrinho.Rows[i]), id_usuario()));
            List<Produte> produto = convert_list(WSE.busca_produto_nome(produto_grid(gvCarrinho.Rows[i])));
            WSE.retorna_carrinho(enderecos[0].id_local, produto[0].id_produto, qtd_grid(gvCarrinho.Rows[i]));
        }
        //Response.Redirect("area_cliente.aspx");
    }
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
    //verifica os campos de cadastro de endereco
    public Boolean verifica_cad_endereco()
    {
        if (txtCEP.Text == "")
        {
            lblErroCEP.Text = "O CEP deve ser preenchido!";
            lblErroCEP.ForeColor = Color.Red;
            return false;

        }
        else if (txtCEP.Text.Length != 8)
        {
            lblErroCEP.Text = "O CEP deve ter apenas 8 digitos!";
            lblErroCEP.ForeColor = Color.Red;
            return false;
        }
        else if (txtPais.Text == "")
        {
            return false;
        }
        else if (ddlEstados.SelectedValue == "UF")
        {
            lblErroUF.Text = "Deve ser selecionado um Estado diferente de UF";
            return false;
        }
        else if (txtCidade.Text == "")
        {
            return false;
        }
        else if (txtBairro.Text == "")
        {
            return false;
        }
        else if (txtRua.Text == "")
        {
            return false;
        }
        else if (txtNumero_residencia.Text == "")
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}