//PDF
using iTextSharp.text;
using iTextSharp.text.pdf;
//CORREO
using System.IO;
using System;
using System.Net.Mail;
using System.Web;
using System.Collections.Generic;
using System.Diagnostics;
//OTROS
using PA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;


namespace PA.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _config;

        IWebHostEnvironment env;

        public HomeController(ILogger<HomeController> logger, IConfiguration config, IWebHostEnvironment webhost)
        {
            _logger = logger;
            _config = config;
            env = webhost;
        }

        #region INDEX/NOSOTROS/MISION/MENU/PRIVACY
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Nosotros()
        {
            return View();
        }

        public IActionResult Mision()
        {
            return View();
        }

        public IActionResult Menu()
        {
            ViewBag.Mensaje = "Bienvenido, Fecha: " + (DateTime.Today.ToShortDateString()) + " Hora: " + (DateTime.Now.ToString("hh:mm ")) + (DateTime.Now.Hour < 12 ? "a.m." : "p.m.");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        #endregion

        #region AGENDAR CITA
        [HttpGet]
        public IActionResult Cita()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cita(string para, string asunto, string mensaje, HttpPostAttribute fichero)
        {
            try
            {
                MailMessage correo = new MailMessage();
                correo.From = new MailAddress("ayudatest.cecyt13@gmail.com");
                correo.To.Add(para);
                correo.Subject = asunto;
                correo.Body = mensaje;
                correo.IsBodyHtml = true;
                correo.Priority = MailPriority.Normal;

                //se almacenan archivos adjuntos en una carpeta

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 25;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = true;
                string sCuentaCorreo = "ayudatest.cecyt13@gmail.com";
                string sPasswordCorreo = "aopj2003";
                smtp.Credentials = new System.Net.NetworkCredential(sCuentaCorreo, sPasswordCorreo);

                smtp.Send(correo);
                ViewBag.Mensaje = "Mensaje enviado correctamente";

            }

            catch (Exception Ex)
            {
                ViewBag.Error = Ex.Message;
            }
            return View();
        }
        #endregion

        #region REGISTRO
        [HttpGet]
        public IActionResult Regis()
        {
            ViewBag.Captcha = CRUDModel.CAPTCHA();
            return View();
        }

        [HttpPost]
        public IActionResult Regis(IDModel usuario)
        {
            string _cadenaCon = _config.GetConnectionString("stringConexion");
            ViewBag.Captcha = CRUDModel.CAPTCHA();
            if (usuario._Captcha != usuario._CaptchaConfirm)
            {
                ViewBag.Nota = "El CAPTCHA es incorrecto, por favor intente de nuevo";
                return View();
            }
            else if (CRUDModel.BuscarBol(usuario, _cadenaCon) == true)
            {
                ViewBag.Nota = "La BOLETA que intentas registrar ya existe";
                return View();
            }
            else if (CRUDModel.BuscarBol(usuario, _cadenaCon) == false)
            {
                ViewBag.ID = usuario.ObtenerID();
                CRUDModel.Create(usuario, _cadenaCon);

                Document doc = new Document(PageSize.LETTER);
                MemoryStream ms = new MemoryStream();
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(@"D:\PA\wwwroot\Sources\REGISTRO AYUDATEST_" + usuario._ApPaterno.Substring(0, 1) + usuario._ApMaterno.Substring(0, 1) + usuario._Nombre.Substring(0, 1) + usuario._Boleta + ".pdf", FileMode.Create));
                //PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(@"E:\PA\wwwroot\Sources\REGISTRO AYUDATEST_" + usuario._ApPaterno.Substring(0, 1) + usuario._ApMaterno.Substring(0, 1) + usuario._Nombre.Substring(0, 1) + usuario._Boleta + ".pdf", FileMode.Create));

                #region PDF
                doc.Open();

                iTextSharp.text.Image logo1 = iTextSharp.text.Image.GetInstance("https://www.cecyt13.ipn.mx/assets/files/cecyt13/img/Inicio/banderin.png");
                logo1.ScalePercent(25);
                logo1.SetAbsolutePosition(23, 695);
                doc.Add(logo1);
                iTextSharp.text.Image logo2 = iTextSharp.text.Image.GetInstance("https://multipress.com.mx/wp-content/uploads/2020/04/ipn.jpg");
                logo2.ScalePercent(11);
                logo2.SetAbsolutePosition(490, 690);
                doc.Add(logo2);

                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(new Paragraph("AYUDATEST") { Alignment = Element.ALIGN_CENTER });
                doc.Add(new Paragraph("Centro de Estudios Cientifícos y Técnologicos N.13") { Alignment = Element.ALIGN_CENTER });
                doc.Add(new Paragraph("Ricardo Flores Magón") { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);

                doc.Add(new Paragraph("Fecha de Registro: " + (DateTime.Today.ToShortDateString()) + " Hora: " + (DateTime.Now.ToString("hh:mm ")) + (DateTime.Now.Hour < 12 ? "a.m." : "p.m.")) { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);

                PdfPTable tabl2 = new PdfPTable(2);
                tabl2.WidthPercentage = 100;
                PdfPCell fila1 = new PdfPCell(new Phrase("NOMBRE"));
                fila1.BorderWidth = 0;
                tabl2.AddCell(fila1);
                PdfPCell fila2 = new PdfPCell(new Phrase(usuario._Nombre));
                fila2.BorderWidth = 0;
                tabl2.AddCell(fila2);
                doc.Add(tabl2);

                PdfPTable tabl3 = new PdfPTable(2);
                tabl3.WidthPercentage = 100;
                PdfPCell fil1 = new PdfPCell(new Phrase("APELLIDO PATERNO"));
                fil1.BorderWidth = 0;
                tabl3.AddCell(fil1);
                PdfPCell fil2 = new PdfPCell(new Phrase(usuario._ApPaterno));
                fil2.BorderWidth = 0;
                tabl3.AddCell(fil2);
                doc.Add(tabl3);

                PdfPTable tabl4 = new PdfPTable(2);
                tabl4.WidthPercentage = 100;
                PdfPCell fil3 = new PdfPCell(new Phrase("APELLIDO MATERNO"));
                fil3.BorderWidth = 0;
                tabl4.AddCell(fil3);
                PdfPCell fil4 = new PdfPCell(new Phrase(usuario._ApMaterno));
                fil4.BorderWidth = 0;
                tabl4.AddCell(fil4);
                doc.Add(tabl4);

                PdfPTable tabl5 = new PdfPTable(2);
                tabl5.WidthPercentage = 100;
                PdfPCell fi5 = new PdfPCell(new Phrase("BOLETA"));
                fi5.BorderWidth = 0;
                tabl5.AddCell(fi5);
                PdfPCell fila5 = new PdfPCell(new Phrase(usuario._Boleta.ToString()));
                fila5.BorderWidth = 0;
                tabl5.AddCell(fila5);
                doc.Add(tabl5);

                PdfPTable tabl6 = new PdfPTable(2);
                tabl6.WidthPercentage = 100;
                PdfPCell fil5 = new PdfPCell(new Phrase("NÚMERO TELÉFONICO:"));
                fil5.BorderWidth = 0;
                tabl6.AddCell(fil5);
                PdfPCell fil6 = new PdfPCell(new Phrase(usuario._TelUser.ToString()));
                fil6.BorderWidth = 0;
                tabl6.AddCell(fil6);
                doc.Add(tabl6);

                PdfPTable tabl7 = new PdfPTable(2);
                tabl7.WidthPercentage = 100;
                PdfPCell fil7 = new PdfPCell(new Phrase("TU ID ES EL SIGUIENTE:"));
                fil7.BorderWidth = 0;
                tabl7.AddCell(fil7);
                PdfPCell fi7 = new PdfPCell(new Phrase(usuario._ApPaterno.Substring(0, 1) + usuario._ApMaterno.Substring(0, 1) + usuario._Nombre.Substring(0, 1) + usuario._Boleta));
                fi7.BorderWidth = 0;
                tabl7.AddCell(fi7);
                doc.Add(tabl7);

                doc.Add(Chunk.NEWLINE);
                doc.Add(new Paragraph("© 2022 - AyudaTest® ") { Alignment = Element.ALIGN_LEFT });
                doc.Add(Chunk.NEWLINE);

                doc.Close();
                #endregion

                return View("RegisExito", usuario);
            }
            return View();
        }
        #endregion

        #region LOGUEO
        [HttpGet]
        public IActionResult LogIn()
        {
            ViewBag.Captcha = CRUDModel.CAPTCHA();
            return View();
        }

        [HttpPost]
        public IActionResult LogIn(IDModel usuario)
        {
            string _cadenaCon = _config.GetConnectionString("stringConexion");
            ViewBag.Captcha = CRUDModel.CAPTCHA();
            if (usuario._Captcha != usuario._CaptchaConfirm)
            {
                ViewBag.Nota = "El CAPTCHA es incorrecto, por favor intente de nuevo";
                return View();
            }
            else if (CRUDModel.LogIn(usuario, _cadenaCon) == false)
            {
                ViewBag.error = "El ID no existe :c";
                return View();
            }
            else
            {
                CRUDModel.LogIn(usuario, _cadenaCon);
                ViewBag.Nom = CRUDModel.Usuario(usuario, _cadenaCon);
                return View("Instrucciones", ViewBag.Nom);
            }
        }
        #endregion

        #region VISTAS PDF
        public IActionResult MenuPDF()
        {
            return View();
        }

        //PDF REGISTRO
        [HttpGet]
        public IActionResult BuscarArchivo()
        {
            ViewBag.Captcha = CRUDModel.CAPTCHA();
            return View();
        }

        [HttpPost]
        public IActionResult BuscarArchivo(IDModel usuario)
        {
            string _cadenaCon = _config.GetConnectionString("stringConexion");
            ViewBag.Captcha = CRUDModel.CAPTCHA();
            if (usuario._Captcha != usuario._CaptchaConfirm)
            {
                ViewBag.Nota = "El CAPTCHA es incorrecto, por favor intente de nuevo";
                return View();
            }
            else if (CRUDModel.LogIn(usuario, _cadenaCon) == false)
            {
                ViewBag.error = "El ID no existe :c";
                return View();
            }
            else 
            {
                string rutaArchivo = Path.Combine(env.WebRootPath, "Sources/", "REGISTRO AYUDATEST_" + usuario._ID + ".pdf");
                FileStream ArchivoPDF = new FileStream(rutaArchivo, FileMode.Open);
                return File(ArchivoPDF, "application/pdf");
            }
        }

        //PDF RESULTADOS ANSIEDAD
        [HttpGet]
        public IActionResult BuscarTestResul()
        {
            ViewBag.Captcha = CRUDModel.CAPTCHA();
            return View();
        }

        [HttpPost]
        public IActionResult BuscarTestResul(IDModel usuario)
        {
            string _cadenaCon = _config.GetConnectionString("stringConexion");
            ViewBag.Captcha = CRUDModel.CAPTCHA();
            if (usuario._Captcha != usuario._CaptchaConfirm)
            {
                ViewBag.Nota = "El CAPTCHA es incorrecto, por favor intente de nuevo";
                return View();
            }
            else if (CRUDModel.LogIn(usuario, _cadenaCon) == false)
            {
                ViewBag.error = "El ID no existe";
                return View();
            }
            else
            {
                string rutaArchivo = Path.Combine(env.WebRootPath, "Sources/", "RESULTADOS TEST ANSIEDAD_" + usuario._ID + ".pdf");
                FileStream ArchivoPDF = new FileStream(rutaArchivo, FileMode.Open);
                return File(ArchivoPDF, "application/pdf");
            }
        }

        //PDF RESULTADOS DEPRESION
        [HttpGet]
        public IActionResult BuscarTestResulD()
        {
            ViewBag.Captcha = CRUDModel.CAPTCHA();
            return View();
        }

        [HttpPost]
        public IActionResult BuscarTestResulD(IDModel usuario)
        {
            string _cadenaCon = _config.GetConnectionString("stringConexion");
            ViewBag.Captcha = CRUDModel.CAPTCHA();
            if (usuario._Captcha != usuario._CaptchaConfirm)
            {
                ViewBag.Nota = "El CAPTCHA es incorrecto, por favor intente de nuevo";
                return View();
            }
            else if (CRUDModel.LogIn(usuario, _cadenaCon) == false)
            {
                ViewBag.error = "El ID no existe";
                return View();
            }
            else
            {
                string rutaArchivo = Path.Combine(env.WebRootPath, "Sources/", "RESULTADOS TEST DEPRESION_" + usuario._ID + ".pdf");
                FileStream ArchivoPDF = new FileStream(rutaArchivo, FileMode.Open);
                return File(ArchivoPDF, "application/pdf");
            }
        }
        #endregion

        #region ADMIN
        public IActionResult Registrados()
        {
            string _cadenaCon = _config.GetConnectionString("stringConexion");
            ViewBag.Usuarios = CRUDModel.Read(_cadenaCon);
            return View();
        }

        [HttpGet]
        public IActionResult LogInAdmin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LogInAdmin(IDModel admin)
        {
            string _cadenaCon = _config.GetConnectionString("stringConexion");
            if (admin._Pass != admin._PassConfirm)
            {
                ViewBag.Msj = "Usuario y / o Contraseña Incorrectos ";
                return View();
            }
            else if (CRUDModel.LogueoAdmin(admin, _cadenaCon) == false)
            {
                ViewBag.Msj = "Usuario y / o Contraseña Incorrectos ";
                return View();
            }
            else
            {
                ViewBag.Usuarios = CRUDModel.Read(_cadenaCon);
                return View("Registrados");
            }
        }
        #endregion

        #region BUSCAR/ACTUALIZAR/ELIMINAR
        [HttpGet]
        public IActionResult Buscar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Buscar(IDModel usuario)
        {
            string _cadenaCon = _config.GetConnectionString("stringConexion");
            ViewBag.Mensajito = CRUDModel.ReadAlumno(usuario._Nombre, usuario._ApPaterno, usuario._ApMaterno, _cadenaCon);
            return View("BEx", usuario);
        }

        public IActionResult BEx(IDModel usuario)
        {
            return View();
        }

        public IActionResult UpdateAn(IDModel pregunta)
        {
            string _cadenaCon = _config.GetConnectionString("stringConexion");
            ViewBag.Test1 = CRUDModel.TestAnsiedad(_cadenaCon);
            return View();
        }

        public IActionResult UAn(IDModel pregunta)
        {
            string _cadenaCon = _config.GetConnectionString("stringConexion");
            CRUDModel.UpdateTAn(pregunta, _cadenaCon);
            return View("UAn", pregunta);
        }

        public IActionResult UpdateDe(IDModel pregunta)
        {
            string _cadenaCon = _config.GetConnectionString("stringConexion");
            ViewBag.Test2 = CRUDModel.TestDepresion(_cadenaCon);
            return View();
        }

        public IActionResult UDe(IDModel pregunta)
        {
            string _cadenaCon = _config.GetConnectionString("stringConexion");
            CRUDModel.UpdateTDe(pregunta, _cadenaCon);
            return View("UDe", pregunta);
        }

        public IActionResult Eliminar(IDModel usuario)
        {
            string _cadenaCon = _config.GetConnectionString("stringConexion");
            CRUDModel.Delete(usuario, _cadenaCon);
            return View("ExitoDelete", usuario);
        }
        #endregion

        #region TEST ANSIEDAD
        public IActionResult TestAnsiedad(IDModel usuario)
        {
            string _cadenaCon = _config.GetConnectionString("stringConexion");

            #region PREGUNTAS TEST ANSIEDAD CRUD
            ViewBag.P1 = CRUDModel.TestAnsiedad1(_cadenaCon);
            ViewBag.P2 = CRUDModel.TestAnsiedad2(_cadenaCon);
            ViewBag.P3 = CRUDModel.TestAnsiedad3(_cadenaCon);
            ViewBag.P4 = CRUDModel.TestAnsiedad4(_cadenaCon);
            ViewBag.P5 = CRUDModel.TestAnsiedad5(_cadenaCon);
            ViewBag.P6 = CRUDModel.TestAnsiedad6(_cadenaCon);
            ViewBag.P7 = CRUDModel.TestAnsiedad7(_cadenaCon);
            ViewBag.P8 = CRUDModel.TestAnsiedad8(_cadenaCon);
            ViewBag.P9 = CRUDModel.TestAnsiedad9(_cadenaCon);
            ViewBag.P10 = CRUDModel.TestAnsiedad10(_cadenaCon);
            ViewBag.P11 = CRUDModel.TestAnsiedad11(_cadenaCon);
            ViewBag.P12 = CRUDModel.TestAnsiedad12(_cadenaCon);
            ViewBag.P13 = CRUDModel.TestAnsiedad13(_cadenaCon);
            ViewBag.P14 = CRUDModel.TestAnsiedad14(_cadenaCon);
            ViewBag.P15 = CRUDModel.TestAnsiedad15(_cadenaCon);
            ViewBag.P16 = CRUDModel.TestAnsiedad16(_cadenaCon);
            ViewBag.P17 = CRUDModel.TestAnsiedad17(_cadenaCon);
            ViewBag.P18 = CRUDModel.TestAnsiedad18(_cadenaCon);
            ViewBag.P19 = CRUDModel.TestAnsiedad19(_cadenaCon);
            ViewBag.P20 = CRUDModel.TestAnsiedad20(_cadenaCon);
            ViewBag.P21 = CRUDModel.TestAnsiedad21(_cadenaCon);
            #endregion 

            if (CRUDModel.LogIn(usuario, _cadenaCon) == false)
            {
                ViewBag.error = "El ID no existe";
                return View();
            }
            else if (CRUDModel.LogIn(usuario, _cadenaCon) == true)
            {
                return View("ResultadosAnsiedad", usuario);
            }
            return View();
        }
        #endregion

        #region TEST DEPRESION
        public IActionResult TestDepresion(IDModel usuario)
        {
            string _cadenaCon = _config.GetConnectionString("stringConexion");

            #region PREGUNTAS TEST DEPRESION CRUD
            ViewBag.p1 = CRUDModel.TestDepresion1(_cadenaCon);
            ViewBag.p2 = CRUDModel.TestDepresion2(_cadenaCon);
            ViewBag.p3 = CRUDModel.TestDepresion3(_cadenaCon);
            ViewBag.p4 = CRUDModel.TestDepresion4(_cadenaCon);
            ViewBag.p5 = CRUDModel.TestDepresion5(_cadenaCon);
            ViewBag.p6 = CRUDModel.TestDepresion6(_cadenaCon);
            ViewBag.p7 = CRUDModel.TestDepresion7(_cadenaCon);
            ViewBag.p8 = CRUDModel.TestDepresion8(_cadenaCon);
            ViewBag.p9 = CRUDModel.TestDepresion9(_cadenaCon);
            ViewBag.p10 = CRUDModel.TestDepresion10(_cadenaCon);
            ViewBag.p11 = CRUDModel.TestDepresion11(_cadenaCon);
            ViewBag.p12 = CRUDModel.TestDepresion12(_cadenaCon);
            ViewBag.p13 = CRUDModel.TestDepresion13(_cadenaCon);
            ViewBag.p14 = CRUDModel.TestDepresion14(_cadenaCon);
            ViewBag.p15 = CRUDModel.TestDepresion15(_cadenaCon);
            ViewBag.p16 = CRUDModel.TestDepresion16(_cadenaCon);
            ViewBag.p17 = CRUDModel.TestDepresion17(_cadenaCon);
            ViewBag.p18 = CRUDModel.TestDepresion18(_cadenaCon);
            ViewBag.p19 = CRUDModel.TestDepresion19(_cadenaCon);
            ViewBag.p20 = CRUDModel.TestDepresion20(_cadenaCon);
            ViewBag.p21 = CRUDModel.TestDepresion21(_cadenaCon);
            #endregion

            if (CRUDModel.LogIn(usuario, _cadenaCon) == false)
            {
                ViewBag.error = "El ID no existe";
                return View();
            }
            else if (CRUDModel.LogIn(usuario, _cadenaCon) == true)
            {
                return View("ResultadosAnsiedad", usuario);
            }
            return View();
        }
        #endregion

        #region RESULTADOS TEST ANSIEDAD
        public IActionResult ResultadosAnsiedad(int Op1, int Op2, int Op3, int Op4, int Op5, int Op6, int Op7, int Op8, int Op9, int Op10, int Op11, int Op12, int Op13, int Op14, int Op15, int Op16, int Op17, int Op18, int Op19, int Op20, int Op21, IDModel usuario, string xd)
        {
            string _cadenaCon = _config.GetConnectionString("stringConexion");

            #region CONTADORES
            int resul = 0;
            ContadorModel Conta = new ContadorModel();
            Conta.Op1 = Op1;
            Conta.Op2 = Op2;
            Conta.Op3 = Op3;
            Conta.Op4 = Op4;
            Conta.Op5 = Op5;
            Conta.Op6 = Op6;
            Conta.Op7 = Op7;
            Conta.Op8 = Op8;
            Conta.Op9 = Op9;
            Conta.Op10 = Op10;
            Conta.Op11 = Op11;
            Conta.Op12 = Op12;
            Conta.Op13 = Op13;
            Conta.Op14 = Op14;
            Conta.Op15 = Op15;
            Conta.Op16 = Op16;
            Conta.Op17 = Op17;
            Conta.Op18 = Op18;
            Conta.Op19 = Op19;
            Conta.Op20 = Op20;
            Conta.Op21 = Op21;
            resul = Conta.ObtenerResultadosA();
            ViewBag.CResA = Conta.ObtenerResultadosA();
            #endregion

            if (ViewBag.CResA <= 20)
            {
                Document doc = new Document(PageSize.LETTER);
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(@"D:\PA\wwwroot\Sources\RESULTADOS TEST ANSIEDAD_" + usuario._ID.ToString() + ".pdf", FileMode.Create));
                //PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(@"E:\PA\wwwroot\Sources\RESULTADOS TEST ANSIEDAD_" + usuario._ID.ToString() + ".pdf", FileMode.Create));

                #region PDF
                doc.Open();

                iTextSharp.text.Image logo1 = iTextSharp.text.Image.GetInstance("https://www.cecyt13.ipn.mx/assets/files/cecyt13/img/Inicio/banderin.png");
                logo1.ScalePercent(25);
                logo1.SetAbsolutePosition(23, 695);
                doc.Add(logo1);
                iTextSharp.text.Image logo2 = iTextSharp.text.Image.GetInstance("https://multipress.com.mx/wp-content/uploads/2020/04/ipn.jpg");
                logo2.ScalePercent(11);
                logo2.SetAbsolutePosition(490, 690);
                doc.Add(logo2);

                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(new Paragraph("AYUDATEST") { Alignment = Element.ALIGN_CENTER });
                doc.Add(new Paragraph("Centro de Estudios Cientifícos y Técnologicos N.13") { Alignment = Element.ALIGN_CENTER });
                doc.Add(new Paragraph("Ricardo Flores Magón") { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);

                doc.Add(new Paragraph("Fecha de Realización: " + (DateTime.Today.ToShortDateString()) + " Hora: " + (DateTime.Now.ToString("hh:mm ")) + (DateTime.Now.Hour < 12 ? "a.m." : "p.m.")) { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);
                doc.Add(new Paragraph("ID: " + usuario._ID.ToString()) { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);

                doc.Add(Chunk.NEWLINE);
                doc.Add(new Paragraph("RESULTADOS TEST ANSIEDAD ") { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);

                doc.Add(new Paragraph("TOTAL: " + resul.ToString()) { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);

                doc.Add(Chunk.NEWLINE);
                doc.Add(new Paragraph("ESTOS ALTIBAJOS SON CONSIDERADOS NORMALES") { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);

                doc.Add(Chunk.NEWLINE);
                doc.Add(new Paragraph("Recuerda que tu salud mental es muy importante :)") { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);

                doc.Add(Chunk.NEWLINE);
                doc.Add(new Paragraph("© 2022 - AyudaTest® ") { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);

                doc.Close();
                #endregion

                ViewBag.Diag = "Estos altibajos son considerados normales";

            }
            else if (ViewBag.CResA > 20 & ViewBag.CResA <= 32)
            {
                Document doc = new Document(PageSize.LETTER);
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(@"D:\PA\wwwroot\Sources\RESULTADOS TEST ANSIEDAD_" + usuario._ID.ToString() + ".pdf", FileMode.Create));
                //PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(@"E:\PA\wwwroot\Sources\RESULTADOS TEST ANSIEDAD_" + ViewBag.Nom + ".pdf", FileMode.Create));

                #region PDF
                doc.Open();

                iTextSharp.text.Image logo1 = iTextSharp.text.Image.GetInstance("https://www.cecyt13.ipn.mx/assets/files/cecyt13/img/Inicio/banderin.png");
                logo1.ScalePercent(25);
                logo1.SetAbsolutePosition(23, 695);
                doc.Add(logo1);
                iTextSharp.text.Image logo2 = iTextSharp.text.Image.GetInstance("https://multipress.com.mx/wp-content/uploads/2020/04/ipn.jpg");
                logo2.ScalePercent(11);
                logo2.SetAbsolutePosition(490, 690);
                doc.Add(logo2);

                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(new Paragraph("AYUDATEST") { Alignment = Element.ALIGN_CENTER });
                doc.Add(new Paragraph("Centro de Estudios Cientifícos y Técnologicos N.13") { Alignment = Element.ALIGN_CENTER });
                doc.Add(new Paragraph("Ricardo Flores Magón") { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);

                doc.Add(new Paragraph("Fecha de Realización: " + (DateTime.Today.ToShortDateString()) + " Hora: " + (DateTime.Now.ToString("hh:mm ")) + (DateTime.Now.Hour < 12 ? "a.m." : "p.m.")) { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);
                doc.Add(new Paragraph("ID: " + usuario._ID.ToString()) { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);

                doc.Add(Chunk.NEWLINE);
                doc.Add(new Paragraph("RESULTADOS TEST ANSIEDAD ") { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);

                doc.Add(new Paragraph("TOTAL: " + resul.ToString()) { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);

                doc.Add(Chunk.NEWLINE);
                doc.Add(new Paragraph("LEVE PERTURBACIÓN DEL ESTADO DE ÁNIMO") { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);

                doc.Add(Chunk.NEWLINE);
                doc.Add(new Paragraph("Recuerda que tu salud mental es muy importante :)") { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);

                doc.Add(Chunk.NEWLINE);
                doc.Add(new Paragraph("© 2022 - AyudaTest® ") { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);

                doc.Close();
                #endregion

                ViewBag.Diag = "Leve perturbación del estado de ánimo. ";


            }
            else if (ViewBag.CResA > 32 & ViewBag.CResA <= 42)
            {
                Document doc = new Document(PageSize.LETTER);
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(@"D:\PA\wwwroot\Sources\RESULTADOS TEST ANSIEDAD_" + usuario._ID.ToString() + ".pdf", FileMode.Create));
                //PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(@"E:\PA\wwwroot\Sources\RESULTADOS TEST ANSIEDAD_" + usuario._ID.ToString() + ".pdf", FileMode.Create));

                #region PDF
                doc.Open();

                iTextSharp.text.Image logo1 = iTextSharp.text.Image.GetInstance("https://www.cecyt13.ipn.mx/assets/files/cecyt13/img/Inicio/banderin.png");
                logo1.ScalePercent(25);
                logo1.SetAbsolutePosition(23, 695);
                doc.Add(logo1);
                iTextSharp.text.Image logo2 = iTextSharp.text.Image.GetInstance("https://multipress.com.mx/wp-content/uploads/2020/04/ipn.jpg");
                logo2.ScalePercent(11);
                logo2.SetAbsolutePosition(490, 690);
                doc.Add(logo2);

                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(new Paragraph("AYUDATEST") { Alignment = Element.ALIGN_CENTER });
                doc.Add(new Paragraph("Centro de Estudios Cientifícos y Técnologicos N.13") { Alignment = Element.ALIGN_CENTER });
                doc.Add(new Paragraph("Ricardo Flores Magón") { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);

                doc.Add(new Paragraph("Fecha de Realización: " + (DateTime.Today.ToShortDateString()) + " Hora: " + (DateTime.Now.ToString("hh:mm ")) + (DateTime.Now.Hour < 12 ? "a.m." : "p.m.")) { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);
                doc.Add(new Paragraph("ID: " + usuario._ID.ToString()) { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);

                doc.Add(Chunk.NEWLINE);
                doc.Add(new Paragraph("RESULTADOS TEST ANSIEDAD ") { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);

                doc.Add(new Paragraph("TOTAL: " + resul.ToString()) { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);

                doc.Add(Chunk.NEWLINE);
                doc.Add(new Paragraph("ESTADOS DE ANSIEDAD INTERMITENTES") { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);

                doc.Add(Chunk.NEWLINE);
                doc.Add(new Paragraph("Recuerda que tu salud mental es muy importante :)") { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);

                doc.Add(Chunk.NEWLINE);
                doc.Add(new Paragraph("© 2022 - AyudaTest® ") { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);

                doc.Close();
                #endregion

                ViewBag.Diag = "Estados de ansiedad intermitentes.";


            }
            else if (ViewBag.CResA > 42 & ViewBag.CResA <= 60)
            {
                Document doc = new Document(PageSize.LETTER);
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(@"D:\PA\wwwroot\Sources\RESULTADOS TEST ANSIEDAD_" + usuario._ID.ToString() + ".pdf", FileMode.Create));
                //PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(@"E:\PA\wwwroot\Sources\RESULTADOS TEST ANSIEDAD_" + ViewBag.Nom + ".pdf", FileMode.Create));

                #region PDF
                doc.Open();

                iTextSharp.text.Image logo1 = iTextSharp.text.Image.GetInstance("https://www.cecyt13.ipn.mx/assets/files/cecyt13/img/Inicio/banderin.png");
                logo1.ScalePercent(25);
                logo1.SetAbsolutePosition(23, 695);
                doc.Add(logo1);
                iTextSharp.text.Image logo2 = iTextSharp.text.Image.GetInstance("https://multipress.com.mx/wp-content/uploads/2020/04/ipn.jpg");
                logo2.ScalePercent(11);
                logo2.SetAbsolutePosition(490, 690);
                doc.Add(logo2);

                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(new Paragraph("AYUDATEST") { Alignment = Element.ALIGN_CENTER });
                doc.Add(new Paragraph("Centro de Estudios Cientifícos y Técnologicos N.13") { Alignment = Element.ALIGN_CENTER });
                doc.Add(new Paragraph("Ricardo Flores Magón") { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);

                doc.Add(new Paragraph("Fecha de Realización: " + (DateTime.Today.ToShortDateString()) + " Hora: " + (DateTime.Now.ToString("hh:mm ")) + (DateTime.Now.Hour < 12 ? "a.m." : "p.m.")) { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);
                doc.Add(new Paragraph("ID: " + usuario._ID.ToString()) { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);

                doc.Add(Chunk.NEWLINE);
                doc.Add(new Paragraph("RESULTADOS TEST ANSIEDAD ") { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);

                doc.Add(new Paragraph("TOTAL: " + resul.ToString()) { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);

                doc.Add(Chunk.NEWLINE);
                doc.Add(new Paragraph("ANSIEDAD MODERADA") { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);

                doc.Add(Chunk.NEWLINE);
                doc.Add(new Paragraph("Recuerda que tu salud mental es muy importante :)") { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);

                doc.Add(Chunk.NEWLINE);
                doc.Add(new Paragraph("© 2022 - AyudaTest® ") { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);

                doc.Close();
                #endregion

                ViewBag.Diag = "Ansiedad moderada.";


            }
            else if (ViewBag.CResA > 60)
            {
                Document doc = new Document(PageSize.LETTER);
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(@"D:\PA\wwwroot\Sources\RESULTADOS TEST ANSIEDAD_" + usuario._ID.ToString() + ".pdf", FileMode.Create));
                //PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(@"E:\PA\wwwroot\Sources\RESULTADOS TEST ANSIEDAD_" + ViewBag.Nom + ".pdf", FileMode.Create));

                #region PDF
                doc.Open();

                iTextSharp.text.Image logo1 = iTextSharp.text.Image.GetInstance("https://www.cecyt13.ipn.mx/assets/files/cecyt13/img/Inicio/banderin.png");
                logo1.ScalePercent(25);
                logo1.SetAbsolutePosition(23, 695);
                doc.Add(logo1);
                iTextSharp.text.Image logo2 = iTextSharp.text.Image.GetInstance("https://multipress.com.mx/wp-content/uploads/2020/04/ipn.jpg");
                logo2.ScalePercent(11);
                logo2.SetAbsolutePosition(490, 690);
                doc.Add(logo2);

                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(new Paragraph("AYUDATEST") { Alignment = Element.ALIGN_CENTER });
                doc.Add(new Paragraph("Centro de Estudios Cientifícos y Técnologicos N.13") { Alignment = Element.ALIGN_CENTER });
                doc.Add(new Paragraph("Ricardo Flores Magón") { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);

                doc.Add(new Paragraph("Fecha de Realización: " + (DateTime.Today.ToShortDateString()) + " Hora: " + (DateTime.Now.ToString("hh:mm ")) + (DateTime.Now.Hour < 12 ? "a.m." : "p.m.")) { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);
                doc.Add(new Paragraph("ID: " + usuario._ID.ToString()) { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);

                doc.Add(Chunk.NEWLINE);
                doc.Add(new Paragraph("RESULTADOS TEST ANSIEDAD ") { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);

                doc.Add(new Paragraph("TOTAL: " + resul.ToString()) { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);

                doc.Add(Chunk.NEWLINE);
                doc.Add(new Paragraph("ANSIEDAD GRAVE") { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);

                doc.Add(Chunk.NEWLINE);
                doc.Add(new Paragraph("Recuerda que tu salud mental es muy importante :)") { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);

                doc.Add(Chunk.NEWLINE);
                doc.Add(new Paragraph("© 2022 - AyudaTest® ") { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);

                doc.Close();
                #endregion

                ViewBag.Diag = "Ansiedad grave.";


            }
            return View();
        }
        #endregion

        #region RESULTADOS TEST DEPRESION
        public IActionResult ResultadosDepresion(int OP1, int OP2, int OP3, int OP4, int OP5, int OP6, int OP7, int OP8, int OP9, int OP10, int OP11, int OP12, int OP13, int OP14, int OP15, int OP16, int OP17, int OP18, int OP19, int OP20, int OP21,IDModel usuario)
        {
            string _cadenaCon = _config.GetConnectionString("stringConexion");

            #region CONTADORES
            int resultado = 0;
            ContadorModel Conta = new ContadorModel();
            Conta.OP1 = OP1;
            Conta.OP2 = OP2;
            Conta.OP3 = OP3;
            Conta.OP4 = OP4;
            Conta.OP5 = OP5;
            Conta.OP6 = OP6;
            Conta.OP7 = OP7;
            Conta.OP8 = OP8;
            Conta.OP9 = OP9;
            Conta.OP10 = OP10;
            Conta.OP11 = OP11;
            Conta.OP12 = OP12;
            Conta.OP13 = OP13;
            Conta.OP14 = OP14;
            Conta.OP15 = OP15;
            Conta.OP16 = OP16;
            Conta.OP17 = OP17;
            Conta.OP18 = OP18;
            Conta.OP19 = OP19;
            Conta.OP20 = OP20;
            Conta.OP21 = OP21;
            resultado = Conta.ObtenerResultadosD();
            ViewBag.CResD = Conta.ObtenerResultadosD();
            #endregion

            if (ViewBag.CResD <= 21)
            {
                Document doc = new Document(PageSize.LETTER);
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(@"D:\PA\wwwroot\Sources\RESULTADOS TEST DEPRESION_" + usuario._ID.ToString() + ".pdf", FileMode.Create));
                //PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(@"E:\PA\wwwroot\Sources\RESULTADOS TEST DEPRESION_" + usuario._ID.ToString() + ".pdf", FileMode.Create));

                #region PDF
                doc.Open();

                iTextSharp.text.Image logo1 = iTextSharp.text.Image.GetInstance("https://www.cecyt13.ipn.mx/assets/files/cecyt13/img/Inicio/banderin.png");
                logo1.ScalePercent(25);
                logo1.SetAbsolutePosition(23, 695);
                doc.Add(logo1);
                iTextSharp.text.Image logo2 = iTextSharp.text.Image.GetInstance("https://multipress.com.mx/wp-content/uploads/2020/04/ipn.jpg");
                logo2.ScalePercent(11);
                logo2.SetAbsolutePosition(490, 690);
                doc.Add(logo2);

                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(new Paragraph("AYUDATEST") { Alignment = Element.ALIGN_CENTER });
                doc.Add(new Paragraph("Centro de Estudios Cientifícos y Técnologicos N.13") { Alignment = Element.ALIGN_CENTER });
                doc.Add(new Paragraph("Ricardo Flores Magón") { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);

                doc.Add(new Paragraph("Fecha de Realización: " + (DateTime.Today.ToShortDateString()) + " Hora: " + (DateTime.Now.ToString("hh:mm ")) + (DateTime.Now.Hour < 12 ? "a.m." : "p.m.")) { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);
                doc.Add(new Paragraph("ID: " + usuario._ID.ToString()) { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);

                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(new Paragraph("RESULTADOS TEST DEPRESIÓN ") { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);
                doc.Add(new Paragraph("TOTAL: " + resultado.ToString()) { Alignment = Element.ALIGN_CENTER });

                doc.Add(Chunk.NEWLINE);
                doc.Add(new Paragraph("ESTOS ALTIBAJOS SON CONSIDERADOS NORMALES") { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);

                doc.Add(Chunk.NEWLINE);
                doc.Add(new Paragraph("Recuerda que tu salud mental es muy importante :)") { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);

                doc.Add(Chunk.NEWLINE);
                doc.Add(new Paragraph("© 2022 - AyudaTest® ") { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);

                doc.Close();
                #endregion

                ViewBag.Diag = "Estos altibajos son considerados normales";
            }
            else if (ViewBag.CResD > 21 & ViewBag.CResD < 32)
            {
                Document doc = new Document(PageSize.LETTER);
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(@"D:\PA\wwwroot\Sources\RESULTADOS TEST DEPRESION_" + usuario._ID.ToString() + ".pdf", FileMode.Create));
                //PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(@"E:\PA\wwwroot\Sources\RESULTADOS TEST DEPRESION_" + usuario._ID.ToString() + ".pdf", FileMode.Create));

                #region PDF
                doc.Open();

                iTextSharp.text.Image logo1 = iTextSharp.text.Image.GetInstance("https://www.cecyt13.ipn.mx/assets/files/cecyt13/img/Inicio/banderin.png");
                logo1.ScalePercent(25);
                logo1.SetAbsolutePosition(23, 695);
                doc.Add(logo1);
                iTextSharp.text.Image logo2 = iTextSharp.text.Image.GetInstance("https://multipress.com.mx/wp-content/uploads/2020/04/ipn.jpg");
                logo2.ScalePercent(11);
                logo2.SetAbsolutePosition(490, 690);
                doc.Add(logo2);

                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(new Paragraph("AYUDATEST") { Alignment = Element.ALIGN_CENTER });
                doc.Add(new Paragraph("Centro de Estudios Cientifícos y Técnologicos N.13") { Alignment = Element.ALIGN_CENTER });
                doc.Add(new Paragraph("Ricardo Flores Magón") { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);

                doc.Add(new Paragraph("Fecha de Realización: " + (DateTime.Today.ToShortDateString()) + " Hora: " + (DateTime.Now.ToString("hh:mm ")) + (DateTime.Now.Hour < 12 ? "a.m." : "p.m.")) { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);
                doc.Add(new Paragraph("ID: " + usuario._ID.ToString()) { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);

                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(new Paragraph("RESULTADOS TEST DEPRESIÓN ") { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);
                doc.Add(new Paragraph("TOTAL: " + resultado.ToString()) { Alignment = Element.ALIGN_CENTER });

                doc.Add(Chunk.NEWLINE);
                doc.Add(new Paragraph("LEVE PERTURBACIÓN DEL ESTADO DE ÁNIMO") { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);

                doc.Add(Chunk.NEWLINE);
                doc.Add(new Paragraph("Recuerda que tu salud mental es muy importante :)") { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);

                doc.Add(Chunk.NEWLINE);
                doc.Add(new Paragraph("© 2022 - AyudaTest® ") { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);

                doc.Close();
                #endregion

                ViewBag.Diag = "Leve perturbación del estado de ánimo. ";
            }
            else if (ViewBag.CResD > 32 & ViewBag.CResD <= 42)
            {
                Document doc = new Document(PageSize.LETTER);
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(@"D:\PA\wwwroot\Sources\RESULTADOS TEST DEPRESION_" + usuario._ID.ToString() + ".pdf", FileMode.Create));
                //PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(@"E:\PA\wwwroot\Sources\RESULTADOS TEST DEPRESION_" + usuario._ID.ToString() + ".pdf", FileMode.Create));

                #region PDF
                doc.Open();

                iTextSharp.text.Image logo1 = iTextSharp.text.Image.GetInstance("https://www.cecyt13.ipn.mx/assets/files/cecyt13/img/Inicio/banderin.png");
                logo1.ScalePercent(25);
                logo1.SetAbsolutePosition(23, 695);
                doc.Add(logo1);
                iTextSharp.text.Image logo2 = iTextSharp.text.Image.GetInstance("https://multipress.com.mx/wp-content/uploads/2020/04/ipn.jpg");
                logo2.ScalePercent(11);
                logo2.SetAbsolutePosition(490, 690);
                doc.Add(logo2);

                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(new Paragraph("AYUDATEST") { Alignment = Element.ALIGN_CENTER });
                doc.Add(new Paragraph("Centro de Estudios Cientifícos y Técnologicos N.13") { Alignment = Element.ALIGN_CENTER });
                doc.Add(new Paragraph("Ricardo Flores Magón") { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);

                doc.Add(new Paragraph("Fecha de Realización: " + (DateTime.Today.ToShortDateString()) + " Hora: " + (DateTime.Now.ToString("hh:mm ")) + (DateTime.Now.Hour < 12 ? "a.m." : "p.m.")) { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);
                doc.Add(new Paragraph("ID: " + usuario._ID.ToString()) { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);

                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(new Paragraph("RESULTADOS TEST DEPRESIÓN ") { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);
                doc.Add(new Paragraph("TOTAL: " + resultado.ToString()) { Alignment = Element.ALIGN_CENTER });

                doc.Add(Chunk.NEWLINE);
                doc.Add(new Paragraph("ESTADOS DE DEPRESIÓN INTERMITENTES") { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);

                doc.Add(Chunk.NEWLINE);
                doc.Add(new Paragraph("Recuerda que tu salud mental es muy importante :)") { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);

                doc.Add(Chunk.NEWLINE);
                doc.Add(new Paragraph("© 2022 - AyudaTest® ") { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);

                doc.Close();
                #endregion

                ViewBag.Diag = "Estado de ansiedad intermitentes.";

            }
            else if (ViewBag.CResD > 42 & ViewBag.CResD <= 60)
            {
                Document doc = new Document(PageSize.LETTER);
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(@"D:\PA\wwwroot\Sources\RESULTADOS TEST DEPRESION_" + usuario._ID.ToString() + ".pdf", FileMode.Create));
                // PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(@"E:\PA\wwwroot\Sources\RESULTADOS TEST DEPRESION_" + usuario._ID.ToString() + ".pdf", FileMode.Create));

                #region PDF
                doc.Open();

                iTextSharp.text.Image logo1 = iTextSharp.text.Image.GetInstance("https://www.cecyt13.ipn.mx/assets/files/cecyt13/img/Inicio/banderin.png");
                logo1.ScalePercent(25);
                logo1.SetAbsolutePosition(23, 695);
                doc.Add(logo1);
                iTextSharp.text.Image logo2 = iTextSharp.text.Image.GetInstance("https://multipress.com.mx/wp-content/uploads/2020/04/ipn.jpg");
                logo2.ScalePercent(11);
                logo2.SetAbsolutePosition(490, 690);
                doc.Add(logo2);

                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(new Paragraph("AYUDATEST") { Alignment = Element.ALIGN_CENTER });
                doc.Add(new Paragraph("Centro de Estudios Cientifícos y Técnologicos N.13") { Alignment = Element.ALIGN_CENTER });
                doc.Add(new Paragraph("Ricardo Flores Magón") { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);

                doc.Add(new Paragraph("Fecha de Realización: " + (DateTime.Today.ToShortDateString()) + " Hora: " + (DateTime.Now.ToString("hh:mm ")) + (DateTime.Now.Hour < 12 ? "a.m." : "p.m.")) { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);
                doc.Add(new Paragraph("ID: " + usuario._ID.ToString()) { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);

                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(new Paragraph("RESULTADOS TEST DEPRESIÓN ") { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);
                doc.Add(new Paragraph("TOTAL: " + resultado.ToString()) { Alignment = Element.ALIGN_CENTER });

                doc.Add(Chunk.NEWLINE);
                doc.Add(new Paragraph("DEPRESIÓN MODERADA") { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);

                doc.Add(Chunk.NEWLINE);
                doc.Add(new Paragraph("Recuerda que tu salud mental es muy importante :)") { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);

                doc.Add(Chunk.NEWLINE);
                doc.Add(new Paragraph("© 2022 - AyudaTest® ") { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);

                doc.Close();
                #endregion

                ViewBag.Diag = "Depresion moderada.";

            }
            else if (ViewBag.CResD >60)
            {
                Document doc = new Document(PageSize.LETTER);
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(@"D:\PA\wwwroot\Sources\RESULTADOS TEST DEPRESION_" + usuario._ID.ToString() + ".pdf", FileMode.Create));
                //PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(@"E:\PA\wwwroot\Sources\RESULTADOS TEST DEPRESION_" + usuario._ID.ToString() + ".pdf", FileMode.Create));

                #region PDF
                doc.Open();

                iTextSharp.text.Image logo1 = iTextSharp.text.Image.GetInstance("https://www.cecyt13.ipn.mx/assets/files/cecyt13/img/Inicio/banderin.png");
                logo1.ScalePercent(25);
                logo1.SetAbsolutePosition(23, 695);
                doc.Add(logo1);
                iTextSharp.text.Image logo2 = iTextSharp.text.Image.GetInstance("https://multipress.com.mx/wp-content/uploads/2020/04/ipn.jpg");
                logo2.ScalePercent(11);
                logo2.SetAbsolutePosition(490, 690);
                doc.Add(logo2);

                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(new Paragraph("AYUDATEST") { Alignment = Element.ALIGN_CENTER });
                doc.Add(new Paragraph("Centro de Estudios Cientifícos y Técnologicos N.13") { Alignment = Element.ALIGN_CENTER });
                doc.Add(new Paragraph("Ricardo Flores Magón") { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);

                doc.Add(new Paragraph("Fecha de Realización: " + (DateTime.Today.ToShortDateString()) + " Hora: " + (DateTime.Now.ToString("hh:mm ")) + (DateTime.Now.Hour < 12 ? "a.m." : "p.m.")) { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);
                doc.Add(new Paragraph("ID: " + usuario._ID.ToString()) { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);

                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(new Paragraph("RESULTADOS TEST DEPRESIÓN ") { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);
                doc.Add(new Paragraph("TOTAL: " + resultado.ToString()) { Alignment = Element.ALIGN_CENTER });

                doc.Add(Chunk.NEWLINE);
                doc.Add(new Paragraph("DEPRESIÓN GRAVE") { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);

                doc.Add(Chunk.NEWLINE);
                doc.Add(new Paragraph("Recuerda que tu salud mental es muy importante :)") { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);

                doc.Add(Chunk.NEWLINE);
                doc.Add(new Paragraph("© 2022 - AyudaTest® ") { Alignment = Element.ALIGN_CENTER });
                doc.Add(Chunk.NEWLINE);

                doc.Close();
                #endregion

                ViewBag.Diag = "Depresión grave.";
            }
            return View();
        }
        #endregion

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}