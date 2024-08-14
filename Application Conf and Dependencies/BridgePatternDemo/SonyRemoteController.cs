namespace BridgePatternDemo
{
    public class SonyRemoteControl : RemoteControl
    {
        public SonyRemoteControl(ILEDTV ledTv)
        {
            this.ledTv = ledTv;
        }

        public override void SetChannel(int channelNumber)
        {
            ledTv.SetChannel(channelNumber);
        }

        public override void SwitchOff()
        {
            ledTv.SwitchOff();

        }

        public override void SwitchOn()
        {
            ledTv.SwitchOn();
        }
    }
}