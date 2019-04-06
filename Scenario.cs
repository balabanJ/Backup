using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Backup
{
    //Класс "сценарий"
    enum ScenarioType {зеркальный, инкрементальный, дифференциальный, полный};
    class Scenario
    {
        public ScenarioType scenarioType = ScenarioType.зеркальный; //Тип копирования
        public string Title = "Новый"; //Название сценария
        public bool Zip = false; //Использовать архиватор
        public string Destination = ""; //Путь назначения
        public List<string> Shedule = new List<string>(); //Расписания
        public List<string> Source = new List<string>(); //Источники данных

        static ScenarioType Convert(string s)
        {
            ScenarioType result = ScenarioType.полный;
            if (s == "Инкрементальный") result = ScenarioType.инкрементальный;
            if (s == "Дифференциальный") result = ScenarioType.дифференциальный;
            if (s == "Зеркальный") result = ScenarioType.зеркальный;
            return result;
        }

        static string Convert(ScenarioType s)
        {
            string result;
            switch (s)
            {
                case ScenarioType.дифференциальный: result= "Дифференциальный"; break;
                case ScenarioType.инкрементальный: result = "Инкрементальный"; break;
                case ScenarioType.зеркальный: result = "Зеркальный"; break;
                default: result = "Полный"; break;
            }
            return result;
        }

        public Scenario()
        {

        }

        public Scenario(StreamReader r)
        {
            Load(r);
        }

        public void Load(StreamReader r)
        {
            //Описание сценария состоит из
            //Заголовок
            Title = r.ReadLine();
            //Тип
            scenarioType = Convert(r.ReadLine());
            //Упакованность
            Zip = bool.Parse(r.ReadLine());
            //Назначение
            Destination = r.ReadLine();
            //список источников
            int N;
            N = int.Parse(r.ReadLine());
            Source = new List<string>();
            for (int n = 0; n < N; n++)
                Source.Add(r.ReadLine());
            //список раписаний
            N = int.Parse(r.ReadLine());
            Shedule = new List<string>();
            for (int n = 0; n < N; n++)
                Shedule.Add(r.ReadLine());
        }

        public void Save(StreamWriter w)
        {
            //Описание сценария состоит из
            //Заголовок
            w.WriteLine(Title);
            //Тип
            w.WriteLine(Convert(scenarioType));
            //Упакованность
            w.WriteLine(Zip);
            //Назначение
            w.WriteLine(Destination);
            //список источников
            int N;
            N = Source.Count;
            w.WriteLine(N);
            for (int n = 0; n < N; n++)
                w.WriteLine(Source[n]);
            //список раписаний
            N = Shedule.Count;
            w.WriteLine(N);
            for (int n = 0; n < N; n++)
                w.WriteLine(Shedule[n]);
        }

        public override string ToString()
        {
            return Title;
        }

        public string[] Sources
        {
            get { return Source.ToArray<string>(); }
        }

    }

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

        public void LoadFromSQLite()
        {
            ///!!!
        }

        public void Save(string FileName)
        {
            StreamWriter f = new StreamWriter(FileName);
            foreach (Scenario s in list)
                s.Save(f);
            f.Close();
        }

        public void SaveToSQLite()
        {
            ///!!!
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
