using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using AVLTree;


namespace Test
{
  class Test
  {
    [Test]
    public void TestRightRight()
    {
      MyAVLTREE<int,string> myAVLTREE = new MyAVLTREE<int, string>();

      myAVLTREE.Add(1, "");
      myAVLTREE.Add(2, "");
      myAVLTREE.Add(3, "");

      Assert.That(myAVLTREE.Root.Key, Is.EqualTo(2));
      Assert.That(myAVLTREE.Root.Left.Key, Is.EqualTo(1));
      Assert.That(myAVLTREE.Root.Right.Key, Is.EqualTo(3));
    }
  }
}
