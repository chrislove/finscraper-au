using System;
using System.Collections.Generic;
using System.Text;

namespace FinScraper
{
    public class ScrapeResponse
    {
        public string Result { get; }

        public ScrapeResponse(string result)
        {
            Result = result;
        }
    }
}
