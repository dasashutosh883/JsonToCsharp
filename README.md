

# JSON to C# Class Model Generator

This C# console application converts a JSON response into a C# class model for easy integration and data handling.

## Features

- **JSON to C# Class Model:** Automatically generates C# class models from JSON responses.
- **Easy Integration:** Simplifies the process of creating C# classes based on JSON data structures.
- **Customizable Output:** Allows customization of class names, properties, and namespaces.

## Getting Started

### Prerequisites

- [.NET Core SDK](https://dotnet.microsoft.com/download) installed on your machine.

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/json-to-csharp-model.git
   ```

2. Navigate to the project directory:
   ```bash
   cd json-to-csharp-model
   ```

3. Build the project:
   ```bash
   dotnet build
   ```

### Usage

1. Run the application:
   ```bash
   dotnet run
   ```

2. Enter the JSON response when prompted.

3. The application will generate the corresponding C# class model based on the JSON structure.

### Example

Given JSON Response:
```json
[

  {

    "action": null,

    "inT_LEVELDETAILID": 0,

    "intletterid": 33590804,

    "vchLETTERNO": "1791",

    "intletterno": 1791,

    "vchlettermo": null,

    "vchrecivedate": "06-May-2024",

    "vchfileno": "28491300542024",

    "vchsendby": "17334",

    "intenclosuretype": 0,

    "vcH_DISTRCTID": null,

    "vcH_OFFICETYPE": null,

    "vcH_OFFICEID": null,

    "listMainLetter": null,

    "listENCLOUSERM": null,

    "listENCLOUSERS": null,

    "vchenclosurename": null,

    "intUserid": 0,

    "vchPdfSize": null,

    "intYear": 2024,

    "vcH_SECTIONID": null,

    "vcH_FROMDATE": null,

    "vcH_FROMTODATE": null,

    "vchsubject": "Regarding transfer of RTI application dtd. 27.04.2024 of Sri Priyabrata Panda.",

    "chrpdfstatus": "Y",

    "chrdespatchstatus": "Y",

    "chrdoletter": null,

    "intdono": null,

    "intpdfno": "1791",

    "listMode": [

      {

        "modeName": "EM",

        "modeId": null,

        "listAddressees": null

      },

      {

        "modeName": "SMS",

        "modeId": null,

        "listAddressees": null

      },

      {

        "modeName": "OE",

        "modeId": null,

        "listAddressees": null

      }

    ],

    "tagofficetypE_ID": null,

    "forPublic": null,

    "priority": null,

    "despatchStatus": "0",

    "chrEnclosurestatus": null,

    "chrEnclosureDetails": null,

    "dtmDespatchOn": null

  },

  {

    "action": null,

    "inT_LEVELDETAILID": 0,

    "intletterid": 33590819,

    "vchLETTERNO": "1792",

    "intletterno": 1792,

    "vchlettermo": null,

    "vchrecivedate": "06-May-2024",

    "vchfileno": "28525300862022",

    "vchsendby": "961",

    "intenclosuretype": 0,

    "vcH_DISTRCTID": null,

    "vcH_OFFICETYPE": null,

    "vcH_OFFICEID": null,

    "listMainLetter": null,

    "listENCLOUSERM": null,

    "listENCLOUSERS": null,

    "vchenclosurename": null,

    "intUserid": 0,

    "vchPdfSize": null,

    "intYear": 2024,

    "vcH_SECTIONID": null,

    "vcH_FROMDATE": null,

    "vcH_FROMTODATE": null,

    "vchsubject": "Major achievements, Awards, and Recognition received during the month of April-2024 for appraisal in the All Secretaries meeting scheduled to be held on 7th May2024.",

    "chrpdfstatus": "Y",

    "chrdespatchstatus": "Y",

    "chrdoletter": null,

    "intdono": null,

    "intpdfno": "1792",

    "listMode": [

      {

        "modeName": "EM",

        "modeId": null,

        "listAddressees": null

      },

      {

        "modeName": "SMS",

        "modeId": null,

        "listAddressees": null

      },

      {

        "modeName": "OE",

        "modeId": null,

        "listAddressees": null

      }

    ],

    "tagofficetypE_ID": null,

    "forPublic": null,

    "priority": null,

    "despatchStatus": "0",

    "chrEnclosurestatus": null,

    "chrEnclosureDetails": null,

    "dtmDespatchOn": null

  },

  {

    "action": null,

    "inT_LEVELDETAILID": 0,

    "intletterid": 33592551,

    "vchLETTERNO": "1793",

    "intletterno": 1793,

    "vchlettermo": null,

    "vchrecivedate": "06-May-2024",

    "vchfileno": "28231600042018",

    "vchsendby": "16726",

    "intenclosuretype": 1,

    "vcH_DISTRCTID": null,

    "vcH_OFFICETYPE": null,

    "vcH_OFFICEID": null,

    "listMainLetter": null,

    "listENCLOUSERM": [

      {

        "intletterid": 0,

        "intletterno": 0,

        "vchattachfile": "1793_1.pdf",

        "vchofficename": null,

        "chlorder": null,

        "intdono": 0

      },

      {

        "intletterid": 0,

        "intletterno": 0,

        "vchattachfile": "1793_2.pdf",

        "vchofficename": null,

        "chlorder": null,

        "intdono": 0

      }

    ],

    "listENCLOUSERS": null,

    "vchenclosurename": null,

    "intUserid": 0,

    "vchPdfSize": null,

    "intYear": 2024,

    "vcH_SECTIONID": null,

    "vcH_FROMDATE": null,

    "vcH_FROMTODATE": null,

    "vchsubject": "Status of the projects identified for completion during 2023-24 under Zero\r\nBased Investment Review - Position up to 31.12.2023.",

    "chrpdfstatus": "Y",

    "chrdespatchstatus": "Y",

    "chrdoletter": null,

    "intdono": null,

    "intpdfno": "1793",

    "listMode": [

      {

        "modeName": "EM",

        "modeId": null,

        "listAddressees": null

      },

      {

        "modeName": "SMS",

        "modeId": null,

        "listAddressees": null

      },

      {

        "modeName": "OE",

        "modeId": null,

        "listAddressees": null

      }

    ],

    "tagofficetypE_ID": null,

    "forPublic": null,

    "priority": null,

    "despatchStatus": "0",

    "chrEnclosurestatus": null,

    "chrEnclosureDetails": null,

    "dtmDespatchOn": null

  },

  {

    "action": null,

    "inT_LEVELDETAILID": 0,

    "intletterid": 33592903,

    "vchLETTERNO": "1794",

    "intletterno": 1794,

    "vchlettermo": null,

    "vchrecivedate": "06-May-2024",

    "vchfileno": "28231600042018",

    "vchsendby": "16726",

    "intenclosuretype": 1,

    "vcH_DISTRCTID": null,

    "vcH_OFFICETYPE": null,

    "vcH_OFFICEID": null,

    "listMainLetter": null,

    "listENCLOUSERM": [

      {

        "intletterid": 0,

        "intletterno": 0,

        "vchattachfile": "1794_1.pdf",

        "vchofficename": null,

        "chlorder": null,

        "intdono": 0

      },

      {

        "intletterid": 0,

        "intletterno": 0,

        "vchattachfile": "1794_2.pdf",

        "vchofficename": null,

        "chlorder": null,

        "intdono": 0

      }

    ],

    "listENCLOUSERS": null,

    "vchenclosurename": null,

    "intUserid": 0,

    "vchPdfSize": null,

    "intYear": 2024,

    "vcH_SECTIONID": null,

    "vcH_FROMDATE": null,

    "vcH_FROMTODATE": null,

    "vchsubject": "List of the Projects costing Rs.1.00 Crore above and Rs.10.00 Crore above\r\neach identified for completion during 2024-25 under Zero Based Investment\r\nReview.",

    "chrpdfstatus": "Y",

    "chrdespatchstatus": "Y",

    "chrdoletter": null,

    "intdono": null,

    "intpdfno": "1794",

    "listMode": [

      {

        "modeName": "EM",

        "modeId": null,

        "listAddressees": null

      },

      {

        "modeName": "SMS",

        "modeId": null,

        "listAddressees": null

      },

      {

        "modeName": "OE",

        "modeId": null,

        "listAddressees": null

      }

    ],

    "tagofficetypE_ID": null,

    "forPublic": null,

    "priority": null,

    "despatchStatus": "0",

    "chrEnclosurestatus": null,

    "chrEnclosureDetails": null,

    "dtmDespatchOn": null

  },
]
```

Generated C# Class Model:
```csharp
--> reading json file






public class root
{
        public object action { get; set; }
        public int inT_LEVELDETAILID { get; set; }
        public int intletterid { get; set; }
        public string vchLETTERNO { get; set; }
        public int intletterno { get; set; }
        public object vchlettermo { get; set; }
        public string vchrecivedate { get; set; }
        public string vchfileno { get; set; }
        public string vchsendby { get; set; }
        public int intenclosuretype { get; set; }
        public object vcH_DISTRCTID { get; set; }
        public object vcH_OFFICETYPE { get; set; }
        public object vcH_OFFICEID { get; set; }
        public object listMainLetter { get; set; }
        public List<listENCLOUSERM> listENCLOUSERMList { get; set; }
        public object listENCLOUSERS { get; set; }
        public object vchenclosurename { get; set; }
        public int intUserid { get; set; }
        public object vchPdfSize { get; set; }
        public int intYear { get; set; }
        public object vcH_SECTIONID { get; set; }
        public object vcH_FROMDATE { get; set; }
        public object vcH_FROMTODATE { get; set; }
        public string vchsubject { get; set; }
        public string chrpdfstatus { get; set; }
        public string chrdespatchstatus { get; set; }
        public object chrdoletter { get; set; }
        public object intdono { get; set; }
        public string intpdfno { get; set; }
        public List<listMode> listModeList { get; set; }
        public object tagofficetypE_ID { get; set; }
        public object forPublic { get; set; }
        public object priority { get; set; }
        public string despatchStatus { get; set; }
        public object chrEnclosurestatus { get; set; }
        public object chrEnclosureDetails { get; set; }
        public object dtmDespatchOn { get; set; }

}

public class listENCLOUSERM
{
        public int intletterid { get; set; }
        public int intletterno { get; set; }
        public string vchattachfile { get; set; }
        public object vchofficename { get; set; }
        public object chlorder { get; set; }
        public int intdono { get; set; }

}

public class listMode
{
        public string modeName { get; set; }
        public object modeId { get; set; }
        public object listAddressees { get; set; }
}
```

## Support

For any issues or questions, please open an [issue](https://github.com/yourusername/json-to-csharp-model/issues).

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

In this README example:
- Provide an overview of the application and its features.
- Include instructions for installation, usage, and an example of the JSON response and generated C# class model.
- Offer support information and mention the project's license.

You can customize this template with specific details about your JSON to C# class model generator application.

Citations:
[1] https://ppl-ai-file-upload.s3.amazonaws.com/web/direct-files/17653937/955aa489-cd56-4357-a112-31cb3c10f8e1/paste.txt
