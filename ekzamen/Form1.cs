using System;
using System.Windows.Forms;

namespace ekzamen
{
    public partial class Form1 : Form
    {
        static int count = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void proverka(int number, string Name, string text)
        {
            if (text.Length < number)
            {
                MessageBox.Show($"Введите {number} цифр в {Name}", "Ошибка");
                return;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            // Если кол-во введенных цифр в БИК меньше 9, то выводится сообщение об ошибке.
            proverka(9, "БИК", textBox3.Text);

            // Если кол-во введенных цифр в ИНН меньше 10 или равно 11, то выводится сообщение об ошибке.
            if (textBox4.Text.Length < 10 || textBox4.Text.Length == 11)
            {
                MessageBox.Show("Введите 10 или 12 цифр в ИНН!", "Ошибка");
                return;
            }

            // Если кол-во введенных цифр в Счёте получателя меньше 20, то выводится сообщение об ошибке.
            proverka(20, "Счёте получателя", textBox6.Text);

            // Если не все поля заполнены, выводится ошибка. 
            if ((textBox1.Text == "") || (textBox2.Text == "") || (comboBox1.Text == "") || (textBox3.Text == "") ||
                (textBox4.Text == "") || (comboBox2.Text == "") || (textBox6.Text == ""))
                MessageBox.Show("Заполните все поля!", "Ошибка");
            // Если все поля заполнены, то необходимо подтвердить действие для отправки данных.
            else
            {
                DialogResult result = MessageBox.Show("Отправить?", "Подтверждение отправки", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    label3.Visible = true;
                    label4.Visible = true;
                    label12.Visible = true;
                    label3.Text = (++count).ToString();
                    label4.Text = DateTime.Now.ToString();

                    if (textBox4.Text.Length == 10)
                        label12.Text = "Юридическое лицо";
                    else if (textBox4.Text.Length == 12)
                        label12.Text = "Предприниматель";
                    foreach (Control control in this.Controls)
                    {
                        if ((control is TextBox))
                        {
                           
                            TextBox textBox = control as TextBox;
                            //textBox.ReadOnly = true;
                            comboBox1.Enabled = false;
                            comboBox2.Enabled = false;
                            textBox.Enabled = false;
                            button1.Enabled = false;
                        }
                    }
                }
            }
        }

        // Проверка на ввод нецифрового значения для БИК.
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (textBox3.Text.Length < 9 && char.IsDigit(e.KeyChar))
            {
                return;
            }
            e.Handled = true;
        }

        // Проверка на ввод нецифрового значения для Суммы.
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Преобразование символа запятой в точку.
            if (e.KeyChar == ',')
            {
                e.KeyChar = '.';
            }

            // Ограничение ввода символов.
            if (e.KeyChar < '0' | e.KeyChar > '9' && e.KeyChar != (char)Keys.Back && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            // Запрет ввода разделителя первым символом.
            if (textBox2.SelectionStart == 0 & e.KeyChar == '.')
            {
                e.Handled = true;
            }
            // Запрет ввода числа при вводе первым символом 0. 
            if (textBox2.Text == "0")
            {
                if (e.KeyChar != '.' & e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
            // Запрет на ввод более 2 знаков после точки.
            if (textBox2.Text.IndexOf('.') > 0)
            {
                if (textBox2.Text.Substring(textBox2.Text.IndexOf('.')).Length > 2)
                {
                    if (e.KeyChar != (char)Keys.Back)
                    {
                        e.Handled = true;
                    }
                }
            }
            // Ввод только 1 разделителя.
            if (e.KeyChar == '.')
            {
                if (textBox2.Text.IndexOf('.') != -1)
                {
                    e.Handled = true;
                }
            }

        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (textBox4.Text.Length < 12 && char.IsDigit(e.KeyChar))
            {
                return;
            }
            e.Handled = true;

        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (textBox6.Text.Length < 20 && char.IsDigit(e.KeyChar))
            {
                return;
            }
            e.Handled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Отменить?", "Подтверждение отмены", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                foreach (Control control in this.Controls)
                {
                    if ((control is TextBox))
                    {
                        TextBox textBox = control as TextBox;
                        textBox.Clear();
                        label3.Text = "";
                        label4.Text = string.Empty;
                        label12.Text = "";
                        comboBox1.SelectedIndex = -1;
                        comboBox2.SelectedIndex = -1;
                       
                        comboBox1.Enabled = true;
                        comboBox2.Enabled = true;
                        textBox.Enabled = true;
                        button1.Enabled = true;
                    }
                }
            }
        }
    }
}
