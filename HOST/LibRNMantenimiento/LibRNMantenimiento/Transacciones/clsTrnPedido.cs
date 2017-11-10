using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibRNMantenimiento.Pedido;
using System.Data;
using System.Transactions;

namespace LibRNMantenimiento.Transacciones
{
    public class clsTrnPedido
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

        #endregion



        #region Propiedades

        public Int32 gsNroOrd
        {
            get { return intNroOrd; }
            set { intNroOrd = value; }
        }

        public DateTime gsFecOrd
        {
            get { return datFecOrd; }
            set { datFecOrd = value; }
        }

        public string gsCodCli
        {
            get { return strCodCli; }
            set { strCodCli = value; }
        }

        public string gsCodTec
        {
            get { return strCodTec; }
            set { strCodTec = value; }
        }

        public decimal gsVlr
        {
            get { return decVlr; }
            set { decVlr = value; }
        }

        public decimal gsIva
        {
            get { return decIva; }
            set { decIva = value; }
        }

        public DataTable gsDtDetalle
        {
            get { return dtDetalle; }
            set { dtDetalle = value; }
        }

        public string gError
        {
            get { return strError; }
        }

        #endregion


        #region Metodos Privados

        private bool GrabarCabecera()
        {
            clsCabeceraOrd objCabOrd = new clsCabeceraOrd();

            objCabOrd.gsNroOrd = intNroOrd;
            objCabOrd.gsFecOrd = datFecOrd;
            objCabOrd.gsCodCli = strCodCli;
            objCabOrd.gsCodTec = strCodTec;
            objCabOrd.gsVlr = decVlr;
            objCabOrd.gsIva = decIva;

            if (!objCabOrd.GrabarCabOrd())
            {
                strError = objCabOrd.gError;
                objCabOrd = null;
                return false;
            }

            intNroOrd = objCabOrd.gsNroOrd;
            objCabOrd = null;
            return true;
        }


        private bool GrabarDetalle()
        {
            if (dtDetalle == null)
            {
                strError = "NO se asignaron los productos del pedido";
                return false;
            }

            clsDetalleOrd objDetOrd = new clsDetalleOrd();

            for (int i = 0; i < dtDetalle.Rows.Count; i++)
            {
                //objDetOrd.gsNroOrd = Convert.ToInt32(dtDetalle.Rows[i][0]);
                objDetOrd.gsNroOrd = intNroOrd;
                objDetOrd.gsCodProd = dtDetalle.Rows[i][1].ToString();
                objDetOrd.gsCant = Convert.ToInt16(dtDetalle.Rows[i][2]);
                objDetOrd.gsValor = Convert.ToDecimal(dtDetalle.Rows[i][3]);
                objDetOrd.gsCodTipSer = Convert.ToInt16(dtDetalle.Rows[i][4]);

                if (!objDetOrd.GrabarDetOrd())
                {
                    strError = objDetOrd.gError;
                    objDetOrd = null;
                    return false;
                }
            }

            objDetOrd = null;
            return true;
        }

        #endregion



        #region Metodos Publicos

        public bool GrabarTrnPedido()
        {
            try
            {
                using (TransactionScope objTrnScp = new TransactionScope())
                {
                    if (GrabarCabecera())
                    {
                        if (GrabarDetalle())
                        {
                            objTrnScp.Complete();
                            return true;
                        }
                        else
                        {
                            objTrnScp.Dispose();
                            return false;
                        }
                    }
                    else
                    {
                        objTrnScp.Dispose();
                        return false;
                    }

                }
            }
            catch (TransactionException tex)
            {
                strError = tex.Message;
                return false;
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
