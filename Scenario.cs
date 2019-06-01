/*
 * Пространство имен System содержит фундаментальные и базовые классы, 
 * определяющие часто используемые типы значений и ссылочных данных, события и обработчики событий, 
 * интерфейсы, атрибуты и исключения обработки.
 */
 using System;
/*
 * Пространство имен System.Collections.Generic содержит интерфейсы и классы, определяющие универсальные коллекции, 
 * которые позволяют пользователям создавать строго типизированные коллекции, обеспечивающие повышенную 
 * производительность и безопасность типов по сравнению с неуниверсальными строго типизированными коллекциями.
 */
using System.Collections.Generic;
/*
 * Пространство имен System.Linq содержит классы и интерфейсы, которые поддерживают LINQ.
 */
using System.Linq;
/*
 * Пространство имен System.IO содержит типы, позволяющие осуществлять чтение и запись в файлы и потоки данных, 
 * а также типы для базовой поддержки файлов и папок.
 */
using System.IO;

namespace Backup
{
    public enum ScenarioType {полный, зеркальный, инкрементальный, дифференциальный};

    // Класс "Сценарий"
    public class Scenario
    {
        public string Title = "Новый";                              // Название сценария
        public ScenarioType scenarioType = ScenarioType.полный;     // Тип резервирвания
        public bool Zip = false;                                    // Использование архиватора
        public string Destination = "";                             // Путь назначения
        public List<string> Shedule = new List<string>();           // Расписание
        public List<string> Source = new List<string>();            // Источники данных
        public DateTime LastTime;                                   // Время последнего запуска

        static ScenarioType Convert(string s)
        {
            ScenarioType result = ScenarioType.полный;
            if (s == "Зеркальный")
                result = ScenarioType.зеркальный;
            if (s == "Инкрементальный")
                result = ScenarioType.инкрементальный;
            if (s == "Дифференциальный")
                result = ScenarioType.дифференциальный;
            return result;
        }

        static string Convert(ScenarioType s)
        {
            string result;
            switch (s)
            {
                case ScenarioType.зеркальный:
                    result = "Зеркальный";
                    break;
                case ScenarioType.дифференциальный:
                    result = "Дифференциальный";
                    break;
                case ScenarioType.инкрементальный:
                    result = "Инкрементальный";
                    break;
                default:
                    result = "Полный";
                    break;
            }
            return result;
        }

        public Scenario() { }

        public Scenario(StreamReader r)
        {
            Load(r);
        }

        public Scenario(ref string[] value)
        {
            Load(ref value);
        }

        public void Load(StreamReader r)
        {
            // Описание сценария состоит из следующих элементов:
            // Название сценария
            Title = r.ReadLine();
            // Тип резервирования
            scenarioType = Convert(r.ReadLine());
            // Использование архиватора
            Zip = bool.Parse(r.ReadLine());
            // Путь назначения
            Destination = r.ReadLine();
            // Дата последнего запуска
            string temp = r.ReadLine();
            LastTime = DateTime.Parse(temp);
            // Список источников данных
            int N;
            N = int.Parse(r.ReadLine());
            Source = new List<string>();
            for (int n = 0; n < N; n++)
                Source.Add(r.ReadLine());
            // Список расписания
            N = int.Parse(r.ReadLine());
            Shedule = new List<string>();
            for (int n = 0; n < N; n++)
                Shedule.Add(r.ReadLine());
        }

        void Load(ref string[] value)
        {
            int index = 0;
            // Описание сценария состоит из следующих элементов:
            // Название сценария
            Title = value[index++];
            // Тип резервирования
            scenarioType = Convert(value[index++]);
            // Использование архиватора
            Zip = bool.Parse(value[index++]);
            // Путь назначения
            Destination = value[index++];
            // Дата последнего запуска
            string temp = value[index++];
            LastTime = DateTime.Parse(temp);
            // Список источников
            int N;
            N = int.Parse(value[index++]);
            Source = new List<string>();
            for (int n = 0; n < N; n++)
                Source.Add(value[index++]);
            // Список расписания
            N = int.Parse(value[index++]);
            Shedule = new List<string>();
            for (int n = 0; n < N; n++)
                Shedule.Add(value[index++]);

            string[] newvalue = new string[value.Length - index];
            for (int k = 0; k < newvalue.Length; k++)
                newvalue[k] = value[k + index];
            value = newvalue;

        }

        public void Save(StreamWriter w)
        {
            // Описание сценария состоит из следующих элементов:
            // Название сценария
            w.WriteLine(Title);
            // Тип резервирования
            w.WriteLine(Convert(scenarioType));
            // Использование архиватора
            w.WriteLine(Zip);
            // Путь назначения
            w.WriteLine(Destination);
            // Дата последнего запуска
            w.WriteLine(LastTime.ToString());
            // Список источников данных
            int N;
            N = Source.Count;
            w.WriteLine(N);
            for (int n = 0; n < N; n++)
                w.WriteLine(Source[n]);
            // Список расписания
            N = Shedule.Count;
            w.WriteLine(N);
            for (int n = 0; n < N; n++)
                w.WriteLine(Shedule[n]);
        }

