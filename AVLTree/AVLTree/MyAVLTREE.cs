using System;
using System.Diagnostics;

namespace AVLTree
{
  public class MyAVLTREE<Key, Value> where Key : IComparable<Key>
  {
    internal bool ClassInvariantFullfilled()
    {
      var height = Height();
      var maxHeight = (int)Math.Floor(1.44 * Math.Log(Count() + 2, 2) - .328);
      return height <= maxHeight;
    }

    internal class Node
    {
      public Key Key;
      public Value Value;
      public Node Left;
      public Node Right;
      public Node Parent;
    }

    internal Node Root { get; private set; }
    
    #region Add
    /// <summary>
    /// Add a new value to the tree 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void Add(Key key, Value value)
    {
      if (key == null)
        throw new ArgumentNullException(nameof(key));

      if (Root == null)
        Root = new Node { Key = key, Value = value };
      else
        AddNode(key, value, Root);


      Debug.Assert(ClassInvariantFullfilled());
    }

    private void AddNode(Key key, Value value, Node node)
    {
      Debug.Assert(key != null);
      Debug.Assert(node != null);

      int compare = key.CompareTo(node.Key);
      if (compare < 0)
      {
        if (node.Left == null)
          node.Left = new Node { Key = key, Value = value, Parent = node };     
        else        
          AddNode(key, value, node.Left);      
      }
      else 
      {
        if (node.Right == null)    
          node.Right = new Node { Key = key, Value = value, Parent = node };     
        else    
          AddNode(key, value, node.Right);     
      }
      BalanceAfterInsert(node);
    }

    private void BalanceAfterInsert(Node node)
    {
      switch(GetBalance(node))
      {
        default:
          break;

        case 2:
          if (GetBalance(node.Left) == 1)
            RotateRight(node);
          else //node.Left.Balance == -1
            RotateLeftRight(node);
          break;

        case -2:
          if (GetBalance(node.Right) == -1)
            RotateLeft(node);
          else //node.Left.Balance == 1
            RotateRightLeft(node);
          break;
      }
    }

    private int GetBalance(Node node)
    {
      return Getheight(node.Left) - Getheight(node.Right);
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

      if (rightLeft != null)
        rightLeft.Parent = node;

      if (node == Root)
        Root = right;
      else if (parent.Right == node)
        parent.Right = right;
      else
        parent.Left = right;

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

      if (leftRight != null)
        leftRight.Parent = node;

      if (node == Root)
        Root = left;
      else if (parent.Left == node)
        parent.Left = left;
      else
        parent.Right = left;

      return left;
    }

    private Node RotateLeftRight(Node node)
    {
      Node left = node.Left;
      Node leftRight = node.Left.Right;
      Node parent = node.Parent;
      Node leftRightRight = leftRight.Right;
      Node leftRightLeft = leftRight.Left;

      leftRight.Parent = parent;
      node.Left = leftRightRight;
      left.Right = leftRightLeft;
      leftRight.Left = left;
      leftRight.Right = node;
      left.Parent = leftRight;
      node.Parent = leftRight;

      if (leftRightRight != null)
        leftRightRight.Parent = node;

      if (leftRightLeft != null)
        leftRightLeft.Parent = left;

      if (node == Root)
        Root = leftRight;
      else if (parent.Left == node)
        parent.Left = leftRight;
      else
        parent.Right = leftRight;

      return leftRight;

    }

    private Node RotateRightLeft(Node node)
    {
      Node right = node.Right;
      Node rightLeft = right.Left;
      Node parent = node.Parent;
      Node rightLeftLeft = rightLeft.Left;
      Node rightLeftRight = rightLeft.Right;

      rightLeft.Parent = parent;
      node.Right = rightLeftLeft;
      right.Left = rightLeftRight;
      rightLeft.Right = right;
      rightLeft.Left = node;
      right.Parent = rightLeft;
      node.Parent = rightLeft;

      if (rightLeftLeft != null)
        rightLeftLeft.Parent = node;

      if (rightLeftRight != null)
        rightLeftRight.Parent = right;

      if (node == Root)
        Root = rightLeft;
      else if (parent.Right == node)
        parent.Right = rightLeft;
      else
        parent.Left = rightLeft;

      return rightLeft;
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
      return Contains(Root, key);
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
      return Count(Root);
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
        Node node = GetNode(key, Root);
        if (node == null)
          throw new ArgumentException($"The given {nameof(key)}='{key}' is not in the tree", nameof(key));

        return node.Value;
      }
      set
      {
        Node node = GetNode(key, Root);
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
      if (key == null)
        throw new ArgumentNullException();
      RemoveIter(Root, key);
    }

