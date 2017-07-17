namespace PathsToTree
{
    public class PathsToTreeConverterOptions
    {
        public string DelimiterSymbol { get; set; }

        public static PathsToTreeConverterOptions Defaults => new PathsToTreeConverterOptions()
        {
            DelimiterSymbol = "/"
        };
    }
}