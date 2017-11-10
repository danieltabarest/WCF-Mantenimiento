using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibRNMantenimiento.Pedido;
using LibRNMantenimiento.Transacciones;
using System.Data;
using libConsPedidoMtto;

namespace AppWebMantenimiento.Pedido
{
    public partial class wfGestionarPedido : System.Web.UI.Page
    {

        #region Atributos

        clsCabeceraOrd objCabOrd;
        clsDetalleOrd objDetOrd;
        clsTrnPedido objTrnOrd;

        #endregion


        #region Metodos Privados

        private void LlenarDdlCliente()
        {
            objCabOrd = new clsCabeceraOrd();

            objCabOrd.gsDdlCliente = ddlCliente;

            if (objCabOrd.LlenarCliente())
            {
                ddlCliente = objCabOrd.gsDdlCliente;
            }
            else
            {
                lblMsjCab.Text = objCabOrd.gError;
            }

            objCabOrd = null;
        }

        private void LlenarDdlTecnico()
        {
            objCabOrd = new clsCabeceraOrd();

            objCabOrd.gsDdlTecnico = ddlTecnico;

            if (objCabOrd.LlenarTecnico())
            {
                ddlTecnico = objCabOrd.gsDdlTecnico;
            }
            else
            {
                lblMsjCab.Text = objCabOrd.gError;
            }

            objCabOrd = null;
        }

        private void LlenarDdlTipServicio()
        {
            objDetOrd = new clsDetalleOrd();

            objDetOrd.gsDdlTipServicio = ddlTipServ;

            if (objDetOrd.LlenarTipoServ())
            {
                ddlTipServ = objDetOrd.gsDdlTipServicio;
            }
            else
            {
                lblMsjDet.Text = objDetOrd.gError;
            }

            objDetOrd = null;
        }

        private void LlenarGridDetOrd()
        {
            objDetOrd = new clsDetalleOrd();

            objDetOrd.gsNroOrd = Convert.ToInt32(txtNumOrd.Text);
            objDetOrd.gsGvDetOrd = gvDetalle;

            if (objDetOrd.LlenarGridDetalle())
            {
                gvDetalle = objDetOrd.gsGvDetOrd;
            }
            else
            {
                lblMsjDet.Text = objDetOrd.gError;
            }

            objDetOrd = null;
        }

        private void BuscarCabOrd()
        {
            lblMsjCab.Text = "";

            objCabOrd = new clsCabeceraOrd();

            objCabOrd.gsNroOrd = Convert.ToInt32(txtNumOrd.Text);

            if (objCabOrd.ObtenerCabOrd())
            {
                calFecOrd.SelectedDate = objCabOrd.gsFecOrd;
                calFecOrd.VisibleDate = objCabOrd.gsFecOrd;

                ddlCliente.SelectedValue = objCabOrd.gsCodCli;
                ddlTecnico.SelectedValue = objCabOrd.gsCodTec;

                txtValor.Text = objCabOrd.gsVlr.ToString();
                txtIva.Text = objCabOrd.gsIva.ToString();

                LlenarGridDetOrd();

                BloquearCamposGral(true);
            }
            else
            {
                lblMsjCab.Text = objCabOrd.gError;
                LimpiarCabOrdComp();
            }

            objCabOrd = null;

        }

        private void AgregarDetalle()
        {
            lblMsjDet.Text = "";

            clsGridDetalle objGridDet = new clsGridDetalle();

            if (String.IsNullOrEmpty(txtNumOrd.Text))
            {
                objGridDet.gsNroOrd = 0;
            }
            else
            {
                objGridDet.gsNroOrd = Convert.ToInt32(txtNumOrd.Text);
            }
            objGridDet.gsCodProd = txtProd.Text;
            objGridDet.gsCant = Convert.ToInt16(txtCant.Text);
            objGridDet.gsValor = Convert.ToDecimal(txtVlrServ.Text);
            objGridDet.gsCodTipSer = Convert.ToInt16(ddlTipServ.SelectedValue);

            if (Session["varDtDetalle"] != null)
            {
                objGridDet.gsDtDetalle = (DataTable)Session["varDtDetalle"];
            }

            if (objGridDet.AgregarDetalle())
            {
                Session["varDtDetalle"] = objGridDet.gsDtDetalle;

                gvDetalle.DataSource = (DataTable)Session["varDtDetalle"];
                gvDetalle.DataBind();

                txtValor.Text = objGridDet.gTot.ToString("#,#");
                txtIva.Text = objGridDet.gIva.ToString("#,#");
            }
            else
            {
                lblMsjDet.Text = objGridDet.gError;
            }

            objGridDet = null;

            LimpiarCampDet();
        }

        private void ObtenerDetalle()
        {
            lblMsjDet.Text = "";

            clsGridDetalle objGridDet = new clsGridDetalle();

            objGridDet.gsNroOrd = (Int32)Session["NumOrd"];
            objGridDet.gsCodProd = Session["CodPro"].ToString();

            if (Session["varDtDetalle"] != null)
            {
                objGridDet.gsDtDetalle = (DataTable)Session["varDtDetalle"];
            }

            if (objGridDet.ObtenerDetalle())
            {
                //txtProd.Text = (string)Session["CodPro"];
                txtProd.Text = objGridDet.gsCodProd;
                txtCant.Text = objGridDet.gsCant.ToString();
                txtVlrServ.Text = objGridDet.gsValor.ToString();
                ddlTipServ.SelectedValue = objGridDet.gsCodTipSer.ToString();
            }
            else
            {
                lblMsjDet.Text = objGridDet.gError;
            }

            objGridDet = null;
        }


