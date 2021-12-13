using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Analizador_Lexico
{
    public partial class Form1 : Form
    {
        public static int compilacion;

        string archivo_1;
        string archivo_2;

        public Form1()
        {
            InitializeComponent();
        }

        private void guardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnCompilar_Click(object sender, EventArgs e)
        {
            var lexico = new Lexico(ftextCode.Text);
            lexico.EjecutarLexico();

            var lista = new BindingList<Token>(lexico.listaToken);
            dgvListToken.DataSource = null;
            dgvListToken.DataSource = lista;

            //var listaError = new BindingList<Error>(lexico.listaError);
            dgvList.DataSource = null;
            dgvList.DataSource = Lexico.listaError;

            if (compilacion == 1)
            {
                MessageBox.Show("Compilado con exito");
            }

            compilacion = 0;

        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenFile = new OpenFileDialog();
            OpenFile.Filter = "Texto |*.txt|*.py|";

            if (OpenFile.ShowDialog() == DialogResult.OK)
            {
                archivo_2 = OpenFile.FileName;

                using (StreamReader sr = new StreamReader(archivo_2))
                {
                    ftextCode.Text = sr.ReadToEnd();
                }
            }
        }

        //Funcion adicional del codigo hecha para guardar el codigo en formato txt
        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog SaveFile = new SaveFileDialog();
            SaveFile.Filter = "Texto |*.txt";

            if (archivo_1 != null)
            {
                using (StreamWriter sw = new StreamWriter(archivo_1))
                {
                    sw.Write(ftextCode.Text);
                }
            }
            else
            {
                if (SaveFile.ShowDialog() == DialogResult.OK)
                {
                    archivo_1 = SaveFile.FileName;
                    using (StreamWriter sw = new StreamWriter(SaveFile.FileName))
                    {
                        sw.Write(ftextCode.Text);
                    }
                }
            }
        }
    }
}
