using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Area
    {
        public Area()
        {

        }
        public Area(int idArea, string nombre)
        {
            IdArea = idArea;
            Nombre = nombre;
        }
        public int IdArea { get; set; }
        public string Nombre { get; set; }
        public List<object> Areas { get; set; }
    }
}
