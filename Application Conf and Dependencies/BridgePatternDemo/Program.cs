namespace BridgePatternDemo;

class Program
{
    static void Main(string[] args)
    {
        RemoteControl sonyRemoteControl = new SonyRemoteControl(new SonyLEDTV());
        sonyRemoteControl.SwitchOn();
        sonyRemoteControl.SetChannel(7);
        sonyRemoteControl.SwitchOff();

        Console.WriteLine("------------------");

        RemoteControl samsungRemoteControl = new SonyRemoteControl(new SamsungLEDTV());
        samsungRemoteControl.SwitchOn();     
        samsungRemoteControl.SetChannel(10);
        samsungRemoteControl.SwitchOff();
    }
}
