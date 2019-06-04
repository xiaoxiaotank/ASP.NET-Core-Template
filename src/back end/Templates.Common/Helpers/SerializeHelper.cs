using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Xml.Serialization;

namespace Templates.Common.Helpers
{
    public class SerializeHelper
    {
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static string Serialize<T>(T target, SerializeType type) where T : class
        {
            using (var ms = new MemoryStream())
            {
                switch (type)
                {
                    case SerializeType.Binary:
                        new BinaryFormatter().Serialize(ms, target);
                        break;
                    case SerializeType.Json:
                        new DataContractJsonSerializer(typeof(T)).WriteObject(ms, target);
                        break;
                    case SerializeType.Xml:
                        new XmlSerializer(typeof(T)).Serialize(ms, target);
                        break;
                    default:
                        throw new ArgumentException($"无效的序列化类型:{type}", nameof(type));
                }
                ms.Seek(0, SeekOrigin.Begin);
                return Convert.ToBase64String(ms.ToArray());
            }
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <returns></returns>
        public static T Derialize<T>(string target, SerializeType type)
        {
            using (var ms = new MemoryStream(Convert.FromBase64String(target)))
            {
                object result;
                switch (type)
                {
                    case SerializeType.Binary:
                        result = new BinaryFormatter().Deserialize(ms);
                        break;
                    case SerializeType.Json:
                        result = new DataContractJsonSerializer(typeof(T)).ReadObject(ms);
                        break;
                    case SerializeType.Xml:
                        result = new XmlSerializer(typeof(T)).Deserialize(ms);
                        break;
                    default:
                        throw new ArgumentException($"无效的序列化类型:{type}", nameof(type));
                }
                return (T)result;
            }
        }
    }

    public enum SerializeType
    {
        Binary,
        Json,
        Xml
    }
}