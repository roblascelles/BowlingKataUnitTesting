namespace Bowling
{
    public class Try
    {
        public virtual int PinsKnockedDown { get; }

        public Try(int pinsKnockedDown)
        {
            PinsKnockedDown = pinsKnockedDown;
        }
    }
}