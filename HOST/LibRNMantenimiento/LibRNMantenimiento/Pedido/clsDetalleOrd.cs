using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using LibBasica;
using System.Web.UI.WebControls;

namespace LibRNMantenimiento.Pedido
{
    public class clsDetalleOrd
    {
        #region Atributos

        private Int32 intNroOrd;
        private string strCodProd;
        private int intCant;
        private decimal decValor;
        private int intCodTipSer;

        private GridView gvDetOrd;

        private DropDownList ddlTipServicio;

        

        private string strError;

        private clsConexBd objConBd;
        private clsCombo objCombo;

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


        public GridView gsGvDetOrd
        {
            get { return gvDetOrd; }
            set { gvDetOrd = value; }
        }
        public DropDownList gsDdlTipServicio
        {
            get { return ddlTipServicio; }
            set { ddlTipServicio = value; }
        }


        public string gError
        {
            get { return strError; }
        }

        #endregion


        #region Metodos Privados

        private bool ValDatosDetOrd()
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

        private bool AdicionarParamsDetOrd()
        {
            if (!objConBd.AdicionarParametro(ParameterDirection.Input, "@NUMORD", SqlDbType.BigInt, 10, intNroOrd))
            {
                strError = objConBd.gError;
                return false;
            }

            if (!objConBd.AdicionarParametro(ParameterDirection.Input, "@PROD", SqlDbType.VarChar, 8, strCodProd))
            {
                strError = objConBd.gError;
                return false;
            }

            if (!objConBd.AdicionarParametro(ParameterDirection.Input, "@CANT", SqlDbType.Int, 6, intCant))
            {
                strError = objConBd.gError;
                return false;
            }

            if (!objConBd.AdicionarParametro(ParameterDirection.Input, "@VALOR", SqlDbType.Decimal, 12, decValor))
            {
                strError = objConBd.gError;
                return false;
            }

            if (!objConBd.AdicionarParametro(ParameterDirection.Input, "@TIPSER", SqlDbType.Int, 4, intCodTipSer))
            {
                strError = objConBd.gError;
                return false;
            }

            return true;
        }

        private bool InsertarDetOrd()
        {
            if (!ValDatosDetOrd())
            {
                return false;
            }

            objConBd.gsSql = "DETPEDIDOS_I";

            if (!AdicionarParamsDetOrd())
            {
                return false;
            }

            if (!objConBd.ExecSql(true)) //Metodo que Ejecuta Instrucciones Insert, Update  y Delete en la BD
            {
                strError = objConBd.gError;
                objConBd.gCommand.Parameters.Clear();
                return false;
            }

            objConBd.gCommand.Parameters.Clear();
            return true;
        }

        private bool ModificarDetOrd()
        {
            if (!ValDatosDetOrd())
            {
                return false;
            }

            objConBd.gsSql = "DETPEDIDOS_U";

            if (!AdicionarParamsDetOrd())
            {
                return false;
            }

            if (!objConBd.ExecSql(true)) //Metodo que Ejecuta Instrucciones Insert, Update  y Delete en la BD
            {
                strError = objConBd.gError;
                objConBd.gCommand.Parameters.Clear();
                return false;
            }

            objConBd.gCommand.Parameters.Clear();
            return true;
        }

        private bool ExisteDetalle()
        {
            if (intNroOrd < 1)
            {
                strError = "NO se asigno numero de orden o es un numero invalido";
                return false;
            }

            if (String.IsNullOrEmpty(strCodProd))
            {
                strError = "NO se asigno codigo de producto";
                return false;
            }

            objConBd.gsSql = "DETPEDIDOS_S_nroOrdCodPro";

            if (!objConBd.AdicionarParametro(ParameterDirection.Input, "@NUMORD", SqlDbType.BigInt, 10, intNroOrd))
            {
                strError = objConBd.gError;
                //objConBd = null;
                return false;
            }

            if (!objConBd.AdicionarParametro(ParameterDirection.Input, "@CODPRO", SqlDbType.VarChar, 8, strCodProd))
            {
                strError = objConBd.gError;
                //objConBd = null;
                return false;
            }

            if (!objConBd.GetScalar(true))
            {
                strError = objConBd.gError;
                objConBd.gCommand.Parameters.Clear();
                //objConBd = null;
                return false;
            }

            if (objConBd.gScalar == null)
            {
                strError = "NO Existe Producto de Pedido";
                objConBd.gCommand.Parameters.Clear();
                //objConBd = null;
                return false;
            }

            objConBd.gCommand.Parameters.Clear();
            //objConBd = null;
            return true;
        }


