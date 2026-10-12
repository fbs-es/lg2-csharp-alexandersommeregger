using System.Collections.Generic;

namespace TreeUebung
{
    public class BinarySearchTree
    {
        private int Value { get; set; }
        private BinarySearchTree? Left { get; set; }
        private BinarySearchTree? Right { get; set; }

        public static List<int> list = new List<int>();

        public BinarySearchTree(int value)
        {
            Value = value;
        }

        /// <summary>
        /// Fügt den Wert an der richtigen Stelle des Baumes ein.
        /// </summary>
        /// <param name="newValue">neuer Wert.</param>
        private void InsertInTree(int newValue)
        {
            if (newValue < Value)
            {
                if (Left == null)
                {
                    Left = new BinarySearchTree(newValue);
                }
                else
                {
                    Left.InsertInTree(newValue);
                }
            }
            else if (newValue > Value)
            {
                if (Right == null)
                {
                    Right = new BinarySearchTree(newValue);
                }
                else
                {
                    Right.InsertInTree(newValue);
                }
            }
        }

        /// <summary>
        /// Gibt die Werte sortiert aus.
        /// </summary>
        private void Ordered()
        {
            if (Left != null)
            {
                Left.Ordered();
            }

            Console.Write($"{Value} ");

            if (Right != null)
            {
                Right.Ordered();
            }
        }

        /// <summary>
        /// Wert zuerst, links dann rechts.
        /// </summary>
        private void PreOrder()
        {
            Console.Write($"{Value} ");

            if (Left != null)
            {
                Left.Ordered();
            }

            if (Right != null)
            {
                Right.Ordered();
            }
        }

        /// <summary>
        /// links, rechts, dann wert.
        /// </summary>
        private void PostOrder()
        {
            if (Left != null)
            {
                Left.Ordered();
            }

            if (Right != null)
            {
                Right.Ordered();
            }

            Console.Write($"{Value} ");
        }

        public static void Run()
        {
            BinarySearchTree? tree = null;
            foreach (int number in list)
            {
                if (tree == null)
                {
                    tree = new BinarySearchTree(number);
                }
                else
                {
                    tree.InsertInTree(number);
                }
            }

            tree!.Ordered();
            Console.WriteLine();
            tree!.PreOrder();
            Console.WriteLine();
            tree!.PostOrder();
        }
    }
}