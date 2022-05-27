using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace PA.Models
{
    public class IDModel
    {
        //USUARIOS
        public string _ID { get; set; }
        public string _Nombre { get; set; }
        public string _ApPaterno { get; set; }
        public string _ApMaterno { get; set; }
        public int _Boleta { get; set; }
        public string _TelUser { get; set; }
        public string _Correo { get; set; }

        //ADMINISTRADOR
        public string _Admin { get; set; }
        public string _Pass { get; set; }
        public string _PassConfirm { get; set; }

        //CAPTCHA
        public string _Captcha { get; set; }
        public string _CaptchaConfirm { get; set; }

        //TEST ANSIEDAD
        public string _NoPregA { get; set; }
        public string _PreguntaA { get; set; }
        public int Opciones { get; set; }


        //TEST DEPRESIÓN
        public string _NoPregD { get; set; }
        public string _PreguntaD { get; set; }
        public int Opcion { get; set; }

        public IDModel()
        { }

        public string ObtenerID()
        {
            _ID = _ApPaterno.Substring(0, 1) + _ApMaterno.Substring(0, 1) + _Nombre.Substring(0, 1) + _Boleta.ToString();
            return _ID;
        }

    }
}
