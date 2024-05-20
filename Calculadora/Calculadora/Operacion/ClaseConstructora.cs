using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Calculadora.Operacion
{
    public class ClaseConstructora
    {
        public const char SUMA = '+';
        public const char RESTA = '-';
        public const char MULTIPLICACION = 'x';
        public const char DIVISION = '/';

        public static OperacionesAbstract EjecutarOperacion(char tipo, float numero1, float numero2)
        {
            switch( tipo )
            {
                case SUMA:
                    return new OperacionSuma(numero1, numero2);
                case RESTA:
                    return new OperacionResta(numero1, numero2);
                case MULTIPLICACION:
                    return new OperacionMultiplicacion(numero1, numero2);
                case DIVISION:
                    return new OperacionDivision(numero1, numero2);
            }

            return null;
        }

        public static float DatosCalculadora(List<String> operadores, List<String> numeros)
        {
            List<String> operadores2 = new List<String>(operadores);
            List<String> numeros2 = new List<string>(numeros);
            bool bandera = true;

            while ( bandera ) 
            {
                //tenemos que ir verificando, si de izquierda a derecha encuentra una operacion primero de multiplicacion o division.
                int indiceOperadorEspecial = 0;
                char operadorEncontrado = '.';
                for ( int i = 0; i < operadores2.Count; i++ )
                {
                    if ( char.Parse(operadores2[i]).Equals('x') || char.Parse(operadores2[i]).Equals('/') )
                    {
                        operadorEncontrado = char.Parse(operadores2[i]);
                        indiceOperadorEspecial = i;
                        break;
                    }
                }

                if( indiceOperadorEspecial != 0 && !operadorEncontrado.Equals(".") )
                {
                    //sacamos el resultado de la operacion
                    OperacionesAbstract resultadoOperacion = ClaseConstructora.EjecutarOperacion(operadorEncontrado, float.Parse(numeros2[indiceOperadorEspecial]), float.Parse(numeros2[indiceOperadorEspecial + 1]));
                    float resultadoEncontrado = resultadoOperacion.implementarOperacion();

                    //eliminamos los numeros que ya usamos para la operacion
                    numeros2[indiceOperadorEspecial] = resultadoEncontrado.ToString();
                    numeros2.RemoveAt(indiceOperadorEspecial + 1);

                    //eliminamos nuestro operador ya ocupado.
                    operadores2.RemoveAt(indiceOperadorEspecial);
                }
                else
                {
                    bandera = false;
                }

            }

            int j = 0;
            for(int i = 0; i < operadores2.Count; i++)
            {
                char operador = char.Parse(operadores2[i]);
                float numero1 = float.Parse(numeros2[j]);
                float numero2 = float.Parse(numeros2[j + 1]);

                OperacionesAbstract operacion =  ClaseConstructora.EjecutarOperacion(operador, numero1, numero2);
                float resultado = operacion.implementarOperacion();

                numeros2.RemoveAt(j);
                numeros2.RemoveAt(j);

                numeros2.Insert(0, resultado.ToString());
            }
            
            return float.Parse(numeros2[0]);
        }
    }
}
