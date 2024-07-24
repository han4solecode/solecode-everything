namespace SimpleLibraryManagementSystem
{
    public class Book(string title, string author, int publicationYear)
    {
        // getter props for reading field value
        public string Title
        {
            get { return title; }
        }
        public string Author
        {
            get { return author; }
        }
        public int PublicationYear
        {
            get { return publicationYear; }
        }

    }
}