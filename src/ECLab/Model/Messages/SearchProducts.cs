namespace ECLab.Model.Messages
{
    public sealed record SearchProducts
    {
        public string Keyword { get; init; }
    }

    //public sealed class SearchProducts
    //{
    //    public SearchProducts(string keyword)
    //    {
    //        this.Keyword = keyword;
    //    }

    //    public string Keyword { get; }
    //}
}