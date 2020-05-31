using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;


namespace Airport1
{
    public partial class Form1 : Form
    {
        internal static object planes;
        private Users users;
        private string fileName;
        public Form1()
        {
            InitializeComponent();

            //получаем папку, где хранятся данные
            var fileDir = AppDomain.CurrentDomain.BaseDirectory;

            //получаем имя файла
            fileName = Path.Combine(fileDir, "DataUsers.bin");

            //если файл существует - загружаем его, если нет - создаем новый объект Users
            if (File.Exists(fileName))
                using (var fs = File.OpenRead(fileName))
                    users = (Users)new BinaryFormatter().Deserialize(fs);
            else
                users = new Users();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        internal class Plane
        {
            public Plane()
            {
            }

            public int Number { get; internal set; }
            public int TimeHours { get; internal set; }
            public int TimeMinute { get; internal set; }
            public int Places { get; internal set; }
            public int Days { get; internal set; }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < tbUser.TextLength; i++)
            {

                char ch = Convert.ToChar(tbUser.Text.Substring(i, 1));
                if (ch == ' ')
                {
                    MessageBox.Show("Введите пожалуйста без пробела.");
                    tbUser.Clear();
                }

            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btEnter_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbSignup.Checked)
                {
                    //Лимит символов
                    if (tbUser.TextLength < 4)
                    {
                        throw new Exception("Минимальная длина логина 4 символа.");
                    }
                    if (tbUser.TextLength > 16)
                    {
                        throw new Exception("Максимальная длина логина 16 символов.");
                    }
                    if (tbPassword.TextLength < 4)
                    {
                        throw new Exception("Минимальная длина пароля 4 символа.");
                    }
                    if (tbPassword.TextLength > 16)
                    {
                        throw new Exception("Максимальная длина пароля 16 символов.");
                    }

                    //проверка на пустое значение
                    if (tbUser.Text == "") throw new Exception("Введите имя пользователя.");
                    if (tbPassword.Text == "") throw new Exception("Введите пароль.");
                    //регстрация нового пользователя
                    users.SignupNewUser(tbUser.Text, tbPassword.Text);
                    //сохраняем юзеров в файл
                    using (var fs = File.OpenWrite(fileName))
                        new BinaryFormatter().Serialize(fs, users);
                }
                else
                {
                    //вход существующего пользователя
                    users.SignIn(tbUser.Text, tbPassword.Text);
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;//выходим из метода, не открыв главную форму приложения
            }
            //открываем главную форму приложения
            
            if (tbUser.Text == "admin" && tbPassword.Text == "admin")
            {
                Hide();
                adm admin = new adm();
                admin.Show();
            }
            else {
                Hide();
                MainForm main = new MainForm();
                main.Show();
            }
        }

        private void tbUser_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (Char.IsDigit(e.KeyChar) == false) return;
            if (e.KeyChar == Convert.ToChar(Keys.Back)) return;
            e.Handled = true;
            tbUser.Clear();
            MessageBox.Show("Введите имя без числа.");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
    }
    [Serializable]
    class User
    {
        public string Login;
        public int PasswordHash;

        public User(string login, string password)
        {
        Login = login;
            PasswordHash = password.GetHashCode();
        }
    }
[Serializable]
class Users : List<User>
{
    /// Вход пользователя
    public bool SignIn(string login, string password)
    {
        //ищем юзера по логину
        var user = this.FirstOrDefault(u => u.Login == login);
        if (user == null) throw new Exception("Пользователь не найден.");

        //проверяем пароль
        if (user.PasswordHash != password.GetHashCode()) throw new Exception("Неверный пароль.");
        return true;
    }
    /// Регистрация нового пользователя
    public void SignupNewUser(string login, string password)
    {
        //проверяем, нет ли такого пользователя
        if (this.Any(u => u.Login == login))
            throw new Exception("Пользователь с таким именем уже зарегистрирован.");

        Add(new User(login, password));
    }
}
/// Пользователь

