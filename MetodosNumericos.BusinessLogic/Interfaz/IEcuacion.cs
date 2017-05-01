using MetodosNumericos.Entidades.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetodosNumericos.BusinessLogic.Interfaz
{
    public interface IEcuacion
    {
        double EvaluateInX(string sFunction, double x);
        bool isValidFunction(string sFunction);
        List<CoordenadaModel> EvaluateInX(string sFunction, int inicial, int final);
    }
}
