using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace Dictionary
{
    public class Data
    {
        public struct _Dictionary
        {
            public string Word { get; set; }
            public string Definition { get; set; }
        }
        private int size = 1000;
        public int Size
        {
            get { return size; }
        }
        public  _Dictionary[] Dictionarys;
        public Data()
        {
            Dictionarys = new _Dictionary[size];
        }
        public void Sort(int size)
        {
            _Dictionary tmp;
            int min_index;
            for (int i = 0; i < size - 1; i++) 
            {
                min_index = i;
                for (int j = i + 1; j < size; j++) 
                {
                    if (String.Compare(Dictionarys[min_index].Word, Dictionarys[j].Word) > 0)  { min_index = j; }
                }
                tmp = Dictionarys[i];
                Dictionarys[i] = Dictionarys[min_index];
                Dictionarys[min_index] = tmp;
            }
        }
        public void RemoveAt(int index, ref int size)
        {
            if(index == size - 1)
            {
                Dictionarys[index].Definition = null;
                Dictionarys[index].Word = null;
                size--;
            }
            else
            {
                for (int i = index; i < size; i++)
                {
                    Dictionarys[i] = Dictionarys[i+1];
                }
                Dictionarys[size - 1].Definition = null;
                Dictionarys[size - 1].Word = null;
                size--;
            }
        }
    }
}
