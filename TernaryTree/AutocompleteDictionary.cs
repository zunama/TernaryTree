using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TernaryTree
{
    public class AutocompleteDictionary
    {
        public int WordsStored { get; set; }
        public int NodeStored { get; set; }

        private Node root;

        public IList<string> GetPossibleWords(string prefix)
        {
            IList<string> possibleWords = new List<string>();

            Node matchingPrefixNode = GetNodeThatMatchesPrefix(prefix.ToLower());

            if (matchingPrefixNode != null)
            {
                // prefix may actually be it's own word.
                if (matchingPrefixNode.WordEnd)
                    possibleWords.Add(prefix.ToLower());

                GetPossibleWords(matchingPrefixNode.Middle, prefix.ToLower(), possibleWords);
            }

            return possibleWords;
        }

        public IList<string> GetAllPossibleWords()
        {
            IList<string> possibleWords = new List<string>();
            GetPossibleWords(root, "", possibleWords);
            return possibleWords;
        }

        public void AddWord(string word)
        {
            if (string.IsNullOrEmpty(word))
                throw new ArgumentException("Word is empty or null.");

            WordsStored++;

            if (root == null)
            {
                root = new Node(word[0], false);
                NodeStored++;
            }
            
            Add(word.ToLower(), 0, root);
        }

        private void GetPossibleWords(Node currentNode, string currentWord, IList<string> possibleWords)
        {
            if (currentNode != null)
            {
                if (currentNode.WordEnd)
                    possibleWords.Add(currentWord + currentNode.Letter);

                if (currentNode.Left != null)
                    GetPossibleWords(currentNode.Left, currentWord, possibleWords);

                if (currentNode.Middle != null)
                    GetPossibleWords(currentNode.Middle, currentWord + currentNode.Letter, possibleWords);

                if (currentNode.Right != null)
                    GetPossibleWords(currentNode.Right, currentWord, possibleWords);
            }
        }

        private Node GetNodeThatMatchesPrefix(string prefix)
        {
            int pos = 0;

            Node node = root;

            while (node != null)
            {
                int cmp = prefix[pos] - node.Letter;

                if (prefix[pos] < node.Letter)
                {
                    node = node.Left;
                }
                else if (prefix[pos] > node.Letter)
                {
                    node = node.Right;
                }
                else
                {
                    if (++pos == prefix.Length)
                        return node;

                    node = node.Middle;
                }
            }

            return null;
        }

        private void Add(string word, int currentPos, Node currentNode)
        {
            if (word[currentPos] < currentNode.Letter)
            {
                if (currentNode.Left == null)
                {
                    currentNode.Left = new Node(word[currentPos], false);
                    NodeStored++;
                }

                Add(word, currentPos, currentNode.Left);
            }
            else if (word[currentPos] > currentNode.Letter)
            {
                if (currentNode.Right == null)
                {
                    currentNode.Right = new Node(word[currentPos], false);
                    NodeStored++;
                }

                Add(word, currentPos, currentNode.Right);
            }
            else
            {
                if (currentPos + 1 == word.Length)
                    currentNode.WordEnd = true;
                else
                {
                    if (currentNode.Middle == null)
                    {
                        currentNode.Middle = new Node(word[currentPos + 1], false);
                        NodeStored++;
                    }

                    Add(word, currentPos + 1, currentNode.Middle);
                }
            }
        }

        private class Node
        {
            public char Letter { get; set; }

            public Node Left { get; set; }
            public Node Middle { get; set; }
            public Node Right { get; set; }

            public bool WordEnd { get; set; }

            public Node(char letter, bool wordEnd)
            {
                Letter = letter;
                WordEnd = wordEnd;
            }

            public string ToString()
            {
                return string.Format("Letter: {0}");
            }
        }
    }
}
