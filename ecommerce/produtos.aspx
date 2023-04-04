<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="produtos.aspx.cs" Inherits="produtos" %>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <br />
    <br />
    <div class="d-flex justify-content-center">
        <!--Painel para os botoes de categoria de forma dinamica-->
        <asp:Panel ID="Panel2" runat="server">
        </asp:Panel>
        <!--Painel para os cards dos produtos de forma dinamica-->
        <asp:Panel ID="Panel1" runat="server">
        </asp:Panel>

    </div>
    <center>
        <br />
        <br />
        <h3>
            <asp:Label ID="lblERRO" runat="server" Text=""></asp:Label></h3>
    </center>
    <div class="modal fade" id="Modal1" tabindex="-1" role="dialog" aria-labelledby="ModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">
                        <asp:Label ID="Label1" runat="server" Text=""></asp:Label></h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                    <br />
                    <h5 class="modal-title" id="exampleModalLabel">
                        <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                    </h5>
                </div>
                <div class="modal-footer">
                    <div class="nav-item dropdown">
                        <asp:DropDownList ID="ddlLocal" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                    <div class="nav-item dropdown">
                        <asp:DropDownList ID="ddlQuant" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                    <asp:Button ID="btnCancelar" CssClass="btn btn-secury" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />
                    <asp:Button ID="btnConfirmar" CssClass="btn btn-primary" runat="server" Text="Confirma" OnClick="btnConfirma_Click" />
                </div>
            </div>
        </div>
    </div>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js"></script>
    <script type="text/javascript">
        function openModal1() {
            $('#Modal1').modal('show');
        }
        function closeModal1() {
            $('#Modal1').modal('hide');
        }
        function openModal1() {
            var myModal = new bootstrap.Modal(document.getElementById('Modal1'), { keyboard: false });
            myModal.show();
        }
        function closeModal1() {
            var myModalEl = document.getElementById('Modal1')
            var modal = bootstrap.Modal.getInstance(myModalEl)
            modal.hide()
        }
    </script>

</asp:Content>
