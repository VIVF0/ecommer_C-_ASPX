<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="fale_conosco.aspx.cs" Inherits="fale_conosco" %>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <br />
    <div class="d-flex justify-content-center">
        <div class="container text-center">
            <asp:TextBox ID="txtEmail" runat="server" MaxLength="100" placeholder="Email" class="form-control"></asp:TextBox>
            <asp:TextBox ID="txtNome" runat="server" MaxLength="300" placeholder="Nome" class="form-control"></asp:TextBox>
            <asp:TextBox ID="txtAssunto" runat="server" MaxLength="200" placeholder="Assunto" class="form-control"></asp:TextBox>
            <asp:TextBox ID="txtCorpo" runat="server" MaxLength="200" placeholder="Texto" class="form-control"></asp:TextBox>
            <br />
            <asp:Button ID="btnEnviarEmail" CssClass="btn btn-primary" runat="server" Text="Enviar" OnClick="btnEnviarEmail_Click" />
            <h5><asp:Label ID="email" runat="server" Text=""></asp:Label></h5>
            <img src="https://upload.wikimedia.org/wikipedia/commons/thumb/d/d2/Warner_Bros._%282019%29_logo.svg/800px-Warner_Bros._%282019%29_logo.svg.png" class="rounded mx-auto d-block" style="width: 10%" alt="WB">
        </div>
    </div>
</asp:Content>

