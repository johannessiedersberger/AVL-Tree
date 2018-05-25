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
      MyAVLTREE<int, string> myAVLTREE = new MyAVLTREE<int, string>();
      myAVLTREE.Add(7, "");
      myAVLTREE.Add(7, "");
      myAVLTREE.Add(7, "");
      myAVLTREE.Add(4, "");
      myAVLTREE.Add(2, "");
      myAVLTREE.Add(3, "");
    }
  }
}
