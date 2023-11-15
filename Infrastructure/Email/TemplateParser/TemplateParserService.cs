using RazorEngineCore;

namespace Infrastructure.Email.TemplateParser
{
    internal class TemplateParserService: ITemplateParser
    {
        public async Task<string> ParseAsync<TModel>(string templateName, TModel model)
        {
            //read template file
            var template = File.ReadAllText($"Files/Templates/{templateName}.cshtml");
            
            //feed template with model data
            var razorEngine = new RazorEngine();
            var compiledTemplate = await razorEngine.CompileAsync(template);

            return await compiledTemplate.RunAsync(model);
        }
    }
}
