using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlassdoorReviewsCollector
{
    class Options
    {
        /* Handling program arguments
         *  glassdoorreviews [-l|--location <location search term>]
         *      -l | --location <location search term>
         *      -c | --company <company name search term>
         *      -s | --save-to-db <db file name>
         *      -e | --export-to-csv <csv file name>
         */
        [Option('l', "location", Required = false,
           HelpText = "Location to search companies from.")]
        public string Location { get; set; }

        [Option('c', "company", Required = false,
           HelpText = "Company name to be searched.")]
        public string Company { get; set; }

        [Option('s', "save-to-db", Required = false,
           HelpText = "Save search results to specified .db file.")]
        public string DBFile { get; set; }

        [Option('e', "export-to-csv", Required = false,
           HelpText = "Save search results to specified .csv file.")]
        public string CSVFile { get; set; }

        [ParserState]
        public IParserState LastParserState { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this,
              (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }

        public bool CheckParams()
        {
            if (String.IsNullOrEmpty(this.Location) && String.IsNullOrEmpty(this.Company))
                return false;
            else
                return true;
        }
    }
}
