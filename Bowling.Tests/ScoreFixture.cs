using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using Shouldly;
using Xunit;

namespace Bowling.Tests
{
    public class ScoreFixture
    {
        [Fact]
        public void CanCalculateScoreForEmptyGame()
        {
            var game = A.Fake<Game>();

            var calculator = new ScoreCalculator();
            var score = calculator.Score(game);

            score.ShouldBe(0);
        }

        [Theory]
        [InlineData("0", 0)]  
        [InlineData("1", 1)]
        public void CanCalculateScoreForTry(string tryStr, int expectedScore)
        {
            var @try = TryFromString(tryStr);

            var calculator = new ScoreCalculator();
            var score = calculator.Score(@try);

            score.ShouldBe(expectedScore);
        }
        
        [Theory]
        [InlineData("0", 0)]  
        [InlineData("1", 1)]
        [InlineData("0,1", 1)]
        [InlineData("1,0", 1)]
        [InlineData("5,4", 9)]
        public void CanCalculateScoreForFrame(string frameStr, int expectedScore)
        {
            var frame = FrameFromString(frameStr);

            var calculator = new ScoreCalculator();
            var score = calculator.Score(frame, null);

            score.ShouldBe(expectedScore);

        }

        [Theory]
        [InlineData("5,4", "0,0", 9)]
        [InlineData("5,5", "3,4", 13)]
        [InlineData("10", "3,4", 17)]
        public void CanCalculateScoreFor2Frames(string frame1Str, string frame2Str, int expectedScore)
        {
            var frame1 = FrameFromString(frame1Str);
            var frame2 = FrameFromString(frame2Str);

            var calculator = new ScoreCalculator();
            var score = calculator.Score(frame1, frame2);

            score.ShouldBe(expectedScore);

        }

        [Theory]
        [InlineData("10;0;0;0;0;0;0;0;0;0", 10)]  
        [InlineData("1,1;1,1;1,1;1,1;1,1;1,1;1,1;1,1;1,1;1,1", 20)]
        [InlineData("8,2;2,3;0;0;0;0;0;0;0;0", 17)]  //spare
        [InlineData("10;2,3;0;0;0;0;0;0;0;0", 20)]  //strike
        public void CanCalculateScores(string gameStr, int expectedScore)
        {

            var frames = gameStr.Split(';').Select(FrameFromString).ToList();

            var game = A.Fake<Game>();
            A.CallTo(() => game.Frames).Returns(frames);

            var calculator = new ScoreCalculator();
            var score = calculator.Score(game);

            score.ShouldBe(expectedScore);
        }

        private static Frame FrameFromString(string s)
        {
            var tries = s.Split(',').Select(TryFromString).ToList();

            var frame = A.Fake<Frame>();
            A.CallTo(() => frame.Tries).Returns(tries);

            if (frame.Tries?.ElementAtOrDefault(0)?.PinsKnockedDown == 10)
            {
                A.CallTo(() => frame.IsStrike()).Returns(true);

            } else if (frame.Tries?.ElementAtOrDefault(0)?.PinsKnockedDown + frame.Tries?.ElementAtOrDefault(1)?.PinsKnockedDown == 10)
            {
                A.CallTo(() => frame.IsSpare()).Returns(true);
            }

            return frame;
        }

        private static Try TryFromString(string s)
        {
            var @try = A.Fake<Try>();
            A.CallTo(() => @try.PinsKnockedDown).Returns(int.Parse(s));
            return @try;
        }
    }
}