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
    public void TestRotateLeft()
    {
      MyAVLTREE<int,string> myAVLTREE = new MyAVLTREE<int, string>();

      myAVLTREE.Add(1, "");
      myAVLTREE.Add(2, "");
      myAVLTREE.Add(3, "");

      Assert.That(myAVLTREE.Root.Key, Is.EqualTo(2));
      Assert.That(myAVLTREE.Root.Left.Key, Is.EqualTo(1));
      Assert.That(myAVLTREE.Root.Right.Key, Is.EqualTo(3));
    }

    [Test]
    public void TestRotateRight()
    {
      MyAVLTREE<int, string> myAVLTREE = new MyAVLTREE<int, string>();

      myAVLTREE.Add(3, "");
      myAVLTREE.Add(2, "");
      myAVLTREE.Add(1, "");

      Assert.That(myAVLTREE.Root.Key, Is.EqualTo(2));
      Assert.That(myAVLTREE.Root.Left.Key, Is.EqualTo(1));
      Assert.That(myAVLTREE.Root.Right.Key, Is.EqualTo(3));
    }

    [Test]
    public void TestRotateLeftRight()
    {
      MyAVLTREE<int, string> myAVLTREE = new MyAVLTREE<int, string>();

      myAVLTREE.Add(5, "");
      myAVLTREE.Add(3, "");
      myAVLTREE.Add(4, "");

      Assert.That(myAVLTREE.Root.Key, Is.EqualTo(4));
      Assert.That(myAVLTREE.Root.Left.Key, Is.EqualTo(3));
      Assert.That(myAVLTREE.Root.Right.Key, Is.EqualTo(5));

      Assert.That(myAVLTREE.Root.Balance, Is.EqualTo(0));
      Assert.That(myAVLTREE.Root.Left.Balance, Is.EqualTo(0));
      Assert.That(myAVLTREE.Root.Right.Balance, Is.EqualTo(0));

    }

    [Test]
    public void TestRotateRightLeft()
    {
      MyAVLTREE<int, string> myAVLTREE = new MyAVLTREE<int, string>();

      myAVLTREE.Add(3, "");
      myAVLTREE.Add(5, "");
      myAVLTREE.Add(4, "");

      Assert.That(myAVLTREE.Root.Key, Is.EqualTo(4));
      Assert.That(myAVLTREE.Root.Left.Key, Is.EqualTo(3));
      Assert.That(myAVLTREE.Root.Right.Key, Is.EqualTo(5));

      Assert.That(myAVLTREE.Root.Balance, Is.EqualTo(0));
      Assert.That(myAVLTREE.Root.Left.Balance, Is.EqualTo(0));
      Assert.That(myAVLTREE.Root.Right.Balance, Is.EqualTo(0));
    }
  }
}
