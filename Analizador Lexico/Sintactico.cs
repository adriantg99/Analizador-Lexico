using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 * carnal, llevo mas de 24 horas seguidas sin parar
 * en este codigo, y para ese entonces solo Dios y yo sabiamos como funcionaba
 * pero ahora solo Dios lo sabe
 * 
 * suerte
 * 
 * horas perdidas en esto: 47
*/


namespace Analizador_Lexico
{
    class Sintactico
    {
        public static List<int> Tokens = new List<int>();

        public static int lineasyntax;


        int puntero = 0;
        int maximo = Tokens.Count;
        Error errorSyntax = new Error() { Codigo = 0, MsjError = "", Tipo = TipoError.Sintactico, Linea = 0 };


        public int EjecutarSintactico()
        {
            Lexico.listaError.Clear();

            if (Tokens.Count == 0)
            {
                Tokens.Clear();
                return 1;
            }
            else
            {

                if (Funcdef() == 1)
                {

                    if(ParaOrientar() == 1)
                    {
                        MessageBox.Show("llegue al final de la compilacion");
                        Tokens.Clear();
                        return 1;
                    }
                    else
                    {
                       puntero++;
                       if (Funcdef() == 1)
                        {
                            Tokens.Clear();
                            return 1;
                        }
                        else
                        {
                            Tokens.Clear();
                            return 0;
                        }
                    }



                }
                else
                {
                    Tokens.Clear();
                    return 0;
                }
            }
        }

        private int ParaOrientar()
        {
            int length = Tokens.Count - 1;//Ancho total de la lista para verificar si hay algo despues del cierre
            if (puntero >= length)
            {
                return 1;
            }
            else
            {
                return 0;
            }


        }

