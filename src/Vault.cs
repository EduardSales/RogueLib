using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TheArtOfDev.HtmlRenderer.PdfSharp;

//https://www.youtube.com/watch?v=yhlyoQ2F-NM

//append line
//https://learn.microsoft.com/es-es/dotnet/api/system.text.stringbuilder.appendline?view=net-7.0

//html lists
//https://developer.mozilla.org/en-US/docs/Web/HTML/Reference/Elements/ul

//escape tricky string to CSV format
//https://stackoverflow.com/questions/6377454/escaping-tricky-string-to-csv-format

namespace RogueLib
{
    /// <summary>
    /// Main class designed to be called to store all the data 
    /// </summary>
    public class Vault
    {
        //change the color of pdf headers, accepts hex colors
        const string COLOR_RUNSPECS = "Teal";
        const string COLOR_PROGRESS = "Slate Blue";
        const string COLOR_PICKUPS = "Goldenrod";
        const string COLOR_ITEMS = "Gold";
        const string COLOR_HIGHLIGHTS = "Orange";
        const string COLOR_ERRORS = "Crimson";
        const string COLOR_END = "Green";

        private static Vault instance;
        public static Vault Instance
        {
            get
            {
                if (instance == null)
                    instance = new Vault();
                return instance;
            }
        }
        public List<Run> runs {  get; set; }

        private Vault() 
        {
            runs = new List<Run>();
        }
        public void AddRun(string _player = null)
        {
            runs.Add(new Run(_player));
        }
        public Run GetCurrentRun()
        {
            return runs[runs.Count-1];
        }

        public void GeneratePDFReport(string _path)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("<h1>TEST REPORT</h1>");
            sb.AppendLine($"<p>Report generated on: {DateTime.Now.ToString("yyyy-MM-dd HH:mm")}</p>");
            sb.AppendLine($"<p>A total of : <strong>{runs.Count}</strong> run/s were completed</p>");

            for (int i = 0; i < runs.Count; i++)
            {
                var run = runs[i];

                sb.Append($"<h2>Run number <strong>{i + 1}</strong>");
                if (run.seed != null)
                {
                    sb.Append($" with seed: {run.seed}");
                }
                sb.AppendLine("</h2>");

                if (run.player != null)
                {
                    sb.AppendLine($"<p>Played with <strong>{run.player}</strong></p>");
                }

                if (run.specs != null && run.specs.Count > 0)
                {
                    sb.AppendLine($"<h3 style=\"color: {COLOR_RUNSPECS};\">Run Specs</h3>");
                    sb.AppendLine("<ul>");
                    foreach (RunSpec specs in run.specs)
                    {
                        sb.AppendLine($"  <li>{specs.GetSpec()}</li>");
                    }
                    sb.AppendLine("</ul>");
                }

                if (run.progress != null && run.progress.Count > 0)
                {
                    sb.AppendLine($"<h2 style=\"color: {COLOR_PROGRESS};\">Progress</h2>");
                    sb.AppendLine("<p>A quick peek at how the run went</p>");
                    sb.AppendLine("<ul>");
                    foreach (Progress progress in run.progress)
                    {
                        sb.AppendLine($"  <li>{progress.GetProgress()}</li>");
                    }
                    sb.AppendLine("</ul>");
                }

                if (run.pickUps.Count > 0)
                {
                    sb.AppendLine($"<h2 style=\"color: {COLOR_PICKUPS};\">Pick ups</h2>");
                    sb.AppendLine("<ul>");
                    foreach (PickUp pickUp in run.pickUps)
                    {
                        sb.AppendLine($"  <li>{pickUp.name}: <strong>{pickUp.GetAmmount()}</strong>");
                        if (pickUp.GetAmmount() < pickUp.GetMaxAmmount())
                        {
                            sb.AppendLine("    <ul>");
                            sb.AppendLine($"      <li>With a maximum of: {pickUp.GetMaxAmmount()}</li>");
                            sb.AppendLine("    </ul>");
                        }
                        sb.AppendLine("  </li>");
                    }
                    sb.AppendLine("</ul>");
                }

                if (run.itemList != null && run.itemList.Count > 0)
                {
                    sb.AppendLine($"<h2 style=\"color: {COLOR_ITEMS};\">Items</h2>");
                    sb.AppendLine("<ul>");
                    foreach (Item item in run.itemList)
                    {
                        sb.AppendLine($"  <li>{item.name}");
                        if (item.attributes != null && item.attributes.Count > 0)
                        {
                            sb.AppendLine("    <ul>");
                            foreach (var attribute in item.attributes)
                            {
                                sb.AppendLine($"      <li>{attribute.Key}: {attribute.Value}</li>");
                            }
                            sb.AppendLine("    </ul>");
                        }
                        sb.AppendLine("  </li>");
                    }
                    sb.AppendLine("</ul>");
                }

                if (run.highlights != null && run.highlights.Count > 0)
                {
                    sb.AppendLine($"<h2 style=\"color: {COLOR_HIGHLIGHTS};\">Highlights</h2>");
                    sb.AppendLine("<ul>");
                    foreach (Highlight highlight in run.highlights)
                    {
                        sb.AppendLine($"  <li>{highlight.GetHighlight()}</li>");
                    }
                    sb.AppendLine("</ul>");
                }
                if (run.numberOfDeaths>0)
                {
                    sb.AppendLine($"<p>Run ended after: {run.numberOfDeaths} deaths</p>");
                }

                if (run.endMessage != null)
                {
                    sb.AppendLine($"<h2 style=\"color: {COLOR_END};\">End Message</h2>");
                    sb.AppendLine($"<p>{run.endMessage}</p>");
                }

                if (run.errors != null && run.errors.Count > 0)
                {
                    sb.AppendLine($"<h2 style=\"color: {COLOR_ERRORS};\">Errors</h2>");
                    sb.AppendLine("<p>Sadly some things were not expected</p>");
                    sb.AppendLine("<ul>");
                    foreach (Error error in run.errors)
                    {
                        sb.AppendLine($"  <li>{error.GetErrorMessage()}</li>");
                    }
                    sb.AppendLine("</ul>");
                }
                sb.AppendLine("<hr />");
            }

