using System.Text.RegularExpressions;

namespace BidirectionalUnicodeScanner
{
    class UnicodeMatch
    {
        public char MatchExpression { get; set; }
        public string Description { get; set; }
        public UnicodeMatch(char matchChar, string description)
        {
            MatchExpression = matchChar;
            Description = description;
        }
    }
}
