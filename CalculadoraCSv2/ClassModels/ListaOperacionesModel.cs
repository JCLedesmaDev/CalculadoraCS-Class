using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculadoraCSv2.ClassModels
{
    public class ListaOperacionesModel
    {

        // TODO - Agregar boton para que cambie el .visible() de nuestro dataGrid

        public DataTable DTOperaciones { get; set; } = new DataTable();
        public int ultimo_id { get; set; } = 0;

        /* Creacion de Tabla */
        public ListaOperacionesModel()
        {
            DTOperaciones.TableName = "Lista operaciones";
            DTOperaciones.Columns.Add("Id");
            DTOperaciones.Columns.Add("Operacion");

            verifyFileDTOperaciones();
        }

        /* Verificamos la existencia de nuestro archivo .xml */
        public void verifyFileDTOperaciones()
        {
            if (System.IO.File.Exists("ListaOperaciones.xml"))
            {
                // Lo lee
                DTOperaciones.ReadXml("ListaOperaciones.xml");

                // Obtiene el Id mas grande y lo almacena.
                ultimo_id = getLastId(DTOperaciones, ultimo_id);
            }
        }


        public int getLastId(DataTable table, int id)
        {
            int numberId = 0;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (Convert.ToInt32(table.Rows[i]["Id"]) > id)
                {
                    numberId = Convert.ToInt32(table.Rows[i]["Id"]);
                }
            }
            return numberId;
        }
       
        public void calculateOperation(OperacionModel operacion)
        {
           switch (operacion.operador)
           {
                case "➕":
                    operacion.resultado = operacion.primerNumero + operacion.segundoNumero;

                    break;  

                case "➖":
                    operacion.resultado = operacion.primerNumero - operacion.segundoNumero;
                    break;

                case "✖":
                    operacion.resultado = operacion.primerNumero * operacion.segundoNumero;
                    break;

                case "➗":
                    operacion.resultado = operacion.primerNumero / operacion.segundoNumero;
                    break;

                default:
                    break;
           }

            operacion.calculo = $"{operacion.primerNumero} {operacion.operador} {operacion.segundoNumero} = {operacion.resultado}";
            addOperation(operacion);
        }
       
        private void addOperation (OperacionModel operacion)
        {
            ultimo_id = ultimo_id + 1;
            DTOperaciones.Rows.Add();
            int NumeroRegistro = DTOperaciones.Rows.Count - 1;

            DTOperaciones.Rows[NumeroRegistro]["Id"] = (ultimo_id).ToString();
            DTOperaciones.Rows[NumeroRegistro]["Operacion"] = operacion.calculo;

            DTOperaciones.WriteXml("ListaOperaciones.xml");
        }




        public void delete (string mode, string calculo)
        {
            if (mode == "clearTable")
            {
                DTOperaciones.Rows.Clear();
                DTOperaciones.WriteXml("ListaOperaciones.xml");
                ultimo_id = 0;
            }
            else
            {
                for (int i = 0; i < DTOperaciones.Rows.Count; i++)
                {
                    if (DTOperaciones.Rows[i]["Operacion"].ToString() == calculo)
                    {
                        DTOperaciones.Rows[i].Delete();
                        DTOperaciones.WriteXml("LListaOperacionesista.xml");
                    }
                }
            }
        }

        /// Seleccionar operaicon en particular
    }
}
