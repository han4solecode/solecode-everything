namespace arraynlist;

class Program
{
    static void Main(string[] args)
    {
        // ==== list ====
        // instantiate and create a new list object
        List<string> sList;
        sList = new List<string>();

        // add value to a list
        sList.Add("apple");
        sList.Add("mango");
        sList.Add("pear");
        sList.Add("orange");
        
        // accessing a list
        Console.WriteLine(sList);

        for (int i = 0; i < (sList.Count); i++) {
            Console.WriteLine(sList[i]);
        }

        // check index
        sList.IndexOf("mango");
        sList.IndexOf("orange");
        
        // insert value in what index
        sList.Insert(2, "durian");

        // remove a value
        sList.Remove("apple");
        sList.RemoveAt(1);

        // convert to array
        string[] sArray = sList.ToArray();

        // empty list
        sList.Clear();

        // ==== array ====
        // create an array
        string[] arr; // empty array
        arr = new string[4]; // array of 4 srting

        // add value to array
        arr[1] = "Bob"; // add value to index 1

        // accessing array
        for (int i = 0; i < (arr.Length); i++) {
            Console.WriteLine(arr[i]);
        }

        // array can be multidimensional
        string[,] arr2D = new string[4,4]; // 4 x 4 array, like a 4 x 4 matrix

        // ==== list and array iteration ====
        // foreach can be used
        foreach (string s in arr) {
            // code
        }

        foreach (string item in sList) {
            // code
        }

    }
}
