using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// was WpfApp1
namespace WpfApp1.Models
{
    class ToDoModel : INotifyPropertyChanged
    {
        private bool _isDone;
        private string _text;
        private string _note;
        private string _deadline;

        //public DateTime CreationDate { get; set; } = DateTime.Parse(DateTime.Now.ToString("d"));
        public DateTime CreationDate { get; set; } = DateTime.Now.Date;

        public bool IsDone 
        {
            // чтобы чекбокс понял об изменении - в DataGrid добавить UpdateSourceTrigger=PropertyChanged
            get { return _isDone; }
            set 
            {
                if (_isDone == value)
                    return;

                _isDone = value; 
                OnPropertyChanged("isDone");
            } 
        }

        public string Text 
        {
            get { return _text; }
            set 
            {
                if (_text == value)
                    return;

                _text = value;
                OnPropertyChanged("Text");
            } 
        }

        public string Note
        {
            get { return _note; }
            set
            {
                if (_note == value)
                    return;

                _note = value;
                OnPropertyChanged("Note");
            }
        }

        public string Deadline
        {
            get { return _deadline; }
            set
            {
                if (_deadline == value)
                    return;

                _deadline = value;
                OnPropertyChanged("Deadline");
            }
        }


        // Уведомляет билдинг лист о подписке на это событие, и когда происходит изменение уже в сущ-ем
        // списке - уведомляет об этом, именно когда что-то внутри модели изменилось.
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName ="")
        {
            // Обращение к ивенту.
            // this передает инфу об этой модели, которая вызвала это событие - чтоб избежать null (когда никто не подписался на событие)
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
