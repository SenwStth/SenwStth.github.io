using System; 
using System.Collections.Generic;  
using System.Linq;  

namespace WindowsFormsApp1
{
    /// <summary>
    /// Класс участника соревнований
    /// </summary>
    public class Runner
    {
        private string name;        // Фамилия участника
        private double distance;    // Длина дистанции (в метрах)
        private double time;        // Время (в секундах)
        private double speed;       // Скорость бега (м/с)

        /// <summary>
        /// Конструктор
        /// </summary>
        public Runner(string name, double distance, double time)
        {
            this.name = name;
            this.distance = distance;
            this.time = time;
            CalculateSpeed();
        }

        /// <summary>
        /// Вычисление скорости
        /// </summary>
        private void CalculateSpeed()
        {
            if (time > 0)
                speed = distance / time;
            else
                speed = 0;
        }

        // Свойства
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public double Distance
        {
            get { return distance; }
            set
            {
                distance = value;
                CalculateSpeed();
            }
        }

        public double Time
        {
            get { return time; }
            set
            {
                time = value;
                CalculateSpeed();
            }
        }

        public double Speed
        {
            get { return speed; }
        }

        /// <summary>
        /// Представление для сохранения в файл
        /// </summary>
        public string ToFileString()
        {
            return $"{name};{distance};{time}";
        }

        /// <summary>
        /// Создание участника из строки файла
        /// </summary>
        public static Runner FromFileString(string line)
        {
            string[] parts = line.Split(';');
            if (parts.Length == 3)
            {
                string name = parts[0];
                double distance = double.Parse(parts[1]);
                double time = double.Parse(parts[2]);
                return new Runner(name, distance, time);
            }
            throw new Exception("Неверный формат строки");
        }

        public override string ToString()
        {
            return $"{name}\t{distance} м\t{time} сек\t{speed:F2} м/с";
        }
    }
}