            string html = sb.ToString();

            PdfDocument pdfDocument = PdfGenerator.GeneratePdf(html, PdfSharp.PageSize.A4);
            pdfDocument.Save(_path);
        }
        public void GenerateTXTReport(string path)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("TEST REPORT");
            sb.AppendLine($"A total of : {runs.Count} run/s were completed\n");

            for (int i = 0; i < runs.Count; i++)
            {
                var run = runs[i];
                sb.Append($"Run {i + 1}");
                if (run.seed != null)
                {
                    sb.AppendLine(" with seed: " + run.seed);
                }
                //specs
                if (run.specs != null && run.specs.Count > 0)
                {
                    sb.AppendLine("Run specifications");
                    foreach (RunSpec specs in run.specs)
                    {
                        sb.AppendLine(specs.GetSpec());
                    }
                }
                sb.AppendLine();
                if (run.player != null)
                {
                    sb.AppendLine("Played with " + run.player);
                }

                //pickups
                if (run.pickUps != null && run.pickUps.Count > 0)
                {
                    sb.AppendLine("Pick Ups:");

                    foreach (PickUp pickUp in run.pickUps)
                    {
                        sb.Append(" - " + pickUp.name + ": " + pickUp.GetAmmount());
                        if (pickUp.GetAmmount() < pickUp.GetMaxAmmount())
                        {
                            sb.Append(" With a maximum of: " + pickUp.GetMaxAmmount());
                        }
                        sb.AppendLine();
                    }
                }
                else
                {
                    sb.AppendLine("No pick ups were recolected this run");
                }

                //items
                if (run.itemList != null && run.itemList.Count > 0)
                {
                    sb.AppendLine("Items:");
                    foreach (Item item in run.itemList)
                    {
                        sb.AppendLine(" - " + item.name);
                        if (item.attributes != null && item.attributes.Count > 0)
                        {
                            foreach (var attribute in item.attributes)
                            {
                                sb.AppendLine($"--{attribute.Key} with value {attribute.Value}");
                            }
                        }
                    }
                }
                else
                {
                    sb.AppendLine("No item was picked");
                }
                sb.AppendLine();

                //highlights
                if (run.highlights != null && run.highlights.Count > 0)
                {
                    sb.AppendLine("Hightlights");
                    foreach (Highlight highlight in run.highlights)
                    {
                        sb.AppendLine(" - " + highlight.GetHighlight());
                    }
                }
                //end message
                if (run.endMessage != null)
                {
                    sb.AppendLine(run.endMessage);
                }
                sb.AppendLine();
            }
            File.WriteAllText(path, sb.ToString());
        }
        public void GenerateCSVReport(string path)
        {
            // Runs.csv
            using (var swRuns = new StreamWriter(Path.Combine(path, "Runs.csv")))
            {
                swRuns.WriteLine("RunID,Seed,Character,NumberOfDeaths,EndMessage,ExportDate");
                for (int i = 0; i < runs.Count; i++)
                {
                    var run = runs[i];
                    string line = $"{i + 1},{run.seed},{run.player},{run.numberOfDeaths},\"{EscapeCsv(run.endMessage)}\",{DateTime.Now.ToString("yyyy-MM-dd HH:mm")}";
                    swRuns.WriteLine(line);
                }
            }

            // Specifications.csv
            using (var swSpecs = new StreamWriter(Path.Combine(path, "Specifications.csv")))
            {
                swSpecs.WriteLine("RunID,SpecKey,SpecValue");
                for (int i = 0; i < runs.Count; i++)
                {
                    var run = runs[i];
                    if (run.specs != null)
                    {
                        foreach (var spec in run.specs)
                        {
                            string line = $"{i + 1},{EscapeCsv(spec.Specification)},{EscapeCsv(spec.Description)}";
                            swSpecs.WriteLine(line);
                        }
                    }
                }
            }

            // Pickups.csv
            using (var swPickups = new StreamWriter(Path.Combine(path, "Pickups.csv")))
            {
                swPickups.WriteLine("RunID,PickupType,Quantity,Max");
                for (int i = 0; i < runs.Count; i++)
                {
                    var run = runs[i];
                    if (run.pickUps != null)
                    {
                        foreach (var pickup in run.pickUps)
                        {
                            string line = $"{i + 1},{EscapeCsv(pickup.name)},{pickup.GetAmmount()},{pickup.GetMaxAmmount()}";
                            swPickups.WriteLine(line);
                        }
                    }
                }
            }
            
            // Items.csv
            using (var swItems = new StreamWriter(Path.Combine(path, "Items.csv")))
            {
                //get maxAmmount of atributes in all runs
                int maxAttributeCount = 0;
                foreach (var run in runs)
                {
                    if (run.itemList != null)
                    {
                        foreach (var item in run.itemList)
                        {
                            if (item.attributes != null && item.attributes.Count > maxAttributeCount)
                            {
                                maxAttributeCount = item.attributes.Count;
                            }
                        }
                    }
                }
                //write the headers
                swItems.Write("RunID,ItemName");
                for (int i = 1; i <= maxAttributeCount; i++)
                {
                    swItems.Write($",Type,Attribute{i}");
                }
                swItems.WriteLine();

                for (int i = 0; i < runs.Count; i++)
                {
                    var run = runs[i];
                    if (run.itemList != null)
                    {
                        foreach (var item in run.itemList)
                        {
                            swItems.Write($"{i + 1},{EscapeCsv(item.name)},{EscapeCsv(item.type)}");

                            if (item.attributes != null)
                            {
                                int count = 0;
                                foreach (var kv in item.attributes)
                                {
                                    swItems.Write($",{EscapeCsv($"{kv.Key}={kv.Value}")}");
                                    count++;
                                }
                                // the rest are empty
                                for (int j = count; j < maxAttributeCount; j++)
                                {
                                    swItems.Write(",");
                                }
                            }
                            else
                            {
                                // if there are no atributes all null
                                for (int j = 0; j < maxAttributeCount; j++)
                                {
                                    swItems.Write(",");
                                }
                            }

                            swItems.WriteLine();
                        }
                    }
                }
            }

            // Progress.csv
            using (var swProgress = new StreamWriter(Path.Combine(path, "Progress.csv")))
            {
                swProgress.WriteLine("RunID,Progress");
                for (int i = 0; i < runs.Count; i++)
                {
                    var run = runs[i];
                    if (run.progress != null)
                    {
                        foreach (var progress in run.progress)
                        {
                            string line = $"{i + 1},{EscapeCsv(progress.description)}";
                            swProgress.WriteLine(line);
                        }
                    }
                }
            }
            // Highlights.csv
            using (var swPickups = new StreamWriter(Path.Combine(path, "Highlights.csv")))
            {
                swPickups.WriteLine("RunID,Name,Description");
                for (int i = 0; i < runs.Count; i++)
                {
                    var run = runs[i];
                    if (run.pickUps != null)
                    {
                        foreach (var highlight in run.highlights)
                        {
                            string line = $"{i + 1},{highlight.name},{EscapeCsv(highlight.description)}";
                            swPickups.WriteLine(line);
                        }
                    }
                }
            }
            // Errors.csv
            using (var swError = new StreamWriter(Path.Combine(path, "Errors.csv")))
            {
                swError.WriteLine("RunID,Error");
                for (int i = 0; i < runs.Count; i++)
                {
                    var run = runs[i];
                    if (run.errors != null)
                    {
                        foreach (var error in run.errors)
                        {
                            string line = $"{i + 1},{EscapeCsv(error.message)}";
                            swError.WriteLine(line);
                        }
                    }
                }
            }
        }

        // Function to escape tricky string to CSV format
        private string EscapeCsv(string s)
        {
            if (string.IsNullOrEmpty(s))
                return "";
            if (s.Contains(",") || s.Contains("\"") || s.Contains("\n") || s.Contains("\r"))
            {
                s = s.Replace("\"", "\"\"");
                return $"\"{s}\"";
            }
            return s;
        }
    }
}
