using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeChromeBrowser();
        }
        private void InitializeChromeBrowser()
        {
            CefSettings cefSettings = new CefSettings();
            Cef.Initialize(cefSettings);
            ChromiumWebBrowser chrome = new ChromiumWebBrowser("file:///C:/Users/kdjsf/test/test.html");
            chrome.Dock = DockStyle.Fill;
            this.Controls.Add(chrome);
            chrome.JavascriptObjectRepository.Settings.LegacyBindingEnabled = true;
            chrome.JavascriptObjectRepository.Register("cAPI", new ChromeAPI(), false, BindingOptions.DefaultBinder);
        }

        private void button1_Click(object sender, EventArgs e)
        {
           // _Insert();
        }
    }
    internal class ChromeAPI
    {
        string a = "";
        string b = "";
        public void showMsg(string msg1, string msg2)
        {
            MessageBox.Show("회원가입 완료");
            a = msg1;
            b = msg2;

            Register re = new Register();
            re._Insert(a, b);
        }
    }

    class Register
    {
        string _server = "localhost";
        int _port = 3306;
        string _database = "register";
        string _id = "root";
        string _pw = "root";
        string _connectionAddress = "";

        public Register()
        {
            _connectionAddress = string.Format("Server = {0}; Port = {1}; Database = {2}; Uid = {3}; Pwd = {4}", _server, _port, _database, _id, _pw);
        }
        
        public void _Insert(string a, string b)
        {
            try
            {
                using (MySqlConnection mysql = new MySqlConnection(_connectionAddress))
                {
                    mysql.Open();
                    ChromeAPI api = new ChromeAPI();
                    string insertQuery = string.Format("INSERT INTO res_table(_ID,_PW) VALUES ('{0}','{1}');", a, b);
                    MySqlCommand command = new MySqlCommand(insertQuery, mysql);
                    if (command.ExecuteNonQuery() != 1) MessageBox.Show("Failed to insert");
                }
            }

            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
    }
}
