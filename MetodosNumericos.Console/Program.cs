using MetodosNumericos.BusinessLogic.Implementacion;
using MetodosNumericos.BusinessLogic.Interfaz;
using MetodosNumericos.Entidades.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetodosNumericos.Console
{
    class Program
    {
        static void Main(string[] args)
        {

            // "(e^x)-(2*x^2)"              -> usada en el parcial
            // "cos((x^2+5)/(x^4+1))"       -> usada en un taller
            string sFunction = "(e^x) - (2*x^2)";
            //string sFunction = "(x^2)+(3*x)+2";
            string dFunction = "(e^x) -(4*x)";
            // 1. se escribe la función a utilizar y 
            // 2. luego se escogen los puntos Ai y Bi, siendo este ùltimo como el valor que se encuentre más a la derecha
            IEcuacion _ecuacion = new EcuacionGeneral();

            var valores = _ecuacion.EvaluateInX(sFunction,-10,10);
            foreach (var item in valores)
            {
                System.Console.WriteLine(string.Format("x={0} \t y={1}", item.x, item.y));
            }

            System.Console.ReadLine();


            IMetodoNumerico _metodoNumerico;
            
            int numIteraciones = 8;
            int Ai = -1;
            int Bi = 1;

            // PATRON STRATEGHY para utilizar el método numérico que se quiera
            _metodoNumerico = new ReglaFalsa();
            _metodoNumerico.calculate(sFunction, Ai, Bi, numIteraciones);
            System.Console.WriteLine("----------------------------------------------");
            System.Console.WriteLine("-----------MÉTODO DE REGLA FALSA--------------");
            foreach (var item in _metodoNumerico.getIteraciones())            
                System.Console.WriteLine($"Ci-> {item.Ci} \tError -> {item.Error}");


            System.Console.WriteLine("----------------------------------------------");
            System.Console.WriteLine("-----------MÉTODO DE NEWTON RAPSHON-----------");
            _metodoNumerico = new NewtonRapson();
            _metodoNumerico.calculate(sFunction, dFunction, Ai, numIteraciones);

            foreach (var item in _metodoNumerico.getIteraciones())
                System.Console.WriteLine($"Ci-> {item.Ai} \tError -> {item.Error}");

            System.Console.ReadLine();
           

        }
    }
}
