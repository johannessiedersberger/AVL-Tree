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

    [Test]
    public void TestDelete1()
    {
      MyAVLTREE<int, string> myAVLTREE = new MyAVLTREE<int, string>();

      myAVLTREE.Add(10, "");
      myAVLTREE.Add(8, "");
      myAVLTREE.Add(13, "");
      myAVLTREE.Add(11, "");
      myAVLTREE.Add(14, "");

      myAVLTREE.Remove(8);
      Assert.That(myAVLTREE.Root.Balance, Is.EqualTo(1));
      Assert.That(myAVLTREE.Root.Left.Balance, Is.EqualTo(-1));
      Assert.That(myAVLTREE.Root.Left.Right.Balance, Is.EqualTo(0));
      Assert.That(myAVLTREE.Root.Right.Balance, Is.EqualTo(0));
      Assert.That(myAVLTREE.Root.Left.Key, Is.EqualTo(10));
      Assert.That(myAVLTREE.Root.Right.Key, Is.EqualTo(14));
    }

    [Test]
    public void TestDelete2()
    {
      MyAVLTREE<int, string> myAVLTREE = new MyAVLTREE<int, string>();

      myAVLTREE.Add(10, "");
      myAVLTREE.Add(8, "");
      myAVLTREE.Add(7, "");
      myAVLTREE.Add(9, "");
      myAVLTREE.Add(13, "");
      myAVLTREE.Add(11, "");
      myAVLTREE.Add(14, "");

      myAVLTREE.Remove(10);
    }

    public void PrintArray()
    {
      MyAVLTREE<int, string> myAVLTREE = new MyAVLTREE<int, string>();

      myAVLTREE.Add(10, "");
      myAVLTREE.Add(8, "");
      myAVLTREE.Add(7, "");
      myAVLTREE.Add(9, "");
      myAVLTREE.Add(13, "");
      myAVLTREE.Add(11, "");
      myAVLTREE.Add(14, "");

      MyAVLTREE<int, string> tree = tree.ToArray();

      Console.WriteLine();
    }
  }
}
