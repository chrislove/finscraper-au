using System;
using System.Collections.Generic;
using System.Text;

namespace FinScraper
{
    public interface IScraper
    {
        ScrapeResponse Scrape(ScrapeOptions scrapeOptions);
    }
}
