using Calculadora.Operacion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Calculadora
{
    public partial class MainPage : ContentPage
    {
        private string renderizadoUsuario = "";
        private float resultadoCalculadora = 0.0f;

        private List<String> entradaUsuario = new List<String>();
        private List<String> entradaOperadores = new List<String>();

        public MainPage()
        {
            InitializeComponent();
        }

        
        public void eventoBoton(object sender, EventArgs a)
        {
            Button simbolo = ((Button)sender);
    
            switch (simbolo.Text)
            {
                case "C":
                    //este es diferente ... esto elimina toda la operacion por completo.
                    this.resetearCalculadora();
                    break;

                case "()":
                   
                    break;

                case "%":
                    //this.incluirSimboloOperacion(simbolo.Text);
                    break;

                case "/":
                    this.incluirSimboloOperacion(simbolo.Text);
                    break;

                case "x":
                    this.incluirSimboloOperacion(simbolo.Text);
                    break;

                case "-":
                    this.incluirSimboloOperacion(simbolo.Text);
                    break;

                case "+":
                    this.incluirSimboloOperacion(simbolo.Text);
                    break;

                case ".":
                    if ( this.entradaOperadores.Count == 0 )
                    {
                        this.entradaUsuario[0] += simbolo.Text;
                    }
                    else if ( this.entradaUsuario.Count > this.entradaOperadores.Count )
                    {
                        this.entradaUsuario[this.entradaOperadores.Count] += simbolo.Text;
                    }

                    this.renderizadoUsuario += simbolo.Text;
                    break;
            }

            this.FormatearRenderizado();
            //renderizado
            resultado1.Text = this.renderizadoUsuario;
        }

        public void EventoNumeros(object sender, EventArgs args)
        {
            Button btn = (Button)sender;
            String numero = btn.Text;
            
            if ( this.entradaUsuario.Count >= 1 )
            {
                if ( this.entradaOperadores.Count == 0 ) {
                    this.entradaUsuario[this.entradaUsuario.Count - 1] += numero;
                }
                else
                {
                    int indice = this.entradaOperadores.Count;
                    
                    if ( this.entradaUsuario.Count <= indice  )
                    {
                        this.entradaUsuario.Add(numero);
                    }
                    else
                    {
                        this.entradaUsuario[indice] += numero;
                    }

                    this.resultadoCalculadora = ClaseConstructora.DatosCalculadora(this.entradaOperadores, this.entradaUsuario);
                }
            }
            else
            {
                this.entradaUsuario.Add(numero);
            }

            //renderizado entrada del usuario
            this.FormatearRenderizado();

            resultado1.Text = this.renderizadoUsuario;
            resultado2.Text = resultadoCalculadora.ToString();
        }

        public void incluirSimboloOperacion(string signo)
        {
            /*  debemos validar esta parte de aqui de que no haya una cantidad de operadores mayores o iguales 
                a las entradas de los numeros.
            */
            string pattern = @"[+\-x\/]$";
            Regex regex = new Regex(pattern);
            bool resultadoCoincidencia = regex.IsMatch(this.renderizadoUsuario);

            if ( resultadoCoincidencia )
            {
                this.entradaOperadores[this.entradaOperadores.Count - 1] = signo;
            }
            else
            {
                this.entradaOperadores.Add(signo);
            }
        }

        public void resultadoFinal(object obj , EventArgs args)
        {
            resultado1.Text = this.resultadoCalculadora.ToString();
            this.resetearCalculadora();
        }

        public void FormatearRenderizado()
        {
            string resultado = "";

            if ( this.entradaUsuario.Count == this.entradaOperadores.Count )
            {
                if(this.entradaOperadores.Count == 1)
                {
                    resultado += this.entradaUsuario[0] + this.entradaOperadores[0];
                }
                else
                {
                    for ( int i = 0; i < this.entradaOperadores.Count; i++ )
                    {
                        resultado += this.entradaUsuario[i] + this.entradaOperadores[i];
                    }
                }
            }
            else if ( this.entradaOperadores.Count == 0 )
            {
                resultado = this.entradaUsuario[0];
            }
            else
            {
                for ( int i = 0; i < this.entradaOperadores.Count; i++)
                {
                    string operador = this.entradaOperadores[i];
                    string numero1 = this.entradaUsuario[i];
                    resultado += numero1 + operador;

                    if( i == this.entradaOperadores.Count - 1 )
                    {
                        string numero2 = this.entradaUsuario[i+1];
                        resultado += numero2;
                    }
                }
            }

            this.renderizadoUsuario = resultado;
        }

        public void resetearCalculadora()
        {
            this.renderizadoUsuario = "";
            resultado2.Text = "";
            this.entradaUsuario = new List<string>();
            this.entradaOperadores = new List<string>();
            this.resultadoCalculadora = 0.0f;
        }
    }
}
