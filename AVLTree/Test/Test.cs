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
      Assert.That(myAVLTREE.Height, Is.EqualTo(2));
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
      Assert.That(myAVLTREE.Height, Is.EqualTo(2));
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
      Assert.That(myAVLTREE.Height, Is.EqualTo(3));
    }

    [Test]
    public void RandomAddTest()
    {
      MyAVLTREE<int, string> myAVLTREE = new MyAVLTREE<int, string>();

      List<int> addedValues = new List<int>();
      Random random = new Random(System.DateTime.Now.Millisecond.GetHashCode());
      for (int i = 0; i < 1000; i++)
      {
        int randomNumber = random.Next(10000);
        myAVLTREE.Add(randomNumber, "");
        addedValues.Add(randomNumber);
        Assert.That(Math.Log(myAVLTREE.Count() + 1, 2) <= myAVLTREE.Height(), Is.EqualTo(true));
      }
      for (int i = 0; i < 1000; i++)
      {
        int rnd = random.Next(addedValues.Count());
        int value = addedValues[rnd];
        myAVLTREE.Remove(value);
        addedValues.Remove(value);
        Assert.That(Math.Log(myAVLTREE.Count() + 1, 2) <= myAVLTREE.Height(), Is.EqualTo(true));
      }


    }
  }
}
