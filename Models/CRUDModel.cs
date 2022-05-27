using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;


namespace PA.Models
{
    public class CRUDModel
    {
        #region CREAR
        public static void Create(IDModel nAlumno, string cadenaCon)
        {
            SqlConnection connection = new SqlConnection(cadenaCon);
            connection.Open();
            string query = "INSERT INTO Datos(Boleta, Nombre, ApPaterno, ApMaterno, TelUser, ID) VALUES (@Boleta, @Nombre, @ApPaterno, @ApMaterno, @TelUser,@ID)";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Boleta", nAlumno._Boleta);
            cmd.Parameters.AddWithValue("@Nombre", nAlumno._Nombre);
            cmd.Parameters.AddWithValue("@ApPaterno", nAlumno._ApPaterno);
            cmd.Parameters.AddWithValue("@ApMaterno", nAlumno._ApMaterno);
            cmd.Parameters.AddWithValue("@TelUser", nAlumno._TelUser);
            cmd.Parameters.AddWithValue("@ID", nAlumno._ID);
            int agregado = cmd.ExecuteNonQuery();
            connection.Close();
        }
        #endregion

        #region MOSTRAR TODOS LOS USUARIOS
        public static DataTable Read(string cadenaCon)
        {
            SqlConnection connection = new SqlConnection(cadenaCon);
            connection.Open();
            string query = "SELECT * FROM Datos";
            SqlDataAdapter Alumnos = new SqlDataAdapter(query, cadenaCon);
            DataTable tabla = new DataTable(query);
            Alumnos.Fill(tabla);
            connection.Close();
            return tabla;
        }
        #endregion

        #region BUSCAR PARA RECUPERAR ID
        public static DataTable ReadAlumno(string Nom, string Ap, string Am, string cadenaCon)
        {
            SqlConnection connection = new SqlConnection(cadenaCon);
            connection.Open();
            string query = "SELECT ID,Nombre,ApPaterno,ApMaterno FROM Datos WHERE Nombre='" + Nom + "' AND ApPaterno='" + Ap + "' AND ApMaterno = '" + Am + "'";
            SqlDataAdapter Alumnos = new SqlDataAdapter(query, cadenaCon);
            DataTable tabla = new DataTable(query);
            Alumnos.Fill(tabla);
            connection.Close();
            return tabla;
        }
        #endregion

