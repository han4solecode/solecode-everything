namespace BridgePatternDemo
{
    public class SonyLEDTV : ILEDTV
    {
        public void SetChannel(int channelNumber)
        {
            Console.WriteLine($"Change channel to {channelNumber} on Sony TV");
        }

        public void SwitchOff()
        {
            Console.WriteLine("Turn off Sony TV");
        }

        public void SwitchOn()
        {
            Console.WriteLine("Turn on Sony TV");
        }
    }
}