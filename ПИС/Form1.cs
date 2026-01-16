using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ПИС
{
    public partial class Form1 : Form
    {
        // Ассоциация с контроллером
        private PatentController controller;

        public Form1()
        {
            InitializeComponent();
            controller = new PatentController(); // Создаем контроллер
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 1. Запрашиваем данные у контроллера
            Dictionary<string, List<string>> data = controller.RequestPatentConsultation();

            // 2. Заполняем ComboBox полученными списками
            // (Ключи "purposes", "countries", "statuses" мы задали в Сервисе)

            if (data.ContainsKey("purposes"))
                comboBox1.Items.AddRange(data["purposes"].ToArray());

            if (data.ContainsKey("countries"))
                comboBox2.Items.AddRange(data["countries"].ToArray());

            if (data.ContainsKey("statuses"))
                comboBox3.Items.AddRange(data["statuses"].ToArray());

            // Если нужно отобразить ID, можно вывести его, например, в заголовок или Label
            // if (data.ContainsKey("visitorId"))
            //    this.Text = "Visitor ID: " + data["visitorId"][0];
        }

        private void btnGetConsultation_Click(object sender, EventArgs e)
        {
            // Это будет следующий этап
        }
    }
}