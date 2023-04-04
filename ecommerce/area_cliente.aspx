<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="area_cliente.aspx.cs" Inherits="area_cliente" %>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <br />
    <div class="d-flex justify-content-center">
        <div class="container text-center">
            <div class="row justify-content-start">
                <div class="col">
                    <asp:Button ID="btnPedidos" runat="server" Text="Pedidos" CssClass="btn btn-secondary" OnClick="btnEvent_Click" />
                </div>
                <div class="col">
                    <asp:Button ID="btnEndereco" runat="server" Text="Endereços" CssClass="btn btn-secondary" OnClick="btnEvent_Click" />
                </div>
                <div class="col">
                    <asp:Button ID="btnCarrinho" runat="server" Text="Carrinho" CssClass="btn btn-secondary" OnClick="btnEvent_Click" />
                </div>
                <div class="col">
                    <asp:Button ID="btnFechar" runat="server" Text="Fechar" CssClass="btn btn-secondary" OnClick="btnEvent_Click" />
                </div>
            </div>
        </div>
    </div>
    <br />
    <br />
    <div class="d-flex justify-content-center">
        <div class="container text-center">
            <div class="row justify-content-start">
                <div class="col">
                    <asp:MultiView ID="MT1" runat="server">
                        <asp:View ID="View0" runat="server">
                            <h2>Pedidos</h2>
                            <div class="table-responsive">
                                <asp:GridView ID="gvPedidos" CssClass="table table-striped" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal">
                                    <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                    <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                                    <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                    <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                    <SortedDescendingHeaderStyle BackColor="#242121" />
                                </asp:GridView>
                            </div>
                            <h4>
                                <asp:Label ID="lblErroPedidos" runat="server" Text=""></asp:Label>
                            </h4>
                        </asp:View>
                        <asp:View ID="View1" runat="server">
                            <div class="container text-center">
                                <div class="row justify-content-start">
                                    <div class="col">
                                        <asp:Button ID="btnNovEnd" runat="server" Text="Novo Endereço" CssClass="btn btn-primary" OnClick="btnEvent_Click02" />
                                    </div>
                                    <div class="col">
                                        <asp:Button ID="btnEnderecos" runat="server" Text="Endereços" CssClass="btn btn-primary" OnClick="btnEvent_Click02" />
                                    </div>
                                </div>
                            </div>
                            <asp:MultiView ID="MVEndereco" runat="server">
                                <asp:View ID="View4" runat="server">
                                    <h2>Cadastro de Endereço</h2>
                                    <div class="input-group mb-3">
                                        <asp:TextBox ID="txtCEP" runat="server" CssClass="form-control" placeholder="CEP - Sem Separação"></asp:TextBox>
                                        <asp:TextBox ID="txtPais" Rows="3" runat="server" CssClass="form-control" placeholder="País"></asp:TextBox>
                                        <div cssclass="dropdown-menu">
                                            <asp:DropDownList ID="ddlEstados" CssClass="form-control" runat="server">
                                                <asp:ListItem>UF</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="input-group mb-3">
                                        <asp:TextBox ID="txtCidade" runat="server" CssClass="form-control" placeholder="Cidade"></asp:TextBox>
                                        <asp:TextBox ID="txtBairro" Rows="3" runat="server" CssClass="form-control" placeholder="Bairro"></asp:TextBox>
                                        <asp:TextBox ID="txtRua" runat="server" CssClass="form-control" placeholder="Rua"></asp:TextBox>
                                    </div>
                                    <div class="input-group mb-2">
                                        <asp:TextBox ID="txtNumero_residencia" runat="server" CssClass="form-control" placeholder="Número da Residência"></asp:TextBox>
                                        <asp:TextBox ID="txtComplemento" Rows="3" runat="server" CssClass="form-control" placeholder="Complemento Opcional"></asp:TextBox>
                                    </div>
                                    <asp:Button ID="btnCadEndereco" runat="server" Text="Cadastrar Novo Endereço" CssClass="btn btn-primary" OnClick="btnCadEndereco_Click" />
                                    <h4>
                                        <asp:Label ID="lblErroCadEnd" runat="server" Text=""></asp:Label>
                                    </h4>
                                    <h4>
                                        <asp:Label ID="lblErroCEP" runat="server" Text=""></asp:Label>
                                    </h4>
                                    <h4>
                                        <asp:Label ID="lblErroUF" runat="server" Text=""></asp:Label>
                                    </h4>
                                </asp:View>
                                <asp:View ID="View5" runat="server">
                                    <h2>Endereços</h2>
                                    <h4>
                                        <asp:Label ID="lblEndereco" runat="server" Text=""></asp:Label>
                                    </h4>
                                    <div class="table-responsive">
                                        <center>
                                            <asp:GridView ID="gvEndereco" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal">
                                                <footerstyle backcolor="#CCCC99" forecolor="Black" />
                                                <headerstyle backcolor="#333333" font-bold="True" forecolor="White" />
                                                <pagerstyle backcolor="White" forecolor="Black" horizontalalign="Right" />
                                                <selectedrowstyle backcolor="#CC3333" font-bold="True" forecolor="White" />
                                                <sortedascendingcellstyle backcolor="#F7F7F7" />
                                                <sortedascendingheaderstyle backcolor="#4B4B4B" />
                                                <sorteddescendingcellstyle backcolor="#E5E5E5" />
                                                <sorteddescendingheaderstyle backcolor="#242121" />
                                            </asp:GridView>
                                        </center>

                                    </div>
                                </asp:View>
                            </asp:MultiView>
                        </asp:View>
                        <asp:View ID="View2" runat="server">
                            <h2>Carrinho</h2>

                            <div class="table-responsive">
                                <center>
                                    <asp:GridView ID="gvCarrinho" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal">
                                        <footerstyle backcolor="#CCCC99" forecolor="Black" />
                                        <headerstyle backcolor="#333333" font-bold="True" forecolor="White" />
                                        <pagerstyle backcolor="White" forecolor="Black" horizontalalign="Right" />
                                        <selectedrowstyle backcolor="#CC3333" font-bold="True" forecolor="White" />
                                        <sortedascendingcellstyle backcolor="#F7F7F7" />
                                        <sortedascendingheaderstyle backcolor="#4B4B4B" />
                                        <sorteddescendingcellstyle backcolor="#E5E5E5" />
                                        <sorteddescendingheaderstyle backcolor="#242121" />
                                    </asp:GridView>
                                </center>
                                <br />
                                <br />
                                <asp:Button ID="btnCarrinhoCancelado" runat="server" Text="Cancelar Compra" CssClass="btn btn-primary" OnClick="btnCarrinhoCancelado_Click" />
                                <asp:Button ID="btnCarrinhoFechado" runat="server" Text="Finalizar Compra" CssClass="btn btn-primary" OnClick="btnCarrinhoFechado_Click" />
                            </div>

                            <h4>
                                <asp:Label ID="lblCarrinho" runat="server" Text=""></asp:Label>
                            </h4>
                        </asp:View>
                        <asp:View ID="View3" runat="server">
                            <img src="https://upload.wikimedia.org/wikipedia/commons/thumb/d/d2/Warner_Bros._%282019%29_logo.svg/800px-Warner_Bros._%282019%29_logo.svg.png" class="rounded mx-auto d-block" style="width: 30%" alt="WB">
                        </asp:View>
                    </asp:MultiView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
