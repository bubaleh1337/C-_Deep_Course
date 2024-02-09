/*https://t.me/csharpisti

Напишите приложение, конвертирующее произвольный JSON в XML. Используйте JsonDocument. */
using System;
using System.IO;
using System.Text.Json;
using System.Xml;

namespace JsonToXmlConverter
{
    class Program
    {
        static readonly string JsonFilePath = "jsonfile.json";

        static void Main(string[] args)
        {
            try
            {
                string jsonInput = File.ReadAllText(JsonFilePath);
                ConvertJsonToXml(jsonInput);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"Файл '{JsonFilePath}' не найден.");
            }
            catch (IOException e)
            {
                Console.WriteLine($"Ошибка при чтении файла: {e.Message}");
            }
        }

        static void ConvertJsonToXml(string jsonInput)
        {
            try
            {
                using (JsonDocument doc = JsonDocument.Parse(jsonInput))
                {
                    using (MemoryStream stream = new MemoryStream())
                    {
                        using (XmlWriter writer = XmlWriter.Create(stream, new XmlWriterSettings { Indent = true }))
                        {
                            writer.WriteStartDocument();
                            writer.WriteStartElement("root");
                            WriteJsonToXml(doc.RootElement, writer);
                            writer.WriteEndElement();
                            writer.WriteEndDocument();
                        }

                        stream.Position = 0;
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            string xmlOutput = reader.ReadToEnd();
                            Console.WriteLine("Результат конвертации в XML:");
                            Console.WriteLine(xmlOutput);
                        }
                    }
                }
            }
            catch (JsonException e)
            {
                Console.WriteLine($"Ошибка при разборе JSON: {e.Message}");
            }
        }

        static void WriteJsonToXml(JsonElement element, XmlWriter writer)
        {
            
            switch (element.ValueKind)
            {
                case JsonValueKind.Object:
                    writer.WriteStartElement("object");
                    foreach (JsonProperty prop in element.EnumerateObject())
                    {
                        writer.WriteStartElement("property");
                        writer.WriteAttributeString("name", prop.Name);
                        WriteJsonToXml(prop.Value, writer);
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                    break;
                case JsonValueKind.Array:
                    writer.WriteStartElement("array");
                    foreach (JsonElement item in element.EnumerateArray())
                    {
                        WriteJsonToXml(item, writer);
                    }
                    writer.WriteEndElement();
                    break;
                case JsonValueKind.String:
                    writer.WriteStartElement("string");
                    writer.WriteString(element.GetString());
                    writer.WriteEndElement();
                    break;
                case JsonValueKind.Number:
                    writer.WriteStartElement("number");
                    writer.WriteString(element.GetRawText());
                    writer.WriteEndElement();
                    break;
                case JsonValueKind.True:
                case JsonValueKind.False:
                    writer.WriteStartElement("boolean");
                    writer.WriteString(element.GetBoolean().ToString().ToLower());
                    writer.WriteEndElement();
                    break;
                case JsonValueKind.Null:
                    writer.WriteStartElement("null");
                    writer.WriteEndElement();
                    break;
                default:
                    throw new JsonException($"Неизвестный тип значения: {element.ValueKind}");
            }
        }
    }
}
