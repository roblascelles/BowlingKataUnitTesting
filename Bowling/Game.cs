using System;
using System.Collections.Generic;

namespace Bowling
{
    public class Game
    {
        public virtual List<Frame> Frames { get; }

        public Game(List<Frame> frames)
        {
            Frames = frames;
        }
    }
}