        #endregion


        #region Metodos Publicos

        public bool GrabarDetOrd()
        {
            objConBd = new clsConexBd();

            if (ExisteDetalle()) //Existe, por lo tanto Modifico
            {
                if (!ModificarDetOrd())
                {
                    objConBd = null;
                    return false;
                }

                objConBd = null;
                return true;
            }
            else //NO Existe, por lo tanto Inserto
            {
                if (!InsertarDetOrd())
                {
                    objConBd = null;
                    return false;
                }

                objConBd = null;
                return true;
            }
        }

        public bool LlenarDetOrd()
        {
            if (intNroOrd < 1)
            {
                strError = "NO se asigno numero de orden o es un numero invalido";
                return false;
            }

            if (gvDetOrd == null)
            {
                strError = "NO se asignó GridView a poblar";
                return false;
            }

            objConBd = new clsConexBd();

            objConBd.gsSql = "DETPEDIDOS_S_nroOrd";
            objConBd.gsNomTabla = "Detalle";

            if (!objConBd.AdicionarParametro(ParameterDirection.Input, "@NUMORD", SqlDbType.BigInt, 10, intNroOrd))
            {
                strError = objConBd.gError;
                objConBd = null;
                return false;
            }

            if (!objConBd.GetDataSet(true))
            {
                strError = objConBd.gError;
                objConBd.gCommand.Parameters.Clear();
                objConBd = null;
                return false;
            }

            if (objConBd.gDataSet.Tables[objConBd.gsNomTabla].Rows.Count < 1)
            {
                strError = "No hay registros de Detalle de Orden";
                objConBd.gCommand.Parameters.Clear();
                objConBd = null;
                return false;
            }

            gvDetOrd.DataSource = objConBd.gDataSet.Tables[objConBd.gsNomTabla];
            gvDetOrd.DataBind();

            objConBd.gCommand.Parameters.Clear();
            objConBd = null;
            return true;
        }


        public bool LlenarGridDetalle()
        {
            if (intNroOrd < 1)
            {
                strError = "NO se asigno numero de orden o es un numero invalido";
                return false;
            }

            if (gvDetOrd == null)
            {
                strError = "NO se asignó GridView a poblar";
                return false;
            }

            objConBd = new clsConexBd();

            objConBd.gsSql = "DETPEDIDOS_S_nroOrdCodPro";
            objConBd.gsNomTabla = "Detalle";

            if (!objConBd.AdicionarParametro(ParameterDirection.Input, "@NUMORD", SqlDbType.BigInt, 10, intNroOrd))
            {
                strError = objConBd.gError;
                objConBd = null;
                return false;
            }

            if (!objConBd.GetDataSet(true))
            {
                strError = objConBd.gError;
                objConBd.gCommand.Parameters.Clear();
                objConBd = null;
                return false;
            }

            if (objConBd.gDataSet.Tables[objConBd.gsNomTabla].Rows.Count < 1)
            {
                strError = "No hay registros de Detalle de Orden";
                objConBd.gCommand.Parameters.Clear();
                objConBd = null;
                return false;
            }

            gvDetOrd.DataSource = objConBd.gDataSet.Tables[objConBd.gsNomTabla];
            gvDetOrd.DataBind();

            objConBd.gCommand.Parameters.Clear();
            objConBd = null;
            return true;
        }

        public bool LlenarTipoServ()
        {
            if (ddlTipServicio == null)
            {
                strError = "NO se asignó Lista Despegable de Tipo Servicio";
                return false;
            }

            objCombo = new clsCombo();

            objCombo.gsSql = "TIPSERV_S";
            objCombo.gsNomTabla = "TipServicio";
            objCombo.gsColValor = "codTipServ";
            objCombo.gsColTexto = "desTipServ";

            objCombo.gsDdlGen = ddlTipServicio;

            if (!objCombo.LlenarDdl())
            {
                strError = objCombo.gError;
                objCombo = null;
                return false;
            }

            ddlTipServicio = objCombo.gsDdlGen;

            objCombo = null;
            return true;
        }

        #endregion

    }
}
