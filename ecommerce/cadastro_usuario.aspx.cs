using Classes_Banco;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

public partial class cadastro_usuario : System.Web.UI.Page
{
    WS.e_commerceSoapClient WSE = new WS.e_commerceSoapClient();

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Unnamed2_Click(object sender, EventArgs e)
    {
        try
        {
            //verifica a existencia do email e cpf inseridos
            //verifica se os campos foram preenchidos
            if (Existe_no_Banco(txtEmail.Text, txtCPF.Text) && verifica_campos_cadastro())
            {
                WSE.cadastra_usuario(txtEmail.Text, txtNome.Text, txtCPF.Text, txtSenha.Text);
                HttpCookie email = new HttpCookie("email", txtEmail.Text);
                Response.Cookies.Add(email);
                Response.Cookies.Add(new HttpCookie("senha", txtSenha.Text));
                Session["email"] = txtEmail.Text;
                Response.Redirect("~/produtos.aspx");
            }
            else
            {
                lblErro.Text = "NÃO FOI POSSIVEL FAZER O CADASTRO";
                lblErro.ForeColor = Color.Red;
            }
        }
        catch
        {
            lblErro.Text = "NÃO FOI POSSIVEL FAZER O CADASTRO";
            lblErro.ForeColor = Color.Red;
            txtEmail.Text = "";
            txtNome.Text = "";
            txtCPF.Text = "";
            txtSenha.Text = "";
        }
    }
    public Boolean verifica_campos_cadastro()
    {
        if (txtCPF.Text == null)
        {
            lblCpfErro.Text = "O campo CPF deve ser preenchido";
            lblCpfErro.ForeColor = Color.Red;
            return false;
        }
        if (txtCPF.Text.Length!=11)
        {
            lblCpfErro.Text = "O campo CPF deve ter 11 numeros";
            lblCpfErro.ForeColor = Color.Red;
            return false;
        }
        for(int i=0; i< txtCPF.Text.Length;i++)
        {
            if (Regex.IsMatch(txtCPF.Text[i].ToString(), "^[0-9]")==false)
            {
                lblCpfErro.Text = "O campo CPF deve ter somente Numeros";
                lblCpfErro.ForeColor = Color.Red;
                return false;
                i= txtCPF.Text.Length-1;
            }
        }
        if (txtEmail == null)
        {
            lblEmailErro.Text = "O campo Email deve ser preenchido";
            lblEmailErro.ForeColor = Color.Red;
            return false;
        }
        if (txtNome == null)
        {
            lblNomeErro.Text = "O campo Nome deve ser preenchido";
            lblNomeErro.ForeColor = Color.Red;
            return false;
        }
        if (txtSenha == null)
        {
            lblSenhaErro.Text = "O campo Senha deve ser preenchido";
            lblSenhaErro.ForeColor = Color.Red;
            return false;
        }
        else
        {
            return true;
        }
        
    }
    public Boolean Existe_no_Banco(String email, String cpf)
    {
        try
        {
            List<Usuario> usuarios = new List<Usuario>();
            DataSet pessoa = new DataSet();
            pessoa = WSE.listar_usuarios();
            bool retorno=new bool();
            foreach (DataRow dr in pessoa.Tables[0].Rows)
            {
                usuarios.Add(new Usuario
                {
                    id_usuario = Convert.ToInt32(dr["id_usuario"]),
                    nome = Convert.ToString(dr["nome"]),
                    cpf = Convert.ToString(dr["cpf"]),
                    email = Convert.ToString(dr["email"]),
                    senha = Convert.ToString(dr["senha"]),
                    tipo = Convert.ToInt16(dr["tipo"]),
                });
            }
            for (int i = 0;i < usuarios.Count; i++)
            {
                if (usuarios[i].email == email && usuarios[i].cpf == cpf)
                {
                    lblEmailErro.Text = "Esse Email já está sendo usado";
                    lblEmailErro.ForeColor = Color.Red;
                    lblCpfErro.Text = "Esse CPF já está sendo usado";
                    lblCpfErro.ForeColor = Color.Red;
                    retorno = false;
                    break;
                }
                else if(usuarios[i].email == email)
                {
                    lblEmailErro.Text = "Esse Email já está sendo usado";
                    lblEmailErro.ForeColor = Color.Red;
                    retorno= false;
                    break;
                }
                else if(usuarios[i].cpf == cpf)
                {
                    lblCpfErro.Text = "Esse CPF já está sendo usado";
                    lblCpfErro.ForeColor = Color.Red;
                    retorno= false;
                    break;
                }
                else
                {
                    retorno = true;
                }

            }
            return retorno;
        }
        catch
        {
            return false;
        }
    }
}