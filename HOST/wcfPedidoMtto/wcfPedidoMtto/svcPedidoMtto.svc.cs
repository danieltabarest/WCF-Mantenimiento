using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using LibRNMantenimiento.Transacciones;

namespace wcfPedidoMtto
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class svcPedidoMtto : IPedidoMtto
    {

        public clsPedidoMtto GrabarPedido(clsPedidoMtto objPedido)
        {
            if (objPedido == null)
            {
                objPedido.Error = "NO se asigno objeto con datos de pedidos";
            }

            clsTrnPedido objTrnPed = new clsTrnPedido();

            objTrnPed.gsNroOrd = objPedido.NumeroOrden;
            objTrnPed.gsFecOrd = objPedido.FechaOrden;
            objTrnPed.gsCodCli = objPedido.CodCliente;
            objTrnPed.gsCodTec = objPedido.CodTecnico;
            objTrnPed.gsVlr = objPedido.ValorOrd;
            objTrnPed.gsIva = objPedido.IvaOrden;

            objTrnPed.gsDtDetalle = objPedido.DetalleProd;

            if (objTrnPed.GrabarTrnPedido())
            {
                objPedido.NumeroOrden = objTrnPed.gsNroOrd;
                objPedido.Funciono = true;
            }
            else
            {
                objPedido.Error = objTrnPed.gError;
                objPedido.Funciono = false;
            }

            return objPedido;
        }
    }
}
