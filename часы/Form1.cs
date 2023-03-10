using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace часы
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
       // string connStr = "server=192.168.25.23;port=44444;user=st_1_20_6;database=is_1_20_st6_KURS;password=40112334;";
        string connStr = "server=chuc.caseum.ru;port=33333;user=st_1_20_6;database=is_1_20_st6_KURS;password=40112334;";
        //Переменная соединения
           //string connStr = "server=10.90.12.110;port=33333;user=st_1_20_6;database=is_1_20_st6_KURS;password=40112334;";
        MySqlConnection conn;
        public void SetMyCustomFormat()
        {
            // Set the Format type and the CustomFormat string.
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "YYYY-MM-MM";
        }
        public string data1(DateTimePicker k)
        {
           var DataTime1 = dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss");
            return DataTime1.ToString();
        } 

        private void button1_Click(object sender, EventArgs e)
        {

           
            if (textBox1.Text == "")
            {
                MessageBox.Show("Введите мне имя", "Ошибка");
                return;

            }
            if (textBox2.Text == "")
            {
                MessageBox.Show("Какова моя цена?", "Ошибка");
                return;

            }

            if (dateTimePicker1.Text == "")
            {
                MessageBox.Show("Дата изготовки", "Ошибка");
                return;

            }
            string ca = data1(dateTimePicker1);

            MySqlCommand command = new MySqlCommand($"INSERT INTO Eda (Name,Price,Data) VALUES('@Name', @Price,'{ca}');", conn);

            conn.Open();
            command.Parameters.Add("@Name", MySqlDbType.VarChar, 25).Value = textBox1.Text;
            command.Parameters.Add("@Price", MySqlDbType.Float, 25).Value = textBox2.Text;
            // command.Parameters.Add("@Data", MySqlDbType.DateTime, 25).Value =ca;
            //     adapter.SelectCommand = command;
            //  adapter.Fill(table);
            try
            {
                if (command.ExecuteNonQuery() == 1)
                {
                   
                    MessageBox.Show("Вы успешно добавили");
                    GetListUsers();
                }
                else
                {

                    MessageBox.Show("Произошла ошибка");
                }
            }
            catch(Exception ex)
            {
               
                MessageBox.Show("Ошибка" + ex);
            }
            finally
            {
                conn.Close();
            }

            

        }
      
        DataTable table = new DataTable();
        //Объявляем адаптер
        MySqlDataAdapter adapter = new MySqlDataAdapter();
        //Объявляем команду
        private BindingSource bSource = new BindingSource();


        // string connStr = "server=10.90.12.110;port=33333;user=st_1_20_6;database=is_1_20_st6_KURS;password=40112334;";
        //Метод обновления DataGreed
       
        public void GetListUsers()
        {
            dataGridView1.DataSource = default;
            //Запрос для вывода строк в БД

            string commandStr = $"SELECT * FROM Eda";
            conn = new MySqlConnection(connStr);
            //Открываем соединение

            conn.Open();
            
            //Объявляем команду, которая выполнить запрос в соединении conn
            adapter.SelectCommand = new MySqlCommand(commandStr, conn);
            //Заполняем таблицу записями из БД
            adapter.Fill(table);
            //Указываем, что источником данных в bindingsource является заполненная выше таблица
            bSource.DataSource = table;
            //Указываем, что источником данных ДатаГрида является bindingsource 
            dataGridView1.DataSource = bSource;
            //Закрываем соединение
            conn.Close();
            //Отражаем количество записей в ДатаГриде
            int count_rows = dataGridView1.RowCount - 1;
            label1.Text = (count_rows).ToString();

        }
      
        private void Form1_Load(object sender, EventArgs e)
        {

            // строка подключения к БД
            // string connStr = "server=10.90.12.110;port=33333;user=st_1_20_6;database=is_1_20_st6_KURS;password=40112334;";
         //   string connStr = "server=chuc.caseum.ru;port=33333;user=st_1_20_6;database=is_1_20_st6_KURS;password=40112334;";
            // создаём объект для подключения к БД
              conn = new MySqlConnection(connStr);
            //Вызываем метод для заполнение дата Грида
           GetListUsers();
            //Видимость полей в гриде
            dataGridView1.Columns[0].Visible = true;
            dataGridView1.Columns[1].Visible = true;
            dataGridView1.Columns[2].Visible = true;
            dataGridView1.Columns[3].Visible = true;
          

            //Ширина полей
            dataGridView1.Columns[0].FillWeight = 15;
            dataGridView1.Columns[1].FillWeight = 40;
            dataGridView1.Columns[2].FillWeight = 15;
            dataGridView1.Columns[3].FillWeight = 15;
        
            //Режим для полей "Только для чтения"
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[2].ReadOnly = true;
            dataGridView1.Columns[3].ReadOnly = true;
           
            //Растягивание полей грида
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
          
            //Убираем заголовки строк
            dataGridView1.RowHeadersVisible = false;
            //Показываем заголовки столбцов
            dataGridView1.ColumnHeadersVisible = true;
            //Вызываем метод покраски ДатаГрид
            //ChangeColorDGV();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        /*  private void ChangeColorDGV()
          {
              //Отражаем количество записей в ДатаГриде
              int count_rows = dataGridView1.RowCount - 1;
              label1.Text = (count_rows).ToString();
              DateTime now = DateTime.Now;

              //Проходимся по ДатаГриду и красим строки в нужные нам цвета, в зависимости от статуса студента
              for (int i = 0; i < count_rows; i++)
              {

                  //статус конкретного студента в Базе данных, на основании индекса строки
                  bool id_selected_status = Convert.ToBoolean(dataGridView1.Rows[i].Cells[3].Value);
                  //Логический блок для определения цветности
                  if (id_selected_status = DateTime)
                  {
                      //Красим в красный
                      dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                  }
                  if (id_selected_status = DateTime) 
                  {
                      //Красим в зелёный
                      dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Green;
                  }
                  if (id_selected_status = DateTime)
                  {
                      //Красим в желтый
                      dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Cyan;
                  }
              }
          }*/

    }
}
