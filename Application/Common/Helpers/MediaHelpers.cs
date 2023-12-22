namespace Application.Common.Helpers
{
    internal static class MediaHelpers
    {
        public static string GetFullUrl(ICurrentHttpRequest request,string? path)
        {
            if (string.IsNullOrEmpty(path))
                return null;

            return $"{request.Scheme}://{request.Host}/{path}";
        }
    }
}
