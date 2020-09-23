using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using FakeItEasy;
using Shouldly;
using Xunit;

namespace Bowling.Tests
{
    public class GameFixture
    {
        [Fact]
        public void CanCreateGame()
        {
            var try1 = A.Fake<Try>();
            A.CallTo(() => try1.PinsKnockedDown).Returns(10);

            var tries = new List<Try>() {try1};

            var frame1 = A.Fake<Frame>();
            A.CallTo(() => frame1.Tries).Returns(tries);

            var game = new Game(new List<Frame>() {frame1});

            game.Frames[0].Tries[0].PinsKnockedDown.ShouldBe(10);
        }

        [Fact]
        public void CanCreateFrame()
        {
            var try1 = A.Fake<Try>();
            A.CallTo(() => try1.PinsKnockedDown).Returns(8);

            var try2 = A.Fake<Try>();
            A.CallTo(() => try2.PinsKnockedDown).Returns(1);


            var frame = new Frame(new List<Try>() {try1, try2});

            frame.Tries[0].PinsKnockedDown.ShouldBe(8);
            frame.Tries[1].PinsKnockedDown.ShouldBe(1);
        }

        [Fact]
        public void CanCreateTry()
        {
            var @try = new Try(3);
            @try.PinsKnockedDown.ShouldBe(3);
        }

    }
}
