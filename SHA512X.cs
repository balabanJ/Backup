/*
 * Пространство имен System.IO содержит типы, позволяющие осуществлять чтение и запись в файлы и потоки данных, 
 * а также типы для базовой поддержки файлов и папок.
 */
using System.IO;
/*
 * Пространство имен System.Security.Cryptography предоставляет криптографические службы, 
 * включающие безопасное кодирование и декодирование данных, а также целый ряд других функций, 
 * таких как хэширование, генерация случайных чисел и проверка подлинности сообщений. 
 */
using System.Security.Cryptography;


namespace Backup
{
    // Класс "SHA512X"
    class SHA512X
    {
        public static byte[] CreateSHA512(byte[] data)
        {
            byte[] result;

            SHA512 shaM = new SHA512Managed();
            result = shaM.ComputeHash(data);

            shaM.Dispose();

            return result;
        }

        public static bool CheckSHA512(byte[] data, byte[] control)
        {
            byte[] result;

            SHA512 shaM = new SHA512Managed();
            result = shaM.ComputeHash(data);

            shaM.Dispose();

            for (int k = 0; k < result.Length; k++)
                if (result[k] != control[k])
                    return false;

            return true;
        }

        public static byte[] CreateSHA512(string filename, bool CreateFile = true)
        {
            if (Path.GetExtension(filename).ToLower() == ".sha512")
                return new byte[0]; // Не будем создавать контрольную сумму для контрольной суммы 

            byte[] data = File.ReadAllBytes(filename);
            byte[] result = CreateSHA512(data);

            if (CreateFile)
                File.WriteAllBytes(filename + ".sha512", result);

            return result;
        }

        public static bool CheckSHA512(string filename, string controlfilename="")
        {
            if (controlfilename == "")
                controlfilename = filename + ".sha512";

            if (!File.Exists(controlfilename))
                return false;

            if (!File.Exists(filename))
                return false;

            byte[] control = File.ReadAllBytes(controlfilename);
            byte[] data = File.ReadAllBytes(filename);

            return CheckSHA512(data,control);
        }
    }
}
