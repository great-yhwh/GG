using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ПИС
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.AddRange(File.ReadAllLines("ЦельВъезда.txt"));
            comboBox2.Items.AddRange(File.ReadAllLines("Гражданства.txt"));
            comboBox3.Items.AddRange(File.ReadAllLines("Статус.txt"));
        }

        private void btnGetConsultation_Click(object sender, EventArgs e)
        {

        }
    }
}
