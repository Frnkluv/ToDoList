using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using WpfApp1.Models;
using WpfApp1.Services;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Environment.CurrentDirectory - расположение exe файла + после \\ сам json. В том же месте получается.
        private readonly string PATH = $"{Environment.CurrentDirectory}\\todoDataList.json";
        
        //контейнер в котором будут храниться данные:
        private BindingList<ToDoModel> _toDoDataList;

        // подкл библ Json и созд класс для загрузки/сохранения с ЖД
        private FileIOServece _fileIOServece;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _fileIOServece = new FileIOServece(PATH);

            try
            {
                _toDoDataList = _fileIOServece.LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Close();       // тк вызыв в MainWindow - то this.Close() упрощаем
            }


            dgToDoList.ItemsSource = _toDoDataList;
            //подписка на метод, который 
            _toDoDataList.ListChanged += _toDoDataList_ListChanged;
        }


        // Сохраняет данные на жесткий, когда что-то обновилось
        // sender и _toDoDataList ссылаются на один и тот же объект в куче.
        private void _toDoDataList_ListChanged(object sender, ListChangedEventArgs e)
        {
            //чтобы метод понимал что что-то изменилось (в тексте, например) - надо реализовать INotifyPropertyChanged, подключенный в ToDoModel

            if (e.ListChangedType == ListChangedType.ItemAdded || 
                e.ListChangedType == ListChangedType.ItemChanged || 
                e.ListChangedType == ListChangedType.ItemDeleted)
            {
                try
                {
                    _fileIOServece.SaveData(sender);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Close();
                }
            }
        }
    }
}