        private int Funcdef()
        {

            if (puntero < Tokens.Count && Tokens[puntero] == 223)//import
            {
                puntero++;
                if (puntero < Tokens.Count && Tokens[puntero] == 100)//ID
                {
                    puntero++;
                    if (puntero < Tokens.Count && Tokens[puntero] == 120)//Punto y coma ;
                    {
                        puntero++;
                        if (puntero < Tokens.Count && Tokens[puntero] == 225 || Tokens[puntero] == 226)//Abstract || public
                        {
                            puntero++;
                            if (puntero < Tokens.Count && Tokens[puntero] == 211) // def
                            {
                                puntero++;
                                if (Funcname() == 1) //palabra
                                {
                                    if (puntero < Tokens.Count && Tokens[puntero] == 113) // (
                                    {
                                        puntero++;
                                        if (puntero < Tokens.Count && Tokens[puntero] == 114) // )
                                        {
                                            puntero++;
                                            if (puntero < Tokens.Count && Tokens[puntero] == 121) //:
                                            {
                                                puntero++;
                                                if (puntero < Tokens.Count && Tokens[puntero] == 222)//extends
                                                {
                                                    puntero++;
                                                    if (puntero < Tokens.Count && Tokens[puntero] == 100)//palabra
                                                    {
                                                        puntero++;
                                                        if (puntero < Tokens.Count && Tokens[puntero] == 113)//(
                                                        {
                                                            puntero++;
                                                            if (puntero < Tokens.Count && Tokens[puntero] == 114)//)
                                                            {
                                                                puntero++;
                                                                if (puntero < Tokens.Count && Tokens[puntero] == 117)//{
                                                                {
                                                                    puntero++;

                                                                    if (Suite() == 118)//}
                                                                    {
                                                                        puntero++;
                                                                        return 1;
                                                                    }

                                                                    else
                                                                    {
                                                                        errorSyntax.Codigo = 621;
                                                                        errorSyntax.MsjError = "Se esperaba un }";
                                                                        errorSyntax.Linea = 0;
                                                                        Lexico.listaError.Add(errorSyntax);

                                                                        return 0;
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    errorSyntax.Codigo = 609;
                                                                    errorSyntax.MsjError = "Se esperaba un {";
                                                                    errorSyntax.Linea = 0;
                                                                    Lexico.listaError.Add(errorSyntax);
                                                                    return 0;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                errorSyntax.Codigo = 603;
                                                                errorSyntax.MsjError = "Se esperaba un )";
                                                                errorSyntax.Linea = 0;
                                                                Lexico.listaError.Add(errorSyntax);
                                                                return 0;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            errorSyntax.Codigo = 602;
                                                            errorSyntax.MsjError = "Se esperaba un (";
                                                            errorSyntax.Linea = 0;
                                                            Lexico.listaError.Add(errorSyntax);
                                                            return 0;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        errorSyntax.Codigo = 601;
                                                        errorSyntax.MsjError = "Se esperaba un identificador";
                                                        errorSyntax.Linea = 0;
                                                        Lexico.listaError.Add(errorSyntax);
                                                        return 0;
                                                    }
                                                }

                                                if (puntero < Tokens.Count && Tokens[puntero] == 117) ///{
                                                {
                                                    puntero++;


                                                    if (Suite() == 118)
                                                    {
                                                        return 1;
                                                    }
                                                    else
                                                    {

                                                        errorSyntax.Codigo = 621;
                                                        errorSyntax.MsjError = "Se esperaba un }";
                                                        errorSyntax.Linea = 0;
                                                        Lexico.listaError.Add(errorSyntax);

                                                        return 0;
                                                    }
                                                }
                                                else
                                                {
                                                    errorSyntax.Codigo = 609;
                                                    errorSyntax.MsjError = "Se esperaba un {";
                                                    errorSyntax.Linea = 0;
                                                    Lexico.listaError.Add(errorSyntax);
                                                    return 0;
                                                }
                                            }
                                            else
                                            {
                                                errorSyntax.Codigo = 604;
                                                errorSyntax.MsjError = "Se esperaba un :";
                                                errorSyntax.Linea = 0;
                                                Lexico.listaError.Add(errorSyntax);
                                                return 0;
                                            }
                                        }
                                        else
                                        {
                                            errorSyntax.Codigo = 603;
                                            errorSyntax.MsjError = "Se esperaba un )";
                                            errorSyntax.Linea = 0;
                                            Lexico.listaError.Add(errorSyntax);
                                            return 0;
                                        }
                                    }
                                    else
                                    {
                                        errorSyntax.Codigo = 602;
                                        errorSyntax.MsjError = "Se esperaba un (";
                                        errorSyntax.Linea = 0;
                                        Lexico.listaError.Add(errorSyntax);
                                        return 0;
                                    }
                                }
                                else
                                {
                                    errorSyntax.Codigo = 601;
                                    errorSyntax.MsjError = "Se esperaba un identificador";
                                    errorSyntax.Linea = 0;
                                    Lexico.listaError.Add(errorSyntax);
                                    return 0;
                                }
                            }
                            else  // def
                            {
                                errorSyntax.Codigo = 600;
                                errorSyntax.MsjError = "Se esperaba def aqui trono 1";
                                errorSyntax.Linea = 0;
                                Lexico.listaError.Add(errorSyntax);
                                return 0;
                            }
                        }
                       
                        else if (puntero < Tokens.Count && Tokens[puntero] == 223)//import
                        {
                            puntero++;
                            if (puntero < Tokens.Count && Tokens[puntero] == 100)//Cadena
                            {
                                puntero++;
                                if (puntero < Tokens.Count && Tokens[puntero] == 120)//Punto y coma ;
                                {
                                    puntero++;
                                    if(puntero < Tokens.Count && Tokens[puntero] ==225 || Tokens[puntero] == 226)//Abstract y public
                                    {
                                        puntero++;
                                        if (puntero < Tokens.Count && Tokens[puntero] == 211) // def
                                        {
                                            puntero++;
                                            if (Funcname() == 1) //palabra
                                            {
                                                if (puntero < Tokens.Count && Tokens[puntero] == 113) // (
                                                {
                                                    puntero++;
                                                    if (puntero < Tokens.Count && Tokens[puntero] == 114) // )
                                                    {
                                                        puntero++;
                                                        if (puntero < Tokens.Count && Tokens[puntero] == 121) //:
                                                        {
                                                            puntero++;
                                                            if (puntero < Tokens.Count && Tokens[puntero] == 222)//extends
                                                            {
                                                                puntero++;
                                                                if (puntero < Tokens.Count && Tokens[puntero] == 100)//palabra
                                                                {
                                                                    puntero++;
                                                                    if (puntero < Tokens.Count && Tokens[puntero] == 113)//(
                                                                    {
                                                                        puntero++;
                                                                        if (puntero < Tokens.Count && Tokens[puntero] == 114)//)
                                                                        {
                                                                            puntero++;
                                                                            if (puntero < Tokens.Count && Tokens[puntero] == 117)//{
                                                                            {
                                                                                puntero++;

                                                                                if (Suite() == 118)
                                                                                {
                                                                                    return 1;
                                                                                }

                                                                                else
                                                                                {
                                                                                    errorSyntax.Codigo = 621;
                                                                                    errorSyntax.MsjError = "Se esperaba un }";
                                                                                    errorSyntax.Linea = 0;
                                                                                    Lexico.listaError.Add(errorSyntax);

                                                                                    return 0;
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                errorSyntax.Codigo = 609;
                                                                                errorSyntax.MsjError = "Se esperaba un {";
                                                                                errorSyntax.Linea = 0;
                                                                                Lexico.listaError.Add(errorSyntax);
                                                                                return 0;
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            errorSyntax.Codigo = 603;
                                                                            errorSyntax.MsjError = "Se esperaba un )";
                                                                            errorSyntax.Linea = 0;
                                                                            Lexico.listaError.Add(errorSyntax);
                                                                            return 0;
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        errorSyntax.Codigo = 602;
                                                                        errorSyntax.MsjError = "Se esperaba un (";
                                                                        errorSyntax.Linea = 0;
                                                                        Lexico.listaError.Add(errorSyntax);
                                                                        return 0;
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    errorSyntax.Codigo = 601;
                                                                    errorSyntax.MsjError = "Se esperaba un identificador";
                                                                    errorSyntax.Linea = 0;
                                                                    Lexico.listaError.Add(errorSyntax);
                                                                    return 0;
                                                                }
                                                            }

                                                            if (puntero < Tokens.Count && Tokens[puntero] == 117) ///{
                                                            {
                                                                puntero++;


                                                                if (Suite() == 118)
                                                                {
                                                                    return 1;
                                                                }
                                                                else
                                                                {

                                                                    errorSyntax.Codigo = 621;
                                                                    errorSyntax.MsjError = "Se esperaba un }";
                                                                    errorSyntax.Linea = 0;
                                                                    Lexico.listaError.Add(errorSyntax);

                                                                    return 0;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                errorSyntax.Codigo = 609;
                                                                errorSyntax.MsjError = "Se esperaba un {";
                                                                errorSyntax.Linea = 0;
                                                                Lexico.listaError.Add(errorSyntax);
                                                                return 0;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            errorSyntax.Codigo = 604;
                                                            errorSyntax.MsjError = "Se esperaba un :";
                                                            errorSyntax.Linea = 0;
                                                            Lexico.listaError.Add(errorSyntax);
                                                            return 0;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        errorSyntax.Codigo = 603;
                                                        errorSyntax.MsjError = "Se esperaba un )";
                                                        errorSyntax.Linea = 0;
                                                        Lexico.listaError.Add(errorSyntax);
                                                        return 0;
                                                    }
                                                }
                                                else
                                                {
                                                    errorSyntax.Codigo = 602;
                                                    errorSyntax.MsjError = "Se esperaba un (";
                                                    errorSyntax.Linea = 0;
                                                    Lexico.listaError.Add(errorSyntax);
                                                    return 0;
                                                }
                                            }
                                            else
                                            {
                                                errorSyntax.Codigo = 601;
                                                errorSyntax.MsjError = "Se esperaba un identificador";
                                                errorSyntax.Linea = 0;
                                                Lexico.listaError.Add(errorSyntax);
                                                return 0;
                                            }
                                        }
                                        else  // def
                                        {
                                            errorSyntax.Codigo = 600;
                                            errorSyntax.MsjError = "Se esperaba def aqui trono 1";
                                            errorSyntax.Linea = 0;
                                            Lexico.listaError.Add(errorSyntax);
                                            return 0;
                                        }
                                    }
                                    else
                                    {
                                        errorSyntax.Codigo = 635;
                                        errorSyntax.MsjError = "Se esperaba Operador de alcance";
                                        errorSyntax.Linea = 0;
                                        Lexico.listaError.Add(errorSyntax);
                                        return 0;
                                    }
                                   
                                }
                                else
                                {
                                    errorSyntax.Codigo = 609;
                                    errorSyntax.MsjError = "Se esperaba ;";
                                    errorSyntax.Linea = 0;
                                    Lexico.listaError.Add(errorSyntax);
                                    return 0;
                                }
                            }
                            else
                            {
                                errorSyntax.Codigo = 640;
                                errorSyntax.MsjError = "Se esperaba el llamado de la libreria";
                                errorSyntax.Linea = 0;
                                Lexico.listaError.Add(errorSyntax);
                                return 0;
                            }
                        }
                        else if (puntero < Tokens.Count && Tokens[puntero] == 211) // def
                        {
                            puntero++;
                            if (Funcname() == 1) //palabra
                            {
                                if (puntero < Tokens.Count && Tokens[puntero] == 113) // (
                                {
                                    puntero++;
                                    if (puntero < Tokens.Count && Tokens[puntero] == 114) // )
                                    {
                                        puntero++;
                                        if (puntero < Tokens.Count && Tokens[puntero] == 121) //:
                                        {
                                            puntero++;
                                            if (puntero < Tokens.Count && Tokens[puntero] == 222)//extends
                                            {
                                                puntero++;
                                                if (puntero < Tokens.Count && Tokens[puntero] == 100)//palabra
                                                {
                                                    puntero++;
                                                    if (puntero < Tokens.Count && Tokens[puntero] == 113)//(
                                                    {
                                                        puntero++;
                                                        if (puntero < Tokens.Count && Tokens[puntero] == 114)//)
                                                        {
                                                            puntero++;
                                                            if (puntero < Tokens.Count && Tokens[puntero] == 117)//{
                                                            {
                                                                puntero++;

                                                                if (Suite() == 118)
                                                                {
                                                                    return 1;
                                                                }

                                                                else
                                                                {
                                                                    errorSyntax.Codigo = 621;
                                                                    errorSyntax.MsjError = "Se esperaba un }";
                                                                    errorSyntax.Linea = 0;
                                                                    Lexico.listaError.Add(errorSyntax);

                                                                    return 0;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                errorSyntax.Codigo = 609;
                                                                errorSyntax.MsjError = "Se esperaba un {";
                                                                errorSyntax.Linea = 0;
                                                                Lexico.listaError.Add(errorSyntax);
                                                                return 0;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            errorSyntax.Codigo = 603;
                                                            errorSyntax.MsjError = "Se esperaba un )";
                                                            errorSyntax.Linea = 0;
                                                            Lexico.listaError.Add(errorSyntax);
                                                            return 0;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        errorSyntax.Codigo = 602;
                                                        errorSyntax.MsjError = "Se esperaba un (";
                                                        errorSyntax.Linea = 0;
                                                        Lexico.listaError.Add(errorSyntax);
                                                        return 0;
                                                    }
                                                }
                                                else
                                                {
                                                    errorSyntax.Codigo = 601;
                                                    errorSyntax.MsjError = "Se esperaba un identificador";
                                                    errorSyntax.Linea = 0;
                                                    Lexico.listaError.Add(errorSyntax);
                                                    return 0;
                                                }
                                            }

                                            if (puntero < Tokens.Count && Tokens[puntero] == 117) ///{
                                            {
                                                puntero++;


                                                if (Suite() == 118)
                                                {
                                                    return 1;
                                                }
                                                else
                                                {

                                                    errorSyntax.Codigo = 621;
                                                    errorSyntax.MsjError = "Se esperaba un }";
                                                    errorSyntax.Linea = 0;
                                                    Lexico.listaError.Add(errorSyntax);

                                                    return 0;
                                                }
                                            }
                                            else
                                            {
                                                errorSyntax.Codigo = 609;
                                                errorSyntax.MsjError = "Se esperaba un {";
                                                errorSyntax.Linea = 0;
                                                Lexico.listaError.Add(errorSyntax);
                                                return 0;
                                            }
                                        }
                                        else
                                        {
                                            errorSyntax.Codigo = 604;
                                            errorSyntax.MsjError = "Se esperaba un :";
                                            errorSyntax.Linea = 0;
                                            Lexico.listaError.Add(errorSyntax);
                                            return 0;
                                        }
                                    }
                                    else
                                    {
                                        errorSyntax.Codigo = 603;
                                        errorSyntax.MsjError = "Se esperaba un )";
                                        errorSyntax.Linea = 0;
                                        Lexico.listaError.Add(errorSyntax);
                                        return 0;
                                    }
                                }
                                else
                                {
                                    errorSyntax.Codigo = 602;
                                    errorSyntax.MsjError = "Se esperaba un (";
                                    errorSyntax.Linea = 0;
                                    Lexico.listaError.Add(errorSyntax);
                                    return 0;
                                }
                            }
                            else
                            {
                                errorSyntax.Codigo = 601;
                                errorSyntax.MsjError = "Se esperaba un identificador";
                                errorSyntax.Linea = 0;
                                Lexico.listaError.Add(errorSyntax);
                                return 0;
                            }
                        }
                        else  // def
                        {
                            errorSyntax.Codigo = 600;
                            errorSyntax.MsjError = "Se esperaba def aqui trono 2";
                            errorSyntax.Linea = 0;
                            Lexico.listaError.Add(errorSyntax);
                            return 0;
                        }
                    }
                    else
                    {
                        errorSyntax.Codigo = 609;
                        errorSyntax.MsjError = "Se esperaba ;";
                        errorSyntax.Linea = 0;
                        Lexico.listaError.Add(errorSyntax);
                        return 0;
                    }
                }
                else
                {
                    errorSyntax.Codigo = 640;
                    errorSyntax.MsjError = "Se esperaba el llamado de la libreria";
                    errorSyntax.Linea = 0;
                    Lexico.listaError.Add(errorSyntax);
                    return 0;
                }
            }
            else
            {
                if (puntero < Tokens.Count && Tokens[puntero] == 211) // def
                {
                    puntero++;
                    if (Funcname() == 1) //palabra
                    {
                        if (puntero < Tokens.Count && Tokens[puntero] == 113) // (
                        {
                            puntero++;
                            if (puntero < Tokens.Count && Tokens[puntero] == 114) // )
                            {
                                puntero++;
                                if (puntero < Tokens.Count && Tokens[puntero] == 121) //:
                                {
                                    puntero++;
                                    if (puntero < Tokens.Count && Tokens[puntero] == 222)//extends
                                    {
                                        puntero++;
                                        if (puntero < Tokens.Count && Tokens[puntero] == 100)//palabra
                                        {
                                            puntero++;
                                            if (puntero < Tokens.Count && Tokens[puntero] == 113)//(
                                            {
                                                puntero++;
                                                if (puntero < Tokens.Count && Tokens[puntero] == 114)//)
                                                {
                                                    puntero++;
                                                    if (puntero < Tokens.Count && Tokens[puntero] == 117)//{
                                                    {
                                                        puntero++;

                                                        if (Suite() == 118)//}
                                                        {
                                                            return 1;
                                                        }

                                                        else
                                                        {
                                                            errorSyntax.Codigo = 621;
                                                            errorSyntax.MsjError = "Se esperaba un }";
                                                            errorSyntax.Linea = 0;
                                                            Lexico.listaError.Add(errorSyntax);

                                                            return 0;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        errorSyntax.Codigo = 609;
                                                        errorSyntax.MsjError = "Se esperaba un {";
                                                        errorSyntax.Linea = 0;
                                                        Lexico.listaError.Add(errorSyntax);
                                                        return 0;
                                                    }
                                                }
                                                else
                                                {
                                                    errorSyntax.Codigo = 603;
                                                    errorSyntax.MsjError = "Se esperaba un )";
                                                    errorSyntax.Linea = 0;
                                                    Lexico.listaError.Add(errorSyntax);
                                                    return 0;
                                                }
                                            }
                                            else
                                            {
                                                errorSyntax.Codigo = 602;
                                                errorSyntax.MsjError = "Se esperaba un (";
                                                errorSyntax.Linea = 0;
                                                Lexico.listaError.Add(errorSyntax);
                                                return 0;
                                            }
                                        }
                                        else
                                        {
                                            errorSyntax.Codigo = 601;
                                            errorSyntax.MsjError = "Se esperaba un identificador";
                                            errorSyntax.Linea = 0;
                                            Lexico.listaError.Add(errorSyntax);
                                            return 0;
                                        }
                                    }

                                    if (puntero < Tokens.Count && Tokens[puntero] == 117) ///{
                                    {
                                        puntero++;


                                        if (Suite() == 118)
                                        {
                                            return 1;
                                        }
                                        else
                                        {

                                            errorSyntax.Codigo = 621;
                                            errorSyntax.MsjError = "Se esperaba un }";
                                            errorSyntax.Linea = 0;
                                            Lexico.listaError.Add(errorSyntax);

                                            return 0;
                                        }
                                    }
                                    else
                                    {
                                        errorSyntax.Codigo = 609;
                                        errorSyntax.MsjError = "Se esperaba un {";
                                        errorSyntax.Linea = 0;
                                        Lexico.listaError.Add(errorSyntax);
                                        return 0;
                                    }
                                }
                                else
                                {
                                    errorSyntax.Codigo = 604;
                                    errorSyntax.MsjError = "Se esperaba un :";
                                    errorSyntax.Linea = 0;
                                    Lexico.listaError.Add(errorSyntax);
                                    return 0;
                                }
                            }
                            else
                            {
                                errorSyntax.Codigo = 603;
                                errorSyntax.MsjError = "Se esperaba un )";
                                errorSyntax.Linea = 0;
                                Lexico.listaError.Add(errorSyntax);
                                return 0;
                            }
                        }
                        else
                        {
                            errorSyntax.Codigo = 602;
                            errorSyntax.MsjError = "Se esperaba un (";
                            errorSyntax.Linea = 0;
                            Lexico.listaError.Add(errorSyntax);
                            return 0;
                        }
                    }
                    else
                    {
                        errorSyntax.Codigo = 601;
                        errorSyntax.MsjError = "Se esperaba un identificador";
                        errorSyntax.Linea = 0;
                        Lexico.listaError.Add(errorSyntax);
                        return 0;
                    }
                }
                else  // def
                {
                    errorSyntax.Codigo = 600;
                    errorSyntax.MsjError = "Se esperaba def aqui trono 3";
                    errorSyntax.Linea = 0;
                    Lexico.listaError.Add(errorSyntax);
                    return 0;
                }
            }    
        }

        private int Funcname()
        {
            if (puntero < Tokens.Count && Tokens[puntero] == 100)
            {
                puntero++;
                return 1;
            }
            else
            {
                return 0;
            }

        }

        private int Suite()
        {

            do
            {
                if (puntero >= Tokens.Count)
                {
                    break;
                }
                if (Tokens[puntero] == 213)//ident
                {
                    puntero++;
                    do
                    {

                        Statement();


                        if (Tokens[puntero] == 221) //elsewhile
                        {
                            puntero++;
                            return 221;

                        }
                        else if (Tokens[puntero] == 214)//deden
                        {
                            puntero++;
                            return 214;

                        }
                        else if (Tokens[puntero] == 218)//endif
                        {
                            puntero++;
                            return 218;

                        }
                        else if (Tokens[puntero] == 220)//endfor
                        {
                            puntero++;
                            return 220;

                        }
                        else if (Tokens[puntero] == 219)//endwhile
                        {
                            puntero++;
                            return 219;
                        }
                        else
                        {
                            if (Tokens[puntero] == 118)//}
                            {
                                return 118;

                            }
                            else
                            {
                                continue;
                            }
                        }
                    } while (true);

                }
                else
                {
                    //ciclo statement con }  endif endfor endwhile
                    do
                    {

                        Statement();

                        if (puntero >= Tokens.Count)
                        {
                            break;
                        }

                        if (Tokens[puntero] == 221) //elseWhile
                        {
                            puntero++;
                            return 221;

                        }
                        else if (Tokens[puntero] == 214)//deden
                        {

                            puntero++;
                            return 214;


                        }
                        else if (Tokens[puntero] == 218)//endif
                        {
                            puntero++;
                            return 218;

                        }
                        else if (Tokens[puntero] == 220)//endfor
                        {

                            puntero++;
                            return 220;

                        }
                        else if (Tokens[puntero] == 219)//endwhile
                        {
                            puntero++;
                            return 219;

                        }
                        else if (Tokens[puntero] == 204)//else
                        {
                            puntero++;
                            return 204;
                        }
                        else
                        {
                            if (Tokens[puntero] == 118)//}
                            {
                                return 118;
                            }
                            else
                            {
                                continue;
                                //return 0;
                            }
                        }
                    } while (true);
                }
            } while (puntero < Tokens.Count);
            return 0;
        }

        private void Statement()
        {

            do
            {
                //Asignacion();

                //ya
                Break_stmt();

                //ya
                Print_stmt();

                //ya
                Input_stmt();

                //ya
                Continue_stmt();

                //ya
                If_stmt();

                //ya
                While_stmt();

                //ya
                For_stmt();

                //ya
                Matematicas();

                 Metodos();

                Clases();

                break;

            } while (puntero < Tokens.Count);

        }

        private void Clases()
        {
            do
            {
                if (puntero >= Tokens.Count)
                {
                    break;
                }



                if (Tokens[puntero] == 225 || Tokens[puntero]==226 ) //Abstract public
                {
                    puntero++;
                    if (Tokens[puntero] == 227)//void
                    {
                        puntero++;
                        if (Tokens[puntero] == 100)//Palabra
                        {
                            puntero++;
                            if (Tokens[puntero] == 113)//(
                            {
                                puntero++;
                                if (Tokens[puntero] == 100)//Palabra
                                {
                                    puntero++;

                                    if (Tokens[puntero] == 119)//Coma
                                    {
                                        puntero++;
                                        if (Tokens[puntero] == 100)//Identificador
                                        {
                                            puntero++;
                                            if (Tokens[puntero] == 119)//coma
                                            {
                                                puntero++;
                                                if (Tokens[puntero] == 100)//Palabra
                                                {
                                                    puntero++;
                                                    if (Tokens[puntero] == 119)//coma
                                                    {
                                                        puntero++;
                                                        if (Tokens[puntero] == 100)//palabra
                                                        {
                                                            puntero++;
                                                            if (Tokens[puntero] == 114)//)
                                                            {
                                                                puntero++;
                                                                if (Tokens[puntero] == 117)//{
                                                                {
                                                                    puntero++;
                                                                    if (Suite() == 118)//}
                                                                    {

                                                                        break;
                                                                    }
                                                                    else
                                                                    {
                                                                        errorSyntax.Codigo = 621;
                                                                        errorSyntax.MsjError = "Se esperaba un }";
                                                                        errorSyntax.Linea = 0;
                                                                        Lexico.listaError.Add(errorSyntax);

                                                                        break;
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    puntero++;
                                                                    errorSyntax.Codigo = 620;
                                                                    errorSyntax.MsjError = "Se esperaba un {";
                                                                    errorSyntax.Linea = 0;
                                                                    Lexico.listaError.Add(errorSyntax);
                                                                    break;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                puntero++;
                                                                errorSyntax.Codigo = 612;
                                                                errorSyntax.MsjError = "Se esperaba )";
                                                                errorSyntax.Linea = 0;
                                                                Lexico.listaError.Add(errorSyntax);
                                                                break;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            puntero++;
                                                            errorSyntax.Codigo = 600;
                                                            errorSyntax.MsjError = "Se esperaba un identificador";
                                                            errorSyntax.Linea = 0;
                                                            Lexico.listaError.Add(errorSyntax);
                                                            break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (Tokens[puntero] == 114)//)
                                                        {
                                                            puntero++;
                                                            if (Tokens[puntero] == 117)//{
                                                            {
                                                                puntero++;
                                                                if (Suite() == 118)//}
                                                                {

                                                                    break;
                                                                }
                                                                else
                                                                {
                                                                    errorSyntax.Codigo = 621;
                                                                    errorSyntax.MsjError = "Se esperaba un }";
                                                                    errorSyntax.Linea = 0;
                                                                    Lexico.listaError.Add(errorSyntax);

                                                                    break;
                                                                }
                                                            }
                                                            else
                                                            {

                                                                errorSyntax.Codigo = 620;
                                                                errorSyntax.MsjError = "Se esperaba un {";
                                                                errorSyntax.Linea = 0;
                                                                Lexico.listaError.Add(errorSyntax);
                                                                break;
                                                            }
                                                        }
                                                        else
                                                        {

                                                            errorSyntax.Codigo = 612;
                                                            errorSyntax.MsjError = "Se esperaba )";
                                                            errorSyntax.Linea = 0;
                                                            Lexico.listaError.Add(errorSyntax);
                                                            break;
                                                        }
                                                    }


                                                }
                                                else
                                                {

                                                    errorSyntax.Codigo = 600;
                                                    errorSyntax.MsjError = "Se esperaba un identificador";
                                                    errorSyntax.Linea = 0;
                                                    Lexico.listaError.Add(errorSyntax);
                                                    break;
                                                }
                                            }
                                            else
                                            {
                                                if (Tokens[puntero] == 114)//)
                                                {
                                                    puntero++;
                                                    if (Tokens[puntero] == 117)//{
                                                    {
                                                        puntero++;
                                                        if (Suite() == 118)//}
                                                        {

                                                            break;
                                                        }
                                                        else
                                                        {
                                                            errorSyntax.Codigo = 621;
                                                            errorSyntax.MsjError = "Se esperaba un }";
                                                            errorSyntax.Linea = 0;
                                                            Lexico.listaError.Add(errorSyntax);

                                                            break;
                                                        }
                                                    }
                                                    else
                                                    {

                                                        errorSyntax.Codigo = 620;
                                                        errorSyntax.MsjError = "Se esperaba un {";
                                                        errorSyntax.Linea = 0;
                                                        Lexico.listaError.Add(errorSyntax);
                                                        break;
                                                    }
                                                }
                                                else
                                                {
                                                    errorSyntax.Codigo = 612;
                                                    errorSyntax.MsjError = "Se esperaba )";
                                                    errorSyntax.Linea = 0;
                                                    Lexico.listaError.Add(errorSyntax);
                                                    break;
                                                }
                                            }

                                        }
                                        else
                                        {
                                            errorSyntax.Codigo = 600;
                                            errorSyntax.MsjError = "Se esperaba un identificador";
                                            errorSyntax.Linea = 0;
                                            Lexico.listaError.Add(errorSyntax);
                                            break;
                                        }
                                    }

                                    else if (Tokens[puntero] == 114)//)
                                    {
                                        puntero++;
                                        if (Tokens[puntero] == 117)//{
                                        {
                                            puntero++;
                                            if (Suite() == 118)//}
                                            {

                                                break;
                                            }
                                            else
                                            {
                                                errorSyntax.Codigo = 621;
                                                errorSyntax.MsjError = "Se esperaba un }";
                                                errorSyntax.Linea = 0;
                                                Lexico.listaError.Add(errorSyntax);

                                                break;
                                            }
                                        }
                                        else
                                        {

                                            errorSyntax.Codigo = 620;
                                            errorSyntax.MsjError = "Se esperaba un {";
                                            errorSyntax.Linea = 0;
                                            Lexico.listaError.Add(errorSyntax);
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        errorSyntax.Codigo = 612;
                                        errorSyntax.MsjError = "Se esperaba )";
                                        errorSyntax.Linea = 0;
                                        Lexico.listaError.Add(errorSyntax);
                                        break;
                                    }
                                }
                                else if (Tokens[puntero] == 114)//)
                                {
                                    puntero++;
                                    if (Tokens[puntero] == 117)//{
                                    {
                                        puntero++;
                                        if (Suite() == 118)//}
                                        {
                                            puntero++;
                                            if (puntero >= Tokens.Count)
                                            {
                                                break;
                                            }
                                            break;
                                        }
                                        else
                                        {
                                            errorSyntax.Codigo = 621;
                                            errorSyntax.MsjError = "Se esperaba un }";
                                            errorSyntax.Linea = 0;
                                            Lexico.listaError.Add(errorSyntax);

                                            break;
                                        }
                                    }
                                    else
                                    {

                                        errorSyntax.Codigo = 620;
                                        errorSyntax.MsjError = "Se esperaba un {";
                                        errorSyntax.Linea = 0;
                                        Lexico.listaError.Add(errorSyntax);
                                        break;
                                    }
                                }
                                else
                                {
                                    errorSyntax.Codigo = 612;
                                    errorSyntax.MsjError = "Se esperaba )";
                                    errorSyntax.Linea = 0;
                                    Lexico.listaError.Add(errorSyntax);
                                    break;
                                }
                            }
                            else
                            {
                                errorSyntax.Codigo = 611;
                                errorSyntax.MsjError = "Se esperaba (";
                                errorSyntax.Linea = 0;
                                Lexico.listaError.Add(errorSyntax);
                                break;
                            }
                        }
                        else
                        {
                            errorSyntax.Codigo = 600;
                            errorSyntax.MsjError = "Se esperaba un identificador";
                            errorSyntax.Linea = 0;
                            Lexico.listaError.Add(errorSyntax);
                            break;
                        }
                       
                    }
                    else if (Tokens[puntero] == 100)//Palabra
                    {
                        puntero++;
                        if(Tokens[puntero] == 113)//(
                        {
                            puntero++;
                            if (Tokens[puntero] == 100)//Palabra
                            {
                                puntero++;

                                if (Tokens[puntero] == 119)//Coma
                                {
                                    puntero++;
                                    if (Tokens[puntero] == 100)//Identificador
                                    {
                                        puntero++;
                                        if (Tokens[puntero] == 119)//coma
                                        {
                                            puntero++;
                                            if (Tokens[puntero] == 100)//Palabra
                                            {
                                                puntero++;
                                                if (Tokens[puntero] == 119)//coma
                                                {
                                                    puntero++;
                                                    if (Tokens[puntero] == 100)//palabra
                                                    {
                                                        puntero++;
                                                        if (Tokens[puntero] == 114)//)
                                                        {
                                                            puntero++;
                                                            if (Tokens[puntero] == 117)//{
                                                            {
                                                                puntero++;
                                                                if (Suite() == 118)//}
                                                                {

                                                                    break;
                                                                }
                                                                else
                                                                {
                                                                    errorSyntax.Codigo = 621;
                                                                    errorSyntax.MsjError = "Se esperaba un }";
                                                                    errorSyntax.Linea = 0;
                                                                    Lexico.listaError.Add(errorSyntax);

                                                                    break;
                                                                }
                                                            }
                                                            else
                                                            {

                                                                errorSyntax.Codigo = 620;
                                                                errorSyntax.MsjError = "Se esperaba un {";
                                                                errorSyntax.Linea = 0;
                                                                Lexico.listaError.Add(errorSyntax);
                                                                break;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            errorSyntax.Codigo = 612;
                                                            errorSyntax.MsjError = "Se esperaba )";
                                                            errorSyntax.Linea = 0;
                                                            Lexico.listaError.Add(errorSyntax);
                                                            break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                       

                                                        errorSyntax.Codigo = 600;
                                                        errorSyntax.MsjError = "Se esperaba identificador";
                                                        errorSyntax.Linea = 0;
                                                        Lexico.listaError.Add(errorSyntax);
                                                        break;
                                                    }
                                                }
                                                else
                                                {
                                                    if (Tokens[puntero] == 114)//)
                                                    {
                                                        puntero++;
                                                        if (Tokens[puntero] == 117)//{
                                                        {
                                                            puntero++;
                                                            if (Suite() == 118)//}
                                                            {

                                                                break;
                                                            }
                                                            else
                                                            {
                                                                errorSyntax.Codigo = 621;
                                                                errorSyntax.MsjError = "Se esperaba un }";
                                                                errorSyntax.Linea = 0;
                                                                Lexico.listaError.Add(errorSyntax);

                                                                break;
                                                            }
                                                        }
                                                        else
                                                        {

                                                            errorSyntax.Codigo = 620;
                                                            errorSyntax.MsjError = "Se esperaba un {";
                                                            errorSyntax.Linea = 0;
                                                            Lexico.listaError.Add(errorSyntax);
                                                            break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        errorSyntax.Codigo = 612;
                                                        errorSyntax.MsjError = "Se esperaba )";
                                                        errorSyntax.Linea = 0;
                                                        Lexico.listaError.Add(errorSyntax);
                                                        break;
                                                    }
                                                }

                                            }
                                            else
                                            {
                                                errorSyntax.Codigo = 600;
                                                errorSyntax.MsjError = "Se esperaba un identificador";
                                                errorSyntax.Linea = 0;
                                                Lexico.listaError.Add(errorSyntax);
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            if (Tokens[puntero] == 114)//)
                                            {
                                                puntero++;
                                                if (Tokens[puntero] == 117)//{
                                                {
                                                    puntero++;
                                                    if (Suite() == 118)//}
                                                    {

                                                        break;
                                                    }
                                                    else
                                                    {
                                                        errorSyntax.Codigo = 621;
                                                        errorSyntax.MsjError = "Se esperaba un }";
                                                        errorSyntax.Linea = 0;
                                                        Lexico.listaError.Add(errorSyntax);

                                                        break;
                                                    }
                                                }
                                                else
                                                {

                                                    errorSyntax.Codigo = 620;
                                                    errorSyntax.MsjError = "Se esperaba un {";
                                                    errorSyntax.Linea = 0;
                                                    Lexico.listaError.Add(errorSyntax);
                                                    break;
                                                }
                                            }
                                            else
                                            {
                                                errorSyntax.Codigo = 612;
                                                errorSyntax.MsjError = "Se esperaba )";
                                                errorSyntax.Linea = 0;
                                                Lexico.listaError.Add(errorSyntax);
                                                break;
                                            }
                                        }

                                    }
                                    else
                                    {
                                        errorSyntax.Codigo = 600;
                                        errorSyntax.MsjError = "Se esperaba un identificador";
                                        errorSyntax.Linea = 0;
                                        Lexico.listaError.Add(errorSyntax);
                                        break;
                                    }
                                }

                                else if (Tokens[puntero] == 114)//)
                                {
                                    puntero++;
                                    if (Tokens[puntero] == 117)//{
                                    {
                                        puntero++;
                                        if (Suite() == 118)//}
                                        {

                                            break;
                                        }
                                        else
                                        {
                                            errorSyntax.Codigo = 621;
                                            errorSyntax.MsjError = "Se esperaba un }";
                                            errorSyntax.Linea = 0;
                                            Lexico.listaError.Add(errorSyntax);

                                            break;
                                        }
                                    }
                                    else
                                    {

                                        errorSyntax.Codigo = 620;
                                        errorSyntax.MsjError = "Se esperaba un {";
                                        errorSyntax.Linea = 0;
                                        Lexico.listaError.Add(errorSyntax);
                                        break;
                                    }
                                }
                                else
                                {
                                    errorSyntax.Codigo = 612;
                                    errorSyntax.MsjError = "Se esperaba )";
                                    errorSyntax.Linea = 0;
                                    Lexico.listaError.Add(errorSyntax);
                                    break;
                                }
                            }
                            else if (Tokens[puntero] == 114)//)
                            {
                                puntero++;
                                if (Tokens[puntero] == 117)//{
                                {
                                    puntero++;
                                    if (Suite() == 118)//}
                                    {
                                        puntero++;
                                        if (puntero >= Tokens.Count)
                                        {
                                            break;
                                        }
                                        break;
                                    }
                                    else
                                    {
                                        errorSyntax.Codigo = 621;
                                        errorSyntax.MsjError = "Se esperaba un }";
                                        errorSyntax.Linea = 0;
                                        Lexico.listaError.Add(errorSyntax);

                                        break;
                                    }
                                }
                                else
                                {

                                    errorSyntax.Codigo = 620;
                                    errorSyntax.MsjError = "Se esperaba un {";
                                    errorSyntax.Linea = 0;
                                    Lexico.listaError.Add(errorSyntax);
                                    break;
                                }
                            }
                            else
                            {
                                errorSyntax.Codigo = 612;
                                errorSyntax.MsjError = "Se esperaba )";
                                errorSyntax.Linea = 0;
                                Lexico.listaError.Add(errorSyntax);
                                break;
                            }
                        }
                        else
                        {
                            errorSyntax.Codigo = 611;
                            errorSyntax.MsjError = "Se esperaba (";
                            errorSyntax.Linea = 0;
                            Lexico.listaError.Add(errorSyntax);
                            break;
                        }
                    }
                    else
                    {
                        errorSyntax.Codigo = 601;
                        errorSyntax.MsjError = "Se esperaba un identificador";
                        errorSyntax.Linea = 0;
                        Lexico.listaError.Add(errorSyntax);
                        break;
                    }

                }
                else
                {
                    break;                
                }

            } while (puntero < Tokens.Count); 
        }

        private void Metodos()
        {
            do
            {
                if (puntero >= Tokens.Count)
                {
                    break;
                }

                if (Tokens[puntero] == 224)//private
                {
                    puntero++;
                    if (Tokens[puntero] == 100) //id
                    {
                        puntero++;
                        if(Tokens[puntero] == 120)//punto y coma
                        {
                            puntero++;
                            break;
                        }
                        else
                        {
                            errorSyntax.Codigo = 609;
                            errorSyntax.MsjError = "Se esperaba ;";
                            errorSyntax.Linea = 0;
                            Lexico.listaError.Add(errorSyntax);
                            break;
                        }
                    }
                    else
                    {
                        errorSyntax.Codigo = 601;
                        errorSyntax.MsjError = "Se esperaba un identificador";
                        errorSyntax.Linea = 0;
                        Lexico.listaError.Add(errorSyntax);
                        break;
                    }
            }
                else
                {
                    break;
                }
            } while (true);
            
        }

        private void Abstract()
        {
            do
            {
                if (puntero >= Tokens.Count)
                {
                    break;
                }

                if (Tokens[puntero] == 226)//Abstract
                {
                    puntero++;
                    if(Tokens[puntero] == 100)//id
                    {
                        if (puntero >= Tokens.Count)
                        {
                            break;
                        }

                    }
                }

                } while (true);
        }

   
        //redy
        private void Input_stmt()
        {
            do
            {

                if (puntero >= Tokens.Count)
                {
                    break;
                }
                if (Tokens[puntero] == 210)//input
                {
                    puntero++;

                    if (puntero >= Tokens.Count)
                    {
                        break;
                    }
                    if (Tokens[puntero] == 113)//(
                    {
                        puntero++;

                        if (puntero >= Tokens.Count)
                        {
                            break;
                        }
                        if (Tokens[puntero] == 114)//)
                        {
                            puntero++;
                            break;
                        }
                        else
                        {
                            errorSyntax.Codigo = 612;
                            errorSyntax.MsjError = "Se esperaba )";
                            errorSyntax.Linea = 0;
                            Lexico.listaError.Add(errorSyntax);
                            break;
                        }
                    }
                    else
                    {
                        errorSyntax.Codigo = 613;
                        errorSyntax.MsjError = "Se esperaba (";
                        errorSyntax.Linea = 0;
                        Lexico.listaError.Add(errorSyntax);
                        break;
                    }
                }
                break;

            } while (puntero < Tokens.Count);
        }


        //redy
        private void Matematicas()
        {
            do
            {
                if (puntero > Tokens[puntero])
                {
                    break;
                }

                

                if (Tokens[puntero] == 100 || Tokens[puntero] == 102 || Tokens[puntero] == 101)//id, numero, decimal
                {
 
                    puntero++;

                    if (puntero < Tokens.Count && Tokens[puntero] == 113)//(
                    {
                        puntero++;

                        if (puntero >= Tokens.Count)
                        {
                            break;
                        }
                        if (Tokens[puntero] == 100 || Tokens[puntero] == 102 || Tokens[puntero] == 101)//id, numero, decimal
                        {
                            puntero++;
                            if (puntero < Tokens.Count && Tokens[puntero] == 119)// Coma
                            {
                                puntero++;
                                if (Tokens[puntero] == 100 || Tokens[puntero] == 102 || Tokens[puntero] == 101)//id, numero, decimal
                                {
                                    puntero++;
                                    if(puntero < Tokens.Count && Tokens[puntero] == 119)//Com
                                    {
                                        puntero++;
                                        if (Tokens[puntero] == 100 || Tokens[puntero] == 102 || Tokens[puntero] == 101)//id, numero, decimal
                                        {
                                            puntero++;
                                            if (puntero < Tokens.Count && Tokens[puntero] == 119)//Coma
                                            {
                                                puntero++;
                                                if (Tokens[puntero] == 100 || Tokens[puntero] == 102 || Tokens[puntero] == 101)//id, numero, decimal
                                                {
                                                    puntero++;
                                                    if (puntero < Tokens.Count && Tokens[puntero] == 114)//)
                                                    {
                                                        puntero++;

                                                        if (puntero >= Tokens.Count)
                                                        {
                                                            break;
                                                        }
                                                        if(Tokens[puntero] == 120)
                                                        {
                                                            puntero++;
                                                            break;
                                                        }
                                                        else if (Tokens[puntero] == 117)//{
                                                        {
                                                            puntero++;

                                                            if (Suite() == 118)//}
                                                            {
                                                                puntero++;
                                                                break;
                                                            }

                                                            else
                                                            {
                                                                errorSyntax.Codigo = 621;
                                                                errorSyntax.MsjError = "Se esperaba un }";
                                                                errorSyntax.Linea = 0;
                                                                Lexico.listaError.Add(errorSyntax);

                                                                break;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            errorSyntax.Codigo = 620;
                                                            errorSyntax.MsjError = "Se esperaba {";
                                                            errorSyntax.Linea = 0;
                                                            Lexico.listaError.Add(errorSyntax);
                                                            break;
                                                        }

                                                    }
                                                    else
                                                    {
                                                        errorSyntax.Codigo = 612;
                                                        errorSyntax.MsjError = "Se esperaba )";
                                                        errorSyntax.Linea = 0;
                                                        Lexico.listaError.Add(errorSyntax);
                                                        break;
                                                    }
                                                }
                                                else
                                                {
                                                    errorSyntax.Codigo = 600;
                                                    errorSyntax.MsjError = "Se esperaba un id";
                                                    errorSyntax.Linea = 0;
                                                    Lexico.listaError.Add(errorSyntax);
                                                    break;
                                                }
                                            }
                                            else
                                            {
                                                if (puntero < Tokens.Count && Tokens[puntero] == 114)//)
                                                {
                                                    puntero++;

                                                    if (puntero >= Tokens.Count)
                                                    {
                                                        break;
                                                    }
                                                    if (Tokens[puntero] == 120)
                                                    {
                                                        puntero++;
                                                        break;
                                                    }
                                                    else if (Tokens[puntero] == 117)//{
                                                    {
                                                        puntero++;

                                                        if (Suite() == 118)//}
                                                        {
                                                            puntero++;
                                                            break;
                                                        }

                                                        else
                                                        {
                                                            errorSyntax.Codigo = 621;
                                                            errorSyntax.MsjError = "Se esperaba un }";
                                                            errorSyntax.Linea = 0;
                                                            Lexico.listaError.Add(errorSyntax);

                                                            break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        errorSyntax.Codigo = 620;
                                                        errorSyntax.MsjError = "Se esperaba {";
                                                        errorSyntax.Linea = 0;
                                                        Lexico.listaError.Add(errorSyntax);
                                                        break;
                                                    }

                                                }
                                                else
                                                {
                                                    errorSyntax.Codigo = 612;
                                                    errorSyntax.MsjError = "Se esperaba )";
                                                    errorSyntax.Linea = 0;
                                                    Lexico.listaError.Add(errorSyntax);
                                                    break;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            errorSyntax.Codigo = 600;
                                            errorSyntax.MsjError = "Se esperaba un id";
                                            errorSyntax.Linea = 0;
                                            Lexico.listaError.Add(errorSyntax);
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (puntero < Tokens.Count && Tokens[puntero] == 114)//)
                                        {
                                            puntero++;

                                            if (puntero >= Tokens.Count)
                                            {
                                                break;
                                            }
                                            if (Tokens[puntero] == 120)
                                            {
                                                puntero++;
                                                break;
                                            }
                                            else if (Tokens[puntero] == 117)//{
                                            {
                                                puntero++;

                                                if (Suite() == 118)//}
                                                {
                                                    puntero++;
                                                    break;
                                                }

                                                else
                                                {
                                                    errorSyntax.Codigo = 621;
                                                    errorSyntax.MsjError = "Se esperaba un }";
                                                    errorSyntax.Linea = 0;
                                                    Lexico.listaError.Add(errorSyntax);

                                                    break;
                                                }
                                            }
                                            else
                                            {
                                                errorSyntax.Codigo = 620;
                                                errorSyntax.MsjError = "Se esperaba {";
                                                errorSyntax.Linea = 0;
                                                Lexico.listaError.Add(errorSyntax);
                                                break;
                                            }

                                        }
                                        else
                                        {
                                            errorSyntax.Codigo = 612;
                                            errorSyntax.MsjError = "Se esperaba )";
                                            errorSyntax.Linea = 0;
                                            Lexico.listaError.Add(errorSyntax);
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    errorSyntax.Codigo = 600;
                                    errorSyntax.MsjError = "Se esperaba un id";
                                    errorSyntax.Linea = 0;
                                    Lexico.listaError.Add(errorSyntax);

                                    break;
                                }
                            }
                        }
                        else
                        {
                            if (puntero < Tokens.Count && Tokens[puntero] == 114)//)
                            {
                                puntero++;

                                if (puntero >= Tokens.Count)
                                {
                                    break;
                                }
                                if (Tokens[puntero] == 120)
                                {
                                    puntero++;
                                    break;
                                }
                                else if (Tokens[puntero] == 117)//{
                                {
                                    puntero++;

                                    if (Suite() == 118)//}
                                    { 
                                        puntero++;
                                        break;
                                    }

                                    else
                                    {
                                        errorSyntax.Codigo = 621;
                                        errorSyntax.MsjError = "Se esperaba un }";
                                        errorSyntax.Linea = 0;
                                        Lexico.listaError.Add(errorSyntax);

                                        break;
                                    }
                                }
                                else
                                {
                                    errorSyntax.Codigo = 620;
                                    errorSyntax.MsjError = "Se esperaba {";
                                    errorSyntax.Linea = 0;
                                    Lexico.listaError.Add(errorSyntax);
                                    break;
                                }

                            }
                            else
                            {
                                errorSyntax.Codigo = 612;
                                errorSyntax.MsjError = "Se esperaba )";
                                errorSyntax.Linea = 0;
                                Lexico.listaError.Add(errorSyntax);
                                break;
                            }
                        }
                        
                    }
                    do
                    {
                        if (puntero >= Tokens.Count)
                        {
                            break;
                        }

                        if (Tokens[puntero] == 119)//coma
                        {
                            puntero++;
                            Matematicas();
                        }
                        break;
                    } while (true);
                   
                    
                    do
                    {
                        if (puntero >= Tokens.Count)
                        {
                            break;
                        }
                        if (Tokens[puntero] == 103 || Tokens[puntero] == 104 || Tokens[puntero] == 105 || Tokens[puntero] == 106 || Tokens[puntero] == 130)// + - * / % = 123
                        {
                            puntero++;
                            Matematicas();
                        }
                        break;
                    } while (true);

                    if (puntero >= Tokens.Count)
                    {
                        break;
                    }
                    if (Tokens[puntero] == 123)//=
                    {
                        puntero++;

                        if (puntero >= Tokens.Count)
                        {
                            break;
                        }
                        do
                        {
                            if (Tokens[puntero] == 100 || Tokens[puntero] == 102 || Tokens[puntero] == 101)//id, numero, decimal
                            {
                                puntero++;

                                if (Tokens[puntero] == 103 || Tokens[puntero] == 104 || Tokens[puntero] == 105 || Tokens[puntero] == 106)//+ - * /{
                                {
                                    puntero++;
                                    if (Tokens[puntero] == 100 || Tokens[puntero] == 102 || Tokens[puntero] == 101)//id, numero, decimal
                                    {
                                        puntero++;
                                    }
                                    else
                                    {
                                        errorSyntax.Codigo = 621;
                                        errorSyntax.MsjError = "Se esperaba id o un numero";
                                        errorSyntax.Linea = 0;
                                        Lexico.listaError.Add(errorSyntax);
                                        break;
                                    }
                                }
                                break;
                            }
                            else
                            {
                                errorSyntax.Codigo = 621;
                                errorSyntax.MsjError = "Se esperaba id o un numero";
                                errorSyntax.Linea = 0;
                                Lexico.listaError.Add(errorSyntax);
                                break;
                            }
                        } while (true);
                       
                    }
                    if (Tokens[puntero] == 120)//;
                    {
                        puntero++;
                        break;
                    }
                    else
                    {
                        /*
                        errorSyntax.Codigo = 622;
                        errorSyntax.MsjError = "Se esperaba = o ;";
                        errorSyntax.Linea = 0;
                        Lexico.listaError.Add(errorSyntax);
                        break;
                        */
                    }
                }
                break;
            } while (puntero < Tokens.Count);

        }

        //redy
        private int Expression_list() // TARGET LIST
        {
            do
            {
                if (puntero > Tokens.Count)
                {
                    break;
                }


                if (Tokens[puntero] == 100 || Tokens[puntero] == 101 || Tokens[puntero] == 102 || Tokens[puntero] == 122) // Id, numero, decimal, cadena
                {
                    puntero++;
                    do
                    {
                        if (puntero > Tokens.Count)
                        {
                            break;
                        }
                        if (Tokens[puntero] == 119) //coma
                        {
                            puntero++;
                        }
                        else
                        {
                            break;
                        }
                        break;

                    } while (true);
                    //return 1;
                    break;
                }
                else
                {
                    return 0;
                }
            } while (puntero < Tokens.Count);
            return 1;
        }

        //ready
        private void Print_stmt()
        {
            do
            {
                if (puntero > Tokens.Count)
                {
                    break;
                }

                if (Tokens[puntero] == 209) //print
                {
                    puntero++;

                    do
                    {

                        if (Tokens[puntero] == 100 || Tokens[puntero] == 101 || Tokens[puntero] == 102 || Tokens[puntero] == 122)// id, numero, decimal, cadena
                        {
                            puntero++;

                            if (puntero > Tokens.Count)
                            {
                                break;
                            }

                            if (Tokens[puntero] == 119) //coma
                            {
                                puntero++;
                            }
                            else
                            {
                                if (Tokens[puntero] == 120)//puntoycoma
                                {
                                    puntero++;
                                    break;
                                }
                                else
                                {
                                    errorSyntax.Codigo = 609;
                                    errorSyntax.MsjError = "Se esperaba ;";
                                    errorSyntax.Linea = 0;
                                    Lexico.listaError.Add(errorSyntax);
                                    break;
                                }
                            }
                        }
                        else
                        {
                            errorSyntax.Codigo = 608;
                            errorSyntax.MsjError = "Se esperaban valores para imprimir";
                            errorSyntax.Linea = 0;
                            Lexico.listaError.Add(errorSyntax);
                            break;
                        }
                    } while (true);
                }
                break;
            } while (puntero < Tokens.Count);
        }

        //redy
        private int Operador()
        {
            do
            {
                if (Asignacion_operador() == 1)
                {

                    do
                    {
                        if (puntero >= Tokens.Count)
                        {
                            break;
                        }
                        if (Tokens[puntero] == 126 || Tokens[puntero] == 201 || Tokens[puntero] == 129 || Tokens[puntero] == 200) //or | and &
                        {
                            puntero++;
                            Asignacion_operador();
                        }
                        else
                        {
                            return 1;
                        }
                    } while (true);
                }
                else
                {
                    break;
                }
                break;
            } while (puntero < Tokens.Count);

            return 0;
        }

        //redy
        private void If_stmt()
        {
            do
            {
                if (Tokens[puntero] == 203)//if
                {
                    puntero++;

                    if (Operador() == 1)
                    {

                        if (puntero >= Tokens.Count)
                        {
                            break;
                        }
                        if (Tokens[puntero] == 121)//:
                        {
                            puntero++;

                            if (Suite() == 218)//endif
                            {
                                break;

                            }
                            else
                            {
                                int valor_suite = 0;

                                valor_suite = Suite();

                                if (valor_suite == 218)//endif
                                {
                                    break;
                                }
                                else if (valor_suite == 204)//else
                                {

                                    if (Suite() == 218)//endif
                                    {

                                        break;
                                    }
                                    else
                                    {
                                        errorSyntax.Codigo = 630;
                                        errorSyntax.MsjError = "Se esperaba endif";
                                        errorSyntax.Linea = 0;
                                        Lexico.listaError.Add(errorSyntax);
                                        break;
                                    }
                                }
                                else
                                {
                                    errorSyntax.Codigo = 630;
                                    errorSyntax.MsjError = "Se esperaba endif";
                                    errorSyntax.Linea = 0;
                                    Lexico.listaError.Add(errorSyntax);
                                    break;
                                }
                            }
                        }
                        else
                        {
                            errorSyntax.Codigo = 604;
                            errorSyntax.MsjError = "Se esperaba :";
                            errorSyntax.Linea = 0;
                            Lexico.listaError.Add(errorSyntax);
                            break;
                        }
                    }
                    else
                    {
                        errorSyntax.Codigo = 630;
                        errorSyntax.MsjError = "Se esperaba operador";
                        errorSyntax.Linea = 0;
                        Lexico.listaError.Add(errorSyntax);
                        break;
                    }
                }
                break;
            } while (puntero < Tokens[puntero]);
        }

        //redy
        private int Asignacion_operador()
        {
            do
            {
                if (puntero >= Tokens.Count)
                {
                    break;
                }
                if (Tokens[puntero] == 100) //id   
                {
                    puntero++;

                    if (puntero >= Tokens.Count)
                    {
                        break;
                    }
                    if (Tokens[puntero] == 108
                        || Tokens[puntero] == 107
                        || Tokens[puntero] == 110
                        || Tokens[puntero] == 109
                        || Tokens[puntero] == 128
                        || Tokens[puntero] == 112) //< > >= <= <> != =    
                    {
                        puntero++;

                        if (puntero >= Tokens.Count)
                        {
                            break;
                        }
                        if (Tokens[puntero] == 101 || Tokens[puntero] == 102 || Tokens[puntero] == 100)//numero decimal e id
                        {
                            puntero++;
                            return 1;
                        }
                        else
                        {
                            errorSyntax.Codigo = 615;
                            errorSyntax.MsjError = "Se esperaba un numero";
                            errorSyntax.Linea = 0;
                            Lexico.listaError.Add(errorSyntax);
                            break;
                        }

                    }
                    else if (Tokens[puntero] == 123)//=
                    {
                        puntero++;

                        if (puntero >= Tokens.Count)
                        {
                            break;
                        }

                        if(Tokens[puntero] == 123)//=
                        {
                            if (Tokens[puntero] == 101 || Tokens[puntero] == 102 || Tokens[puntero] == 122 || Tokens[puntero] == 100 || Tokens[puntero] == 228)// numero, decimal, string, id
                            {
                                puntero++;
                                return 1;
                            }
                            else
                            {
                                errorSyntax.Codigo = 614;
                                errorSyntax.MsjError = "Se esperaba un numero o una cadena";
                                errorSyntax.Linea = 0;
                                Lexico.listaError.Add(errorSyntax);
                                break;
                            }
                        }


                        if (Tokens[puntero] == 101 || Tokens[puntero] == 102 || Tokens[puntero] == 122 || Tokens[puntero] == 100 || Tokens[puntero] == 228)// numero, decimal, string, id
                        {
                            puntero++;                         
                            return 1;
                        }
                        else
                        {
                            errorSyntax.Codigo = 614;
                            errorSyntax.MsjError = "Se esperaba un numero o una cadena";
                            errorSyntax.Linea = 0;
                            Lexico.listaError.Add(errorSyntax);
                            break;
                        }
                    }
                }
                break;
            } while (puntero < Tokens.Count);
            return 0;
        }

        //redy
        private void For_stmt()
        {
            do
            {
                if (puntero > Tokens.Count)
                {
                    break;
                }
                if (Tokens[puntero] == 206)//for
                {
                    puntero++;

                    if (puntero >= Tokens.Count)
                    {
                        break;
                    }
                    if (Tokens[puntero] == 115)//[
                    {
                        puntero++;

                        if (Expression_list() == 1)
                        {

                            if (puntero >= Tokens.Count)
                            {
                                break;
                            }
                            if (Tokens[puntero] == 116)//]
                            {
                                puntero++;

                                if (puntero >= Tokens.Count)
                                {
                                    break;
                                }
                                if (Tokens[puntero] == 207)//in
                                {
                                    puntero++;

                                    if (Expression_list() == 1)
                                    {

                                        if (puntero >= Tokens.Count)
                                        {
                                            break;
                                        }
                                        if (Tokens[puntero] == 121)//:
                                        {
                                            puntero++;

                                            if (Suite() == 220)//endfor
                                            {
                                                break;
                                            }
                                            else
                                            {
                                                errorSyntax.Codigo = 627;
                                                errorSyntax.MsjError = "Se esperaba endfor";
                                                errorSyntax.Linea = 0;
                                                Lexico.listaError.Add(errorSyntax);
                                                break;

                                            }
                                        }
                                        else
                                        {
                                            errorSyntax.Codigo = 604;
                                            errorSyntax.MsjError = "Se esperaba puntos dobes :";
                                            errorSyntax.Linea = 0;
                                            Lexico.listaError.Add(errorSyntax);
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        errorSyntax.Codigo = 623;
                                        errorSyntax.MsjError = "Se esperaba expresiones";
                                        errorSyntax.Linea = 0;
                                        Lexico.listaError.Add(errorSyntax);
                                        break;
                                    }
                                }
                                else
                                {

                                    errorSyntax.Codigo = 619;
                                    errorSyntax.MsjError = "Se esperaba in";
                                    errorSyntax.Linea = 0;
                                    Lexico.listaError.Add(errorSyntax);
                                    break;
                                }

                            }
                            else
                            {
                                errorSyntax.Codigo = 618;
                                errorSyntax.MsjError = "Se esperaba ]";
                                errorSyntax.Linea = 0;
                                Lexico.listaError.Add(errorSyntax);
                                break;
                            }
                        }
                        else
                        {
                            errorSyntax.Codigo = 623;
                            errorSyntax.MsjError = "Se esperaba expresiones";
                            errorSyntax.Linea = 0;
                            Lexico.listaError.Add(errorSyntax);
                            break;

                        }
                    }
                    else
                    {
                        errorSyntax.Codigo = 617;
                        errorSyntax.MsjError = "Se esperaba [";
                        errorSyntax.Linea = 0;
                        Lexico.listaError.Add(errorSyntax);
                        break;
                    }
                }
                break;
            } while (puntero < Tokens.Count);
        }

        //redy
        private void While_stmt()
        {
            do
            {
                if (puntero >= Tokens.Count)
                {
                    break;
                }
                if (Tokens[puntero] == 208)//while
                {
                    puntero++;

                    if (Operador() == 1)
                    {

                        if (puntero >= Tokens.Count)
                        {
                            break;
                        }
                        if (Tokens[puntero] == 121)
                        {
                            puntero++;

                            if (Suite() == 221)//elsewhile
                            {

                                if (puntero >= Tokens.Count)
                                {
                                    break;
                                }
                                if (Tokens[puntero] == 121)//:
                                {
                                    puntero++;

                                    if (Suite() == 219)//endwhile
                                    {

                                        break;
                                    }
                                    else
                                    {
                                        errorSyntax.Codigo = 627;
                                        errorSyntax.MsjError = "Se esperaba endwhile";
                                        errorSyntax.Linea = 0;
                                        Lexico.listaError.Add(errorSyntax);
                                        break;
                                    }
                                }
                                else
                                {
                                    errorSyntax.Codigo = 604;
                                    errorSyntax.MsjError = "Se esperaba puntos dobes :";
                                    errorSyntax.Linea = 0;
                                    Lexico.listaError.Add(errorSyntax);
                                    break;
                                }
                            }
                            else
                            {
                                errorSyntax.Codigo = 626;
                                errorSyntax.MsjError = "Se esperaba elsewhile";
                                errorSyntax.Linea = 0;
                                Lexico.listaError.Add(errorSyntax);
                                break;
                            }
                        }
                        else
                        {
                            errorSyntax.Codigo = 604;
                            errorSyntax.MsjError = "Se esperaba puntos dobes :";
                            errorSyntax.Linea = 0;
                            Lexico.listaError.Add(errorSyntax);
                            break;
                        }
                    }
                    else
                    {

                        errorSyntax.Codigo = 630;
                        errorSyntax.MsjError = "Se esperaba operador";
                        errorSyntax.Linea = 0;
                        Lexico.listaError.Add(errorSyntax);
                        break;
                    }
                }
                break;
            } while (puntero < Tokens.Count);
        }

        //redy
        private void Break_stmt()
        {
            do
            {
                if (puntero >= Tokens.Count)
                {
                    break;
                }
                if (Tokens[puntero] == 215)//break;
                {
                    puntero++;
                    break;
                }

                break;
            } while (puntero < Tokens.Count);
        }

        //redy
        private void Continue_stmt()
        {
            do
            {
                if (puntero >= Tokens.Count)
                {
                    break;
                }
                if (Tokens[puntero] == 216)
                {
                    puntero++;
                    break;
                }
                break;

            } while (puntero < Tokens.Count);
        }
    }
}

