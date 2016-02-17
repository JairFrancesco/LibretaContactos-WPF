using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibretaContactosWPF
{
    public class Contacto
    {

        //miembros privados de la clase

        private string _nombre;
        private string _apellido;
        private System.DateTime _fecha_nacimiento;
        private string _telefono;

        private string _correo;
        //miembros públicos: propiedades

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public string Apellido
        {
            get { return _apellido; }
            set { _apellido = value; }
        }

        public string NombreyApellido
        {
            get { return _nombre + " " + _apellido; }
        }
        public System.DateTime FechaNacimiento
        {
            get { return _fecha_nacimiento; }
            set { _fecha_nacimiento = value; }
        }

        public long Edad
        {
            get { return DateTime.Today.AddTicks(-_fecha_nacimiento.Ticks).Year - 1; }
        }

        public string Telefono
        {
            get { return _telefono; }
            set { _telefono = value; }
        }

        public string CorreoElectronico
        {
            get { return _correo; }
            set { _correo = value; }
        }

        //miembros públicos: métodos

        public int GetEdad()
        {
            return DateTime.Today.AddTicks(-_fecha_nacimiento.Ticks).Year - 1;
        }

        // constructor

        public Contacto(string Nombre = "", string Apellido = "")
        {
            _nombre = Nombre;
            _apellido = Apellido;
        }

    }
}
