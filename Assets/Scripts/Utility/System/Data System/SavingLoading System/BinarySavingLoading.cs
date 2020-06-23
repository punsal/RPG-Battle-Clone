using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Utility.System.Data_System.SavingLoading_System
{
    public static class BinarySavingLoading
    {
        public static void Save<T1, T2>(this T1 type2, string fileName, string fileExtension) where T2 : class
        {
            var path = $"{Application.persistentDataPath}/{fileName}.{fileExtension}";
            var formatter = new BinaryFormatter();
            var fileStream = new FileStream(path, FileMode.Create);

            var data = (T2) Activator.CreateInstance(typeof(T2), type2);

            formatter.Serialize(fileStream, data);
            fileStream.Close();
        }

        public static T Load<T>(string fileName, string fileExtension) where T : class
        {
            var path = $"{Application.persistentDataPath}/{fileName}.{fileExtension}";
            if (File.Exists(path))
            {
                var formatter = new BinaryFormatter();
                var fileStream = new FileStream(path, FileMode.Open);

                var data = formatter.Deserialize(fileStream) as T;
                fileStream.Close();

                return data;
            }

            Debug.LogError($"Save File not found in : {path}");
            return null;
        }
    }
}