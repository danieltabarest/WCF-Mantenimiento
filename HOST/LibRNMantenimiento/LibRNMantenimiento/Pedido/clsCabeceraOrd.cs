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
    public class clsCabeceraOrd
    {
        #region Atributos
        
        //Atributos Cabecera Orden
        private Int32 intNroOrd;
        private DateTime datFecOrd;
        private string strCodCli;
        private string strCodTec;
        private decimal decVlr;
        private decimal decIva;

        

        private GridView gvCabOrd;
        private DropDownList ddlCliente;
        private DropDownList ddlTecnico;

        
        private string strError;

        private clsGrid objGrid;
        private clsCombo objCombo;

        private clsConexBd objConBd;



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

        public GridView gsGvCabOrd
        {
            get { return gvCabOrd; }
            set { gvCabOrd = value; }
        }

        public DropDownList gsDdlCliente
        {
            get { return ddlCliente; }
            set { ddlCliente = value; }
        }

        public DropDownList gsDdlTecnico
        {
            get { return ddlTecnico; }
            set { ddlTecnico = value; }
        }


        public string gError
        {
            get { return strError; }
        }

        #endregion


        #region Metodos Privados

        private bool ExistePedido()
        {
            if (intNroOrd < 1)
            {
                strError = "NO se asigno numero de orden o es un numero invalido";
                return false;
            }

            objConBd.gsSql = "CABPEDIDOS_S_nroOrd";

            if (!objConBd.AdicionarParametro(ParameterDirection.Input, "@NUMORD", SqlDbType.BigInt, 10, intNroOrd))
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
                strError = "NO Existe Pedido con numero de orden asignado";
                objConBd.gCommand.Parameters.Clear();
                //objConBd = null;
                return false;
            }

            objConBd.gCommand.Parameters.Clear();
            //objConBd = null;
            return true;
        }

        private bool ValDatosCabOrd()
        {
            

            if (datFecOrd == null)
            {
                strError = "NO se asigno fecha de orden";
                return false;
            }

            if (String.IsNullOrEmpty(strCodCli))
            {
                strError = "NO se asigno Codigo de Cliente";
                return false;
            }

            if (String.IsNullOrEmpty(strCodTec))
            {
                strError = "NO se asigno Codigo de Tecnico";
                return false;
            }

            if (decVlr < 1)
            {
                strError = "Valor Orden menor a Cero o Invalido";
                return false;
            }

            if (decIva < 1)
            {
                strError = "Iva menor a Cero o Invalido";
                return false;
            }

            return true;


        }

        private bool InsertarCabOrd()
        {
            if (!ValDatosCabOrd())
            {
                return false;
            }

            objConBd.gsSql = "CABPEDIDOS_I";

            if (!objConBd.AdicionarParametro(ParameterDirection.Output, "@NUMORD", SqlDbType.BigInt, 10, 0))
            {
                strError = objConBd.gError;
                return false;
            }

            if (!AdicionarParamsCabOrd())
            {
                return false;
            }

            if (!objConBd.ExecSql(true)) //Metodo que Ejecuta Instrucciones Insert, Update  y Delete en la BD
            {
                strError = objConBd.gError;
                objConBd.gCommand.Parameters.Clear();
                return false;
            }

            intNroOrd = Convert.ToInt32(objConBd.gCommand.Parameters["@NUMORD"].Value);
            objConBd.gCommand.Parameters.Clear();
            return true;
        }

        private bool ModificarCabOrd()
        {
            if (intNroOrd < 1)
            {
                strError = "NO se asigno numero de orden o es un numero invalido";
                return false;
            }

            if (!ValDatosCabOrd())
            {
                return false;
            }

            objConBd.gsSql = "CABPEDIDOS_U";

            if (!objConBd.AdicionarParametro(ParameterDirection.Input, "@NUMORD", SqlDbType.BigInt, 10, intNroOrd))
            {
                strError = objConBd.gError;
                return false;
            }

            if (!AdicionarParamsCabOrd())
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



        private bool AdicionarParamsCabOrd()
        {
            

            if (!objConBd.AdicionarParametro(ParameterDirection.Input, "@FECORD", SqlDbType.SmallDateTime, 10, datFecOrd))
            {
                strError = objConBd.gError;
                return false;
            }

            if (!objConBd.AdicionarParametro(ParameterDirection.Input, "@CODCLI", SqlDbType.VarChar, 10, strCodCli))
            {
                strError = objConBd.gError;
                return false;
            }

            if (!objConBd.AdicionarParametro(ParameterDirection.Input, "@CODTEC", SqlDbType.VarChar, 10, strCodTec))
            {
                strError = objConBd.gError;
                return false;
            }

            if (!objConBd.AdicionarParametro(ParameterDirection.Input, "@VLRORD", SqlDbType.Decimal, 12, decVlr))
            {
                strError = objConBd.gError;
                return false;
            }

            if (!objConBd.AdicionarParametro(ParameterDirection.Input, "@IVAORD", SqlDbType.Decimal, 12, decIva))
            {
                strError = objConBd.gError;
                return false;
            }

            return true;
        }



        #endregion


        #region Metodos Publicos

        public bool GrabarCabOrd()
        {
            objConBd = new clsConexBd();

            if (ExistePedido()) //Existe, por lo tanto Modifico
            {
                if (!ModificarCabOrd())
                {
                    objConBd = null;
                    return false;
                }

                objConBd = null;
                return true;
            }
            else //NO Existe, por lo tanto Inserto
            {
                if (!InsertarCabOrd())
                {
                    objConBd = null;
                    return false;
                }

                objConBd = null;
                return true;
            }
        }

        public bool LlenarCabOrd()
        {
            if (gvCabOrd == null)
            {
                strError = "NO se asignó GridView a poblar";
                return false;
            }

            objGrid = new clsGrid();

            objGrid.gsSql = "CABPEDIDOS_S";
            objGrid.gsNomTabla = "Cabecera";
            objGrid.gsGvGen = gvCabOrd;

            if (!objGrid.LlenarGridWeb())
            {
                strError = objGrid.gError;
                objGrid = null;
                return false;
            }

            gvCabOrd = objGrid.gsGvGen;

            objGrid = null;
            return true;
        }

        public bool ObtenerCabOrd()
        {

            objConBd = new clsConexBd();

            if (!ExistePedido())
            {
                objConBd = null;
                return false;
            }

            objConBd.gsSql = "CABPEDIDOS_S_nroOrd";

            if (!objConBd.AdicionarParametro(ParameterDirection.Input, "@NUMORD", SqlDbType.BigInt, 10, intNroOrd))
            {
                strError = objConBd.gError;
                objConBd = null;
                return false;
            }

            if (!objConBd.GetDataReader(true))
            {
                strError = objConBd.gError;
                objConBd.gCommand.Parameters.Clear();
                objConBd = null;
                return false;
            }

            /*while (objConBd.gDataReader.Read())
            {

            }*/

            if (!objConBd.gDataReader.Read())
            {
                strError = "NO Existe Orden en Cabecera";
                objConBd.gCommand.Parameters.Clear();
                objConBd = null;
                return false;
            }

            try
            {
                datFecOrd = (DateTime)objConBd.gDataReader["fecOrdServ"];
                strCodCli = (string)objConBd.gDataReader[2];
                strCodTec = (string)objConBd.gDataReader[3];
                decVlr = (decimal)objConBd.gDataReader[4];
                decIva = (decimal)objConBd.gDataReader[5];

                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
            finally
            {
                objConBd.gCommand.Parameters.Clear();
                objConBd = null;
            }
    
        }

        public bool LlenarCliente()
        {
            if (ddlCliente == null)
            {
                strError = "NO se asignó Lista Despegable de Cliente";
                return false;
            }

            objCombo = new clsCombo();

            objCombo.gsSql = "CLIENTE_S";
            objCombo.gsNomTabla = "Cliente";
            objCombo.gsColValor = "codCli";
            objCombo.gsColTexto = "nombCli";

            objCombo.gsDdlGen = ddlCliente;

            if (!objCombo.LlenarDdl())
            {
                strError = objCombo.gError;
                objCombo = null;
                return false;
            }

            ddlCliente = objCombo.gsDdlGen;

            objCombo = null;
            return true;
        }

        public bool LlenarTecnico()
        {
            if (ddlTecnico == null)
            {
                strError = "NO se asignó Lista Despegable de Tecnico";
                return false;
            }

            objCombo = new clsCombo();

            objCombo.gsSql = "TECNICO_S";
            objCombo.gsNomTabla = "Tecnico";
            objCombo.gsColValor = "codTec";
            objCombo.gsColTexto = "nombTec";

            objCombo.gsDdlGen = ddlTecnico;

            if (!objCombo.LlenarDdl())
            {
                strError = objCombo.gError;
                objCombo = null;
                return false;
            }

            ddlTecnico = objCombo.gsDdlGen;

            objCombo = null;
            return true;
        }

        #endregion
    }
}