        public void Save(ref string[] value)
        {
            List<string> temp = new List<string>();
            string summary = "";
            // Описание сценария состоит из следующих элементов:
            // Название сценария
            //temp.Add(Title);
            summary += Title + Environment.NewLine;
            // Тип резервирования
            //temp.Add(Convert(scenarioType));
            summary += Convert(scenarioType) + Environment.NewLine;
            // Использование архиватора
            //temp.Add(Zip.ToString());
            summary += Zip.ToString() + Environment.NewLine;
            // Путь назначение
            //temp.Add(Destination);
            summary += Destination + Environment.NewLine;
            // Дата последнего запуска
            //temp.Add(LastTime.ToString());
            summary += LastTime.ToString() + Environment.NewLine;
            // Список источников данных
            int N;
            N = Source.Count;
            //temp.Add(N.ToString());
            summary += N.ToString() + Environment.NewLine;
            for (int n = 0; n < N; n++)
                temp.Add(Source[n]);
            // Список расписания
            N = Shedule.Count;
            //temp.Add(N.ToString());
            summary += N.ToString() + Environment.NewLine;
            for (int n = 0; n < N; n++)
            {
                //temp.Add(Shedule[n]);
                summary += N.ToString() + Environment.NewLine;
            }

            temp.Add(summary);

            int Len = value.Length;
            Array.Resize(ref value, Len + temp.Count);
            foreach (string s in temp)
                value[Len++] = s;
        }

        // Получить название сценария
        public override string ToString()
        {
            return Title;
        }

        // Получить список источников данных
        public string[] Sources
        {
            get { return Source.ToArray<string>(); }
        }

        bool CanStart(string value)
        {
            // Строка расписания применима? Если да -  получить тип и ограничения
            string[] ss = value.Split('\t');
            string PeriodText = ss[0];
            string DateFrom = "";
            string DateTo = "";
            string TimeFrom = "";
            string TimeTo = "";

            if (ss[1] != "")
            {
                string[] sss = ss[1].Split(' ');
                DateFrom = sss[2];
                DateTo = sss[4];
            };

            if (ss[2] != "")
            {
                string[] sss = ss[2].Split(' ');
                TimeFrom = sss[2];
                TimeTo = sss[4];
            }

            DateTime CurrentDate  = DateTime.Today;
            TimeSpan CurrentTime = DateTime.Now.TimeOfDay;

            if (DateFrom != "")
            {
                // Если ограничение даты применимо, вернуть "нельзя"
                DateTime dateFrom = DateTime.Parse(DateFrom).Date;
                DateTime dateTo = DateTime.Parse(DateTo).Date;
                if (CurrentDate < dateFrom || CurrentDate >= dateTo) return false; 
            }

            if (TimeFrom != "")
            {
                // Если ограничение времени применимо, вернуть "нельзя"
                TimeSpan timeFrom = TimeSpan.Parse(TimeFrom);
                TimeSpan timeTo = TimeSpan.Parse(TimeTo);
                if (CurrentTime < timeFrom || CurrentTime > timeTo) return false;
            }

            //Узнать интервал времени, если "последний запуск + интервал" раньше, чем "сейчас"
            TimeSpan Delta = DateTime.Now - LastTime; // Сколько прошло времени?
            string[] Verbose = {"Каждый час", "Каждый день", "Каждую неделю", "Каждый месяц", "Каждый год"};
            int []Really = { 3600,3600*24,3600*24*7,3600*24*30,3600*24*365};
            int index = Verbose.ToList().IndexOf(PeriodText);

            if (index < 0)
                return false; // Явно какая-то ошибка в описании

            DateTime NextStart = LastTime.AddSeconds(Really[index]);

            // Вернуть "Пора!"
            return NextStart < DateTime.Now;
        }

        // Может ли стартовать сейчас?
        public bool CanStart()
        {
            // Сценарий готов к запуску, если ХОТЯ БЫ ОДНО из расписаний готово к запуску
            foreach (string s in Shedule)
                if (CanStart(s))
                    return true;
            return false;
        }
    }

    //-----------------------------------------------------------------------------------------//

    // Класс "Список сценариев"
    class ScenarioList
    {
        public List<Scenario> list = new List<Scenario>();
        
        public void Add(Scenario scenario)
        {
            list.Add(scenario);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public void Load(string FileName)
        {
            StreamReader f = new StreamReader(FileName);
            list = new List<Scenario>();
            while (!f.EndOfStream)
            {
                Scenario s = new Scenario(f);
                list.Add(s);
            }
            f.Close();
        }

        public void Load(ref string[] value)
        {
            list = new List<Scenario>();
            while (value.Length > 0)
            {
                Scenario s = new Scenario(ref value);
                list.Add(s);
            }
        }

        public void Save(string FileName)
        {
            StreamWriter f = new StreamWriter(FileName);
            foreach (Scenario s in list)
                s.Save(f);
            f.Close();
        }

        public void Save(ref string[] value)
        {
            value = new string[0];
            foreach (Scenario s in list)
                s.Save(ref value);
        }

        public Scenario this[int index]
        {
            get { return list[index]; }
        }

        public int Count
        {
            get { return list.Count; }
        }
    }
}
