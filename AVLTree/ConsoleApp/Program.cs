using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AVLTree;

namespace ConsoleApp
{
  class Program
  {
    static void Main(string[] args)
    {
      MyAVLTREE<int, int> myAVLTREE = new MyAVLTREE<int, int>();
      myAVLTREE.Add(7, 7);
      myAVLTREE.Add(7, 7);
      myAVLTREE.Add(7, 7);
      myAVLTREE.Add(4, 7);
      myAVLTREE.Add(2, 7);
      myAVLTREE.Add(3, 7);
    }
  }
}
