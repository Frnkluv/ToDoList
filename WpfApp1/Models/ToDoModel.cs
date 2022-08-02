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


        public DateTime CreationDate { get; set; } = DateTime.Now;
        public bool IsDone 
        {
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


        // уведомляет билдинг лист подписывается на это событие, и когда происходит изменение уже в сущ-ем
        // списке - уведомляет об этом, именно когда что-то внутри модели изменилось
        public event PropertyChangedEventHandler PropertyChanged;


        protected virtual void OnPropertyChanged(string propertyName ="")
        {
            // this передает инфу об этой модели, которая вызвала это событие
            // чтоб избежать null
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
