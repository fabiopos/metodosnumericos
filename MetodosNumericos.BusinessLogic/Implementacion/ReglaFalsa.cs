using MetodosNumericos.BusinessLogic.Interfaz;
using MetodosNumericos.Entidades.Models;
using MathNet.Symbolics;
using System.Collections.Generic;
using System;

namespace MetodosNumericos.BusinessLogic.Implementacion
{
    public class ReglaFalsa : IMetodoNumerico
    {
        private MetodoNumericoModel _model;
        private List<MetodoNumericoModel> _iteraciones;

        public List<MetodoNumericoModel> Iteraciones
        {
            get
            {
                return _iteraciones;
            }

            set
            {
                _iteraciones = value;
            }
        }

        public ReglaFalsa()
        {            
            Iteraciones = new List<MetodoNumericoModel>();
        }

        public void calculate(string sFunction, double Ai, double Bi, int iterLength)
        {
            calculateIterationsValues(sFunction, Ai, Bi, iterLength);
            calculateIterationsErrorValues(iterLength);
        }

        public List<MetodoNumericoModel> getIteraciones()
        {
            return Iteraciones;
        }

        public bool isValidFunction(string sFunction)
        {
            return Infix.ParseOrUndefined(sFunction) != Expression.Undefined;            
        }

        public void setCurrentRegla(MetodoNumericoModel model)
        {
            _model = model;
        }

        public void saveCurrentRegla()
        {
            Iteraciones.Add(_model);
        }


        /// <summary>
        /// Calcula el valor por iteración por cada variable
        /// </summary>
        /// <param name="sFunction"></param>
        /// <param name="Ai"></param>
        /// <param name="Bi"></param>
        /// <param name="iterLength"></param>
        private void calculateIterationsValues(string sFunction, double Ai, double Bi, int iterLength)
        {
            // calcula valores
            for (int i = 0; i < iterLength; i++)
            {
                calculateReglaItem(sFunction, Ai, Bi, i);
                Ai = _model.Ci;
            }

        }

        /// <summary>
        /// Recorre las iteraciones y calcula el valor del error
        /// </summary>
        /// <param name="iterLength"></param>
        public void calculateIterationsErrorValues(int iterLength)
        {
            for (int i = 0; i < iterLength; i++)
            {
                if (Iteraciones[i].i != 0)                                   
                    Iteraciones[i].Error = calculateError(Iteraciones[i - 1].Ci, Iteraciones[i].Ci, i);                
            }
        }
        
        private void setPointA(double a)
        {
            _model.Ai = a;
        }

        private void setPointB(double b)
        {
            _model.Bi = b;
        }

        private void calculateFuncAi(string sFunction, double x)
        {
            var e = Infix.ParseOrUndefined(sFunction);
            var result = Evaluate.Evaluate(new Dictionary<string, FloatingPoint> { { "x" , x } }, e);
            _model.funcAi = result.RealValue;            
        }

        private void calculateFuncBi(string sFunction, double x)
        {
            var e = Infix.ParseOrUndefined(sFunction);
            var result = Evaluate.Evaluate(new Dictionary<string, FloatingPoint> { { "x", x } }, e);
            _model.funcBi = result.RealValue;
        }

        private void calculatePointC(double a, double b, double fa, double fb)
        {
            //  ci = ((a*fb) - (b*fa)) / (fb - fa)
            _model.Ci = ((a * fb) - (b * fa)) / (fb - fa);
        }

        private void calculateReglaItem(string sFunction, double Ai, double Bi, int i)
        {
            setCurrentRegla(new MetodoNumericoModel { sFunction = sFunction, Ai = Ai, Bi = Bi, i = i });
            calculateFuncAi(sFunction, Ai);
            calculateFuncBi(sFunction, Bi);
            calculatePointC(Ai, Bi, _model.funcAi, _model.funcBi);
            saveCurrentRegla();
                      
        }

        public double calculateError(double ciAnterior, double ciActual, int i)
        {          
           return Math.Abs((ciActual - ciAnterior) / ciActual);
        }

        public void calculate(string sFunction, string dFunction, double Ai, int iterLength)
        {
            throw new NotImplementedException();
        }
    }
}
