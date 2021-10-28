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
        public bool validationOperation()
        {
            bool error = false;
            if (primerNumero == 0 && operador == "")
            {
               error = true;
            }
            return error;
        }

    }
}