    private void RemoveIter(Node node, Key key)
    {
      if (node == null)
        return;

      int compare = key.CompareTo(node.Key);
      if (compare < 0)
      {
        RemoveIter(node.Left, key);
      }
      else if (compare > 0)
      {
        RemoveIter(node.Right, key);
      }
      else // compare = 0
      {
        RemoveNode(node);
      }
    }

    private void RemoveNode(Node node)
    {
      Node right = node.Right;
      Node left = node.Left;
      Node parent = node.Parent;

      if (NoChild(Root))
      {
        Root = null;
      }
      else if(HasParent(node) && NoChild(node))
      {
        if (parent.Left == node)
        {
          parent.Left = null;
          BalanceAfterDelete(parent);
        }
        else if (parent.Right == node)
        {
          parent.Right = null;
          BalanceAfterDelete(parent);
        }
      }    
      else if (OnlyRightChild(node))
      {
        Replace(node, right);
        BalanceAfterDelete(node);
      }
      else if (OnlyLeftChield(node))
      {
        Replace(node, left);
        BalanceAfterDelete(node);
      }
      else if (TwoChilds(node) && right.Left == null)
      {
        right.Parent = parent;
        right.Left = left;

        left.Parent = right;
        if (node == Root)
          Root = right;
        else
        {
          if (parent.Left == node)
            parent.Left = right;
          else
            parent.Right = right;
        }

        BalanceAfterDelete(right);
      }
      else if (TwoChilds(node) && right.Left != null)
      {
        Node successor = GetLeftMostNode(right);
        Node successorParent = successor.Parent;
        Node successorRight = successor.Right;

        successorParent.Left = successorRight;
        if (successorRight != null)
          successorRight.Parent = successorParent;

        successor.Parent = parent;
        successor.Left = left;
        successor.Right = right;
        right.Parent = successor;

        if (left != null)
          left.Parent = successor;

        if (node == Root)
          Root = successor;
        else
        {
          if (parent.Left == node)
            parent.Left = successor;
          else
            parent.Right = successor;
        }
        BalanceAfterDelete(successorParent);
      }
    }

    private bool NoChild(Node node)
    {
      return node.Right == null && node.Left == null;
    }

    private bool HasParent(Node node)
    {
      return node.Parent != null;
    }

    private bool OnlyRightChild(Node node)
    {
      return node.Right != null && node.Left == null;
    }

    private bool OnlyLeftChield(Node node)
    {
      return node.Right == null && node.Left != null;
    }

    private bool TwoChilds(Node node)
    {
      return node.Right != null && node.Left != null;
    }

    private static Node GetLeftMostNode(Node node)
    {
      if (node.Left == null)
        return node;
      else
        return GetLeftMostNode(node.Left);
    }

    private void Replace(Node target, Node source)
    {
      Node left = source.Left;
      Node right = source.Right;

      target.Key = source.Key;
      target.Value = source.Value;
      target.Left = left;
      target.Right = right;

      if (left != null)
        left.Parent = target;

      if (right != null)
        right.Parent = target;
    }

    private void BalanceAfterDelete(Node node)
    {
      int balance = GetBalance(node);

      if (balance == 2)
      {
        if (GetBalance(node.Left) >= 0)
        {
          node = RotateRight(node);

          if (GetBalance(node) == -1)
            return;
        }
        else
          node = RotateLeftRight(node);
      }
      else if (balance == -2)
      {
        if (GetBalance(node.Right) <= 0)
        {
          node = RotateLeft(node);

          if (GetBalance(node) == 1)
            return;
        }
        else
          node = RotateRightLeft(node);
      }
      else if (balance != 0)
        return;

      Node parent = node.Parent;

      if (parent != null)
        balance = parent.Left == node ? -1 : 1;

      if (parent != null)
        BalanceAfterDelete(parent);
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
      ToArray(Root, arr, ref index);
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

    public int Height()
    {
      return Getheight(Root);
    }

    private int Getheight(Node node)
    {
      if (node == null)
        return 0;
      else
        return Math.Max(Getheight(node.Left), Getheight(node.Right)) + 1;
    }
  }
}
