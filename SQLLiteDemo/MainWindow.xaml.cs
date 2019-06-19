using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SQLLiteDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private SQLLiteHepler _db;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this._db = new SQLLiteHepler("sqlite.db");
            _db.Open();
            TestSave();
            TestTableExists();
            TestExecuteRow();
            TestSave();   
            TestUpdate();    
            TestQueryOne();   
            TestDelete();
        }

        public void TestTableExists()
        {
            Console.WriteLine("表test是否存在: " + this._db.TableExists("test"));
        }

        public void TestExecuteRow()
        {
            List<object> list = this._db.ExecuteRow("select * from test", null, null);
            foreach (object o in list)
            {
                Dictionary<string, object> d = (Dictionary<string, object>)o;
                foreach (string k in d.Keys)
                {
                    Console.Write(k + "=" + d[k] + ",");
                }
                Console.WriteLine();
            }
        }

        public void TestSave()
        {
            Dictionary<string, object> entity = new Dictionary<string, object>();
            entity.Add("username", "u1");
            entity.Add("password", "p1");
            this._db.Save("test", entity);
        }

        public void TestUpdate()
        {
            Dictionary<string, object> entity = new Dictionary<string, object>();
            entity.Add("username", "u1");
            entity.Add("password", "123456");

            int c = this._db.Update("test", entity, "username=@username", new System.Data.SQLite.SQLiteParameter[] {
                new SQLiteParameter("username","u1")
            });
            Console.WriteLine(c);
        }

        public void TestQueryOne()
        {
            Dictionary<string, object> entity = this._db.QueryOne("test", "username", "a");
            foreach (string k in entity.Keys)
            {
                Console.Write(k + "=" + entity[k] + ",");
            }
        }

        public void TestDelete()
        {
            int c = this._db.Delete("test", "username=@username", new SQLiteParameter[] {
                new SQLiteParameter("username","a")
            });
            Console.WriteLine("c=" + c);
        }
    }
}
