using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descrição resumida de classes
/// </summary>
namespace Classes_Banco
{    
    public class Usuario
    {
        public int id_usuario { get; set; }
        public string nome { get; set; }
        public string cpf { get; set; }
        public string email { get; set; }
        public string senha { get; set; }
        public int tipo { get; set; }
    }
    public class ultimoid
    {
        public int id { get; set; }
    }
    public class Carrinho
    {
        public int id_carrinho{ get; set; }
        public int id_produto { get; set; }
        public int id_local { get; set; }
        public int quantidade_produto { get; set; }
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
    public class Subcategorias
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
        public string forma_pag { get; set; }
    }
    public class Produte
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

}