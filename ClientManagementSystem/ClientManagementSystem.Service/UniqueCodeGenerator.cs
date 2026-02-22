namespace ClientManagementSystem.Service
{
    public static class UniqueCodeGenerator
    {
	    public static string GenerateCode(string text)
	    {
			if(string.IsNullOrWhiteSpace(text))
				return string.Empty;

			var wordOfText = text.Split([' ']);

			return "";
	    }

	    public static string GetAlphabets(string text)
	    {
		    if (string.IsNullOrWhiteSpace(text))
			    return string.Empty;

		    text = text.ToUpper();
			var wordOfText = text.Split([' '], StringSplitOptions.RemoveEmptyEntries);
			var requiredCharacters = string.Empty;

			if (wordOfText.Length == 1)
			{
				requiredCharacters = wordOfText.FirstOrDefault()?.Substring(0, 3);
			}
			else if (wordOfText.Length == 2)
			{

			}
			else
			{
				foreach (var word in wordOfText)
				{
					if (requiredCharacters.Length == 3)
						continue;

					requiredCharacters += word[..1];
				}
			}

			return requiredCharacters;
	    }
    }
}