using System.Text;

namespace ClientManagementSystem.Service
{
	public class UniqueCodeGenerator
	{
		private static readonly Dictionary<string, int> ClientCounters = new();

		public static string GenerateUniqueAlphaNumericHandler(string clientName, string? clientCodeToIncrement = null)
		{
			if (string.IsNullOrWhiteSpace(clientName))
				return string.Empty;

			if (string.IsNullOrWhiteSpace(clientCodeToIncrement))
				return GetUniqueAlphaNumeric(clientName);

			var oldClientCodeLastDigit = int.Parse(clientCodeToIncrement.Substring(5, 1));
			oldClientCodeLastDigit++;
			return clientCodeToIncrement[..5] + oldClientCodeLastDigit;
		}

		public static string GetUniqueAlphaNumeric(string clientName)
		{
			if (string.IsNullOrWhiteSpace(clientName))
				return string.Empty;

			var alphaPrefix = GetAlphaPrefix(clientName);
			if (!ClientCounters.TryAdd(alphaPrefix, 1))
			{
				ClientCounters[alphaPrefix]++;
			}

			var numericPart = ClientCounters[alphaPrefix].ToString("D3");
			ClientCounters.Clear();
			return alphaPrefix + numericPart;
		}

		public static string GetAlphaPrefix(string clientName)
		{
			var words = clientName.Split([' '], StringSplitOptions.RemoveEmptyEntries);
			var prefix = new StringBuilder();

			if (words.Length > 1)
			{
				foreach (var word in words)
				{
					if (prefix.Length >= 3 || string.IsNullOrEmpty(word)) continue;
					var firstChar = char.ToUpper(word[0]);
					if (char.IsLetter(firstChar))
					{
						prefix.Append(firstChar);
					}
				}
			}
			else
			{
				var word = words[0];
				foreach (var c in word.Where(c => prefix.Length < 3 && char.IsLetter(c)))
				{
					prefix.Append(char.ToUpper(c));
				}
			}

			var fillChar = 'A';
			while (prefix.Length < 3)
			{
				prefix.Append(fillChar);
				fillChar++;
			}
			return prefix.ToString();
		}
	}
}