using CalculadoraCSv2.ClassModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculadoraCSv2
{
    public partial class Form1 : Form
    {
        public ListaOperacionesModel ListaOperaciones { get; set; } = new ListaOperacionesModel();
        public OperacionModel Operacion { get; set; } = new OperacionModel {
            primerNumero = 0,
            segundoNumero = 0,
            operador = "",
            resultado = 0,
            calculo = "",
        };

        public Form1()
        {
            InitializeComponent();
            dataTableOperations.DataSource = ListaOperaciones.DTOperaciones;

            // Ocultamos la columna del Id
            dataTableOperations.Columns["Id"].Visible = false;
        }

        /* VARIABLES FRONT */
        string contador = ""; /// Contendra los valores numericos proveniente de "getNumbers"
        string calculo = ""; /// Contentra la operacion en cuestion (valores numericos + operadores)

        #region Obtenemos los valores numericos y los mostramos en pantalla.

        private void getNumbers(object sender, EventArgs e)
        {
            /// Manipulamos el sender para obtener el .Text del boton que ejecuta dicho metodo.
            var btn = (Button)sender;

            /*Si el contador, NO posee una "," (Debido a que ya pusimos una antes)
              no pondra otra coma mas.*/
            if (btn.Text == "," && !contador.Contains(","))
            {
                showNumberInScreen(btn.Text);
            }
            if (btn.Text != ",")
            {
                /* Resto de botones */
                showNumberInScreen(btn.Text);
            }
        }
        private void showNumberInScreen(string BtnText)
        {
            /// Mostrar numeros en pantalla y almacenar en "contador".
            if (calculo != "")
            { /// Si ya hemos escrito algo previamente, nos borrara el contenido y escribira lo nuevo.
                labelContador.Text = "";
            }
            contador += BtnText;
            labelContador.Text = contador;
        }

        #endregion

        #region Obtenemos el "operador" de la operacion y lo almacenamos con "contador" en "calculo"

        /* Metodo para los operadores (+ - * /).*/
        private void getOperator(object sender, EventArgs e)
        {
            /// Manipulamos el sender para obtener el .Text del boton que ejecuta dicho metodo.
            var btn = (Button)sender;

            if (Operacion.operador != "" && 
                Operacion.primerNumero != 0 && 
                contador != "")
            {
                if (Operacion.resultado != 0)
                {
                   Operacion.primerNumero = Operacion.resultado;
                    Operacion.resultado = 0;
                }

                addOperatorToCalcule(Operacion.operador);
                ListaOperaciones.calculateOperation(Operacion);

                contador = ""; /// Permite escribir nuevamente un numero de 0.
                labelContador.Text = Operacion.resultado.ToString();
            }

            ///Evitamos poder poner primero un operador.
            if (labelContador.Text != "0")
            {
                Operacion.operador = btn.Text;
                addOperatorToCalcule(Operacion.operador);
            }
        }
        private void addOperatorToCalcule(string BtnOperador)
        {
            ///Agregamos el operador al primer termino de nuestra operacion. 
            if (calculo == "" && Operacion.resultado == 0)
            {
                loadFirstNumber(BtnOperador);
                return;
            }

            /// Se ejecutara al realizar la primera "operacion". Por ejem.: 2 + 9 = 
            if (calculo != "" && Operacion.resultado == 0)
            {
                loadSecondNumber(BtnOperador);
                return;
            }

            /* En caso de que hayamos hecho una operacion y queramos continuar. Nos aparecera el 
             resultado de la  primera operacion seguido del OPERADOR en cuestion */
            if (calculo != "" && Operacion.resultado != 0)
            {
                calculo = Operacion.resultado + " " + Operacion.operador;
                labelResult.Text = calculo;
                return;
            }
        }
        private void loadFirstNumber (string BtnOperador)
        {
            calculo = contador + " " + BtnOperador;
            /* PD: Contador posee el "primer termino" obtenido por el metodo "showNumberScreen"*/

            /// Convertimos el 1er numero a numero flotante y lo almacenamos.
            Operacion.primerNumero = System.Convert.ToSingle(contador);

            /// Mostramos operacion en pantalla.
            showCalculeInScreen(calculo);
        }
        private void loadSecondNumber(string BtnOperador) {
            calculo = calculo + " " + contador + " " + BtnOperador;
            /* PD: Contador posee el "segundo termino" obtenido por el metodo "showNumberScreen"*/

            /// Convertimos el 2do numero a numero flotante.
            Operacion.segundoNumero = System.Convert.ToSingle(contador);

            /// Mostramos operacion en pantalla.
            showCalculeInScreen(calculo);
        }
        private void showCalculeInScreen(string calculo)
        {
            labelResult.Text = calculo;
            contador = ""; /// Permite escribir nuevamente un numero de 0.
        }

        #endregion

        private void boton_igual_Click(object sender, EventArgs e)
        {
            /// Manipulamos el sender para obtener el .Text del boton que ejecuta dicho metodo.
            var btn = (Button)sender;
            try
            {
                /// Si no escribimos nada, no se ejecuta el igual.
                if (Operacion.validationOperation(Operacion))
                {
                    throw new Exception("Ingrese 2 numeros y operador.");
                }
                addOperatorToCalcule(btn.Text);
                if (Operacion.resultado == 0)
                {
                    doOperation(Operacion);
                }
                else {
                    Operacion.primerNumero = System.Convert.ToInt32(Operacion.resultado);
                    Operacion.operador = Operacion.operador;
                    Operacion.segundoNumero = System.Convert.ToInt32(contador);

                    doOperation(Operacion);
                }
                contador = ""; /// Permite escribir nuevamente un numero de 0.
                labelContador.Text = Operacion.resultado.ToString();
            }
            catch (Exception error)
            {
                labelResult.Text = error.Message.ToString();
            }
        }
        private void doOperation(OperacionModel Operacion)
        {
            ListaOperaciones.calculateOperation(Operacion);
            Operacion.primerNumero = 0;
            Operacion.operador = "";
            Operacion.segundoNumero = 0;
            Operacion.calculo = "";
        }


        #region Botones particulares 
        /* Metodos puramente de Front - Edicion de labels */
        private void boton_negacion_Click(object sender, EventArgs e)
        {
            //Si nuestro label esta en "0". no pondra signo -
            if (labelContador.Text != "0")
            {
                //Si nuestro contador NO posee el "-", permite agregarselo                         ////if (!contador.Contains("-")) 
                if (!contador.Contains("-"))
                {
                    contador = "-" + contador;
                }
                else
                {// caso contrario, se lo quita.
                    contador = contador.Replace("-", "");
                }
                labelContador.Text = contador;
            }
        }
        private void boton_clear_Click(object sender, EventArgs e)
        {
            contador = "";
            calculo = "";

            Operacion.primerNumero = 0;
            Operacion.operador = "";
            Operacion.segundoNumero = 0;
            Operacion.resultado = 0;
            Operacion.calculo = "";

            labelContador.Text = "0";
            labelResult.Text = "";
        }

        private void boton_delete_Click(object sender, EventArgs e)
        {
            if (contador.Length != 0)
            {
                contador = contador.Remove(contador.Length - 1);
                labelContador.Text = contador;
            }
            if (labelContador.Text == "")
            {
                labelContador.Text = "0";
            }
        }

        private void boton_history(object sender, EventArgs e)
        {
            if (dataTableOperations.Visible == true && botonDelete.Visible == true)
            {
                dataTableOperations.Visible = false;
                botonDelete.Visible = false;
            }
            else
            {
                dataTableOperations.Visible = true;
                botonDelete.Visible = true;
            }
        }

        private void boton_delete_history(object sender, EventArgs e)
        {
            /// Si no se selecciono ninguna opcion, se elimina todos.
            if (dataTableOperations.CurrentCell.Value.ToString() == "")
            {
                ListaOperaciones.delete("clearTable", "");
            }
            else
            {
                //labelContador.Text = dataTableOperations.CurrentCell.Value.ToString();
                ListaOperaciones.delete(
                    "deleteOperation",
                    dataTableOperations.CurrentCell.Value.ToString() // Valor seleccionado.
                );
            }
            boton_clear_Click(sender, e);
        }


        /// TODO - BUG: Error al finalizar un calculo y querer escribir un numero para emepzar de 0 
        /// TODO - BUG: Error al seleccionar desde el listado y seguir suamndo, no funciona la manera continua, pero si sumando de uno en uno.

        #endregion

        private void getOneOperation(object sender, EventArgs e)
        {
            string operacion = dataTableOperations.CurrentCell.Value.ToString();

            if (operacion != "")
            {
                int longTotal = operacion.Length;
                int textoCortado = operacion.IndexOf("=") + 2;

                labelResult.Text = operacion.Substring(0, textoCortado);
                contador = "";
                showNumberInScreen(operacion.Substring(textoCortado, longTotal - textoCortado));
            }
            else
            {
                labelResult.Text = "";
            }
        }
    }
}