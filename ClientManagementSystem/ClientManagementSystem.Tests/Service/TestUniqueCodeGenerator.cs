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
			var actual = UniqueCodeGenerator.GetAlphabets(input);

			//Assert
			actual.Should().Be(string.Empty);
		}

		//[TestCase("First National Bank", "FNB")]
		[TestCase("Standard Bank", "SBA")]
	    public void GetAlphabets_GivenTextWith3Words_ShouldReturnFirst3LettersOfWord(string input, string expected)
	    {
			//Arrange


			//Act
			var actual = UniqueCodeGenerator.GetAlphabets(input);

			//Assert
			actual.Should().Be(expected);

		}

		[Test]
	    public void GetUniqueDigits_WhenCalled_ShouldGenerateUniqueDigits()
	    {
		    //Arrange
		    var text = string.Empty;

		    //Act
		    var actual = UniqueCodeGenerator.GenerateCode(text);

		    //Assert
		    actual.Should().Be(string.Empty);
	    }

	}
}