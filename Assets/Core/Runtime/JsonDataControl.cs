//------------------------------------------------------------
// 脚本名:		JsonDataControl.cs
// 作者:			海星
// 描述:			基于Newtonsoft的Json序列化和反序列化
//------------------------------------------------------------

using UnityEngine;
using Newtonsoft.Json;
using System.IO;

namespace StarFramework.Runtime
{

    public class JsonDataControl
    {
        /// <summary>
        /// 运行时保存路径 
        /// </summary>
        public static string RuntimePath = Application.persistentDataPath + "/Save/";

        /// <summary>
        /// 自定义的后缀名
        /// </summary>
        public static string suffix = ".star";

        /// <summary>
        /// 序列化
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="dataObject">目标数据类</param>
        /// <param name="fileName">要保存的文件名</param>
        public static void JsonSerialize<T>(T dataObject, string fileName)
        {
            var data = JsonConvert.SerializeObject(dataObject, Formatting.Indented);
            byte[] binaryData = System.Text.Encoding.UTF8.GetBytes(data);

            string filePath = Path.Combine(RuntimePath, fileName + suffix);

            if (!Directory.Exists(Path.GetDirectoryName(filePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            }

            File.WriteAllBytes(filePath, binaryData);
            Debug.Log("序列化数据路径" + filePath);
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="fileName">要读取的文件名</param>
        /// <returns></returns>
        public static T JsonDeSerialize<T>(string fileName)
        {
            string filePath = Path.Combine(RuntimePath + fileName + suffix);

            if (File.Exists(filePath))
            {
                byte[] binaryData = File.ReadAllBytes(filePath);
                string jsonData = System.Text.Encoding.UTF8.GetString(binaryData);
                Debug.Log("从运行时路径进行读取" + filePath);

                return JsonConvert.DeserializeObject<T>(jsonData);
            }
            else
            {
                Debug.LogWarning("警告，没有读取到任何数据，返回初始数据");
                return default;
            }
        }

        /// <summary>
        /// 反序列化 并返回json
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="fileName">要读取的文件名</param>
        /// <param name="json">返回的json数据</param>
        /// <returns></returns>
        public static T JsonDeSerialize<T>(string fileName, out string json)
        {
            string filePath = Path.Combine(RuntimePath + fileName + suffix);

            if (File.Exists(filePath))
            {
                byte[] binaryData = File.ReadAllBytes(filePath);
                string jsonData = System.Text.Encoding.UTF8.GetString(binaryData);
                json = jsonData;
                Debug.Log("从运行时路径进行读取" + filePath);
                return JsonConvert.DeserializeObject<T>(jsonData);
            }
            else
            {
                Debug.LogWarning("警告，没有读取到任何数据，返回初始数据");
                json = string.Empty;
                return default;
            }
        }
    }
}
