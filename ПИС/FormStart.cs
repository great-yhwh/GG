using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ПИС
{
    public partial class FormStart : Form
    {
        // Стандартный конструктор (пусть останется)
        public FormStart()
        {
            InitializeComponent();
        }

        // --- ДОБАВЛЯЕМ ЭТОТ КОНСТРУКТОР ---
        public FormStart(string message)
        {
            InitializeComponent();

            // Выводим полученное сообщение в элемент на форме
            // Убедитесь, что вы добавили labelResult на форму!
            // Если вы не меняли имя элемента, он может называться label1
            if (textBox1 != null)
            {
                textBox1.Text = message;
            }
            else
            {
                // Запасной вариант, если элемента нет - покажем всплывающее окно
                MessageBox.Show(message);
            }
        }
    }
}