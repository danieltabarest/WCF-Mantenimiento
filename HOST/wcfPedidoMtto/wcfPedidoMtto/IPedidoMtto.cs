using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data;

namespace wcfPedidoMtto
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IPedidoMtto
    {

        
        [OperationContract]
        clsPedidoMtto GrabarPedido(clsPedidoMtto objPedido);

        // TODO: Add your service operations here
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class clsPedidoMtto
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

        private bool bolFunc;

        

        #endregion



        #region Propiedades

        [DataMember]
        public Int32 NumeroOrden
        {
            get { return intNroOrd; }
            set { intNroOrd = value; }
        }

        [DataMember]
        public DateTime FechaOrden
        {
            get { return datFecOrd; }
            set { datFecOrd = value; }
        }

        [DataMember]
        public string CodCliente
        {
            get { return strCodCli; }
            set { strCodCli = value; }
        }

        [DataMember]
        public string CodTecnico
        {
            get { return strCodTec; }
            set { strCodTec = value; }
        }

        [DataMember]
        public decimal ValorOrd
        {
            get { return decVlr; }
            set { decVlr = value; }
        }

        [DataMember]
        public decimal IvaOrden
        {
            get { return decIva; }
            set { decIva = value; }
        }

        [DataMember]
        public DataTable DetalleProd
        {
            get { return dtDetalle; }
            set { dtDetalle = value; }
        }

        [DataMember]
        public string Error
        {
            get { return strError; }
            set { strError = value; }
        }

        [DataMember]
        public bool Funciono
        {
            get { return bolFunc; }
            set { bolFunc = value; }
        }

        #endregion
    }
}
