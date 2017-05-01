using MathNet.Symbolics;
using MetodosNumericos.BusinessLogic.Interfaz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetodosNumericos.Entidades.Models;

namespace MetodosNumericos.BusinessLogic.Implementacion
{
    public class EcuacionGeneral : IEcuacion
    {
        public double EvaluateInX(string sFunction, double x)
        {            

            if (!isValidFunction(sFunction))
                throw new Exception("Error de sintaxis en la ecuación: "+sFunction);

            var e = Infix.ParseOrUndefined(sFunction);
            var result = Evaluate.Evaluate(new Dictionary<string, FloatingPoint> { { "x", x } }, e);

            if (!result.IsReal)
                throw new Exception("El resultado no es un número real");

            return result.RealValue;

        }

        public List<CoordenadaModel> EvaluateInX(string sFunction, int inicial, int final)
        {
            var listaCoordenadas = new List<CoordenadaModel>();

            for (int i = inicial; i < final; i++)            
                listaCoordenadas.Add(new CoordenadaModel { x = i , y =EvaluateInX(sFunction, i) });
            
            return listaCoordenadas;
        }

        public bool isValidFunction(string sFunction)
        {
            if (string.IsNullOrEmpty(sFunction))
                throw new ArgumentNullException("sFunction");

            return Infix.ParseOrUndefined(sFunction) != Expression.Undefined;
        }
    }
}