        private void GrabarPedido()
        {
            lblMsjCab.Text = "";

            objTrnOrd = new clsTrnPedido();

            if (String.IsNullOrEmpty(txtNumOrd.Text))
            {
                objTrnOrd.gsNroOrd = 0;
            }
            else
            {
                objTrnOrd.gsNroOrd = Convert.ToInt32(txtNumOrd.Text);
            }
            objTrnOrd.gsFecOrd = calFecOrd.SelectedDate;
            objTrnOrd.gsCodCli = ddlCliente.SelectedValue;
            objTrnOrd.gsCodTec = ddlTecnico.SelectedValue;
            objTrnOrd.gsVlr = Convert.ToDecimal(txtValor.Text);
            objTrnOrd.gsIva = Convert.ToDecimal(txtIva.Text);

            objTrnOrd.gsDtDetalle = (DataTable)Session["varDtDetalle"];

            if (objTrnOrd.GrabarTrnPedido())
            {
                lblMsjCab.Text = "Orden # " + objTrnOrd.gsNroOrd.ToString() + " Grabada Exitosamente";
                BloquearCamposGral(false);
                LimpiarCampDet();
                LimpiarCabOrdComp();
                txtNumOrd.Enabled = true;
            }
            else
            {
                lblMsjCab.Text = objTrnOrd.gError;
            }

            objTrnOrd = null;
        }

        private void GrabarPedidoWCF()
        {
            lblMsjCab.Text = "";

            clsConsPedidoMtto objConsPedMtto = new clsConsPedidoMtto();
            //objTrnOrd = new clsTrnPedido();

            if (String.IsNullOrEmpty(txtNumOrd.Text))
            {
                objConsPedMtto.NumeroOrden = 0;
            }
            else
            {
                objConsPedMtto.NumeroOrden = Convert.ToInt32(txtNumOrd.Text);
            }
            objConsPedMtto.FechaOrden = calFecOrd.SelectedDate;
            objConsPedMtto.CodCliente = ddlCliente.SelectedValue;
            objConsPedMtto.CodTecnico = ddlTecnico.SelectedValue;
            objConsPedMtto.ValorOrd = Convert.ToDecimal(txtValor.Text);
            objConsPedMtto.IvaOrden = Convert.ToDecimal(txtIva.Text);

            objConsPedMtto.DetalleProd = (DataTable)Session["varDtDetalle"];

            if (objConsPedMtto.GrabarPedido())
            {
                lblMsjCab.Text = "Orden # " + objConsPedMtto.NumeroOrden.ToString() + " Grabada Exitosamente";
                BloquearCamposGral(false);
                LimpiarCampDet();
                LimpiarCabOrdComp();
                txtNumOrd.Enabled = true;
            }
            else
            {
                lblMsjCab.Text = objConsPedMtto.Error;
            }

            objConsPedMtto = null;
        }


        private void LimpiarCabOrdComp()
        {
            txtNumOrd.Text = "";
            calFecOrd.SelectedDate = DateTime.Now;
            calFecOrd.VisibleDate = DateTime.Now;
            ddlCliente.SelectedIndex = -1;
            ddlTecnico.SelectedIndex = -1;
            txtValor.Text = "";
            txtIva.Text = "";
            txtNumOrd.Focus();

            gvDetalle.DataSource = null;
            gvDetalle.DataBind();

            Session["varDtDetalle"] = null;
        }

        private void LimpiarCampDet()
        {
            txtProd.Text = "";
            txtCant.Text = "";
            txtVlrServ.Text = "";
            ddlTipServ.SelectedIndex = -1;
            txtProd.Focus();
        }

        private void BloquearCamposGral(bool pBolBloq)
        {

            txtNumOrd.Focus();
            calFecOrd.Enabled = pBolBloq;
            ddlCliente.Enabled = pBolBloq;
            ddlTecnico.Enabled = pBolBloq;

            btnCancelarCab.Enabled = pBolBloq;
            btnGuardarCab.Enabled = pBolBloq;

            txtProd.Enabled = pBolBloq;
            txtCant.Enabled = pBolBloq;
            txtVlrServ.Enabled = pBolBloq;
            ddlTipServ.Enabled = pBolBloq;
            btnNuevoDet.Enabled = pBolBloq;
            btnAgregarDet.Enabled = pBolBloq;
            btnEliminarDet.Enabled = pBolBloq;


        }


        #endregion


        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) //Es la primera vez que se cargo la pagina
            {
                LlenarDdlCliente();
                LlenarDdlTecnico();
                LlenarDdlTipServicio();
                BloquearCamposGral(false);
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarCabOrd();
        }

        protected void gvDetalle_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDetalle.PageIndex = e.NewPageIndex;
            LlenarGridDetOrd();
        }

        protected void btnAgregarDet_Click(object sender, EventArgs e)
        {
            AgregarDetalle();
        }


        protected void btnNuevoCab_Click(object sender, EventArgs e)
        {
            LimpiarCampDet();
            LimpiarCabOrdComp();
            BloquearCamposGral(true);
            txtNumOrd.Enabled = false;
        }

        protected void btnCancelarCab_Click(object sender, EventArgs e)
        {
            LimpiarCampDet();
            LimpiarCabOrdComp();
            BloquearCamposGral(false);
            txtNumOrd.Enabled = false;
        }

        protected void gvDetalle_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["NumOrd"] = Convert.ToInt32(gvDetalle.SelectedRow.Cells[1].Text);
            Session["CodPro"] = Convert.ToInt32(gvDetalle.SelectedRow.Cells[2].Text);

            ObtenerDetalle();
        }

        protected void btnGuardarCab_Click(object sender, EventArgs e)
        {
            //GrabarPedido();
            GrabarPedidoWCF();
        }

        #endregion

        

        

        

        

        

        

        
    }
}