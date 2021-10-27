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

        /* Instancia de clases */
        public ListaOperacionesModel ListaOperaciones { get; set; } = new ListaOperacionesModel();
        public OperacionModel Operacion { get; set; } = new OperacionModel { 
            primerNumero = 0,
            segundoNumero = 0,
            operador = "",
            resultado = 0,
        };
        //public OperacionModel Operacion { get; set; } = new OperacionModel();

        public Form1()
        {
            InitializeComponent();
            dataTableOperations.DataSource = ListaOperaciones.DTOperaciones;
        }

        /* VARIABLES FRONT */
        string contador = ""; /// Label primario
        string acumulador = ""; /// Label post resultado


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
            acumulador = "";

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

        /* ---------------------------------------------------------  */

        /// Metodo para los operadores (+ - * /).
        private void getTextButtonOperator(object sender, EventArgs e)
        {
            /// Manipulamos el sender para obtener el .Text del boton que ejecuta dicho metodo.
            var btn = (Button)sender;

            ///Evitamos poder poner primero un operador.
            if (labelContador.Text != "0")
            {
                /// Almacenamos el valor en nuestro Model.
                Operacion.operador = btn.Text;

                /// Agregamos el operador a nuestra operacion total
                showOperation(Operacion.operador);
            }
        }
        private void showOperation(string BtnOperador)
        {

            /// Se valida aca, cuando ingresamos el primer numero de la primera operacion
            if (acumulador == "")
            {
                /// Si valida aca, es porque hemos puesto el primer numero
                acumulador = contador + " " + BtnOperador;

                /// Convertimos el 1er numero a numero flotante.
                Operacion.primerNumero = System.Convert.ToSingle(contador);

                /// Mostramos operacion en pantalla.
                showScreen(acumulador);
                return;
            }

            /// Se valida aca, cuando ingresamos el segundo numero de la primera operacion
            if (acumulador != "")
            {
                acumulador = acumulador + " " + contador + " " + BtnOperador;

                /// Convertimos el 2do numero a numero flotante.
                Operacion.segundoNumero = System.Convert.ToSingle(contador);

                /// Mostramos operacion en pantalla.
                showScreen(acumulador);
                return;
            }

            /// TODO - En caso de que hayamos hehco una operacion y queramos continuar
            /*else if (acumulador != "" && resultado != "")
            {
                labelResult.Text = resultado + " " + BtnOperador;
            }*/



        }
        private void showScreen(string acumulador)
        {
            labelResult.Text = acumulador;
            contador = ""; /// Permite escribir nuevamente un numero de 0.
        }


        /// Metodo para los botones numericos y la ",".
        private void getTextButtonNumbers (object sender, EventArgs e)
        {
            /// Manipulamos el sender para obtener el .Text del boton que ejecuta dicho metodo.
            var btn = (Button)sender;  

            /*Si el contador, NO posee una "," (Debido a que ya pusimos una antes)
              no pondra otra coma mas.*/
            if (btn.Text == "," && !contador.Contains(","))
            {
                showValueNumbersScreen(btn.Text);
            }
            if (btn.Text != ",")
            {
                /* Resto de botones */
                showValueNumbersScreen(btn.Text);
            }
        }
        /// Metodo que permite plasmar el valor del boton en el primer label.
        private void showValueNumbersScreen(string BtnText)
        {
            if (acumulador != "")
            { /// Si ya hemos escrito algo previamente, nos borrara el contenido y escribira lo nuevo.
                labelContador.Text = "";
            }
            contador += BtnText;
            labelContador.Text = contador;
        }


        /* OBSERVAR */
        private void boton_igual_Click (object sender, EventArgs e)
        {
            /// Manipulamos el sender para obtener el .Text del boton que ejecuta dicho metodo.
            var btn = (Button)sender;

            /*try
            {
                /// Si no escribimos nada, no se ejecuta el igual.
                if (Operacion.primerNumero == 0 && Operacion.operador == "")
                {
                    throw new Exception("Ingrese 2 numeros y operador.");
                }

                /// Caso de que hayamos escrito un numero y un operador, SI.
                showOperation(btn.Text);


                if (resultado == "")
                { /// Se ejecuta cuando obtenemos un primer resultado.
                    calculateOperation(
                        operador,
                        primerNumero,
                        segundoNumero
                    );
                }
                else
                { /// Se ejecuta cuando sumamos un valor al resultado ya obtenido.
                    labelResult.Text = resultado + " " + operador + " " + contador + " " + boton_igual.Text;
                    calculateOperation(
                        operador,
                        System.Convert.ToSingle(resultado),
                        System.Convert.ToSingle(contador)
                    );
                }
                contador = ""; /// Permite escribir nuevamente un numero de 0.
                labelContador.Text = resultado;

            }
            catch (Exception error)
            {
                labelResult.Text = error.Message.ToString();
            }*/

        }
        /* OBSERVAR */
    }
}