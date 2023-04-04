<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="cadastro_usuario.aspx.cs" Inherits="cadastro_usuario" %>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <br />
    <br />
    <div class="d-flex justify-content-center">
        <asp:Panel ID="Panel1" runat="server">
            Email
                <br />
            <asp:TextBox ID="txtEmail" runat="server" type="email" placeholder="Email" class="form-control"></asp:TextBox>
            <asp:Label ID="lblEmailErro" runat="server" Text=""></asp:Label>
            <br />
            Senha
                <br />
            <asp:TextBox ID="txtSenha" runat="server" TextMode="Password" placeholder="Senha" class="form-control"></asp:TextBox>
            <br /><asp:Label ID="lblSenhaErro" runat="server" Text=""></asp:Label>
             Nome
                <br />
            <asp:TextBox ID="txtNome" runat="server" placeholder="Nome" class="form-control"></asp:TextBox>
            <br /><asp:Label ID="lblNomeErro" runat="server" Text=""></asp:Label>
             CPF
                <br />
            <asp:TextBox ID="txtCPF" runat="server" placeholder="CPF" class="form-control"></asp:TextBox>
            <asp:Label ID="Label2" runat="server" Text="CPF sem Separação"></asp:Label>
            <br /><asp:Label ID="lblCpfErro" runat="server" Text=""></asp:Label>
            <br />
            <asp:Button runat="server" Text="Cadastrar" class="btn btn-primary" OnClick="Unnamed2_Click" />
            <br />
            
            <asp:Label ID="lblErro" runat="server" Text=""></asp:Label>
        </asp:Panel>
    </div>
</asp:Content>
