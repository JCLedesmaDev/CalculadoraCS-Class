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


        private void getTextButton(object sender, EventArgs e)
        {
            ///Evitamos poder poner primero un operador.
            if (labelContador.Text != "0")
            {

                /// Manipulamos el sender para obtener el .Text del boton que ejecuta dicho metodo.
                var btn = (Button)sender;

                /// TODO - EJECUTAR METODO PARA MOSTRAR EN PANTALLA 
                /// Almacenamos el valor en nuestro Model.
                Operacion.operador = btn.Text;
            }
        }

        private void boton_division_Click(object sender, EventArgs e)
        {

        }

        private void boton_negacion_Click(object sender, EventArgs e)
        {

        }

        private void boton_clear_Click(object sender, EventArgs e)
        {

        }

        private void boton_delete_Click(object sender, EventArgs e)
        {

        }

        private void boton_7_Click(object sender, EventArgs e)
        {

        }

        private void boton_8_Click(object sender, EventArgs e)
        {

        }

        private void boton_9_Click(object sender, EventArgs e)
        {

        }

        private void boton_multi_Click(object sender, EventArgs e)
        {

        }

        private void boton_resta_Click(object sender, EventArgs e)
        {

        }

        private void boton_6_Click(object sender, EventArgs e)
        {

        }

        private void boton_5_Click(object sender, EventArgs e)
        {

        }

        private void boton_4_Click(object sender, EventArgs e)
        {

        }

        private void boton_1_Click(object sender, EventArgs e)
        {

        }

        private void boton_2_Click(object sender, EventArgs e)
        {

        }

        private void boton_3_Click(object sender, EventArgs e)
        {

        }

        private void boton_suma_Click(object sender, EventArgs e)
        {

        }

        private void boton_igual_Click(object sender, EventArgs e)
        {

        }

        private void boton_coma_Click(object sender, EventArgs e)
        {

        }

        private void boton_0_Click(object sender, EventArgs e)
        {

        }
    }
}
