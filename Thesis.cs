using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelieveOrNot
{
    public enum ThesisResult
    {
        Yes,
        No
    }
    internal class Thesis
    {
        public string Question { get; set; }
        public ThesisResult Answer { get; set; }
        public string Comment { get; set; }

        public static Thesis ThesisLineParser(string line)
        {
            string[] lines = line.Split(';');
            return new Thesis
            {
                Question = lines[0],
                Comment = lines[2],
                Answer = lines[1] == "Yes" ? ThesisResult.Yes : ThesisResult.No,
            };
        }
    }
}
