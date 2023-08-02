namespace tmdb.Service.Integrations.GmailSmtp.Model
{
    public interface IEmailContentModel
    {
        public string poster_uri { get; set; }
        public string rating { get; set; }
        public string wiki_description { get; set; }
        public string film_name { get; set; }
    }
}
