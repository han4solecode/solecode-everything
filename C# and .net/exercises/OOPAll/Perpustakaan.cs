using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPAll
{
    public class Perpustakaan
    {
        private List<Item> listItems = [];
        private List<Pengguna> listPengguna = [];

        private Dictionary<string, List<Item>> itemDict = []; 

        public void TambahItem(Item item)
        {
            listItems.Add(item);
        }

        public bool HapusItem(Item item)
        {
            if (listItems.Contains(item))
            {
                return false;
            }
            else 
            {
                listItems.Remove(item);
                return true;
            }
            
        }

        public void TambahPengguna(Pengguna pengguna)
        {
            listPengguna.Add(pengguna);
        }

        public Item? CariItemBerdasarkanJudul(string judul)
        {
            var itemToDisplay = listItems.SingleOrDefault(i => i.Judul == judul);

            return itemToDisplay;
        }

        public void TampilkanSemuaItem()
        {
            foreach(Item i in listItems)
            {
                i.DisplayInfo();
            }
        }

        public void TampilkanSemuaPengguna()
        {
            foreach(Pengguna p in listPengguna)
            {
                p.DisplayInfo();
            }
        }
    }
}
