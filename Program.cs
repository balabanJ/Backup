/*
 * Пространство имен System содержит фундаментальные и базовые классы, 
 * определяющие часто используемые типы значений и ссылочных данных, события и обработчики событий, 
 * интерфейсы, атрибуты и исключения обработки.
 */
using System;
/*
 * Пространство имен System.Windows.Forms содержит классы для создания приложений Windows, которые позволяют 
 * наиболее эффективно использовать расширенные возможности пользовательского интерфейса, 
 * доступные в операционной системе Microsoft Windows.
 */
using System.Windows.Forms;

namespace Backup
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        // Указывает, что потоковая модель COM для приложения является однопотоковым подразделением (STA)
        [STAThread]
        static void Main()
        {
            // Включает визуальные стили для приложения
            Application.EnableVisualStyles();
            // Если задано значение false, новые элементы управления используют класс TextRenderer, основанный на GDI
            Application.SetCompatibleTextRenderingDefault(false);
            // Запускает стандартный цикл обработки сообщений приложения в текущем потоке и делает указанную форму видимой
            Application.Run(new Form1());
        }
    }
}
