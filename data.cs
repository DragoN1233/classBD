using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace otdelkadrov
{
    class data
    {
        SqlConnection connect = null;
        public void connect_db()
        {
            String adres = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" 
                            + Properties.Settings.Default.data +
                            ";Integrated Security=True"; // строка с адресом поключения
            connect = new SqlConnection(adres);
            connect.Open();    // Открываем соединение с базой 

        }
        public void insert(string table, string[] param,string[] table_tab,string id)
        {

            connect_db(); // подключаемся к бд

            // составление строки заплроса к бд
            String zapros = "INSERT INTO "+table+" (id"; 
            foreach (string table_in in table_tab)
            {
                zapros += ","+table_in ;

            }
            zapros += ") VALUES ('"+id+"'";
            foreach (string table_in in param)
            {
                zapros += ",'" + table_in+"'";
            }
            zapros += ")";
            SqlCommand comand = new SqlCommand(zapros, connect); // отправка запроса к бд
            comand.ExecuteNonQuery();
            connect.Close(); // закрываем подключение в бд
        }
        public void delete(string table, string id) {
            // ф-я для удаления записей из таблици 
            // принемает 2 пораметра: название таблици и id 

            connect_db(); 
            String zapros = "DELETE FROM ["+ table + "] WHERE id =" + id + "";
            SqlCommand comand = new SqlCommand(zapros, connect);
            comand.ExecuteNonQuery();
            connect.Close();



        }
        public List<string[]> select(string table, string param, int nid = 8)
        {
            // выборка из базы 
            string zapros = "SELECT * FROM [" + table + "]";
            if (param != "")
            {

                zapros += " WHERE " + param;

            }
            connect_db();
            SqlCommand comand = new SqlCommand(zapros, connect);
            SqlDataReader read = comand.ExecuteReader();
            List<string[]> data = new List<string[]>();
            String[] data_b = new String[nid];
            int ids = nid-1;
            int index = 0;
            while (read.Read())
            {
                data.Add(new string[nid]);

                while (index<ids) {

                    data[data.Count - 1][index] = read[index].ToString();
                    index++;
                }
                index = 0;
              

            }


            connect.Close();
            return data; // возвроцает массив полученых данных
        }
    }
}
