using ClientManagementSystem.Service;
using FluentAssertions;

namespace ClientManagementSystem.Tests.Service
{
    public class TestUniqueCodeGenerator
    {
	    [TestCase("")]
	    [TestCase(" ")]
	    public void GetAlphabets_GivenInvalidText_ShouldEmptyText(string input)
		{
			//Arrange

			//Act
			var actual = UniqueCodeGenerator.GetUniqueAlphaNumeric(input);

			//Assert
			actual.Should().Be(string.Empty);
		}

		[TestCase("First National Bank", "FNB")]
		[TestCase("Standard Bank", "SBA")]
		[TestCase("Protea", "PRO")]
		public void GetAlphaPrefix_GivenText_ShouldReturnOnlyFirst3LettersOfWord(string input, string expected)
	    {
			//Arrange


			//Act
			var actual = UniqueCodeGenerator.GetAlphaPrefix(input);

			//Assert
			actual.Should().Be(expected);
		}

		[TestCase("First National Bank", "FNB001")]
		[TestCase("Standard Bank", "SBA001")]
		[TestCase("Protea","PRO001")]
		public void GetAlphabets_GivenText_ShouldReturnAlphaNumeric(string input, string expected)
		{
			//Arrange

			//Act
			var actual = UniqueCodeGenerator.GetUniqueAlphaNumeric(input);

			//Assert
			actual.Should().Be(expected);
		}

		[TestCase("First National Bank", "FNB002", "FNB003")]
		[TestCase("Standard Bank", "SBA002", "SBA003")]
		public void GetAlphabets_GivenSameTextForSecondTime_ShouldReturnAlphaNumeric(string input, string oldClientCode, string expected)
		{
			//Arrange

			//Act
			var actual = UniqueCodeGenerator.GenerateUniqueAlphaNumericHandler(input, oldClientCode);

			//Assert
			actual.Should().Be(expected);
		}
	}
}