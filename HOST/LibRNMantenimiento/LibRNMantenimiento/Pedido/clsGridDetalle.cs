using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace LibRNMantenimiento.Pedido
{
    public class clsGridDetalle
    {
        #region Atributos

        private Int32 intNroOrd;
        private string strCodProd;
        private int intCant;
        private decimal decValor;
        private int intCodTipSer;

        private string strError;

        private DataTable dtDetalle;

        private decimal decTot;

        
        private decimal decIva;

        
        private const decimal decPctIva = 10;
        
        #endregion



        #region Propiedades

        public Int32 gsNroOrd
        {
            get { return intNroOrd; }
            set { intNroOrd = value; }
        }
        public string gsCodProd
        {
            get { return strCodProd; }
            set { strCodProd = value; }
        }
        public int gsCant
        {
            get { return intCant; }
            set { intCant = value; }
        }
        public decimal gsValor
        {
            get { return decValor; }
            set { decValor = value; }
        }
        public int gsCodTipSer
        {
            get { return intCodTipSer; }
            set { intCodTipSer = value; }
        }

        public string gError
        {
            get { return strError; }
        }

        public DataTable gsDtDetalle
        {
            get { return dtDetalle; }
            set { dtDetalle = value; }
        }


        public decimal gTot
        {
            get { return decTot; }
        }
        public decimal gIva
        {
            get { return decIva; }
        }

        #endregion



        #region Metodos Privados

        private bool ValDatosDetOrd()
        {
            /*if (intNroOrd < 1)
            {
                strError = "NO se asigno numero de orden o es un numero invalido";
                return false;
            }*/

            if (String.IsNullOrEmpty(strCodProd))
            {
                strError = "NO se asigno Codigo de Producto";
                return false;
            }

            if (intCant < 1)
            {
                strError = "NO se asigno cantidad o es una cantidad invalida";
                return false;
            }

            if (decValor < 1)
            {
                strError = "Valor Item menor a Cero o Invalido";
                return false;
            }

            if (intCodTipSer < 1)
            {
                strError = "NO se asigno Tipo Servicio o es un Codigo Invalido";
                return false;
            }

            return true;
        }

        private bool CrearTabla()
        {
            try
            {
                dtDetalle = new DataTable("Detalle");

                dtDetalle.Columns.Add("nroOrdServ", typeof(Decimal));
                dtDetalle.Columns.Add("codProd", typeof(String));
                dtDetalle.Columns.Add("nroUnidServ", typeof(Int32));
                dtDetalle.Columns.Add("vlrServ", typeof(Decimal));
                dtDetalle.Columns.Add("codTipServ", typeof(Int32));

                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }

        private bool CalcularTotalPed()
        {
            try
            {
                object objSum = dtDetalle.Compute("Sum(vlrServ)", "");

                decTot = (decimal)objSum;
                decIva = decTot * (decPctIva / 100);

                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }

        #endregion



        #region Metodos Publicos

        public bool AgregarDetalle()
        {
            if (dtDetalle == null) //Es el primer Detalle a Agregar
            {
                if (!CrearTabla())
                {
                    return false;
                }
            }

            if (!ValDatosDetOrd())
            {
                return false;
            }

            try
            {
                DataRow drFila = dtDetalle.NewRow();

                drFila["nroOrdServ"] = intNroOrd;
                drFila["codProd"] = strCodProd;
                drFila["nroUnidServ"] = intCant;
                drFila["vlrServ"] = decValor;
                drFila["codTipServ"] = intCodTipSer;

                dtDetalle.Rows.Add(drFila);

                if (!CalcularTotalPed())
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }

        public bool ObtenerDetalle()
        {
            if (intNroOrd < 1)
            {
                strError = "NO se asigno numero de orden o es un numero invalido";
                return false;
            }

            if (String.IsNullOrEmpty(strCodProd))
            {
                strError = "NO se asigno Codigo de Producto";
                return false;
            }

            if (dtDetalle == null) //Es el primer Detalle a Agregar
            {
                strError = "NO se asigno Tabla Detalle";
                return false;
            }

            try
            {
                DataRow[] drFilaRes;

                //string strBus = "nroOrdServ = '10' AND codProd = '70001'";
                string strBus = "nroOrdServ = '" + intNroOrd + "' AND codProd = '" + strCodProd + "'";

                drFilaRes = dtDetalle.Select(strBus);

                if(drFilaRes.Length > 1)
                {
                    strError = "La busqueda retorno varias filas";
                    return false;
                }

                intCant = Convert.ToInt16(drFilaRes[0]["nroUnidServ"]);
                decValor = Convert.ToDecimal(drFilaRes[0]["vlrServ"]);
                intCodTipSer = Convert.ToInt16(drFilaRes[0]["codTipServ"]);

                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }


        }

        public bool BorrarDetalle()
        {
            if (intNroOrd < 1)
            {
                strError = "NO se asigno numero de orden o es un numero invalido";
                return false;
            }

            if (String.IsNullOrEmpty(strCodProd))
            {
                strError = "NO se asigno Codigo de Producto";
                return false;
            }

            if (dtDetalle == null) //Es el primer Detalle a Agregar
            {
                strError = "NO se asigno Tabla Detalle";
                return false;
            }

            try
            {
                DataRow[] drFilaRes;

                //string strBus = "nroOrdServ = '10' AND codProd = '70001'";
                string strBus = "nroOrdServ = '" + intNroOrd + "' AND codProd = '" + strCodProd + "'";

                drFilaRes = dtDetalle.Select(strBus);

                foreach (DataRow drFila in drFilaRes)
                {
                    dtDetalle.Rows.Remove(drFila);
                }


                /*if (drFilaRes.Length > 1)
                {
                    strError = "La busqueda retorno varias filas";
                    return false;
                }

                dtDetalle.Rows.Remove(drFilaRes[0]);*/

                return true;
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
