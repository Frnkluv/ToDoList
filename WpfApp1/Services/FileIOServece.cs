using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;

namespace WpfApp1.Services
{
    internal class FileIOServece
    {
        private readonly string PATH;

        public FileIOServece(string path)
        {
            PATH = path;
        }

        public BindingList<ToDoModel> LoadData()
        {
            // проверка существования файла, перед его загрузкой, если - нет, то освобождаю память, и создаю пустой список
            var fileExists = File.Exists(PATH);
            if (!fileExists)
            {
                File.CreateText(PATH).Dispose();
                return new BindingList<ToDoModel>();
            }

            using (var reader = File.OpenText(PATH))
            {
                var fileText = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<BindingList<ToDoModel>>(fileText);
            }
        }

        public void SaveData(object toDoDataList) 
        {
            using (StreamWriter writer = File.CreateText(PATH))
            {
                string output = JsonConvert.SerializeObject(toDoDataList);
                writer.Write(output);
            }
        }
    }
}
