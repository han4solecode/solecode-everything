namespace StudentSystemAPILinq.Models
{
    public class Kehadiran
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int GuruId { get; set; }
        public DateTime Tanggal { get; set; }
        public string? Status { get; set; }
    }
}
