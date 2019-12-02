using System;
using System.Collections.Generic;
using System.Text;

namespace FinScraper
{
    public class ScrapeOptions
    {
        public string FromDate;
        public string ToDate;
        public LoginCredentials LoginCredentials;

        public ScrapeOptions(LoginCredentials loginCredentials, string fromDate, string toDate)
        {
            FromDate = fromDate;
            ToDate = toDate;
            LoginCredentials = loginCredentials;
        }

    }
}
