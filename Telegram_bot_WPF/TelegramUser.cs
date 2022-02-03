using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram_bot_WPF
{
    public class TelegramUser : INotifyPropertyChanged, IEnumerable<TelegramUser>
    {
        public TelegramUser(string Nickname, long ChatId)
        {
            this.nick = Nickname;
            this.id = ChatId;
            Messages = new ObservableCollection<string>();
        }

        private string nick;

        public string Nick
        {
            get { return this.nick; }
            set
            {
                this.nick = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Nick)));
            }
        }
        private long id;
        public long Id
        {
            get { return this.id; }
            set {
                this.id = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Id)));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged; //Сповіщення зовнішніх агентів 
        /// <summary>
        /// Метод порівняння двох користувачів
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>>
        /// 
        public bool Equals(TelegramUser other) => other.Id == this.id;
        /// <summary>
        /// Всі повідомлення
        /// </summary>

        public ObservableCollection<string> Messages {get; set;}
        /// <summary>
        /// Метод додавання повідомлення 
        /// </summary>
        /// <param name="Text">Текст повідомлення</param>

        public void AddMessage(string Text) => Messages.Add(Text);

    }
}
