using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetodosNumericos.Entidades.Models
{
    public class MetodoNumericoModel
    {
        public int i { get; set; }
        public double Xi { get; set; }
        public double Ai { get; set; }
        public double Bi { get; set; }
        public double Ci { get; set; }
        public double dAi { get; set; }
        public double Error { get; set; }

        public double aux { get; set; } // en Newton guarda la division entre f(Ai) / f'(Ai)

        public double funcAi { get; set; }
        public double funcBi { get; set; }
        public double dfuncAi { get; set; }
        public string sFunction { get; set; } // funcion primaria
        public string dFunction { get; set; } // funciòn derivada
    }
}
