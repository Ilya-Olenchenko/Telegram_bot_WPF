using System;
using System.IO;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Telegram.Bot;
using System.Diagnostics;

namespace Telegram_bot_WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string token = "5122212670:AAEVIgyIc5IlrlcWpmmchjaNzBVYLWEWEdw";
        ObservableCollection<TelegramUser> Users;
        TelegramBotClient bot;
        public MainWindow()
        {
            InitializeComponent();
            Users = new ObservableCollection<TelegramUser>();
            userList.ItemsSource = Users;


            bot = new TelegramBotClient(token);

            bot.OnMessage += delegate (object sebder, Telegram.Bot.Args.MessageEventArgs e)
            {
                string msg = $"{DateTime.Now}: {e.Message.Chat.FirstName} {e.Message.Chat.Id} {e.Message.Text}";

                System.IO.File.AppendAllText("data.log", $"{msg}\n"); // Зберігання повідомлень в файл.

                Debug.WriteLine(msg);
                this.Dispatcher.Invoke(() => 
                {
                    var person = new TelegramUser(e.Message.Chat.FirstName, e.Message.Chat.Id);
                    if(!Users.Contains(person)) Users.Add(person);
                    Users[Users.IndexOf(person)].AddMessage($"{person.Nick}: {e.Message.Text}");
                });
            };

            bot.StartReceiving();
            btnSendMsg.Click += delegate { SendMsg(); };
            txtBxSendMsg.KeyDown += (s, e) => { if (e.Key == Key.Return) { SendMsg(); } };
        }

        public void SendMsg()
        {
            var concreteUser = Users[Users.IndexOf(userList.SelectedItem as TelegramUser)];
            string responseMsg = $"Support: {txtBxSendMsg.Text}";
            concreteUser.Messages.Add(responseMsg);

            bot.SendTextMessageAsync(concreteUser.Id, txtBxSendMsg.Text);
            string logText = $"{DateTime.Now}: >> {concreteUser.Id} {concreteUser.Nick} {responseMsg}\n";
            System.IO.File.AppendAllText("data.log", logText);

            txtBxSendMsg.Text = String.Empty;
        }
    }
}
