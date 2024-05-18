using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Numerics;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Channels;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class Program
{
    private static Type GetPropertyType(object value)
    {
        if (value is int)
            return typeof(int);
        else if (value is string)
            return typeof(string);
        else if (value is bool)
            return typeof(bool);
        else if (value is DateTime)
            return typeof(DateTime);
        else if (value is IList)
            return typeof(IList);
        else if (value is IDictionary)
            return typeof(IDictionary);
        else
            return typeof(object);
    }
    //old code
    public static void Main()
    {
        Console.WriteLine("--> reading json file");
        string jsonFilePath = @"./Json/ocelot.json";
        //string jsonFilePath = @"./Json/Data.json";
        try
        {
            string jsonData = File.ReadAllText(jsonFilePath);
            JToken token = JToken.Parse(jsonData);
            if (token.Type.ToString().ToLower() == "object")
            {
                JArray array = new JArray(token);
                jsonData = array.ToString();
            }
            List<dynamic> dynamicList = JsonConvert.DeserializeObject<List<dynamic>>(jsonData);
            List<Tuple<string, string, dynamic>> Records = new List<Tuple<string, string, dynamic>>();
            foreach (var dynamicObjectList in dynamicList)
            {
                foreach (var item in dynamicObjectList)
                {
                    string Type = item.Value.Type == null ? "object" : item.Value.Type.ToString();
                    string Name = item.Name.ToString();
                    dynamic Value = item.Value;
                    // Check if a tuple with the same key already exists in Records
                    var existingTuple = Records.FirstOrDefault(t => t.Item2 == Name);

                    if (existingTuple != null)
                    {
                        // Update the existing tuple with new data
                        if (Type.ToLower() != "null")
                        {
                            if (Type.ToLower() == "array")
                            {
                                dynamic childTuple = ProcessNestedArray(Value);
                                Records[Records.IndexOf(existingTuple)] = new Tuple<string, string, dynamic>(Type, Name, childTuple);
                            }
                            else if (Type.ToLower() == "object")
                            {
                                dynamic childTuple = ProcessNestedObject(Value);
                                Records[Records.IndexOf(existingTuple)] = new Tuple<string, string, dynamic>(Type, Name, childTuple);
                            }
                            else
                            {
                                Records[Records.IndexOf(existingTuple)] = new Tuple<string, string, dynamic>(Type, Name, item.Value.Value);
                            }
                        }
                    }
                    else
                    {
                        // Add a new tuple if a tuple with the same key does not exist
                        if (Type.ToLower() == "array")
                        {
                            dynamic childTuple = ProcessNestedArray(Value);
                            Records.Add(new Tuple<string, string, dynamic>(Type, Name, childTuple));
                        }
                        else if (Type.ToLower() == "object")
                        {
                            dynamic childTuple = ProcessNestedObject(Value);
                            Records.Add(new Tuple<string, string, dynamic>(Type, Name, childTuple));
                        }
                        else
                        {
                            Records.Add(new Tuple<string, string, dynamic>(Type, Name, item.Value.Value));
                        }
                    }
                }
            }

            Console.WriteLine("\n");
            List<List<Tuple<string, List<Tuple<string, string>>>>> dict = ProcessRecords(Records);
            dict.Reverse();
            if (dict.Count > 0)
            {
                foreach (var nestedList in dict)
                {
                    foreach (var pair in nestedList)
                    {
                        StringBuilder nsb = new StringBuilder();
                        string ClassName = pair.Item1;
                        foreach (var propitem in pair.Item2)
                        {
                            string Name = propitem.Item1;
                            string Type = propitem.Item2;
                            nsb.Append(GetPropertyString(Type, Name));
                        }
                        Console.WriteLine(ConvertStringBuilderToEntity(nsb, ClassName));
                    }
                }
            }
        }
        catch (Exception ex) { Console.WriteLine(ex.Message); }

    }
    static List<List<Tuple<string, List<Tuple<string, string>>>>> ProcessRecords(List<Tuple<string, string, dynamic>> records, int level = 1, string keyname = "")
    {
        List<List<Tuple<string, List<Tuple<string, string>>>>> result = new List<List<Tuple<string, List<Tuple<string, string>>>>>();
        List<Tuple<string, List<Tuple<string, string>>>> currentList = new List<Tuple<string, List<Tuple<string, string>>>>();
        List<Tuple<string, string>> props = new List<Tuple<string, string>>();

        foreach (var record in records)
        {
            string Type = record.Item1;
            string Name = record.Item2;
            dynamic Value = record.Item3;

            if (Type.ToLower() == "array")
            {
                props.Add(new Tuple<string, string>(Name, Value.Count > 0?Type:"listobj"));
                if (Value.Count > 0)
                {
                    string arrayMemType = Value[0].GetType().BaseType.Name.ToString();
                    if (arrayMemType.ToLower() != "jtoken")
                    {
                        var res = ProcessRecords(Value, level + 1, Name);
                        result.AddRange(res);

                    }
                }
            }
            else if (Type.ToLower() == "object")
            {
                props.Add(new Tuple<string, string>(Name, Type));
                var res = ProcessRecords(Value, level + 1, Name);
                result.AddRange(res);
            }
            else
            {
                props.Add(new Tuple<string, string>(Name, Type));
            }
        }
        currentList.Add(new Tuple<string, List<Tuple<string, string>>>(keyname, props));
        result.Add(currentList);

        return result;
    }
    static dynamic ProcessNestedArray(dynamic array, int depth = 1)
    {
        List<dynamic> nestedRecords = new List<dynamic>();
        List<Tuple<string, string, dynamic>> Records = new List<Tuple<string, string, dynamic>>();
        foreach (var nestedItem in array)
        {
            foreach (var item in nestedItem)
            {
                string Type = item.Value.Type == null ? "object" : item.Value.Type.ToString();
                string Name = item.Name.ToString();
                dynamic Value = item.Value;

                var existingTuple = Records.FirstOrDefault(t => t.Item2 == Name);
                if (existingTuple != null)
                {
                    if (Type.ToLower() != "null")
                    {
                        if (Type.ToLower() == "array")
                        {
                            string arrayValueType = item.Value.First.Type == null ? "object" : item.Value.First.Type.ToString();
                            if (arrayValueType.ToLower() == "string" || arrayValueType.ToLower() == "integer" ||
                            arrayValueType.ToLower() == "float" || arrayValueType.ToLower() == "decimal")
                            {
                                Records[Records.IndexOf(existingTuple)] = new Tuple<string, string, dynamic>(Type, Name, Value);
                            }
                            // Recursively process nested arrays
                            else
                            {
                                Records[Records.IndexOf(existingTuple)] = new Tuple<string, string, dynamic>(Type, Name, ProcessNestedArray(Value, depth + 1));
                            }
                        }
                        else if (Type.ToLower() == "object")
                        {
                            Records[Records.IndexOf(existingTuple)] = new Tuple<string, string, dynamic>(Type, Name, ProcessNestedObject(Value));
                        }
                        else
                        {
                            Records[Records.IndexOf(existingTuple)] = new Tuple<string, string, dynamic>(Type, Name, item.Value.Value);
                        }
                    }
                }
                else
                {
                    if (Type.ToLower() == "array")
                    {

                        string arrayValueType = item.Value.First.Type == null ? "object" : item.Value.First.Type.ToString();
                        if (arrayValueType.ToLower() == "string" || arrayValueType.ToLower() == "integer" ||
                            arrayValueType.ToLower() == "float" || arrayValueType.ToLower() == "decimal")
                        {
                            Records.Add(new Tuple<string, string, dynamic>(Type, Name, Value));
                        }
                        else
                        {
                            // Recursively process nested arrays
                            Records.Add(new Tuple<string, string, dynamic>(Type, Name, ProcessNestedArray(Value, depth + 1)));
                        }
                    }
                    else if (Type.ToLower() == "object")
                    {
                        Records.Add(new Tuple<string, string, dynamic>(Type, Name, ProcessNestedObject(Value)));
                    }
                    else
                    {
                        Records.Add(new Tuple<string, string, dynamic>(Type, Name, item.Value.Value));
                    }
                }
            }
        }
        return Records;
    }
    static dynamic ProcessNestedObject(JToken obj)
    {
        List<Tuple<string, string, dynamic>> Records = new List<Tuple<string, string, dynamic>>();

        foreach (var property in obj)
        {
            string Type = property.First.Type == JTokenType.Object ? "object" : property.First.Type.ToString();
            string[] namestrings = property.Path.Split('.');
            string Name = namestrings[namestrings.Length - 1].ToString();
            dynamic Value = property.First;

            if (Type.ToLower() == "object")
            {
                Value = ProcessNestedObject(Value);
            }
            else if (Type.ToLower() == "array")
            {
                Value = ProcessNestedArray(Value);
            }

            // Add to records
            Records.Add(new Tuple<string, string, dynamic>(Type, Name, Value));
        }

        return Records;
    }

    public static string GetPropertyString(string type, string propertyName)
    {
        string propertyString = "";

        switch (type.ToLower())
        {
            case "integer":
                propertyString = $"\tpublic int {propertyName} {{ get; set; }}\n";
                break;
            case "string":
                propertyString = $"\tpublic string {propertyName} {{ get; set; }}\n";
                break;
            case "decimal":
                propertyString = $"\tpublic decimal {propertyName} {{ get; set; }}\n";
                break;
            case "datetime":
                propertyString = $"\tpublic datetime {propertyName} {{ get; set; }}\n";
                break;
            case "array":
                propertyString = $"\tpublic List<{propertyName}> {propertyName} {{ get; set; }}\n";
                break;
            case "char":
                propertyString = $"\tpublic char {propertyName} {{ get; set; }}\n";
                break;
            case "boolean":
                propertyString = $"\tpublic bool {propertyName} {{ get; set; }}\n";
                break;
            case "float":
                propertyString = $"\tpublic float {propertyName} {{ get; set; }}\n";
                break;
            case "object":
                propertyString = $"\tpublic {propertyName} {propertyName} {{ get; set; }}\n";
                break;
            case "listobj":
                propertyString = $"\tpublic List<Object> {propertyName} {{ get; set; }}\n";
                break;
            default:
                propertyString = $"\tpublic Object {propertyName} {{ get; set; }}\n";
                break;
        }

        return propertyString;
    }

    public static string ConvertStringBuilderToEntity(StringBuilder sb, string name)
    {
        StringBuilder entityClass = new StringBuilder();
        name = name != "" ? name : "root";
        // Start of the class
        entityClass.AppendLine($"public class {name}");
        entityClass.AppendLine("{");

        // Append the StringBuilder strings to the class
        entityClass.AppendLine(sb.ToString());

        // End of the class
        entityClass.AppendLine("}");

        return entityClass.ToString();
    }
}

