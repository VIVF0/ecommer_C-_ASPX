<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="login.aspx.cs" Inherits="login" %>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <br />
    <br />
    <div class="d-flex justify-content-center">
        <asp:Panel ID="Panel1" runat="server">
            Login
                <br />
            <asp:TextBox ID="txtEmail" runat="server" type="email" placeholder="Email" class="form-control"></asp:TextBox>
            <br />
            Senha
                <br />
            <asp:TextBox ID="txtSenha" runat="server" TextMode="Password" placeholder="Senha" class="form-control"></asp:TextBox>
            <asp:Label ID="lblErro" runat="server" Text=""></asp:Label>
            <br />
            <div class="form-group form-check">
                <asp:CheckBox ID="cbLembre" runat="server" class="form-check-input" />
                <label class="form-check-label" for="exampleCheck1">Lembre de mim</label>
            </div>
            <asp:Button ID="btnEntrar" runat="server" Text="Entrar" class="btn btn-primary" OnClick="btnEntrar_Click" />
            <br />
            <div class="d-flex justify-content-center">
                <asp:HyperLink ID="hlCadastro" NavigateUrl="cadastro_usuario.aspx" class="nav-link" runat="server">Cadastre-se</asp:HyperLink>
            </div>
            <br /><asp:Label ID="msgErro" CssClass="form-check-label" runat="server" Text=""></asp:Label>
        </asp:Panel>
    </div>
</asp:Content>
