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
        public OperacionModel Operacion { get; set; } = new OperacionModel();

        public Form1()
        {
            InitializeComponent();

            dataTableOperations.DataSource = ListaOperaciones.DTOperaciones;
        }


        /// VARIABLES FRONT
        string contador = ""; /// Label primario
        string acumulador = ""; /// Label post resultado



        /// METODOS FRONT

        /// 1) Metodo que permite plasmar el valor del boton en el primer label.
        private void changeLabelContador(string BtnText)
        {
            if (acumulador != "")
            { /// Si ya hemos escrito algo previamente, nos borrara el contenido y escribira lo nuevo.
                labelContador.Text = "";
            }
            contador += BtnText;
            labelContador.Text = contador;
        }



        /// Metodo para los operadores.
        private void getTextButtonOperator(object sender, EventArgs e)
        {
            /// Manipulamos el sender para obtener el .Text del boton que ejecuta dicho metodo.
            var btn = (Button)sender;

            ///Evitamos poder poner primero un operador.
            if (labelContador.Text != "0")
            {
                /// TODO - EJECUTAR METODO " changeLabelResult " PARA MOSTRAR EN PANTALLA 
                /// Almacenamos el valor en nuestro Model.
                Operacion.operador = btn.Text;
            }
        }

        private void boton_igual_Click(object sender, EventArgs e)
        {

        }


        /// Metodo para los botones numericos.

        private void getTextButtonNumbers (object sender, EventArgs e)
        {
            /// Manipulamos el sender para obtener el .Text del boton que ejecuta dicho metodo.
            var btn = (Button)sender;


            /*Si el contador, NO posee una "," (Debido a que ya pusimos una antes)
              no pondra otra coma mas.*/
            if (btn.Text == "," && !contador.Contains(","))
            {
                changeLabelContador(btn.Text);
            }
            else
            {
                /* Resto de botones */
                changeLabelContador(btn.Text);
            }
        }



        ////////////////////

        private void boton_negacion_Click(object sender, EventArgs e)
        {

        }

        private void boton_clear_Click(object sender, EventArgs e)
        {

        }

        private void boton_delete_Click(object sender, EventArgs e)
        {

        }

    }
}
