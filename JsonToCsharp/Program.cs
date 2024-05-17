using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

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
        string jsonFilePath = @"./Json/TestData.json";// @"./Json/Data.json";
        try
        {
            string jsonData = File.ReadAllText(jsonFilePath);
            List<dynamic> dynamicList = JsonConvert.DeserializeObject<List<dynamic>>(jsonData);
            List<Tuple<string, string, dynamic[]>> Records = new List<Tuple<string, string, dynamic[]>>();
            foreach (var dynamicObjectList in dynamicList)
            {
                foreach (var item in dynamicObjectList)
                {
                    string Type = item.Value.Type.ToString();
                    string Name = item.Name.ToString();

                    // Check if a tuple with the same key already exists in Records
                    var existingTuple = Records.FirstOrDefault(t => t.Item2 == Name);

                    if (existingTuple != null)
                    {
                        // Update the existing tuple with new data
                        if (Type.ToLower() != "null")
                        {
                            if (Type.ToLower() == "array")
                            {
                                dynamic[] childTokens = item.Value.ToObject<dynamic[]>();
                                Records[Records.IndexOf(existingTuple)] = new Tuple<string, string, dynamic[]>(Type, Name, item.Value.ToObject<dynamic[]>());
                            }
                            else
                            {
                                Records[Records.IndexOf(existingTuple)] = new Tuple<string, string, dynamic[]>(Type, Name, new dynamic[] { item.Value.Value });
                            }
                        }
                    }
                    else
                    {
                        // Add a new tuple if a tuple with the same key does not exist
                        if (Type.ToLower() == "array")
                        {
                            dynamic[] childTokens = item.Value.ToObject<dynamic[]>();
                            Records.Add(new Tuple<string, string, dynamic[]>(Type, Name, item.Value.ToObject<dynamic[]>()));
                        }
                        else
                        {
                            Records.Add(new Tuple<string, string, dynamic[]>(Type, Name, new dynamic[] { item.Value.Value }));
                        }
                    }
                }
            }

            Dictionary<string, dynamic[]> dict = new Dictionary<string, dynamic[]>();
            Console.WriteLine("\n\n");
            StringBuilder sb = new StringBuilder();
            foreach (var item in Records)
            {
                string Type = item.Item1;
                string Name = item.Item2;
                dynamic[] Value = item.Item3;

                sb.Append(GetPropertyString(Type, Name));

                if (Type.ToLower() == "array")
                {
                    dict.Add(Name, Value);
                }
            }
            Console.WriteLine("\n\n");
            Console.WriteLine(ConvertStringBuilderToEntity(sb, ""));
            if (dict.Count > 0)
            {
                foreach (var pair in dict)
                {
                    StringBuilder nsb = new StringBuilder();
                    foreach (var childpair in pair.Value[0])
                    {
                        string Type = childpair.Value.Type.ToString();
                        string Name = childpair.Name.ToString();
                        nsb.Append(GetPropertyString(Type, Name));
                    }
                    Console.WriteLine(ConvertStringBuilderToEntity(nsb, pair.Key));
                }
            }
        }
        catch (Exception ex) { Console.WriteLine(ex.Message); }

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
                propertyString = $"\tpublic List<{propertyName}> {propertyName}List {{ get; set; }}\n";
                break;
            case "char":
                propertyString = $"\tpublic char {propertyName} {{ get; set; }}\n";
                break;
            case "bool":
                propertyString = $"\tpublic bool {propertyName}List {{ get; set; }}\n";
                break;
            default:
                propertyString = $"\tpublic object {propertyName} {{ get; set; }}\n";
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

