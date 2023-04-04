<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="admin.aspx.cs" Inherits="admin" %>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <br />
    <br />
    <div class="d-flex justify-content-center">
        <div class="container text-center">
            <asp:Label ID="lblusuario" runat="server" Text=""></asp:Label>
            <div class="row justify-content-start">
                <div class="col">
                    <asp:Button ID="btnCadProduto" runat="server" Text="Novo Produto" CssClass="btn btn-secondary" OnClick="btnEvent_Click" />
                </div>
                <div class="col">
                    <asp:Button ID="btnProds" runat="server" Text="Produtos" CssClass="btn btn-secondary" OnClick="btnEvent_Click" />
                </div>
                <div class="col">
                    <asp:Button ID="btnCadCategoria" runat="server" Text="Nova Categoria" CssClass="btn btn-secondary" OnClick="btnEvent_Click" />
                </div>
                <div class="col">
                    <asp:Button ID="btnUsuarios" runat="server" Text="Clientes" CssClass="btn btn-secondary" OnClick="btnEvent_Click" />
                </div>
                <div class="col">
                    <asp:Button ID="btnVendas" runat="server" Text="Vendas" CssClass="btn btn-secondary" OnClick="btnEvent_Click" />
                </div>
                <div class="col">
                    <asp:Button ID="btnFechar" runat="server" Text="Fechar" CssClass="btn btn-secondary" OnClick="btnEvent_Click" />
                </div>
            </div>
        </div>
    </div>
    <br />
    <br />
    <br />
    <div class="d-flex justify-content-center">
        <div class="container text-center">
            <div class="row justify-content-start">
                <div class="col">
                    <asp:MultiView ID="MultiView1" runat="server">

                        <asp:View ID="View0" runat="server">
                            <h2>Cadastro de Produto</h2>
                            <br />
                            <div class="input-group mb-3">
                                <asp:TextBox ID="txtNomeProd" runat="server" CssClass="form-control" placeholder="Nome do Produto"></asp:TextBox>
                                <asp:TextBox ID="txtDescricaoProd" Rows="3" runat="server" CssClass="form-control" placeholder="Descrição"></asp:TextBox>
                                <asp:TextBox ID="txtPrecoProd" runat="server" CssClass="form-control" placeholder="Preço"></asp:TextBox>
                            </div>
                            <div class="input-group mb-3">
                                <div cssclass="dropdown-menu">
                                    <asp:DropDownList ID="ddlProduto" CssClass="form-control" runat="server">
                                        <asp:ListItem>Categorias</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <asp:TextBox ID="txtMarcaProd" runat="server" CssClass="form-control" placeholder="Marca"></asp:TextBox>
                                <asp:TextBox ID="txtDataLancProd" runat="server" type="date" CssClass="form-control" placeholder="Data de Lançamento"></asp:TextBox>
                            </div>
                            <div class="input-group mb-2">
                                <asp:TextBox ID="txtQuantProd" runat="server" CssClass="form-control" placeholder="Quantidade no Estoque"></asp:TextBox>
                                <asp:TextBox ID="txtPeso" runat="server" CssClass="form-control" placeholder="Peso em Gramas"></asp:TextBox>
                            </div>
                            <div class="input-group mb-1">
                                <asp:TextBox ID="txtURLProd" runat="server" CssClass="form-control" placeholder="Url da Imagem" />
                            </div>
                            <br />
                            <asp:Button ID="btnCadastrarPRod" CssClass="btn btn-primary" runat="server" Text="Cadastrar" OnClick="btnCadastrarPRod_Click" />
                            <br />
                            <br />
                            <asp:Label ID="lblCadProd" runat="SERVER" Text=""></asp:Label>
                        </asp:View>

                        <asp:View ID="View1" runat="server">
                            <h2>Cadastro de Categoria</h2>
                            <br />
                            <div class="input-group mb-2">
                                <asp:TextBox ID="txtSubcategoria" runat="server" CssClass="form-control" placeholder="Subcategoria"></asp:TextBox>
                                <asp:TextBox ID="txtCategoria" runat="server" CssClass="form-control" placeholder="Categoria"></asp:TextBox>
                                <asp:Button ID="btnSubCategoriaEnviar" CssClass="btn btn-primary" runat="server" Text="Cadastrar" OnClick="btnSubCategoriaEnviar_Click" />
                            </div>
                            <br />
                            <br />
                            <asp:Label ID="lblErroSubC" runat="SERVER" Text=""></asp:Label>
                        </asp:View>

                        <asp:View ID="View2" runat="server">
                            <asp:Label ID="lblUsuarios" runat="server" Text=""></asp:Label>
                            <div class="table-responsive" style="width: 50%; margin: auto">
                                <asp:GridView ID="gvResultadoUsuarios" runat="server" AutoGenerateColumns="true" CssClass="table table-striped">
                                </asp:GridView>
                            </div>
                        </asp:View>

                        <asp:View ID="View3" runat="server">
                            <asp:Label ID="lblVendas" runat="server" Text=""></asp:Label>
                            <div class="table-responsive" style="width: 50%; margin: auto">
                                <asp:GridView ID="gvResultadoVendas" runat="server" AutoGenerateColumns="true" CssClass="table table-striped">
                                </asp:GridView>
                            </div>
                        </asp:View>
                        <asp:View ID="View4" runat="server">
                            <asp:Label ID="lblProds" runat="server" Text=""></asp:Label>
                            <div class="table-responsive">
                                <asp:GridView ID="gvResultadoProds" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" AllowPaging="True" AllowSorting="True" DataKeyNames="id_produto" DataSourceID="SqlDataSource1">
                                    <Columns>
                                        <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                                        <asp:BoundField DataField="id_produto" HeaderText="id_produto" InsertVisible="False" ReadOnly="True" SortExpression="id_produto" />
                                        <asp:BoundField DataField="nome_produto" HeaderText="nome_produto" SortExpression="nome_produto" />
                                        <asp:BoundField DataField="descricao" HeaderText="descricao" SortExpression="descricao" />
                                        <asp:BoundField DataField="categoria" HeaderText="categoria" SortExpression="categoria" />
                                        <asp:BoundField DataField="preco_unitario" HeaderText="preco_unitario" SortExpression="preco_unitario" />
                                        <asp:BoundField DataField="data_lancamento" HeaderText="data_lancamento" SortExpression="data_lancamento" />
                                        <asp:BoundField DataField="quantidade" HeaderText="quantidade" SortExpression="quantidade" />
                                        <asp:BoundField DataField="peso_gramas" HeaderText="peso_gramas" SortExpression="peso_gramas" />
                                        <asp:BoundField DataField="caminho_imagem" HeaderText="caminho_imagem" SortExpression="caminho_imagem" />
                                    </Columns>
                                </asp:GridView>
                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:bancoConnectionString %>" DeleteCommand="DELETE FROM [produto] WHERE [id_produto] = @original_id_produto AND [nome_produto] = @original_nome_produto AND [descricao] = @original_descricao AND [categoria] = @original_categoria AND [preco_unitario] = @original_preco_unitario AND [data_lancamento] = @original_data_lancamento AND [quantidade] = @original_quantidade AND [peso_gramas] = @original_peso_gramas AND (([caminho_imagem] = @original_caminho_imagem) OR ([caminho_imagem] IS NULL AND @original_caminho_imagem IS NULL))" InsertCommand="INSERT INTO [produto] ([nome_produto], [descricao], [categoria], [preco_unitario], [data_lancamento], [quantidade], [peso_gramas], [caminho_imagem]) VALUES (@nome_produto, @descricao, @categoria, @preco_unitario, @data_lancamento, @quantidade, @peso_gramas, @caminho_imagem)" OldValuesParameterFormatString="original_{0}" ProviderName="<%$ ConnectionStrings:bancoConnectionString.ProviderName %>" SelectCommand="SELECT [id_produto], [nome_produto], [descricao], [categoria], [preco_unitario], [data_lancamento], [quantidade], [peso_gramas], [caminho_imagem] FROM [produto]" UpdateCommand="UPDATE [produto] SET [nome_produto] = @nome_produto, [descricao] = @descricao, [categoria] = @categoria, [preco_unitario] = @preco_unitario, [data_lancamento] = @data_lancamento, [quantidade] = @quantidade, [peso_gramas] = @peso_gramas, [caminho_imagem] = @caminho_imagem WHERE [id_produto] = @original_id_produto AND [nome_produto] = @original_nome_produto AND [descricao] = @original_descricao AND [categoria] = @original_categoria AND [preco_unitario] = @original_preco_unitario AND [data_lancamento] = @original_data_lancamento AND [quantidade] = @original_quantidade AND [peso_gramas] = @original_peso_gramas AND (([caminho_imagem] = @original_caminho_imagem) OR ([caminho_imagem] IS NULL AND @original_caminho_imagem IS NULL))" OnUpdating="SqlDataSource1_Updating" OnUpdated="OnSqlUpdated" OnDeleting="SqlDataSource1_Deleting" OnDeleted="OnSqlDeleted">
                                    <DeleteParameters>
                                        <asp:Parameter Name="original_id_produto" Type="Int32" />
                                        <asp:Parameter Name="original_nome_produto" Type="String" />
                                        <asp:Parameter Name="original_descricao" Type="String" />
                                        <asp:Parameter Name="original_categoria" Type="Int32" />
                                        <asp:Parameter Name="original_preco_unitario" Type="Decimal" />
                                        <asp:Parameter DbType="Date" Name="original_data_lancamento" />
                                        <asp:Parameter Name="original_quantidade" Type="Int32" />
                                        <asp:Parameter Name="original_peso_gramas" Type="Double" />
                                        <asp:Parameter Name="original_caminho_imagem" Type="String" />
                                    </DeleteParameters>
                                    <InsertParameters>
                                        <asp:Parameter Name="nome_produto" Type="String" />
                                        <asp:Parameter Name="descricao" Type="String" />
                                        <asp:Parameter Name="categoria" Type="Int32" />
                                        <asp:Parameter Name="preco_unitario" Type="Decimal" />
                                        <asp:Parameter DbType="Date" Name="data_lancamento" />
                                        <asp:Parameter Name="quantidade" Type="Int32" />
                                        <asp:Parameter Name="peso_gramas" Type="Double" />
                                        <asp:Parameter Name="caminho_imagem" Type="String" />
                                    </InsertParameters>
                                    <UpdateParameters>
                                        <asp:Parameter Name="nome_produto" Type="String" />
                                        <asp:Parameter Name="descricao" Type="String" />
                                        <asp:Parameter Name="categoria" Type="Int32" />
                                        <asp:Parameter Name="preco_unitario" Type="Decimal" />
                                        <asp:Parameter DbType="Date" Name="data_lancamento" />
                                        <asp:Parameter Name="quantidade" Type="Int32" />
                                        <asp:Parameter Name="peso_gramas" Type="Double" />
                                        <asp:Parameter Name="caminho_imagem" Type="String" />
                                        <asp:Parameter Name="original_id_produto" Type="Int32" />
                                        <asp:Parameter Name="original_nome_produto" Type="String" />
                                        <asp:Parameter Name="original_descricao" Type="String" />
                                        <asp:Parameter Name="original_categoria" Type="Int32" />
                                        <asp:Parameter Name="original_preco_unitario" Type="Decimal" />
                                        <asp:Parameter DbType="Date" Name="original_data_lancamento" />
                                        <asp:Parameter Name="original_quantidade" Type="Int32" />
                                        <asp:Parameter Name="original_peso_gramas" Type="Double" />
                                        <asp:Parameter Name="original_caminho_imagem" Type="String" />
                                    </UpdateParameters>
                                </asp:SqlDataSource>
                            </div>
                        </asp:View>

                        <asp:View ID="View5" runat="server">
                            <img src="https://upload.wikimedia.org/wikipedia/commons/thumb/d/d2/Warner_Bros._%282019%29_logo.svg/800px-Warner_Bros._%282019%29_logo.svg.png" class="rounded mx-auto d-block" style="width: 30%" alt="WB">
                        </asp:View>
                    </asp:MultiView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
