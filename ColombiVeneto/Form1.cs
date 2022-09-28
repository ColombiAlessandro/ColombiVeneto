using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace ColombiVeneto
{
    public partial class Form1 : Form
    {
        int recordLength;
        int numeroRighe;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StreamReader sr = new StreamReader(@"veneto_verona.csv");
            recordLength = sr.ReadLine().ToCharArray().Length + 1;
            sr.Close();
            var f = new FileStream(@"veneto_verona.csv", FileMode.Open, FileAccess.ReadWrite);
            numeroRighe = (int)(f.Length) / recordLength;
            numeroRighe += 1;
            f.Close();
            
        }

        private void confermaButton_Click(object sender, EventArgs e)
        {
            var f = new FileStream(@"veneto_verona.csv", FileMode.Open, FileAccess.ReadWrite);
            if (!string.IsNullOrEmpty(comuneBox.Text) && !string.IsNullOrWhiteSpace(comuneBox.Text))
            {
                int posizioneNome = 7;
                int pos;
                
                BinaryReader reader = new BinaryReader(f);
                string line;
                byte[] br;
                int i = 0;
                int j = numeroRighe - 1;
                int ctrl = -1;
                try
                {
                    do
                    {
                        pos = j + i / 2;
                        f.Seek(pos * recordLength, SeekOrigin.Begin);
                        br = reader.ReadBytes(recordLength);
                        line = Encoding.ASCII.GetString(br, 0, br.Length);
                        if (line.Split(';')[0] == comuneBox.Text.ToUpper())
                        {
                            MessageBox.Show(line.Split(';')[posizioneNome],"Nome struttura:");
                            f.Close();
                            return;
                        }
                        else if (string.Compare(line.Split(';')[0], comuneBox.Text, false) < 0)
                        {
                            i = pos + 1;
                        }
                        else
                            j = pos - 1;

                    } while (i <= j && ctrl == -1);
                    if (ctrl == -1)
                    {
                        MessageBox.Show("Comune non trovato", "Errore");
                    }
                }
                catch (Exception p)
                {
                    MessageBox.Show("Comune non trovato", "Errore");
                }
            } else
            {
                MessageBox.Show("Valore inserito non valido", "Errore");
            }
            f.Close();
        }
    }
}
