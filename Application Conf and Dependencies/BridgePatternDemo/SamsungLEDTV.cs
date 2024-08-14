namespace BridgePatternDemo
{
    public class SamsungLEDTV : ILEDTV
    {
        public void SetChannel(int channelNumber)
        {
            Console.WriteLine($"Change channel to {channelNumber} on Samsung TV");
        }

        public void SwitchOff()
        {
            Console.WriteLine("Turn off Samsung TV");
        }

        public void SwitchOn()
        {
            Console.WriteLine("Turn on Samsung TV");
        }
    }
}