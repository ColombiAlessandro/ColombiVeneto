using System.IO;
using System.Text;
using System;
namespace ColombiVeneto
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int recordLength;

        private void Form1_Load(object sender, EventArgs e)
        {
            StreamReader sr = new StreamReader(@"veneto_verona.csv");
            recordLength = sr.ReadLine().ToCharArray().Length+1;
            sr.Close();
            comuneBox.Text = recordLength.ToString();
            var f = new FileStream(@"veneto_verona.csv", FileMode.Open, FileAccess.ReadWrite);
            int numeroRighe = (int)(f.Length) / recordLength;
            numeroRighe += 1;
            int posizioneNome = 7;
            int pos;
            comuneBox.Text = numeroRighe.ToString();
            BinaryReader reader = new BinaryReader(f);
            string line;
            byte[] br;
            do
            {
                pos = numeroRighe / 2;
                f.Seek(pos * recordLength, SeekOrigin.Begin);
                br = reader.ReadBytes(recordLength);
                line = Encoding.ASCII.GetString(br, 0, br.Length);
                if (line.Split(';')[0] == comuneBox.Text)
                {
                    MessageBox.Show(line.Split()[posizioneNome]);
                }
                else if (string.Compare(line.Split(';')[0], comuneBox.Text, false))
                    i = m + 1;
                else
                    j = m - 1;

            } while (i <= j && pos == -1);
        }
    }
}