using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Расписание

namespace Backup
{

    class Shedule
    {
        public List<string> list = new List<string>(); //Список правил 

        public Shedule(string s)
        {

        }

        public override string ToString()
        {
            return "Расписание";
        }

        public void Add(string shed)
        {

        }

        public void RemoveAt(int index)
        {

        }

        public bool CanDoItNow() //Сейчас применимо хоть одно?
        {
            return false;
        }

    }
}
