namespace VehiclesSystemAPI.Interfaces
{
    public interface IElektrik
    {
        int DayaBaterai {  get; set; }

        void Charge(int jumlah);
    }
}
