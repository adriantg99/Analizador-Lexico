using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analizador_Lexico
{

  
    public class Error
    {
        //creacion de los atributos y variables que tendran los elementos de la lista de error
        private int codigo;
        private string msjError;
        private TipoError tipo;
        private int linea;
        #region .
        public int Codigo
        {
            get
            {
                return codigo;
            }
            set
            {
                codigo = value;
            }


        }

        public string MsjError
        {
            get
            {
                return msjError;
            }
            set
            {
                msjError = value;
            }
        }

        public TipoError Tipo
        {
            get
            {
                return tipo;
            }
            set
            {
                tipo = value;
            }
        }

        public int Linea
        {
            get
            {
                return linea;
            }
            set
            {
                linea = value;
            }
        }

        #endregion
    }
    public class Token
    {
        //creacion de los atributos y variables que tendran los elementos de la lista de token
        private int valorToken;
        private string lexema;
        private int linea;
        private TipoToken tipoToken;
        #region .

        public int ValorToken
        {
            get
            {
                return valorToken;
            }
            set
            {
                valorToken = value;
            }

        }

        public string Lexema
        {
            get
            {
                return lexema;
            }
            set
            {
                lexema = value;
            }

        }

        public int Linea
        {
            get
            {
                return linea;
            }
            set
            {
                linea = value;
            }

        }

        public TipoToken TipoToken
        {
            get
            {
                return tipoToken;
            }
            set
            {
                tipoToken = value;
            }
        }

        #endregion
    }

    //crea un listado de los tipos de token que pueden haber, esto para enviarlo al momento
    //de añadir un elemento a la lista y asignar el tipo de token que tiene el elemento.
    public enum TipoToken
    {
        Identificador,
        NumeroEntero,
        NumeroDecimal,
        SimboloSimple,
        OperadorAritmetico,
        OperadorRelacional,
        SimboloAgrupacion,
        SimboloEspecial,
        OperadorAsignacion,
        PalabraReservada,
        CadenaVacia,
        SimboloPuntual,
        Cadena,
        Opcion
    }

    //crea un listado de los tipos de error que puede haber, esto para enviarlo al momento de 
    //añadir un elemento a la lista y asignar el tipo de error que seria en caso de haber un error.
    public enum TipoError
    {
        Lexico,
        Sintactico,
        Semantico,
        Ejecucion
    }

    //Se inicializa la clase Lexico
    public class Lexico
    {
        Sintactico sin = new Sintactico();

        //Se crean las variables a utilizar en la clase lexico. Seran variables mas especificas nomas utilizadas por
        //la clase lexico

        //creacion de la lista de tokens
        public List<Token> listaToken;

        //creacion de la variable que guardara el codigo fuente
        public static string codigoFuente;

        //creacion de la lista de errores
        public static List<Error> listaError;

        //creacion de la variable en la que se guardara la linea actual en la que esta el puntero de lectura
        private int linea;

        //creacion de la matriz de transicion
        private int[,] matrizTransicion =
        {
            //      |     0            1             2            3            4            5            6            7            8            9           10            11           12           13           14          15            16           17           18           19           20           21           22           23           24          25           26            27            28           29           30          31          32           33     |
            //      |     L      |     D      |      _     |      .     |      +     |      -     |      *     |      /     |      <     |      >     |     =      |      !     |      (     |      )     |      [     |      ]     |      {     |      }     |      ,     |      ;     |      :     |      "     |      #     |     nl     |     eof    |	  eol     |     oc	   |     esp      |    tab     |     |      |	  ^     |     &     |     %      |     \      |
            /*00*/  {     1      ,     2      ,     502    ,     502    ,     103    ,     104    ,      19    ,     106    ,     15     ,     14     ,     17     ,     16     ,     113    ,     114    ,     115    ,     116    ,     117    ,     118    ,     119    ,     120    ,     121    ,     7      ,     13     ,     0      ,     0      ,     0      ,     502    ,      0       ,     0      ,    126     ,    127    ,    129    ,    130     ,     0      },
            /*01*/  {     1      ,     1      ,     1      ,     100    ,     100    ,     100    ,     100    ,     100    ,     100    ,     100    ,     100    ,     100    ,     100    ,     100    ,     100    ,     100    ,     100    ,     100    ,     100    ,     100    ,     100    ,     100    ,     100    ,     100    ,     100    ,     100    ,     100    ,     100      ,    100     ,    100     ,    100    ,    100    ,    100     ,    100     },
            /*02*/  {    101     ,     2      ,     101    ,     3      ,     101    ,     101    ,     101    ,     101    ,     101    ,     101    ,     101    ,     101    ,     101    ,     101    ,     101    ,     101    ,     101    ,     101    ,     101    ,     101    ,     101    ,     101    ,     101    ,     101    ,     101    ,     101    ,     101    ,     101      ,    101     ,    101     ,    101    ,    101    ,    101     ,    101     },
            /*03*/  {    500     ,     4      ,     500    ,     500    ,     500    ,     500    ,     500    ,     500    ,     500    ,     500    ,     500    ,     500    ,     500    ,     500    ,     500    ,     500    ,     500    ,     500    ,     500    ,     500    ,     500    ,     500    ,     500    ,     500    ,     500    ,     500    ,     500    ,     500      ,    500     ,    500     ,    500    ,    500    ,    500     ,    500     },
            /*04*/  {    102     ,     4      ,     102    ,     102    ,     102    ,     102    ,     102    ,     102    ,     102    ,     102    ,     102    ,     102    ,     102    ,     102    ,     102    ,     102    ,     102    ,     102    ,     102    ,     102    ,     102    ,     102    ,     102    ,     102    ,     102    ,     102    ,     102    ,     102      ,    102     ,    102     ,    102    ,    102    ,    102     ,    102     },
            /*05*/  {    106     ,     106    ,     106    ,     106    ,     106    ,     106    ,     106    ,     6      ,     106    ,     106    ,     106    ,     106    ,     106    ,     106    ,     106    ,     106    ,     106    ,     106    ,     106    ,     106    ,     106    ,     106    ,     106    ,     106    ,     106    ,     106    ,     106    ,     106      ,    106     ,    106     ,    106    ,    106    ,    106     ,    106     },
            /*06*/  {    6       ,     6      ,     6      ,     6      ,     6      ,     6      ,     6      ,     6      ,     6      ,     6      ,     6      ,     6      ,     6      ,     6      ,     6      ,     6      ,     6      ,     6      ,     6      ,     6      ,     6      ,     6      ,     6      ,     0      ,     6      ,     0      ,     6      ,      6       ,     6      ,     6      ,     6     ,     6     ,     6      ,     6      },
            /*07*/  {    12      ,     12     ,     12     ,     12     ,     12     ,     12     ,     12     ,     12     ,     12     ,     12     ,     12     ,     12     ,     12     ,     12     ,     12     ,     12     ,     12     ,     12     ,     12     ,     12     ,     12     ,     8      ,     12     ,     12     ,     12     ,     12     ,     12     ,      12      ,     12     ,     12     ,     12    ,     12    ,     12     ,     12     },
            /*08*/  {    122     ,     122    ,     122    ,     122    ,     122    ,     122    ,     122    ,     122    ,     122    ,     122    ,     122    ,     122    ,     122    ,     122    ,     122    ,     122    ,     122    ,     122    ,     122    ,     122    ,     122    ,     9      ,     122    ,     122    ,     122    ,     122    ,     122    ,     122      ,    122     ,    122     ,    122    ,    122    ,    122     ,    122     },
            /*09*/  {    9       ,     9      ,     9      ,     9      ,     9      ,     9      ,     9      ,     9      ,     9      ,     9      ,     9      ,     9      ,     9      ,     9      ,     9      ,     9      ,     9      ,     9      ,     9      ,     9      ,     9      ,     10     ,     9      ,     9      ,     9      ,     9      ,     9      ,      9       ,     9      ,     9      ,     9     ,     9     ,     9      ,     9      },
            /*10*/  {    9       ,     9      ,     9      ,     9      ,     9      ,     9      ,     9      ,     9      ,     9      ,     9      ,     9      ,     9      ,     9      ,     9      ,     9      ,     9      ,     9      ,     9      ,     9      ,     9      ,     9      ,     11     ,     9      ,     9      ,     9      ,     9      ,     9      ,      9       ,     9      ,     9      ,     9     ,     9     ,     9      ,     9      },
            /*11*/  {    9       ,     9      ,     9      ,     9      ,     9      ,     9      ,     9      ,     9      ,     9      ,     9      ,     9      ,     9      ,     9      ,     9      ,     9      ,     9      ,     9      ,     9      ,     9      ,     9      ,     9      ,     0      ,     9      ,     9      ,     9      ,     9      ,     9      ,      9       ,     9      ,     9      ,     9     ,     9     ,     9      ,     9      },
            /*12*/  {    12      ,     12     ,     12     ,     12     ,     12     ,     12     ,     12     ,     12     ,     12     ,     12     ,     12     ,     12     ,     12     ,     12     ,     12     ,     12     ,     12     ,     12     ,     12     ,     12     ,     12     ,     122    ,     12     ,     503    ,     503    ,     503    ,     12     ,      18      ,     12     ,     12     ,     12    ,     12    ,     12     ,     12     },
            /*13*/  {    13      ,     13     ,     13     ,     13     ,     13     ,     13     ,     13     ,     13     ,     13     ,     13     ,     13     ,     13     ,     13     ,     13     ,     13     ,     13     ,     13     ,     13     ,     13     ,     13     ,     13     ,     13     ,     13     ,     0      ,     13     ,     0      ,     13     ,      13      ,     13     ,     13     ,     13    ,     13    ,     13     ,     13     },
            /*14*/  {    107     ,     107    ,     107    ,     107    ,     107    ,     107    ,     107    ,     107    ,     107    ,     107    ,     110    ,     107    ,     107    ,     107    ,     107    ,     107    ,     107    ,     107    ,     107    ,     107    ,     107    ,     107    ,     107    ,     107    ,     107    ,     107    ,     107    ,     107      ,    107     ,    107     ,    107    ,    107    ,    107     ,    107     },
            /*15*/  {    108     ,     108    ,     108    ,     108    ,     108    ,     108    ,     108    ,     108    ,     108    ,     128    ,     109    ,     108    ,     108    ,     108    ,     108    ,     108    ,     108    ,     108    ,     108    ,     108    ,     108    ,     108    ,     108    ,     108    ,     108    ,     108    ,     108    ,     108      ,    108     ,    108     ,    108    ,    108    ,    108     ,    108     },
            /*16*/  {    501     ,     501    ,     501    ,     501    ,     501    ,     501    ,     501    ,     501    ,     501    ,     501    ,     112    ,     501    ,     501    ,     501    ,     501    ,     501    ,     501    ,     501    ,     501    ,     501    ,     501    ,     501    ,     501    ,     501    ,     501    ,     501    ,     501    ,     501      ,    501     ,    501     ,    501    ,    501    ,    501     ,    501     },
            /*17*/  {    123     ,     123    ,     123    ,     123    ,     123    ,     123    ,     123    ,     123    ,     123    ,     123    ,     111    ,     123    ,     123    ,     123    ,     123    ,     123    ,     123    ,     123    ,     123    ,     123    ,     123    ,     123    ,     123    ,     123    ,     123    ,     123    ,     123    ,     123      ,    123     ,    123     ,    123    ,    123    ,    123     ,    123     },
            /*18*/  {    12      ,     12     ,     12     ,     12     ,     12     ,     12     ,     12     ,     12     ,     12     ,     12     ,     12     ,     12     ,     12     ,     12     ,     12     ,     12     ,     12     ,     12     ,     12     ,     12     ,     12     ,     122    ,     12     ,     503    ,     503    ,     503    ,     12     ,     503      ,    503     ,     12     ,     12    ,     12    ,     12     ,     12     },
            /*19*/  {    105     ,     105    ,     105    ,     105    ,     105    ,     105    ,     131    ,     105    ,     105    ,     105    ,     105    ,     105    ,     105    ,     105    ,     105    ,     105    ,     105    ,     105    ,     105    ,     105    ,     105    ,     122    ,     105    ,     105    ,     105    ,     105    ,     105    ,     105      ,    105     ,    105     ,    105    ,    105    ,    105     ,    105     }

        };

        //Creacion de la variable que guardara el codigo fuente del textbox, y creacion de la lista de token y la lista de error
        //que se usaran en la corrida del lexico
        public Lexico(string codigoFuenteInterface)
        {
            codigoFuente = codigoFuenteInterface + " ";
            listaToken = new List<Token>();
            listaError = new List<Error>();

        }

        //El metodo que va a recibir el string incluyendo el lexema y que va a compararlo con todas las palabras reservadas
        //para ver si coincide con una, en caso de ser asi, retornara el valor del token de la palabra reservada con la que coincidio.
        private int esPalabraReservada(string lexema)
        {
            switch (lexema)
            {
                case "and":
                    return 200;

                case "or":
                    return 201;

                case "not":
                    return 202;

                case "if":
                    return 203;

                case "else":
                    return 204;

                case "elif":
                    return 205;

                case "for":
                    return 206;

                case "in":
                    return 207;

                case "while":
                    return 208;

                case "print":
                    return 209;

                case "input":
                    return 210;

                case "def":
                    return 211;

                case "main":
                    return 212;

                case "ident":
                    return 213;

                case "dedent":
                    return 214;

                case "break":
                    return 215;

                case "continue":
                    return 216;

                case "is":
                    return 217;

                case "endif":
                    return 218;

                case "endwhile":
                    return 219;

                case "endfor":
                    return 220;

                case "elsewhile":
                    return 221;

                case "extends":
                    return 222;

                case "import":
                    return 223;

                case "private":
                    return 224;

                case "public":
                    return 225;

                case "Abstract":
                    return 226;
                case "void":
                    return 227;

                case "datetime.today":
                    return 228;


                default:
                    return 100;
            }
        }


        //el metodo que va a guardar el lexema mientras el puntero se vaya moviendo
        //Este recibira el valor de la ubicacion del puntero y agarrara como sub string del codigo fuente y retornara UN solo caracter.
        private char SiguienteCaracter(int i)
        {
            return Convert.ToChar(codigoFuente.Substring(i, 1));
        }

        //el metodo que va a recibir el caracter actual y lo va a comparar con los simbolos del lenguaje. En caso de coincidir, retornara el valor
        //de la columnda en la que se ubica dicho caracter en la matriz.
        private int RegresarColumna(char caracter)
        {
            if (char.IsLetter(caracter))
            {
                return 0;
            }
            else if (char.IsDigit(caracter))
            {
                return 1;
            }
            else if (caracter.Equals('_'))
            {
                return 2;
            }
            else if (caracter.Equals('.'))
            {
                return 3;
            }
            else if (caracter.Equals('+'))
            {
                return 4;
            }
            else if (caracter.Equals('-'))
            {
                return 5;
            }
            else if (caracter.Equals('*'))
            {
                return 6;
            }
            else if (caracter.Equals('/'))
            {
                return 7;
            }
            else if (caracter.Equals('<'))
            {
                return 8;
            }
            else if (caracter.Equals('>'))
            {
                return 9;
            }
            else if (caracter.Equals('='))
            {
                return 10;
            }
            else if (caracter.Equals('!'))
            {
                return 11;
            }
            else if (caracter.Equals('('))
            {
                return 12;
            }
            else if (caracter.Equals(')'))
            {
                return 13;
            }
            else if (caracter.Equals('['))
            {
                return 14;
            }
            else if (caracter.Equals(']'))
            {
                return 15;
            }
            else if (caracter.Equals('{'))
            {
                return 16;
            }
            else if (caracter.Equals('}'))
            {
                return 17;
            }
            else if (caracter.Equals(','))
            {
                return 18;
            }
            else if (caracter.Equals(';'))
            {
                return 19;
            }
            else if (caracter.Equals(':'))
            {
                return 20;
            }
            else if (caracter.Equals('"'))
            {
                return 21;
            }
            else if (caracter.Equals('#'))
            {
                return 22;
            }
            else if (caracter.Equals('\n')) //nueva linea
            {
                return 23;
                //linea = linea + 1;
            }
            else if (caracter.Equals('\r')) //eof o end of file
            {
                return 24;
            }
            else if (caracter.Equals('\n')) //eol o end of line
            {
                return 25;
            }
            else if (caracter.Equals(' ')) //espacio
            {
                return 27;
            }
            else if (caracter.Equals('\t')) //tab
            {
                return 28;
            }
            else if (caracter.Equals('|'))
            {
                return 29;
            }
            else if (caracter.Equals('^'))
            {
                return 30;
            }
            else if (caracter.Equals('&'))
            {
                return 31;
            }
            else if (caracter.Equals('%'))
            {
                return 32;
            }
            else if (caracter.Equals('\\'))
            {
                return 33;
            }
            else
                return 26; //otro caracter
        }

        //Creacion del metodo que va a reicibir el valor del token y lo comparara con los tokens de la lista de simbolos
        //para ver que tipo de token es y retornar el tipo.
        private TipoToken esTipo(int estado)
        {
            switch (estado)
            {
                case 100:
                    return TipoToken.Identificador;
                case 101:
                    return TipoToken.NumeroEntero;
                case 102:
                    return TipoToken.NumeroDecimal;
                case 103:
                    return TipoToken.OperadorAritmetico;
                case 104:
                    return TipoToken.OperadorAritmetico;
                case 105:
                    return TipoToken.OperadorAritmetico;
                case 106:
                    return TipoToken.OperadorAritmetico;
                case 107:
                    return TipoToken.OperadorRelacional;
                case 108:
                    return TipoToken.OperadorRelacional;
                case 109:
                    return TipoToken.OperadorRelacional;
                case 110:
                    return TipoToken.OperadorRelacional;
                case 111:
                    return TipoToken.OperadorRelacional;
                case 112:
                    return TipoToken.OperadorRelacional;
                case 113:
                    return TipoToken.SimboloAgrupacion;
                case 114:
                    return TipoToken.SimboloAgrupacion;
                case 115:
                    return TipoToken.SimboloAgrupacion;
                case 116:
                    return TipoToken.SimboloAgrupacion;
                case 117:
                    return TipoToken.SimboloAgrupacion;
                case 118:
                    return TipoToken.SimboloAgrupacion;
                case 119:
                    return TipoToken.SimboloPuntual;
                case 120:
                    return TipoToken.SimboloPuntual;
                case 121:
                    return TipoToken.SimboloPuntual;
                case 122:
                    return TipoToken.Cadena;
                case 123:
                    return TipoToken.OperadorAsignacion;
                case 124:
                    return TipoToken.SimboloPuntual;
                case 125:
                    return TipoToken.CadenaVacia;
                case 200:
                    return TipoToken.PalabraReservada;
                case 201:
                    return TipoToken.PalabraReservada;
                case 202:
                    return TipoToken.PalabraReservada;
                case 203:
                    return TipoToken.PalabraReservada;
                case 204:
                    return TipoToken.PalabraReservada;
                case 205:
                    return TipoToken.PalabraReservada;
                case 206:
                    return TipoToken.PalabraReservada;
                case 207:
                    return TipoToken.PalabraReservada;
                case 208:
                    return TipoToken.PalabraReservada;
                case 209:
                    return TipoToken.PalabraReservada;
                case 210:
                    return TipoToken.PalabraReservada;
                case 211:
                    return TipoToken.PalabraReservada;
                case 212:
                    return TipoToken.PalabraReservada;
                case 213:
                    return TipoToken.PalabraReservada;
                case 214:
                    return TipoToken.PalabraReservada;
                case 215:
                    return TipoToken.PalabraReservada;
                case 216:
                    return TipoToken.PalabraReservada;
                case 217:
                    return TipoToken.PalabraReservada;
                case 218:
                    return TipoToken.PalabraReservada;
                case 219:
                    return TipoToken.PalabraReservada;
                case 220:
                    return TipoToken.PalabraReservada;
                case 221:
                    return TipoToken.PalabraReservada;
                case 222:
                    return TipoToken.PalabraReservada;
                case 223:
                    return TipoToken.PalabraReservada;
                case 224:
                    return TipoToken.PalabraReservada;
                case 225:
                    return TipoToken.PalabraReservada;
                case 226:
                    return TipoToken.PalabraReservada;
                case 227:
                    return TipoToken.PalabraReservada;
                case 228:
                    return TipoToken.PalabraReservada;

                default:
                    return TipoToken.PalabraReservada;
            }

        }

        //Creacion del metodo que va a recibir el valor del token y lo va a comparar con los tokens de error, si coincide
        //retornara un nuevo constructor para crear un elemento de la lista con sus datos (incluyendo el mensaje de error)
        private Error ManejoErrores(int estado)
        {
            string mensajeError;

            switch (estado)
            {
                case 500:
                    mensajeError = "Se esperaba una digito";
                    break;
                case 501:
                    mensajeError = "Se esperaba un signo =";
                    break;
                case 502:
                    mensajeError = "caracter no identificado";
                    break;
                case 503:
                    mensajeError = "Se esperaban comillas dobles";
                    break;
                default:
                    mensajeError = "Error inesperado";
                    break;
            }
            return new Error() { Codigo = estado, MsjError = mensajeError, Tipo = TipoError.Lexico, Linea = linea };
        }

        //Este es el metodo que ejecutara el analisis del lexico
        //el metodo retornara la lista ya armada de tokens, ya sea de error o de tokens normales
        public List<Token> EjecutarLexico()
        {
            //creacion de las variables que se van usar para guardar el token y la columna
            int estado = 0;
            int columna = 0;

            //variable que guardara el caracter actual
            char caracterActual;

            //variable que guardara el lexema
            string lexema = string.Empty;

            //variable que guardara el valor de la linea en la que esta el puntero
            linea = 1;

            //metodo que va a ejecutar el puntero 
            for (int puntero = 0; puntero < codigoFuente.ToCharArray().Length; puntero++)
            {

                //funciones que haran el movimiento del puntero entre caracteres y la matriz para verificar y obtener los tokens

                caracterActual = SiguienteCaracter(puntero);

                if (caracterActual.Equals('\n'))
                {
                    linea++;
                }

                lexema += caracterActual;

                columna = RegresarColumna(caracterActual);

                estado = matrizTransicion[estado, columna];


                //este if revisara si el token es mayor o igual a 100 (token normal), o menor a 500 (codigo de error), en caso de ser asi entrara al if
                //añadir un elemento a la lista de tokens                            
                if (estado >= 100 && estado < 500)
                {

                    if (lexema.Length > 1)
                    {
                        lexema = lexema.Remove(lexema.Length - 1);
                        puntero--;
                    }

                    //creacion del objeto que creara los elementos de la lista
                    Token nuevoToken = new Token() { ValorToken = estado, Lexema = lexema, Linea = linea };

                    int verificador;
                    verificador = esPalabraReservada(nuevoToken.Lexema);

                    //el metodo que va a verificar si el token actual es token de palabra reservada
                    //en caso de ser asi, añadira un elemento a la lista de toke y reiniciara el puntero
                    if (verificador >= 200)
                    {
                        nuevoToken.ValorToken = verificador;
                        Sintactico.Tokens.Add(verificador);

                        nuevoToken.TipoToken = esTipo(nuevoToken.ValorToken);

                        listaToken.Add(nuevoToken);

                        estado = 0;
                        columna = 0;

                        lexema = string.Empty;

                    }
                    else
                    {
                        //esta seccion de codigo verificara que, en caso de que el token haya sido un token de simbolo doble, aqui se arregla
                        //el lexema del simbolo doble para que su impresion sea correcta
                        switch (estado)
                        {
                            case 109:
                                nuevoToken.Lexema += "=";
                                puntero++;
                                break;
                            case 110:
                                nuevoToken.Lexema += "=";
                                puntero++;
                                break;
                            case 111:
                                nuevoToken.Lexema += "=";
                                puntero++;
                                break;
                            case 112:
                                nuevoToken.Lexema += "=";
                                puntero++;
                                break;
                            case 122:
                                nuevoToken.Lexema += '"';
                                puntero++;
                                break;
                            case 125:
                                nuevoToken.Lexema += '"';
                                puntero++;
                                break;
                            case 128:
                                nuevoToken.Lexema += '>';
                                puntero++;
                                break;
                            case 131:
                                nuevoToken.Lexema += '*';
                                puntero++;
                                break;
                        }

                        //aqui se añade cualquier token que no sea palabra reservada
                        nuevoToken.TipoToken = esTipo(nuevoToken.ValorToken);
                        listaToken.Add(nuevoToken);
                        Sintactico.Tokens.Add(estado);




                        estado = 0;
                        columna = 0;

                        lexema = string.Empty;
                    }
                }

                //en caso de que el token sea mayor o igual a 500, se agregara un elemento a la lista de error
                else if (estado >= 500)
                {
                    listaError.Add(ManejoErrores(estado));
                    estado = 0;
                    columna = 0;
                    lexema = string.Empty;
                }

                //en caso de que el token sea 0, se reinicia todo el puntero
                else if (estado == 0)
                {
                    columna = 0;
                    lexema = string.Empty;
                }
            }


            //el metodo retornara la lista de token generada.
            Sintactico.lineasyntax = linea;

            if (sin.EjecutarSintactico() == 1)
            {
                Form1.compilacion = 1;
            }
            else
            {
                Form1.compilacion = 0;
            }

            return listaToken;
        }
    }
}
