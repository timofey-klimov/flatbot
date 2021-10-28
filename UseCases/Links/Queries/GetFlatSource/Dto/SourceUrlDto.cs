namespace UseCases.Links.Queries.GetFlatSource.Dto
{
    public class SourceUrlDto
    {
        public string Url { get; }

        public SourceUrlDto(string url)
        {
            Url = url;
        }
    }
}
