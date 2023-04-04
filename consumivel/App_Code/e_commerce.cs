using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Permissions;
using System.Web.Services;

/// <summary>
/// Descrição resumida de e_commerce
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// Para permitir que esse serviço da web seja chamado a partir do script, usando ASP.NET AJAX, remova os comentários da linha a seguir. 
// [System.Web.Script.Services.ScriptService]
public class Usuario
{
    public int id_usuario { get; set; }
    public string nome { get; set; }
    public string cpf { get; set; }
    public string email { get; set; }
    public string senha { get; set; }
    public int tipo { get; set; }
}
public class Endereco
{
    public int id_local { get; set; }
    public int id_usuario { get; set; }
    public string cep { get; set; }
    public string pais { get; set; }
    public string estado { get; set; }
    public string cidade { get; set; }
    public string bairro { get; set; }
    public string rua { get; set; }
    public int numero_residencia { get; set; }
    public string complemento { get; set; }
}
public class Itens_Pedido
{
    public int id_pedido { get; set; }
    public int id_venda { get; set; }
    public int id_produto { get; set; }
    public int quantidade_produto { get; set; }
}
public class Subcategoria
{
    public int id_subcategoria { get; set; }
    public string subcategoria { get; set; }
    public string categoria { get; set; }
}
public class Vendas
{
    public int id_venda { get; set; }
    public int id_local { get; set; }
    public string data_venda { get; set; }
}
public class Produtos
{
    public int id_produto { get; set; }
    public string nome_produto { get; set; }
    public string descricao { get; set; }
    public int categoria { get; set; }
    public decimal preco_unitario { get; set; }
    public string marca { get; set; }
    public string data_lancamento { get; set; }
    public string data_cadastro { get; set; }
    public int quantidade { get; set; }
    public float peso_gramas { get; set; }
    public string caminho_imagm { get; set; }
}
public class e_commerce : System.Web.Services.WebService
{
    public e_commerce()
    {

        //Remova os comentários da linha a seguir se usar componentes designados 
        //InitializeComponent(); 
    }
    //Criptografa a senha
    public string getMD5Hash(string input)
    {
        System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
        byte[] hash = md5.ComputeHash(inputBytes);
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        for (int i = 0; i < hash.Length; i++)
        {
            sb.Append(hash[i].ToString("X2"));
        }
        return sb.ToString();
    }
    String strConexao = ConfigurationManager.ConnectionStrings["WS"].ConnectionString;
    string SQL = "";
    public List<Usuario> convert_usuario_to_list(DataSet ds)
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
    public List<Produtos> convert_produto_to_list(DataSet ds)
    {
        List<Produtos> produto = new List<Produtos>();
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            produto.Add(new Produtos
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
        return produto;
    }
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
    //buscar tabelas no banco
    public DataSet consultaSQL(String SQL, String Conexao)
    {
        SqlConnection sqlConn = new SqlConnection(Conexao);
        SqlDataAdapter objAdapter = new SqlDataAdapter();
        objAdapter.SelectCommand = new SqlCommand(SQL, sqlConn);
        DataSet dSet = new DataSet();
        objAdapter.Fill(dSet);
        return dSet;
    }
    //inserir,deletar e updates nas tabelas do banco
    public void ComandosSQL(String SQL, String Conexao)
    {
        SqlConnection sqlConnection1 = new SqlConnection(Conexao);
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = SQL;
        cmd.Connection = sqlConnection1;

        sqlConnection1.Open();
        cmd.ExecuteNonQuery();
        sqlConnection1.Close();
    }
    //Insere a venda na tabela vendas com a data do momento da insercao
    [WebMethod]
    public int insere_vendas(int id_local, string forma_pag)
    {
        String data_now = DateTime.Now.ToString("yyyy-MM-dd");
        using (SqlConnection connection = new SqlConnection(strConexao))
        {
            connection.Open();
            string query = "insert into vendas(id_local,data_venda,forma_pag) " +
            $"values(@id_local,@data_now,'@forma_pag')" +
             "SELECT SCOPE_IDENTITY();";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id_local", id_local);
                command.Parameters.AddWithValue("@data_now", data_now);
                command.Parameters.AddWithValue("@forma_pag", forma_pag);
                int id = Convert.ToInt32(command.ExecuteScalar());
                return id;
            }
        }
    }
    [WebMethod]
    public void cadastra_usuario(String email, String nome, String cpf, String senha)
    {
        //tipo=0 : admin | tipo=1 : usuario padrao
        senha = getMD5Hash(senha);
        SQL = "insert into usuario(tipo,email,cpf,nome,senha)" +
                $"values(1,'{email}','{cpf}','{nome}','{senha}');";
        ComandosSQL(SQL, strConexao);
    }
    [WebMethod]
    public void cadastra_localidade(int id_usuario, String cep, String pais, String estado, String cidade, String bairro, String rua, int numero_residencia, String complemento)
    {
        SQL = "insert into localidade(id_usuario,cep,pais,estado,cidade,bairro,rua,numero_residencia,complemento) " +
            $"values({id_usuario},'{cep}','{pais}','{estado}','{cidade}','{bairro}','{rua}',{numero_residencia},'{complemento}')";
        ComandosSQL(SQL, strConexao);
    }
    [WebMethod]
    public void cadastra_subcategoria(String subcategoria, String categoria)
    {
        SQL = $"insert into subcategoria(subcategoria,categoria) values('{subcategoria}','{categoria}')";
        ComandosSQL(SQL, strConexao);
    }
    [WebMethod]
    public void cadastra_produto(String nome_produto, String descricao, int categoria, Decimal preco, String marca, String data_lancamento, int quantidade, float peso, String caminho_imagem)
    {
        //pega o dia no momento que o produto ta sendo cadastrado
        String data_now = DateTime.Now.ToString("yyyy-MM-dd");
        //o programa deve fazer o filtro da categoria antes de chamar essa função e passar o id nela
        //preco deve ser cadastrado como  10.00 no banco sera 10,00
        SQL = "insert into produto(nome_produto,descricao,categoria,preco_unitario,marca,data_lancamento,data_cadastro,quantidade,peso_gramas,caminho_imagem)" +
            $"values('{nome_produto}','{descricao}',{categoria},'{preco}','{marca}','{data_lancamento}','{data_now}',{quantidade},{peso},{caminho_imagem})";
        ComandosSQL(SQL, strConexao);
    }
    [WebMethod]
    public void insere_item_pedido(int id_venda, int id_produto, int quantidade)
    {
        SQL = "insert into itens_pedido(id_venda,id_produto,quantidade_produto) " +
            $"values({id_venda},{id_produto},{quantidade})";
        ComandosSQL(SQL, strConexao);
    }
    
