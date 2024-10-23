using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeReportsApplication.BusinessLayer.Business;
     public class StringToBolleanConverter : DefaultTypeConverter
    {
    public override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
    {
        if(string.IsNullOrWhiteSpace(text)) return false;

        string textToLower = text.ToLower();

        if(textToLower == "true") return true;

        return false;
    }
}

