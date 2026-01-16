using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ПИС
{
    public partial class Form1 : Form
    {
        private PatentController controller;
        private String currentVisitorId;

        public Form1()
        {
            InitializeComponent();
            controller = new PatentController(); 
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Dictionary<string, List<string>> data = controller.RequestPatentConsultation();


            if (data.ContainsKey("purposes"))
                comboBox1.Items.AddRange(data["purposes"].ToArray());

            if (data.ContainsKey("countries"))
                comboBox2.Items.AddRange(data["countries"].ToArray());

            if (data.ContainsKey("statuses"))
                comboBox3.Items.AddRange(data["statuses"].ToArray());

            if (data.ContainsKey("visitorId") && data["visitorId"].Count > 0)
            {
                currentVisitorId = data["visitorId"][0];
            }
        }

        private void btnGetConsultation_Click(object sender, EventArgs e)
        {
            string country = comboBox2.Text;
            string purpose = comboBox1.Text;
            string status = comboBox3.Text;
            string date = dateTimePicker1.Text;

            if (string.IsNullOrEmpty(country) || string.IsNullOrEmpty(purpose))
            {
                MessageBox.Show("Пожалуйста, выберите страну и цель въезда.");
                return;
            }

            string message = controller.createPatentMessage(country, purpose, status, date, currentVisitorId);

            FormStart formStart = new FormStart(message); 

            this.Hide();
            formStart.ShowDialog();
            this.Close();
        }
    }
}