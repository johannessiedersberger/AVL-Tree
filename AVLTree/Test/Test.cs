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
      Assert.That(Math.Log(myAVLTREE.Count() + 1, 2) <= myAVLTREE.Height(), Is.EqualTo(true));
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
      Assert.That(Math.Log(myAVLTREE.Count() + 1, 2) <= myAVLTREE.Height(), Is.EqualTo(true));
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
      Assert.That(myAVLTREE.Height, Is.EqualTo(2));
      Assert.That(Math.Log(myAVLTREE.Count() + 1, 2) <= myAVLTREE.Height(), Is.EqualTo(true));
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
      Assert.That(Math.Log(myAVLTREE.Count() + 1, 2) <= myAVLTREE.Height(), Is.EqualTo(true));

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
      Assert.That(myAVLTREE.Root.Left.Key, Is.EqualTo(10));
      Assert.That(myAVLTREE.Root.Right.Key, Is.EqualTo(14));
      Assert.That(myAVLTREE.Height, Is.EqualTo(3));
    }

    [Test]
    public void TestNoDegeneratedAdd()
    {
      var tree = new MyAVLTREE<int, int>();
      foreach (var value in Enumerable.Range(0, 100))
      {
        tree.Add(value, value);
        Assert.That(tree.Height, Is.LessThan(MaxHeight(tree) + 1));
      }
    }

    [Test, Repeat(100), Parallelizable]
    public void RandomAddRemoveTest()
    {
      const int NUMBERS_TO_ADD = 200;

      MyAVLTREE<int, string> myAVLTREE = new MyAVLTREE<int, string>();
      List<int> addedValues = new List<int>();
      Random random = new Random(System.DateTime.Now.Millisecond.GetHashCode());
      for (int i = 0; i < NUMBERS_TO_ADD; i++)
      {
        int randomNumber = random.Next(100);
        myAVLTREE.Add(randomNumber, "");
        addedValues.Add(randomNumber);

        Assert.That(myAVLTREE.Height(), Is.LessThan(MaxHeight(myAVLTREE) + 1), $"Add from: {string.Join(",", addedValues)}");
      }

      var allAdded = addedValues.ToArray();
      List<int> removedIndeces = new List<int>();

      while (addedValues.Count > 0)
      {
        int rnd = random.Next(addedValues.Count());
        removedIndeces.Add(rnd);
        int toRemove = addedValues[rnd];
        addedValues.RemoveAt(rnd);

        myAVLTREE.Remove(toRemove);

        Assert.That(myAVLTREE.Height(), Is.LessThan(MaxHeight(myAVLTREE) + 1), $"Removed indices from {string.Join(",", addedValues)}: {string.Join(",", removedIndeces)}");
      }
    }

    private int MaxHeight<Key,Value>(MyAVLTREE<Key,Value> tree) where Key : IComparable<Key>
    {
      return (int)Math.Floor(1.44 * Math.Log(tree.Count() + 2, 2) - .328);
    }

    [Test]
    public void Test1()
    {
      List<int> list = new List<int>() { 79,67,82,39,10,71,96 };
      MyAVLTREE<int, int> myAVLTREE = new MyAVLTREE<int, int>();
      foreach(int value in list)
      {
        myAVLTREE.Add(value, value);
      }
    }

  }
}
