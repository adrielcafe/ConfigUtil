/*
    Copyright (c) 2012 Adriel Café <ac@adrielcafe.com>
    GitHub Repository: http://github.com/adrielcafe/ConfigUtil
*/

using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Config 
{
	public sealed class ConfigUtil
	{
        /// <summary>
        /// Config filename
        /// </summary>
        public static string fileName = "config.xml";

		private static string configFilePath = AppDomain.CurrentDomain.BaseDirectory + fileName;
		private static UTF8Encoding utf8 = new UTF8Encoding();
		private static XmlSerializer serializer = null;
		private static XmlTextReader reader = null;
		private static XmlTextWriter writer = null;
		private static MemoryStream stream = null;
		private static string xmlContent = null;

        /// <summary>
        /// Loads the config file and deserialize him for the informed type
        /// </summary>
        /// <typeparam name="T">Type that will be used in deserialization</typeparam>
        /// <returns>An object from the informed type</returns>
		public static T Load<T>()
		{
			if (File.Exists(configFilePath))
				try
				{
                    object config = null;
                    serializer = new XmlSerializer(typeof(T));
					reader = new XmlTextReader(configFilePath);
					config = serializer.Deserialize(reader);
					reader.Close();
                    return (T)config;
				}
				catch (Exception e)
                {
                    Console.WriteLine("Error while loading config file: " + e.Message);
					try { reader.Close(); } catch { }
                    return (T)new object();
				}
            else 
            {
                Console.WriteLine("Config file not found");
                return (T)new object();
            }
		}

        /// <summary>
        /// Saves the informed object in a XML file
        /// </summary>
        /// <param name="config">The object that will be saved</param>
        /// <returns>True if success</returns>
		public static bool Save(object config)
		{
			if (config != null)
				try
				{
                    serializer = new XmlSerializer(config.GetType());
					stream = new MemoryStream();
					writer = new XmlTextWriter(stream, Encoding.UTF8);
					writer.Formatting = Formatting.Indented;
				
					serializer.Serialize(writer, config);
					stream = (MemoryStream)writer.BaseStream;
					xmlContent = utf8.GetString(stream.ToArray());
					stream.Close();

					File.WriteAllText(configFilePath, xmlContent);
                    return true;
				}
				catch (Exception e) 
                { 
                    Console.WriteLine("Error saving config file: " + e.Message);
                    return false;
                }
            else
            {
                Console.WriteLine("Config object is NULL");
                return false;
            }
		}
	}
}