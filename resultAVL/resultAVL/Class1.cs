using System;
using System.Collections.Generic;
using System.IO;

namespace resultAVL
{
	class Program
	{
		static void Main(string[] args)
		{
			AVL<int, string> tree = new AVL<int, string>();
			tree.Add(5, "a");
			tree.Add(3, "b");
			tree.Add(7, "c");
			tree.Add(2, "d");
			tree.Add(4, "e");
			tree.Add(9, "j");
			tree.Print();
		}
	}
	class AVL<Tkey, Tvalue> where Tkey : System.IComparable, System.IEquatable<Tkey>
	{
		public class Node
		{
			public Tkey key;
			public Tvalue value;
			public Node Left;
			public Node Right;
			public int Balance;
		};
		private Node Root;

		public AVL()
		{
			Root = null;
		}
		public int Height()
		{
			return Height(Root);
		}
		public void Print()
		{
			Print(Root);
		}
		public void Add(Tkey key, Tvalue value)
		{
			Add(ref Root, key, value);
		}
		int Height(Node Branch)
		{
			if (Branch == null)
				return 0;
			int hLeft = 0;
			int hRight = 0;
			hLeft = Height(Branch.Left);
			hRight = Height(Branch.Right);
			if (hLeft > hRight)
				return hLeft + 1;
			else
				return hRight + 1;
		}

		void SetBalance(Node Branch)//out
		{
			if (Branch != null)
				Branch.Balance = Height(Branch.Right) - Height(Branch.Left);
		}

		void TurnRight(Node Branch)//out
		{
			Node leftSubtree = Branch.Left;
			Node leftSubtreeRightSubtree = leftSubtree.Right;

			leftSubtree.Right = Branch;
			Branch.Left = leftSubtreeRightSubtree;
			Branch = leftSubtree;
			SetBalance(Branch.Right);
			SetBalance(Branch);
		}

		void TurnLeft(Node Branch)
		{
			Node rightSubtree = Branch.Right;
			Node rightSubtreeLeftSubtree = rightSubtree.Left;

			rightSubtree.Left = Branch;
			Branch.Right = rightSubtreeLeftSubtree;
			Branch = rightSubtree;
			SetBalance(Branch.Left);
			SetBalance(Branch);
		}

		void Add(ref Node Branch, Tkey key, Tvalue value)//добавление
		{
			if (Branch == null)//нашли пустой узел
			{
				Branch = new Node();
				Branch.key = key;
				Branch.value = value;
				Branch.Balance = 0;
				Branch.Left = null;
				Branch.Right = null;
			}
			else//текущий узел занят
			{//>
				if (key.CompareTo(Branch.key) > 0)//идем в правое поддерево
				{
					Add(ref Branch.Right, key, value);
					if (Height(Branch.Right) - Height(Branch.Left) > 1)//Если нарушен баланс после добавления
					{
						if (Height(Branch.Right.Right) < Height(Branch.Right.Left))//
							TurnRight(Branch.Right);//предварительный поворот правого поддерева
						TurnLeft(Branch);//поворот влево
					}
				}
				else
					if (key.CompareTo(Branch.key) < 0)//идем в левое поддерево
				{
					Add(ref Branch.Left, key, value);
					if (Height(Branch.Left) - Height(Branch.Right) > 1)//Если нарушен баланс после добавления
					{
						if (Height(Branch.Left.Left) < Height(Branch.Left.Right))
							TurnLeft(Branch);//предварительный поворот левого поддерева
						TurnRight(Branch);//поворот вправо
					}
				}
				SetBalance(Branch);//пересчитываем баланс
			}
		}

		void Output(Node Branch)
		{
			if (Branch != null)
			{
				Output(Branch.Left);
				//cout << Branch.Data << " ";
				Console.Write($"{Branch.key}  {Branch.value}  ");
				Output(Branch.Right);
			}
		}

		public void Print_n(Node Branch, int n, int level, int space)//печать по уровням
		{
			if (Branch != null)
			{
				if (level == n)
				{
					for (int i = 1; i <= space; i++)
						Console.Write(" ");
					Console.Write($"{Branch.key} ");
				}
				else
				{
					Print_n(Branch.Left, n, level + 1, space);
					Print_n(Branch.Right, n, level + 1, space);
				}
				return;
			}
			for (int i = 1; i <= space; i++)
				Console.Write(" ");
			Console.Write("#");
		}

		public void Print(Node Branch)//распечатка дерева по уровням
		{
			int h = Height(Branch);
			int space = 3;
			if (Branch != null)
			{
				for (int i = 0; i < h; i++)//задача номеров уровней
				{
					Print_n(Branch, i, 0, space * (h - i));//функция печати уровня
					Console.WriteLine();
				}
			}
		}


	}
}