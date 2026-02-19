using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Mappings
{
    public class ApiResponse
    {
        public List<CountryData> Response { get; set; }
    }

    public class CountryData
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Flag { get; set; }
    }
}
