using System;
using System.Collections.Generic;
using System.Text;

namespace Calculadora.Operacion
{
    public class OperacionResta : OperacionesAbstract
    {
        private float numero1;
        private float numero2;

        public OperacionResta(float numero1, float numero2)
        {
            this.numero1 = numero1;
            this.numero2 = numero2;
        }

        public override float implementarOperacion()
        {
            return this.numero1 - this.numero2; 
        }
    }
}
