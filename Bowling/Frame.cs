using System.Collections.Generic;

namespace Bowling
{
    public class Frame 
    {
        public virtual List<Try> Tries { get; }

        public Frame(List<Try> tries)
        {
            Tries = tries;
        }

        public virtual bool IsStrike()
        {
            return false;
        }

        public virtual bool IsSpare()
        {
            return false;
        }

    }
}