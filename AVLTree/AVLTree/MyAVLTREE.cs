﻿using System;
using System.Collections.Generic;
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
      AddNode(key, value, _root);
    }

    private void AddNode(Key key, Value value, Node node)
    {
      if (_root == null)
      {
        _root = new Node { Key = key, Value = value };
      }
      else
      {
        int compare = key.CompareTo(node.Key);
        if (compare < 0)
        {
          Node left = node.Left;
          if (left == null)
          {
            node.Left = new Node { Key = key, Value = value, Parent = node };
            InsertBalance(node);
            return;
          }
          else
          {
            AddNode(key, value, node.Left);
          }
        }
        else if (compare >= 0)
        {
          Node right = node.Right;
          if (right == null)
          {
            node.Right = new Node { Key = key, Value = value, Parent = node };
            InsertBalance(node);
            return;
          }
          else
          {
            AddNode(key, value, node.Right);
          }
        }
      }
    }

    private void InsertBalance(Node node)
    {
      int balance = GetBalance(node);

      if (balance == 0)
      {
        return;
      }
      else if (balance == 2)
      {
        if (GetBalance(node.Left) == 1)
          RotateRight(node);
        else //node.Left.Balance == -1
          RotateLeftRight(node);

        return;
      }
      else if (balance == -2)
      {
        if (GetBalance(node.Right) == -1)
          RotateLeft(node);
        else //node.Left.Balance == 1
          RotateRightLeft(node);
        return;
      }

      Node parent = node.Parent;
      if (parent != null)
      {
        balance = parent.Left == node ? 1 : -1;
        InsertBalance(parent);
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

      if (node == _root)
        _root = right;
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

      if (node == _root)
        _root = left;
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

      if (node == _root)
        _root = leftRight;
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

      if (node == _root)
        _root = rightLeft;
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

    private void RemoveIter(ref Node node, Key key)
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

    private void RemoveNode(ref Node node)
    {
      Node right = node.Right;
      Node left = node.Left;
      Node parent = node.Parent;

      if (_root.Left == null && _root.Right == null)
      {
        _root = null;
      }
      else if (parent != null && parent.Left == node && left == null && right == null)
      {
        parent.Left = null;
        DeleteBalance(parent);
      }
      else if (parent != null && parent.Right == node && left == null && right == null)
      {
        parent.Right = null;
        DeleteBalance(parent);
      }
      else if (left == null && right != null)
      {
        Replace(node, right);
        DeleteBalance(node);
      }
      else if (left != null && right == null)
      {
        Replace(node, left);
        DeleteBalance(node);
      }
      else if (right != null && left != null && right.Left == null)
      {
        right.Parent = parent;
        right.Left = left;

        left.Parent = right;
        if (node == _root)
          _root = right;
        else
        {
          if (parent.Left == node)
            parent.Left = right;
          else
            parent.Right = right;
        }

        DeleteBalance(right);
      }
      else if (right != null && left != null && right.Left != null)
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

        if (node == _root)
          _root = successor;
        else
        {
          if (parent.Left == node)
            parent.Left = successor;
          else
            parent.Right = successor;
        }
        DeleteBalance(successorParent);
      }
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

    private void DeleteBalance(Node node)
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
        DeleteBalance(parent);
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

    public int Height()
    {
      return Getheight(_root);
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