    [WebMethod]
    public DataSet listar_usuarios()
    {
        SQL = "select * from usuario";
        return (consultaSQL(SQL, strConexao));
    }
    //Busca o usuario pelo email:
    [WebMethod]
    public DataSet busca_usuario(String email)
    {
        SQL = $"Select * from usuario where email='{email}'";
        return (consultaSQL(SQL, strConexao));
    }
    [WebMethod]
    public DataSet listar_categoria()
    {
        SQL = "select * from subcategoria";
        return (consultaSQL(SQL, strConexao));
    }
    [WebMethod]
    public DataSet listar_vendas()
    {
        SQL = "select * from vendas";
        return (consultaSQL(SQL, strConexao));
    }
    [WebMethod]
    public DataSet busca_produto_nome(string nome_produto)
    {
        SQL = $"select * from produto where nome_produto LIKE '%{nome_produto}%'";
        return (consultaSQL(SQL, strConexao));
    }
    [WebMethod]
    public DataSet busca_localidade_rua(string rua, int id_usuario)
    {
        SQL = $"select * from localidade where rua='{rua}' and id_usuario={id_usuario}";
        return consultaSQL(SQL, strConexao);
    }

    [WebMethod]
    public DataSet busca_produto_id(int id)
    {
        SQL = $"select * from produto where id_produto={id}";
        return (consultaSQL(SQL, strConexao));
    }
    [WebMethod]
    public DataSet listar_itens_pedido()
    {
        SQL = "select * from itens_pedido";
        return (consultaSQL(SQL, strConexao));
    }
    [WebMethod]
    public DataSet listar_produtos()
    {
        SQL = "select * from produto";
        return (consultaSQL(SQL, strConexao));
    }
    //Retorna um dataset do total de produtos por categoria:
    [WebMethod]
    public DataSet count_prod_categoria()
    {
        SQL = "select sb.categoria, count(nome_produto) as 'Total de Produtos' from produto pt, subcategoria sb where pt.categoria=sb.id_subcategoria group by sb.categoria";
        return (consultaSQL(SQL, strConexao));
    }
    //busca os produtos pela categoria:
    [WebMethod]
    public DataSet listar_produtos_categoria(String categoria)
    {
        List<Subcategoria> subcategoria = listar_categorias_list();
        int id_subcategoria = new int();
        foreach (Subcategoria id_categoria in subcategoria)
        {
            if (id_categoria.categoria == categoria)
            {
                id_subcategoria = id_categoria.id_subcategoria;
            }
        }
        SQL = $"select * from produto where categoria={id_subcategoria}";
        return (consultaSQL(SQL, strConexao));
    }
    //Busca o Endereco pelo id do usuario
    [WebMethod]
    public DataSet busca_localidade(int id)
    {
        SQL = $"select * from localidade where id_usuario={id}";
        return (consultaSQL(SQL, strConexao));
    }
    [WebMethod]
    public void deleta_localidade(int id_local)
    {
        SQL = $"delete localidade where id_local={id_local}";
        ComandosSQL(SQL, strConexao);
    }
    //Converte o dataset da busca de um produto em uma lista
    [WebMethod]
    public List<Produtos> buscar_produto(int id_produto)
    {
        SQL = $"select * from produto where id_produto={id_produto}";
        List<Produtos> produto = new List<Produtos>();
        produto = convert_produto_to_list(consultaSQL(SQL, strConexao));
        return produto;
    }
    //Converte a tabela produto em uma lista:
    public List<Produtos> listar_produtos_list()
    {
        List<Produtos> produto = new List<Produtos>();
        produto = convert_produto_to_list(listar_produtos());
        return produto;
    }
    //Converte a tabela de subcategorias em uma lista:
    [WebMethod]
    public List<Subcategoria> listar_categorias_list()
    {
        List<Subcategoria> subcategoria = new List<Subcategoria>();
        subcategoria = convert_categoria_to_list(listar_categoria());
        return subcategoria;
    }
    //busca a linha da subcategorias pelo nome da subcategoria
    [WebMethod]
    public DataSet buscar_subcategoria(string subcategoria)
    {
        SQL = $"select * from subcategoria where subcategoria='{subcategoria}'";
        return consultaSQL(SQL, strConexao);
    }

