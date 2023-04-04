<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="sobre.aspx.cs" Inherits="sobre" %>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <br />
    <div class="d-flex justify-content-center">
        <div class="container text-center">
            <p>
                Bom Dia, Boa Tarde ou Boa Noite a todos que estão me lendo nesse exato momento!
                <br />Nesse Site vendemos produtos Eletronicos de diversas categorias e com os piores preços do mercado
                <br />Não sei porque você está pensando em comprar nossos produtos mas o problema não é meu né! :)
            </p>
            <br />
            <img src="https://upload.wikimedia.org/wikipedia/commons/thumb/d/d2/Warner_Bros._%282019%29_logo.svg/800px-Warner_Bros._%282019%29_logo.svg.png" class="rounded mx-auto d-block" style="width: 10%" alt="WB">
            <br />
            <br />
            <asp:Button ID="btnFale" runat="server" Text="Fale Conosco" CssClass="btn btn-primary" OnClick="btnFale_Click" />  
        </div>
    </div>
</asp:Content>
