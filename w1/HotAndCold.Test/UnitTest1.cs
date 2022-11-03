using HotAndCold;

namespace HotAndCold.Test;

public class UnitTest1
{
    [Fact]
    // unitOfTest_TestConditon_CorrectBehavior
    public void PrintResult_CorrectGuess_Congratulate()
    {
        // a unit test needs to be precise! 
        // Arrange - What needs to be in place for your test to work properly?
        Guessing tmp = new Guessing();
        int secret = 5;
        int user = 5;
        string expected = "Congatulations, you've guessed the secret number!";

        // Act - What behavior are we checking?
        string result = tmp.PrintResult( secret, user);

        // Assert - What are your expected results? Think both positive and negative.
        Assert.Equal( expected: expected, actual: result);
    }
    
    [Fact]
    public void PrintResult_TooHigh_StateTooHigh()
    {
        // Arrange
        Guessing tmp = new Guessing();
        int secret = 5;
        int user = 6;
        string expected = "Oops, too high!";

        // Act - What behavior are we checking?
        string result = tmp.PrintResult( secret, user);

        // Assert - What are your expected results? Think both positive and negative.
        Assert.Equal( expected: expected, actual: result);
    }
}