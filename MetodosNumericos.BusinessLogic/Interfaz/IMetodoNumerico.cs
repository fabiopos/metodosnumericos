using MetodosNumericos.Entidades.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetodosNumericos.BusinessLogic.Interfaz
{
    public interface IMetodoNumerico 
    {        
        void calculate(string sFunction, double Ai, double Bi, int iterLength);
        void calculate(string sFunction, string dFunction, double Ai, int iterLength);
        List<MetodoNumericoModel> getIteraciones();
        bool isValidFunction(string sFunction);
        void saveCurrentRegla();
        void setCurrentRegla(MetodoNumericoModel model);
        void calculateIterationsErrorValues(int iterLength);
        double calculateError(double ciAnterior, double ciActual, int i);
    }
}
