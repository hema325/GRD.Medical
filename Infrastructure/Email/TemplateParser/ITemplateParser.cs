namespace Infrastructure.Email.TemplateParser
{
    internal interface ITemplateParser
    {
        Task<string> ParseAsync<TModel>(string templateName, TModel model);
    }
}
