namespace ZAM.Core.Infrastructure.Tahoma.Extensions;

internal static class TahomaExtensions
{
    internal static string GetCookieValue(this string cookies)
    {
        var cookiesStrings = cookies!.Split(";");

        var cookieValue = cookiesStrings[0].Split("=");

        return cookieValue[1];
    }

    internal static string ObfuscateCredentials(this string source)
    {
        var array = source.ToCharArray();

        for (var index = 0; index < array.Length; index++)
        {
            if (index % 2 == 1)
            {
                array[index] = '*';
            }
        }

        return new string(array);
    }
}
