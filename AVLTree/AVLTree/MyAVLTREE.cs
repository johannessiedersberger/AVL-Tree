using System;

namespace AVLTree
{
  public class MyAVLTREE<Key, Value> where Key : IComparable<Key>
  {
    public class Node
    {
      public Key Key;
      public Value Value;
      public Node Left;
      public Node Right;
      public Node Parent;
      public int Balance;    
    }

    private Node _root;
    public Node Root
    {
      get { return _root; }
    }
   

    #region Add
    /// <summary>
    /// Add a new value to the tree 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void Add(Key key, Value value)
    {
      if(_root == null)
      {
        _root = new Node { Key = key, Value = value };
      }
      else
      {
        Node node = _root;
        while(node != null)
        {
          int compare = key.CompareTo(node.Key);
          if(compare < 0)
          {
            Node left = node.Left;
            if(left == null)
            {
              node.Left = new Node { Key = key, Value = value, Parent = node };
              InsertBalance(node, 1);
              return;
            }
            else
            {
              node = left;
            }
          }
          else if(compare >= 0)
          {
            Node right = node.Right;
            if(right == null)
            {
              node.Right = new Node { Key = key, Value = value, Parent = node };
              InsertBalance(node, -1);
              return;
            }
            else
            {
              node = right;
            }
          }
        }
      }
    }

    private void InsertBalance(Node node, int balance)
    {
      while(node != null)
      {
        balance = (node.Balance += balance);
        if(balance == 0)
        {
          return;
        }
        else if(balance == 2)
        {
          if(node.Left.Balance == 1)
          {
            RotateRight(node);
          }
          else
          {
            //RotateLeftRight
          }
          return;
        }
        else if(balance == -2)
        {
          if(node.Right.Balance == -1)
          {
            RotateLeft(node);
          }
          else
          {
            //Rotate Right Left
          }
          return;
        }
        Node parent = node.Parent;

        if(parent != null)
        {
          balance = parent.Left == node ? 1 : -1;
        }

        node = parent;
      }
    }

    private Node RotateLeft(Node node)
    {
      Node right = node.Right;
      Node rightLeft = right.Left;
      Node parent = node.Parent;

      right.Parent = parent;
      right.Left = node;
      node.Right = rightLeft;
      node.Parent = right;

      if(rightLeft != null)
      {
        rightLeft.Parent = node;
      }

      if(node == _root)
      {
        _root = right;
      }
      else if(parent.Right == node)
      {
        parent.Right = right;
      }
      else
      {
        parent.Left = right;
      }
      right.Balance++;
      node.Balance = -right.Balance;
      return right;
    }

    private Node RotateRight(Node node)
    {
      Node left = node.Left;
      Node leftRight = left.Right;
      Node parent = node.Parent;

      left.Parent = parent;
      left.Right = node;
      node.Left = leftRight;
      node.Parent = left;

      if(leftRight != null)
      {
        leftRight.Parent = node;
      }

      if(node == _root)
      {
        _root = left;
      }
      else if(parent.Left == node)
      {
        parent.Left = left;
      }
      else
      {
        parent.Right = left;
      }

      left.Balance--;
      node.Balance = -left.Balance;

      return left;
    }



  

    #endregion

    #region Contains
    /// <summary>
    /// Checks if the tree contains the given key
    /// </summary>
    /// <param name="key"></param>
    /// <returns>returns true if the given value is included in the tree</returns>
    public bool Contains(Key key)
    {
      return Contains(_root, key);
    }

    private static bool Contains(Node current, Key key)
    {
      if (current == null)
        return false;

      int cmpt = key.CompareTo(current.Key);
      if (cmpt < 0)
      {
        return Contains(current.Left, key);
      }
      else if (cmpt > 0)
      {
        return Contains(current.Right, key);
      }
      else //cmpt = 0
      {
        return true;
      }
    }

    #endregion

    #region Count

    /// <summary>
    /// Returns the amount of all Elements in the tree
    /// </summary>
    /// <returns>The amount of all elements in the tree</returns>
    public int Count()
    {
      return Count(_root);
    }

    private static int Count(Node node)
    {
      if (node == null)
        return 0;
      else
        return Count(node.Left) + 1 + Count(node.Right);
    }

    #endregion

    #region Indexer

    /// <summary>
    /// set or get the value at the key
    /// </summary>
    /// <param name="key"></param>
    /// <returns>get the value at the key</returns>
    public Value this[Key key]
    {
      get
      {
        Node node = GetNode(key, _root);
        if (node == null)
          throw new ArgumentException("The given Key is not in the tree");

        return node.Value;
      }
      set
      {
        Node node = GetNode(key, _root);
        if (node == null)
          throw new ArgumentException("The given Key is not in the tree");

        node.Value = value;
      }
    }

    private static Node GetNode(Key key, Node node)
    {
      if (node == null)
        return null;

      int cmpt = key.CompareTo(node.Key);
      if (cmpt == 0)
      {
        return node;
      }
      else if (cmpt < 0)
      {
        return GetNode(key, node.Left);
      }
      else
      {
        return GetNode(key, node.Right);
      }
    }

    #endregion

    #region Remove

    /// <summary>
    /// Removes the Value at the given key
    /// </summary>
    /// <param name="key"></param>
    public void Remove(Key key)
    {
      RemoveIter(ref _root, key);
    }

    private static void RemoveIter(ref Node node, Key key)
    {
      if (node == null)
        return;

      int cmpt = key.CompareTo(node.Key);
      if (cmpt < 0)
      {
        RemoveIter(ref node.Left, key);
      }
      else if (cmpt > 0)
      {
        RemoveIter(ref node.Right, key);
      }
      else // cmpt = 0
      {
        RemoveNode(ref node);
      }
    }

    private static void RemoveNode(ref Node node)
    {
      if (node.Left == null && node.Right != null)
      {
        node = node.Right;
      }
      else if (node.Right == null && node.Left != null)
      {
        node = node.Left;
      }
      else if (node.Left == null && node.Right == null)
      {
        node = null;
      }
      else // left & Right are not null
      {
        Node leftMost = GetLeftMostNode(node.Right);
        leftMost.Left = node.Left;
        node = node.Right;
      }
    }

    private static Node GetLeftMostNode(Node node)
    {
      if (node.Left == null)
        return node;
      else
        return GetLeftMostNode(node.Left);
    }

    #endregion

    #region ToArray

    /// <summary>
    /// creates an array of the tree
    /// </summary>
    /// <returns>returns an array of the tree</returns>
    public Value[] ToArray()
    {
      int index = 0;
      Value[] arr = new Value[Count()];
      ToArray(_root, arr, ref index);
      return arr;
    }

    private static void ToArray(Node node, Value[] arr, ref int index)
    {
      if (node == null)
        return;

      ToArray(node.Left, arr, ref index);
      arr[index++] = node.Value;
      ToArray(node.Right, arr, ref index);
    }

    #endregion
  }
}
