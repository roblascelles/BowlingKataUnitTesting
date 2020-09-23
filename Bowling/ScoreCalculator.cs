using System.Linq;

namespace Bowling
{
    public class ScoreCalculator
    {
        public int Score(Game game)
        {
            var score = 0;

            for (var i = 0; i < 10; i++)
            {
                score += Score(game.Frames?.ElementAtOrDefault(i), game.Frames?.ElementAtOrDefault(i + 1));
            }

            return score;
        }

        public int Score(Frame frame, Frame nextFrame)
        {
            if (frame?.IsStrike() ?? false)
            {
                return 10 + Score(nextFrame);
            }

            if (frame?.IsSpare() ?? false)
            {
                return 10 + Score(nextFrame?.Tries.ElementAtOrDefault(0));
            }

            return Score(frame);
        }
        public int Score(Frame frame)
        {
            return Score(frame?.Tries.ElementAtOrDefault(0)) + Score(frame?.Tries.ElementAtOrDefault(1));
        }

        public int Score(Try @try)
        {
            return @try?.PinsKnockedDown ?? 0;
        }
    }
}