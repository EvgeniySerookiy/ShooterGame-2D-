using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using UnityEngine;

namespace ProjectAssets.Scripts.GoogleImporter
{
    public class GoogleSheetsImporter
    {
        private readonly List<string> _headers = new();
        private readonly string _sheetId;
        private readonly SheetsService _sheetsService;

        public GoogleSheetsImporter(string credentialsPath, string sheetId)
        {
            _sheetId = sheetId;
            GoogleCredential credential;
            
            using (var stream = new FileStream(credentialsPath, FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream)
                    .CreateScoped(SheetsService.Scope.Spreadsheets);
            }

            
            _sheetsService = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential
            });
        }

        public async Task DawnloadAndParseSheet(string sheetName, IGoogleSheetParser parser)
        {
            var range = $"{sheetName}!A1:Z";
            var request = _sheetsService.Spreadsheets.Values.Get(_sheetId, range);

            ValueRange response;
            try
            {
                response = await request.ExecuteAsync();
            }
            catch (System.Exception e)
            {
                Debug.Log("error");
                return;
            }
            
            if (response != null && response.Values != null)
            {
                var tableArray = response.Values;

                var firstRow = tableArray[0];
                foreach (var cell in firstRow)
                {
                    _headers.Add(cell.ToString());
                }

                var rowsCount = tableArray.Count;
                for (int i = 1; i < rowsCount; i++)
                {
                    var row = tableArray[i];
                    var rowLength = row.Count;

                    for (int j = 0; j < rowLength; j++)
                    {
                        var cell = row[j];
                        var header = _headers[j];
                        
                        parser.Parse(header, cell.ToString());
                        
                        Debug.Log($"Header: {header}, value: {cell}.");
                    }
                }
                
                Debug.Log("Sheet parsed seccessfully.");

            }
            else
            {
                Debug.Log("No data found in Google Sheets.");
            }
        }
    }
}