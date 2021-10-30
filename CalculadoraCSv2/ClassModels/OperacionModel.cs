using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculadoraCSv2.ClassModels
{
    public class OperacionModel
    {
        public int _id;
        public float primerNumero;
        public float segundoNumero;
        public string operador;
        public float resultado;
        public string calculo;



        // METODOS DE VALIDACIONES
        public bool validationOperation( OperacionModel Operacion)
        {
            bool error = false;
            if (Operacion.primerNumero == 0 && Operacion.operador == "")
            {
               error = true;
            }
            if ((Operacion.segundoNumero == 0 && Operacion.operador == ""))
            {
                error = true;
            }

            return error;
        }

    }
}
