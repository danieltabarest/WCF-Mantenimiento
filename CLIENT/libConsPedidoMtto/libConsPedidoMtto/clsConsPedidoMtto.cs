using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using libConsPedidoMtto.refSvcPedidoMtto;

namespace libConsPedidoMtto
{
    public class clsConsPedidoMtto
    {

        #region Atributos

        //Atributos Cabecera Orden
        private Int32 intNroOrd;
        private DateTime datFecOrd;
        private string strCodCli;
        private string strCodTec;
        private decimal decVlr;
        private decimal decIva;

        //Atributo Detalle Orden;
        private DataTable dtDetalle;

        private string strError;

        private clsPedidoMtto objRefPed;
        private PedidoMttoClient objRefSvcPed;

        #endregion


        #region Propiedades

        public Int32 NumeroOrden
        {
            get { return intNroOrd; }
            set { intNroOrd = value; }
        }

        public DateTime FechaOrden
        {
            get { return datFecOrd; }
            set { datFecOrd = value; }
        }

        public string CodCliente
        {
            get { return strCodCli; }
            set { strCodCli = value; }
        }

        public string CodTecnico
        {
            get { return strCodTec; }
            set { strCodTec = value; }
        }

        public decimal ValorOrd
        {
            get { return decVlr; }
            set { decVlr = value; }
        }

        public decimal IvaOrden
        {
            get { return decIva; }
            set { decIva = value; }
        }

        public DataTable DetalleProd
        {
            get { return dtDetalle; }
            set { dtDetalle = value; }
        }

        public string Error
        {
            get { return strError; }
            //set { strError = value; }
        }

        #endregion



        #region Metodos Publicos

        public bool GrabarPedido()
        {
            try
            {
                objRefPed = new clsPedidoMtto();
                //clsPedidoMtto objRefPedRES = new clsPedidoMtto();

                objRefPed.NumeroOrden = intNroOrd;
                objRefPed.FechaOrden = datFecOrd;
                objRefPed.CodCliente = strCodCli;
                objRefPed.CodTecnico = strCodTec;
                objRefPed.ValorOrd = decVlr;
                objRefPed.IvaOrden = decIva;

                objRefPed.DetalleProd = dtDetalle;


                objRefSvcPed = new PedidoMttoClient();

                objRefPed = objRefSvcPed.GrabarPedido(objRefPed);

                if (objRefPed.Funciono)
                {
                    intNroOrd = objRefPed.NumeroOrden;
                    objRefPed = null;
                    objRefSvcPed = null;
                    return true;
                }
                else
                {
                    strError = objRefPed.Error;
                    objRefPed = null;
                    objRefSvcPed = null;
                    return false;
                }

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }

        #endregion
    }
}
