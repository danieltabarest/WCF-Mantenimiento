using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibRNMantenimiento.Pedido;

namespace AppWebMantenimiento.Pedido
{
    public partial class wfListarPedidos : System.Web.UI.Page
    {
        #region Atributos

        //Int32 intNroOrd;
        clsCabeceraOrd objCabOrd;
        clsDetalleOrd objDetOrd;

        #endregion


        #region Metodos Privados

        private void LlenarGridCabOrd()
        {
            objCabOrd = new clsCabeceraOrd();

            objCabOrd.gsGvCabOrd = gvCabOrd;

            if (objCabOrd.LlenarCabOrd())
            {
                gvCabOrd = objCabOrd.gsGvCabOrd;
            }
            else
            {
                lblMsj.Text = objCabOrd.gError;
            }

            objCabOrd = null;
        }

        private void LlenarGridDetOrd()
        {
            objDetOrd = new clsDetalleOrd();

            objDetOrd.gsNroOrd = (int)Session["NumOrd"];
            objDetOrd.gsGvDetOrd = gvDetOrd;

            if (objDetOrd.LlenarDetOrd())
            {
                gvDetOrd = objDetOrd.gsGvDetOrd;
            }
            else
            {
                lblMsj.Text = objDetOrd.gError;
            }

            objDetOrd = null;
        }

        #endregion



        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnListar_Click(object sender, EventArgs e)
        {
            LlenarGridCabOrd();
        }

        protected void gvCabOrd_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCabOrd.PageIndex = e.NewPageIndex;
            LlenarGridCabOrd();
        }

        protected void gvCabOrd_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["NumOrd"] = Convert.ToInt32(gvCabOrd.SelectedRow.Cells[1].Text);
            gvDetOrd.PageIndex = 0;
            LlenarGridDetOrd();
        }

        protected void gvDetOrd_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDetOrd.PageIndex = e.NewPageIndex;
            LlenarGridDetOrd();
        }

        #endregion

        

        

        
    }
}