    //verifica a existencia da subcategoria
    [WebMethod]
    public Boolean verifica_subcategoria(string subcategoria)
    {
        try
        {
            List<Subcategoria> subcategorias = new List<Subcategoria>();
            subcategorias = convert_categoria_to_list(listar_categoria());
            if (subcategorias[0].subcategoria == subcategoria)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        catch
        {
            return true;
        }
    }

    //Verifica se é um usuario do site
    [WebMethod]
    public Boolean verifica_usuario(String email, String senha)
    {
        try
        {
            List<Usuario> pessoa = new List<Usuario>();
            pessoa = convert_usuario_to_list(busca_usuario(email));
            if (pessoa[0].email == email)
            {
                if (pessoa[0].senha == getMD5Hash(senha))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        catch
        {
            return false;
        }
    }

    //Verifica se o ususario é ADMIN
    [WebMethod]
    public Boolean verifica_permissao(String email)
    {
        try
        {
            List<Usuario> pessoa = new List<Usuario>();
            pessoa = convert_usuario_to_list(busca_usuario(email));
            if (pessoa[0].email == email)
            {
                if (pessoa[0].tipo == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        catch
        {
            return false;
        }
    }

    //Retorna um dataset com o valor total de vendas por cada produto
    [WebMethod]
    public DataSet total_vendas()
    {
        SQL = "select pt.nome_produto,SUM(ip.quantidade_produto*pt.preco_unitario) as 'Valor Total', SUM(ip.quantidade_produto) as 'Quantidade' from produto pt,itens_pedido ip where ip.id_produto=pt.id_produto group by pt.nome_produto";
        return (consultaSQL(SQL, strConexao));
    }

    //retorna um dataset com o total de usuario cadastros que não sao ADMIN
    [WebMethod]
    public DataSet total_usuarios()
    {
        SQL = "select count(nome) as 'Total de Usuarios' from usuario where tipo!=0";
        return (consultaSQL(SQL, strConexao));
    }

    //Retorna um dataset com os pedidos do cliente **DEVE SER PASSADO O ID DO CLIENTE**
    [WebMethod]
    public DataSet pedido_usuario(int id_usuario)
    {
        SQL = $"select vd.id_venda as 'Pedido',vd.data_venda as 'Data da Compra',SUM(ip.quantidade_produto*pt.preco_unitario) as 'Valor Total' from produto pt,itens_pedido ip,vendas vd,localidade lc where lc.id_usuario={id_usuario} and vd.id_local=lc.id_local and vd.id_venda=ip.id_venda and ip.id_produto=pt.id_produto group by vd.id_venda,vd.data_venda";
        return (consultaSQL(SQL, strConexao));
    }

    //Inserir os produtos no carrinho e tirar a quantidade do produto
    [WebMethod]
    public void insert_carrinho(int id_local, int id_produto, int qtd)
    {
        SQL = $"update produto set quantidade-={qtd} where id_produto={id_produto}";
        ComandosSQL(SQL, strConexao);
        SQL = $"insert into carrinho(id_local,id_produto,quantidade_produto) values({id_local},{id_produto},{qtd})";
        ComandosSQL(SQL, strConexao);
    }

    //Passa o conteudo do carrinho pra tabela itens_pedido
    [WebMethod]
    public void insere_tabela_itens(int id_local, int id_produto, int qtd, int id_venda)
    {
        SQL = $"insert into itens_pedido(id_venda,id_produto,quantidade_produto) values({id_venda},{id_produto},{qtd})";
        ComandosSQL(SQL, strConexao);
        SQL = $"delete carrinho where id_local={id_local} and id_produto={id_produto}";
        ComandosSQL(SQL, strConexao);
    }

    //Retorna os produtos pro estoque
    [WebMethod]
    public void retorna_carrinho(int id_local, int id_produto, int qtd)
    {
        SQL = $"delete carrinho where id_local={id_local} and id_produto={id_produto}";
        ComandosSQL(SQL, strConexao);
        SQL = $"update produto set quantidade+={qtd} where id_produto={id_produto}";
        ComandosSQL(SQL, strConexao);
    }

    //Busca o Carrinho do Usuario
    [WebMethod]
    public DataSet carrinho_usuario(int id_usuario)
    {
        SQL = $"select ld.rua as 'Endereço', cr.id_carrinho as 'Pedido' ,pt.nome_produto as 'Produto',cr.quantidade_produto as 'Quantidade',SUM(cr.quantidade_produto*pt.preco_unitario) as 'Valor' from carrinho cr,localidade ld,produto pt where ld.id_usuario={id_usuario} and cr.id_local=ld.id_local and cr.id_produto=pt.id_produto GROUP BY pt.nome_produto, cr.id_carrinho,cr.quantidade_produto,ld.rua";
        return (consultaSQL(SQL, strConexao));
    }

    //busca o carrinho pelo local
    [WebMethod]
    public DataSet busca_carrinho(int id_local)
    {
        SQL = $"select * from carrinho where id_local={id_local}";
        return consultaSQL(strConexao, strConexao);
    }

    //busca a venda pelo local
    [WebMethod]
    public DataSet busca_venda_local(int id_local)
    {
        SQL = $"select * from vendas where id_local={id_local}";
        return consultaSQL(SQL, strConexao);
    }

    [WebMethod]
    public DataSet id_ultimo_insert()
    {
        SQL = "select SCOPE_IDENTITY()";
        return consultaSQL(SQL, strConexao);
    }
}