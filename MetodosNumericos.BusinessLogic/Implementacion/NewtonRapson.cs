using MetodosNumericos.BusinessLogic.Interfaz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetodosNumericos.Entidades.Models;
using MathNet.Symbolics;
using MathNet.Numerics.Integration;
using MathNet.Numerics;
using System.Numerics;
using static MathNet.Symbolics.Expression;

namespace MetodosNumericos.BusinessLogic.Implementacion
{
    public class NewtonRapson : IMetodoNumerico
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

        public NewtonRapson()
        {
            Iteraciones = new List<MetodoNumericoModel>();
        }
        public List<MetodoNumericoModel> getIteraciones()
        {
            return Iteraciones;
        }

        public bool isValidFunction(string sFunction)
        {
            return Infix.ParseOrUndefined(sFunction) != Expression.Undefined;
        }

        public void saveCurrentRegla()
        {
            Iteraciones.Add(_model);
        }
        public double calculateError(double ciAnterior, double ciActual, int i)
        {
            return Math.Abs((ciActual - ciAnterior) / ciActual);
        }
        public void setCurrentRegla(MetodoNumericoModel model)
        {
            _model = model;
        }
        public void calculate(string sFunction, string dFunction, double Ai, int iterLength)
        {
            calculateIterationsValues(sFunction, dFunction, Ai, iterLength);
            calculateIterationsErrorValues(iterLength);
        }

        private void calculateIterationsValues(string sFunction, string dFunction, double Ai, int iterLength)
        {
            // calcula valores
            for (int i = 0; i < iterLength; i++)
            {
                calculateReglaItem(sFunction, dFunction, Ai, i);
                Ai = Ai - _model.aux;
            }

        }

        private void calculateReglaItem(string sFunction, string dFunction, double Ai,  int i)
        {
            setCurrentRegla(new MetodoNumericoModel { sFunction = sFunction, Ai = Ai, dFunction = dFunction, i = i });
            // calcular f(Ai)
            calculateFnAi(sFunction, Ai);
            // calcular f'(Ai)
            calculateFdAi(dFunction, Ai);
            // calcular division   fAi / f'(Ai)   
            calculateDivision(_model.funcAi, _model.dfuncAi);
            // guardar regla actual
            saveCurrentRegla();
         }


        private void calculateFnAi(string sFunction, double x) {
            var e = Infix.ParseOrUndefined(sFunction);
            var result = MathNet.Symbolics.Evaluate.Evaluate(new Dictionary<string, FloatingPoint> { { "x", x } }, e);
            _model.funcAi = result.RealValue;
        }

        private void calculateFdAi(string sFunction, double x) {
            var e = Infix.ParseOrUndefined(sFunction);
            var result = MathNet.Symbolics.Evaluate.Evaluate(new Dictionary<string, FloatingPoint> { { "x", x } }, e);
            _model.dfuncAi = result.RealValue;
        }

        private void calculateDivision(double funcAi,double dfuncAi)
        {
            _model.aux = (funcAi / dfuncAi);
        }

        public void calculate(string sFunction, double Ai, double Bi, int iterLength)
        {
            throw new NotImplementedException();
        }

        public void calculateIterationsErrorValues(int iterLength)
        {
            for (int i = 0; i < iterLength; i++)
            {
                if (Iteraciones[i].i != 0)
                    Iteraciones[i].Error = calculateError(Iteraciones[i - 1].Ai, Iteraciones[i].Ai, i);
            }
        }
    }
}
