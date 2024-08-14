namespace BridgePatternDemo
{
    public abstract class RemoteControl
    {
        protected ILEDTV? ledTv;
        public abstract void SwitchOn();
        public abstract void SwitchOff();
        public abstract void SetChannel(int channelNumber);
    }
}