        #region BUSCAR ID LOGIN
        public static bool LogIn(IDModel nUsuario, string cadenaCon)
        {
            SqlConnection connection = new SqlConnection(cadenaCon);
            connection.Open();
            SqlCommand cmd = new SqlCommand(string.Format("SELECT ID FROM Datos WHERE ID = '{0}'", nUsuario._ID), connection);
            SqlDataAdapter sqlda = new SqlDataAdapter(cmd);
            DataTable tabla = new DataTable();
            sqlda.Fill(tabla);
            connection.Close();
            if (tabla.Rows.Count == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        #endregion

        #region DAR NOMBRE DEL QUE SE LOGUEO
        public static DataTable Usuario(IDModel nUsuario, string cadenaCon)
        {
            SqlConnection connection = new SqlConnection(cadenaCon);
            connection.Open();
            string query = "SELECT Nombre,ApPaterno,ApMaterno FROM Datos WHERE ID='" + nUsuario._ID + "'";
            SqlDataAdapter Alumnos = new SqlDataAdapter(query, cadenaCon);
            DataTable tabla = new DataTable(query);
            Alumnos.Fill(tabla);
            connection.Close();
            return tabla;
        }
        #endregion

        #region BUSCAR ADMIN LOGIN
        public static bool LogueoAdmin(IDModel nAdmin, string cadenaCon)
        {
            SqlConnection connection = new SqlConnection(cadenaCon);
            connection.Open();
            SqlCommand cmd = new SqlCommand(string.Format("SELECT Usuario, Contraseña FROM Administrador WHERE Usuario = '{0}' AND Contraseña = '{1}'", nAdmin._Admin, nAdmin._Pass), connection);
            SqlDataAdapter sqlda = new SqlDataAdapter(cmd);
            DataTable tabla = new DataTable();
            sqlda.Fill(tabla);
            connection.Close();
            if (tabla.Rows.Count == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region VALIDACIONES
        public static bool BuscarBol(IDModel nUsuario, string cadenaCon)
        {
            SqlConnection connection = new SqlConnection(cadenaCon);
            connection.Open();
            SqlCommand cmd = new SqlCommand(string.Format("SELECT Boleta FROM Datos WHERE Boleta = '{0}'", nUsuario._Boleta), connection);
            SqlDataAdapter sqlda = new SqlDataAdapter(cmd);
            DataTable tabla = new DataTable();
            sqlda.Fill(tabla);
            connection.Close();
            if (tabla.Rows.Count == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region CAPTCHA
        public static string CAPTCHA()
        {
            Random clave = new Random();
            string cap = string.Empty;
            string l1;
            string l2;
            char D1;
            char D2;
            int L1 = 0;
            L1 = clave.Next(65, 90);
            int L2 = 0;
            L2 = clave.Next(65, 90);
            int N = 0;
            N = Convert.ToInt32(clave.Next(10, 90));
            string captcha = string.Empty;
            D1 = Convert.ToChar(L1);
            D2 = Convert.ToChar(L2);
            l1 = D1.ToString();
            l2 = D2.ToString();
            captcha = l1 + N.ToString() + l2.ToString();
            return captcha;
        }

        public static bool Validar(IDModel nUser)
        {
            if (nUser._Captcha == nUser._CaptchaConfirm)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region ELIMINAR USUARIO
        public static void Delete(IDModel nUsuario, string cadenaCon)
        {
            SqlConnection connection = new SqlConnection(cadenaCon);
            connection.Open();
            string query = "DELETE FROM Datos WHERE ID = @Id";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Id", nUsuario._ID);
            int agregado = cmd.ExecuteNonQuery();
            connection.Close();
        }
        #endregion

        #region TEST ANSIEDAD
        public static DataTable TestAnsiedad(string cadenaCon)
        {
            SqlConnection connection = new SqlConnection(cadenaCon);
            connection.Open();
            string query = "SELECT * FROM TestAns";
            SqlDataAdapter Alumnos = new SqlDataAdapter(query, cadenaCon);
            DataTable tabla = new DataTable(query);
            Alumnos.Fill(tabla);
            connection.Close();
            return tabla;
        }

        public static DataTable TestAnsiedad1( string cadenaCon)
        {
            SqlConnection connection = new SqlConnection(cadenaCon);
            connection.Open();
            string query = "SELECT No_Pregunta,Pregunta FROM TestAns WHERE No_Pregunta='1'";
            SqlDataAdapter Alumnos = new SqlDataAdapter(query, cadenaCon);
            DataTable tabla = new DataTable(query);
            Alumnos.Fill(tabla);
            connection.Close();
            return tabla;
        }

        public static DataTable TestAnsiedad2(string cadenaCon)
        {
            SqlConnection connection = new SqlConnection(cadenaCon);
            connection.Open();
            string query = "SELECT No_Pregunta,Pregunta FROM TestAns WHERE No_Pregunta='2'";
            SqlDataAdapter Alumnos = new SqlDataAdapter(query, cadenaCon);
            DataTable tabla = new DataTable(query);
            Alumnos.Fill(tabla);
            connection.Close();
            return tabla;
        }

        public static DataTable TestAnsiedad3(string cadenaCon)
        {
            SqlConnection connection = new SqlConnection(cadenaCon);
            connection.Open();
            string query = "SELECT No_Pregunta,Pregunta FROM TestAns WHERE No_Pregunta='3'";
            SqlDataAdapter Alumnos = new SqlDataAdapter(query, cadenaCon);
            DataTable tabla = new DataTable(query);
            Alumnos.Fill(tabla);
            connection.Close();
            return tabla;
        }

        public static DataTable TestAnsiedad4(string cadenaCon)
        {
            SqlConnection connection = new SqlConnection(cadenaCon);
            connection.Open();
            string query = "SELECT No_Pregunta,Pregunta FROM TestAns WHERE No_Pregunta='4'";
            SqlDataAdapter Alumnos = new SqlDataAdapter(query, cadenaCon);
            DataTable tabla = new DataTable(query);
            Alumnos.Fill(tabla);
            connection.Close();
            return tabla;
        }

        public static DataTable TestAnsiedad5(string cadenaCon)
        {
            SqlConnection connection = new SqlConnection(cadenaCon);
            connection.Open();
            string query = "SELECT No_Pregunta,Pregunta FROM TestAns WHERE No_Pregunta='5'";
            SqlDataAdapter Alumnos = new SqlDataAdapter(query, cadenaCon);
            DataTable tabla = new DataTable(query);
            Alumnos.Fill(tabla);
            connection.Close();
            return tabla;
        }

        public static DataTable TestAnsiedad6(string cadenaCon)
        {
            SqlConnection connection = new SqlConnection(cadenaCon);
            connection.Open();
            string query = "SELECT No_Pregunta,Pregunta FROM TestAns WHERE No_Pregunta='6'";
            SqlDataAdapter Alumnos = new SqlDataAdapter(query, cadenaCon);
            DataTable tabla = new DataTable(query);
            Alumnos.Fill(tabla);
            connection.Close();
            return tabla;
        }

        public static DataTable TestAnsiedad7(string cadenaCon)
        {
            SqlConnection connection = new SqlConnection(cadenaCon);
            connection.Open();
            string query = "SELECT No_Pregunta,Pregunta FROM TestAns WHERE No_Pregunta='7'";
            SqlDataAdapter Alumnos = new SqlDataAdapter(query, cadenaCon);
            DataTable tabla = new DataTable(query);
            Alumnos.Fill(tabla);
            connection.Close();
            return tabla;
        }

        public static DataTable TestAnsiedad8(string cadenaCon)
        {
            SqlConnection connection = new SqlConnection(cadenaCon);
            connection.Open();
            string query = "SELECT No_Pregunta,Pregunta FROM TestAns WHERE No_Pregunta='8'";
            SqlDataAdapter Alumnos = new SqlDataAdapter(query, cadenaCon);
            DataTable tabla = new DataTable(query);
            Alumnos.Fill(tabla);
            connection.Close();
            return tabla;
        }

        public static DataTable TestAnsiedad9(string cadenaCon)
        {
            SqlConnection connection = new SqlConnection(cadenaCon);
            connection.Open();
            string query = "SELECT No_Pregunta,Pregunta FROM TestAns WHERE No_Pregunta='9'";
            SqlDataAdapter Alumnos = new SqlDataAdapter(query, cadenaCon);
            DataTable tabla = new DataTable(query);
            Alumnos.Fill(tabla);
            connection.Close();
            return tabla;
        }

        public static DataTable TestAnsiedad10(string cadenaCon)
        {
            SqlConnection connection = new SqlConnection(cadenaCon);
            connection.Open();
            string query = "SELECT No_Pregunta,Pregunta FROM TestAns WHERE No_Pregunta='10'";
            SqlDataAdapter Alumnos = new SqlDataAdapter(query, cadenaCon);
            DataTable tabla = new DataTable(query);
            Alumnos.Fill(tabla);
            connection.Close();
            return tabla;
        }

        public static DataTable TestAnsiedad11(string cadenaCon)
        {
            SqlConnection connection = new SqlConnection(cadenaCon);
            connection.Open();
            string query = "SELECT No_Pregunta,Pregunta FROM TestAns WHERE No_Pregunta='11'";
            SqlDataAdapter Alumnos = new SqlDataAdapter(query, cadenaCon);
            DataTable tabla = new DataTable(query);
            Alumnos.Fill(tabla);
            connection.Close();
            return tabla;
        }
        public static DataTable TestAnsiedad12(string cadenaCon)
        {
            SqlConnection connection = new SqlConnection(cadenaCon);
            connection.Open();
            string query = "SELECT No_Pregunta,Pregunta FROM TestAns WHERE No_Pregunta='12'";
            SqlDataAdapter Alumnos = new SqlDataAdapter(query, cadenaCon);
            DataTable tabla = new DataTable(query);
            Alumnos.Fill(tabla);
            connection.Close();
            return tabla;
        }

        public static DataTable TestAnsiedad13(string cadenaCon)
        {
            SqlConnection connection = new SqlConnection(cadenaCon);
            connection.Open();
            string query = "SELECT No_Pregunta,Pregunta FROM TestAns WHERE No_Pregunta='13'";
            SqlDataAdapter Alumnos = new SqlDataAdapter(query, cadenaCon);
            DataTable tabla = new DataTable(query);
            Alumnos.Fill(tabla);
            connection.Close();
            return tabla;
        }

        public static DataTable TestAnsiedad14(string cadenaCon)
        {
            SqlConnection connection = new SqlConnection(cadenaCon);
            connection.Open();
            string query = "SELECT No_Pregunta,Pregunta FROM TestAns WHERE No_Pregunta='14'";
            SqlDataAdapter Alumnos = new SqlDataAdapter(query, cadenaCon);
            DataTable tabla = new DataTable(query);
            Alumnos.Fill(tabla);
            connection.Close();
            return tabla;
        }

        public static DataTable TestAnsiedad15(string cadenaCon)
        {
            SqlConnection connection = new SqlConnection(cadenaCon);
            connection.Open();
            string query = "SELECT No_Pregunta,Pregunta FROM TestAns WHERE No_Pregunta='15'";
            SqlDataAdapter Alumnos = new SqlDataAdapter(query, cadenaCon);
            DataTable tabla = new DataTable(query);
            Alumnos.Fill(tabla);
            connection.Close();
            return tabla;
        }

        public static DataTable TestAnsiedad16(string cadenaCon)
        {
            SqlConnection connection = new SqlConnection(cadenaCon);
            connection.Open();
            string query = "SELECT No_Pregunta,Pregunta FROM TestAns WHERE No_Pregunta='16'";
            SqlDataAdapter Alumnos = new SqlDataAdapter(query, cadenaCon);
            DataTable tabla = new DataTable(query);
            Alumnos.Fill(tabla);
            connection.Close();
            return tabla;
        }
        public static DataTable TestAnsiedad17(string cadenaCon)
        {
            SqlConnection connection = new SqlConnection(cadenaCon);
            connection.Open();
            string query = "SELECT No_Pregunta,Pregunta FROM TestAns WHERE No_Pregunta='17'";
            SqlDataAdapter Alumnos = new SqlDataAdapter(query, cadenaCon);
            DataTable tabla = new DataTable(query);
            Alumnos.Fill(tabla);
            connection.Close();
            return tabla;
        }

        public static DataTable TestAnsiedad18(string cadenaCon)
        {
            SqlConnection connection = new SqlConnection(cadenaCon);
            connection.Open();
            string query = "SELECT No_Pregunta,Pregunta FROM TestAns WHERE No_Pregunta='18'";
            SqlDataAdapter Alumnos = new SqlDataAdapter(query, cadenaCon);
            DataTable tabla = new DataTable(query);
            Alumnos.Fill(tabla);
            connection.Close();
            return tabla;
        }

        public static DataTable TestAnsiedad19(string cadenaCon)
        {
            SqlConnection connection = new SqlConnection(cadenaCon);
            connection.Open();
            string query = "SELECT No_Pregunta,Pregunta FROM TestAns WHERE No_Pregunta='19'";
            SqlDataAdapter Alumnos = new SqlDataAdapter(query, cadenaCon);
            DataTable tabla = new DataTable(query);
            Alumnos.Fill(tabla);
            connection.Close();
            return tabla;
        }

        public static DataTable TestAnsiedad20(string cadenaCon)
        {
            SqlConnection connection = new SqlConnection(cadenaCon);
            connection.Open();
            string query = "SELECT No_Pregunta,Pregunta FROM TestAns WHERE No_Pregunta='20'";
            SqlDataAdapter Alumnos = new SqlDataAdapter(query, cadenaCon);
            DataTable tabla = new DataTable(query);
            Alumnos.Fill(tabla);
            connection.Close();
            return tabla;
        }

        public static DataTable TestAnsiedad21(string cadenaCon)
        {
            SqlConnection connection = new SqlConnection(cadenaCon);
            connection.Open();
            string query = "SELECT No_Pregunta,Pregunta FROM TestAns WHERE No_Pregunta='21'";
            SqlDataAdapter Alumnos = new SqlDataAdapter(query, cadenaCon);
            DataTable tabla = new DataTable(query);
            Alumnos.Fill(tabla);
            connection.Close();
            return tabla;
        }
        #endregion

        #region TEST DEPRESION
        public static DataTable TestDepresion(string cadenaCon)
        {
            SqlConnection connection = new SqlConnection(cadenaCon);
            connection.Open();
            string query = "SELECT * FROM TestDep";
            SqlDataAdapter Alumnos = new SqlDataAdapter(query, cadenaCon);
            DataTable tabla = new DataTable(query);
            Alumnos.Fill(tabla);
            connection.Close();
            return tabla;
        }

        public static DataTable TestDepresion1(string cadenaCon)
        {
            SqlConnection connection = new SqlConnection(cadenaCon);
            connection.Open();
            string query = "SELECT No_Pregunta,Pregunta FROM TestDep WHERE No_Pregunta='1'";
            SqlDataAdapter Alumnos = new SqlDataAdapter(query, cadenaCon);
            DataTable tabla = new DataTable(query);
            Alumnos.Fill(tabla);
            connection.Close();
            return tabla;
        }

        public static DataTable TestDepresion2(string cadenaCon)
        {
            SqlConnection connection = new SqlConnection(cadenaCon);
            connection.Open();
            string query = "SELECT No_Pregunta,Pregunta FROM TestAns WHERE No_Pregunta='2'";
            SqlDataAdapter Alumnos = new SqlDataAdapter(query, cadenaCon);
            DataTable tabla = new DataTable(query);
            Alumnos.Fill(tabla);
            connection.Close();
            return tabla;
        }

        public static DataTable TestDepresion3(string cadenaCon)
        {
            SqlConnection connection = new SqlConnection(cadenaCon);
            connection.Open();
            string query = "SELECT No_Pregunta,Pregunta FROM TestDep WHERE No_Pregunta='3'";
            SqlDataAdapter Alumnos = new SqlDataAdapter(query, cadenaCon);
            DataTable tabla = new DataTable(query);
            Alumnos.Fill(tabla);
            connection.Close();
            return tabla;
        }

        public static DataTable TestDepresion4(string cadenaCon)
        {
            SqlConnection connection = new SqlConnection(cadenaCon);
            connection.Open();
            string query = "SELECT No_Pregunta,Pregunta FROM TestDep WHERE No_Pregunta='4'";
            SqlDataAdapter Alumnos = new SqlDataAdapter(query, cadenaCon);
            DataTable tabla = new DataTable(query);
            Alumnos.Fill(tabla);
            connection.Close();
            return tabla;
        }

        public static DataTable TestDepresion5(string cadenaCon)
        {
            SqlConnection connection = new SqlConnection(cadenaCon);
            connection.Open();
            string query = "SELECT No_Pregunta,Pregunta FROM TestDep WHERE No_Pregunta='5'";
            SqlDataAdapter Alumnos = new SqlDataAdapter(query, cadenaCon);
            DataTable tabla = new DataTable(query);
            Alumnos.Fill(tabla);
            connection.Close();
            return tabla;
        }

        public static DataTable TestDepresion6(string cadenaCon)
        {
            SqlConnection connection = new SqlConnection(cadenaCon);
            connection.Open();
            string query = "SELECT No_Pregunta,Pregunta FROM TestDep WHERE No_Pregunta='6'";
            SqlDataAdapter Alumnos = new SqlDataAdapter(query, cadenaCon);
            DataTable tabla = new DataTable(query);
            Alumnos.Fill(tabla);
            connection.Close();
            return tabla;
        }

        public static DataTable TestDepresion7(string cadenaCon)
        {
            SqlConnection connection = new SqlConnection(cadenaCon);
            connection.Open();
            string query = "SELECT No_Pregunta,Pregunta FROM TestDep WHERE No_Pregunta='7'";
            SqlDataAdapter Alumnos = new SqlDataAdapter(query, cadenaCon);
            DataTable tabla = new DataTable(query);
            Alumnos.Fill(tabla);
            connection.Close();
            return tabla;
        }

        public static DataTable TestDepresion8(string cadenaCon)
        {
            SqlConnection connection = new SqlConnection(cadenaCon);
            connection.Open();
            string query = "SELECT No_Pregunta,Pregunta FROM TestDep WHERE No_Pregunta='8'";
            SqlDataAdapter Alumnos = new SqlDataAdapter(query, cadenaCon);
            DataTable tabla = new DataTable(query);
            Alumnos.Fill(tabla);
            connection.Close();
            return tabla;
        }

        public static DataTable TestDepresion9(string cadenaCon)
        {
            SqlConnection connection = new SqlConnection(cadenaCon);
            connection.Open();
            string query = "SELECT No_Pregunta,Pregunta FROM TestDep WHERE No_Pregunta='9'";
            SqlDataAdapter Alumnos = new SqlDataAdapter(query, cadenaCon);
            DataTable tabla = new DataTable(query);
            Alumnos.Fill(tabla);
            connection.Close();
            return tabla;
        }

        public static DataTable TestDepresion10(string cadenaCon)
        {
            SqlConnection connection = new SqlConnection(cadenaCon);
            connection.Open();
            string query = "SELECT No_Pregunta,Pregunta FROM TestDep WHERE No_Pregunta='10'";
            SqlDataAdapter Alumnos = new SqlDataAdapter(query, cadenaCon);
            DataTable tabla = new DataTable(query);
            Alumnos.Fill(tabla);
            connection.Close();
            return tabla;
        }

        public static DataTable TestDepresion11(string cadenaCon)
        {
            SqlConnection connection = new SqlConnection(cadenaCon);
            connection.Open();
            string query = "SELECT No_Pregunta,Pregunta FROM TestDep WHERE No_Pregunta='11'";
            SqlDataAdapter Alumnos = new SqlDataAdapter(query, cadenaCon);
            DataTable tabla = new DataTable(query);
            Alumnos.Fill(tabla);
            connection.Close();
            return tabla;
        }
        public static DataTable TestDepresion12(string cadenaCon)
        {
            SqlConnection connection = new SqlConnection(cadenaCon);
            connection.Open();
            string query = "SELECT No_Pregunta,Pregunta FROM TestDep WHERE No_Pregunta='12'";
            SqlDataAdapter Alumnos = new SqlDataAdapter(query, cadenaCon);
            DataTable tabla = new DataTable(query);
            Alumnos.Fill(tabla);
            connection.Close();
            return tabla;
        }

        public static DataTable TestDepresion13(string cadenaCon)
        {
            SqlConnection connection = new SqlConnection(cadenaCon);
            connection.Open();
            string query = "SELECT No_Pregunta,Pregunta FROM TestDep WHERE No_Pregunta='13'";
            SqlDataAdapter Alumnos = new SqlDataAdapter(query, cadenaCon);
            DataTable tabla = new DataTable(query);
            Alumnos.Fill(tabla);
            connection.Close();
            return tabla;
        }

        public static DataTable TestDepresion14(string cadenaCon)
        {
            SqlConnection connection = new SqlConnection(cadenaCon);
            connection.Open();
            string query = "SELECT No_Pregunta,Pregunta FROM TestDep WHERE No_Pregunta='14'";
            SqlDataAdapter Alumnos = new SqlDataAdapter(query, cadenaCon);
            DataTable tabla = new DataTable(query);
            Alumnos.Fill(tabla);
            connection.Close();
            return tabla;
        }

        public static DataTable TestDepresion15(string cadenaCon)
        {
            SqlConnection connection = new SqlConnection(cadenaCon);
            connection.Open();
            string query = "SELECT No_Pregunta,Pregunta FROM TestDep WHERE No_Pregunta='15'";
            SqlDataAdapter Alumnos = new SqlDataAdapter(query, cadenaCon);
            DataTable tabla = new DataTable(query);
            Alumnos.Fill(tabla);
            connection.Close();
            return tabla;
        }

        public static DataTable TestDepresion16(string cadenaCon)
        {
            SqlConnection connection = new SqlConnection(cadenaCon);
            connection.Open();
            string query = "SELECT No_Pregunta,Pregunta FROM TestDep WHERE No_Pregunta='16'";
            SqlDataAdapter Alumnos = new SqlDataAdapter(query, cadenaCon);
            DataTable tabla = new DataTable(query);
            Alumnos.Fill(tabla);
            connection.Close();
            return tabla;
        }
        public static DataTable TestDepresion17(string cadenaCon)
        {
            SqlConnection connection = new SqlConnection(cadenaCon);
            connection.Open();
            string query = "SELECT No_Pregunta,Pregunta FROM TestDep WHERE No_Pregunta='17'";
            SqlDataAdapter Alumnos = new SqlDataAdapter(query, cadenaCon);
            DataTable tabla = new DataTable(query);
            Alumnos.Fill(tabla);
            connection.Close();
            return tabla;
        }

        public static DataTable TestDepresion18(string cadenaCon)
        {
            SqlConnection connection = new SqlConnection(cadenaCon);
            connection.Open();
            string query = "SELECT No_Pregunta,Pregunta FROM TestDep WHERE No_Pregunta='18'";
            SqlDataAdapter Alumnos = new SqlDataAdapter(query, cadenaCon);
            DataTable tabla = new DataTable(query);
            Alumnos.Fill(tabla);
            connection.Close();
            return tabla;
        }

        public static DataTable TestDepresion19(string cadenaCon)
        {
            SqlConnection connection = new SqlConnection(cadenaCon);
            connection.Open();
            string query = "SELECT No_Pregunta,Pregunta FROM TestDep WHERE No_Pregunta='19'";
            SqlDataAdapter Alumnos = new SqlDataAdapter(query, cadenaCon);
            DataTable tabla = new DataTable(query);
            Alumnos.Fill(tabla);
            connection.Close();
            return tabla;
        }

        public static DataTable TestDepresion20(string cadenaCon)
        {
            SqlConnection connection = new SqlConnection(cadenaCon);
            connection.Open();
            string query = "SELECT No_Pregunta,Pregunta FROM TestDep WHERE No_Pregunta='20'";
            SqlDataAdapter Alumnos = new SqlDataAdapter(query, cadenaCon);
            DataTable tabla = new DataTable(query);
            Alumnos.Fill(tabla);
            connection.Close();
            return tabla;
        }

        public static DataTable TestDepresion21(string cadenaCon)
        {
            SqlConnection connection = new SqlConnection(cadenaCon);
            connection.Open();
            string query = "SELECT No_Pregunta,Pregunta FROM TestDep WHERE No_Pregunta='21'";
            SqlDataAdapter Alumnos = new SqlDataAdapter(query, cadenaCon);
            DataTable tabla = new DataTable(query);
            Alumnos.Fill(tabla);
            connection.Close();
            return tabla;
        }
        #endregion

        #region ACTUALIZAR PREGUNTAS DE TEST
        public static void UpdateTAn(IDModel nPreg, string cadenaCon)
        {
            SqlConnection connection = new SqlConnection(cadenaCon);
            connection.Open();
            string query = "UPDATE TestAns SET Pregunta = @Pregunta  WHERE No_Pregunta  = @NoPregunta";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@NoPregunta", nPreg._NoPregA);
            cmd.Parameters.AddWithValue("@Pregunta ", nPreg._PreguntaA);
            int agregado = cmd.ExecuteNonQuery();
            connection.Close();
        }

        public static void UpdateTDe(IDModel nPreg, string cadenaCon)
        {
            SqlConnection connection = new SqlConnection(cadenaCon);
            connection.Open();
            string query = "UPDATE TestDep SET Pregunta = @Pregunta  WHERE No_Pregunta  = @NoPregunta";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@NoPregunta", nPreg._NoPregD);
            cmd.Parameters.AddWithValue("@Pregunta ", nPreg._PreguntaD);
            int agregado = cmd.ExecuteNonQuery();
            connection.Close();
        }
        #endregion
    }
}
