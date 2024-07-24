namespace OOPInterface;

class Program
{
    static void Main(string[] args)
    {
        // create new objects
        var mobil = new Mobil(2, "Ferrari", 2024);
        var motor = new Motor("Sport", "kawasaki", 2016);
        var mobilE = new MobilListrik("Hyundai", 2024, 30);
        var motorE = new MotorListrik("Pacific", 2023, 20);

        // create new KoleksiKendaraan object
        var koleksi = new KoleksiKendaraan();

        // add kendaraan to koleksi
        koleksi.TambahKendaraan(mobil);
        koleksi.TambahKendaraan(motor);
        koleksi.TambahKendaraan(mobilE);
        koleksi.TambahKendaraan(motorE);

        // display all kendaraan
        koleksi.TampilkanSemuaInfo();

        // charge all kendaraan listrik
        koleksi.ChargeKendaraanListrik(50);

        koleksi.TampilkanSemuaInfo();
    }
}
