<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wfGestionarPedido.aspx.cs" Inherits="AppWebMantenimiento.Pedido.wfGestionarPedido" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1>Gestion Pedido</h1>
            </hgroup>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="aupCabOrd" runat="server">
        <ContentTemplate>
            <table style="width:50%;"align="center">
                <tr>
                    <td>Orden:</td>
                    <td>
                        <asp:TextBox ID="txtNumOrd" runat="server" Height="12px" Width="105px" TabIndex="1"></asp:TextBox>
                        <asp:Button ID="btnBuscar" runat="server" OnClick="btnBuscar_Click" TabIndex="2" Text="B" Height="24px" />
                    </td>
                </tr>
                <tr>
                    <td>Fecha:</td>
                    <td>
                        <asp:Calendar ID="calFecOrd" runat="server" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="160px" Width="182px" TabIndex="3">
                            <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                            <NextPrevStyle VerticalAlign="Bottom" />
                            <OtherMonthDayStyle ForeColor="#808080" />
                            <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                            <SelectorStyle BackColor="#CCCCCC" />
                            <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                            <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                            <WeekendDayStyle BackColor="#FFFFCC" />
                        </asp:Calendar>
                    </td>
                </tr>
                <tr>
                    <td>Cliente:</td>
                    <td>
                        <asp:DropDownList ID="ddlCliente" runat="server" TabIndex="4">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>Tecnico:</td>
                    <td>
                        <asp:DropDownList ID="ddlTecnico" runat="server" TabIndex="5">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>Valor:</td>
                    <td>
                        <asp:TextBox ID="txtValor" runat="server" Height="12px" Width="105px" TabIndex="6" Enabled="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Iva:</td>
                    <td>
                        <asp:TextBox ID="txtIva" runat="server" Height="12px" Width="105px" TabIndex="7" Enabled="False"></asp:TextBox>
                    </td>
                </tr>
                <tr style="text-align: center">
                    <td colspan="2">
                        <asp:Button ID="btnNuevoCab" runat="server" OnClick="btnNuevoCab_Click" Text="Nuevo" />
                        <asp:Button ID="btnGuardarCab" runat="server" Text="Guardar" OnClick="btnGuardarCab_Click" />
                        <asp:Button ID="btnCancelarCab" runat="server" OnClick="btnCancelarCab_Click" Text="Cancelar" />
                    </td>
                </tr>
                <tr style="text-align: center">
                    <td colspan="2">
                        <asp:Label ID="lblMsjCab" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="aupDetOrd" runat="server">
        <ContentTemplate>
            <table style="width:50%;" align="center">
                <tr>
                    <td colspan="2">
                        <asp:GridView ID="gvDetalle" runat="server" AllowPaging="True" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" OnPageIndexChanging="gvDetalle_PageIndexChanging" PageSize="5" OnSelectedIndexChanged="gvDetalle_SelectedIndexChanged">
                            <Columns>
                                <asp:CommandField ShowSelectButton="True" />
                            </Columns>
                            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                            <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F7F7F7" />
                            <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                            <SortedDescendingCellStyle BackColor="#E5E5E5" />
                            <SortedDescendingHeaderStyle BackColor="#242121" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">Producto:</td>
                    <td>
                        <asp:TextBox ID="txtProd" runat="server" Height="12px" Width="103px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">Cantidad:</td>
                    <td>
                        <asp:TextBox ID="txtCant" runat="server" Height="12px" Width="103px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">Valor:</td>
                    <td>
                        <asp:TextBox ID="txtVlrServ" runat="server" Height="12px" Width="103px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">Tipo Servicio:</td>
                    <td>
                        <asp:DropDownList ID="ddlTipServ" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center">
                        <asp:Button ID="btnNuevoDet" runat="server" Text="Nuevo" />
                        <asp:Button ID="btnAgregarDet" runat="server" OnClick="btnAgregarDet_Click" Text="Agregar" />
                        <asp:Button ID="btnEliminarDet" runat="server" Text="Eliminar" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center">
                        <asp:Label ID="lblMsjDet" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
