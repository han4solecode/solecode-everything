namespace StudentSystemAPILinq.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string? Nama { get; set; }
        public int Umur {  get; set; }
        public string? Kelas { get; set; }
        public string? Jurusan { get; set; }
        public double NilaiRataRata { get; set; }
    }
}
