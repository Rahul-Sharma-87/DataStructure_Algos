using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace IntPrac {
    class Node:IEqualityComparer<Node> {
        public Node(string value) {
            Value = value;
        }
        public Node NextNode { get; set; }
        public string Value { get; set; }

        public bool Equals(Node x, Node y) {
            return x?.Value == y?.Value;
        }

        public int GetHashCode(Node obj) {
            return Value.GetHashCode();
        }
    }

    class LinkedList {
        private Node head = null;
        private Node last = null;

        public Node Head => head;

        public LinkedList(string headValue) {
            head = new Node(headValue);
        }

        public void Push(string value) {
            if (head.NextNode == null) {
                head.NextNode = new Node(value);
                last = head.NextNode;
                return;
            }
            last.NextNode = new Node(value);
            last = last.NextNode;
        }
    }


    class Program {

        static void Main(string[] args) {
            LinkedList llst= new LinkedList("head"); 
            llst.Push("1");
            llst.Push("2");
            llst.Push("3");
            llst.Push("4");
            llst.Push("5");
            llst.Push("6");
            llst.Push("7");
            llst.Push("8");
            llst.Push("9");
            llst.Push("10");

            ReverseLinkedList(llst.Head);


            //
            var list = GetPalendromeLinkedList1();
            //CheckPalindrome(list.Head);

            CheckPalindromeUsingStack(list.Head);

            list = GetPalendromeLinkedList2();
            CheckPalindrome(list.Head);

            Node head = llst.Head;
            //Find Nth node in link list
            var node = FindNthNode(head, 5);
            Console.WriteLine("Nth node is :"+ node.Value);

            //Middle node is link list
            var middleNode = FindMiddleNode(head);
            Console.WriteLine("Middle node is :"+ middleNode.Value);

            //Nth node from end of link list
            var NthFromLast = FindNthFromEnd(head,3);
            Console.WriteLine("Nth node from end is :"+ NthFromLast.Value);

            //Detect loop in list
            var repeat = DetectLoopInLinkList(head);
            Console.WriteLine(repeat?.Value);

            //Find length of loops
            int length = FindLengthOfLoop(head);
            Console.WriteLine(length);
        }

        private static LinkedList GetPalendromeLinkedList1() {
            LinkedList llst= new LinkedList("head"); 
            llst.Push("R");
            llst.Push("A");
            llst.Push("D");
            llst.Push("A");
            llst.Push("R");
            return llst;
        }

        private static LinkedList GetPalendromeLinkedList2() {
            LinkedList llst= new LinkedList("head"); 
            llst.Push("R");
            llst.Push("A");
            llst.Push("D");
            llst.Push("D");
            llst.Push("A");
            llst.Push("R");
            return llst;
        }

        private static Node FindNthFromEnd(Node head, int nthPos) {
            int counter = 0;
            Node current = head.NextNode;
            Node nthNode = head.NextNode;
            while (current != null) {
                counter++;
                //Unless it is less than 0 till 
                //that time it should wait at start
                if (counter - nthPos > 0) {
                    nthNode = nthNode.NextNode;
                }
                current = current.NextNode;
            }
            return nthNode;
        }

        private static Node FindMiddleNode(Node head) {
            Node slowPtr = head;
            Node twotimesFasterPtr = head;
            while (twotimesFasterPtr != null && twotimesFasterPtr.NextNode != null){
                slowPtr = slowPtr.NextNode;
                twotimesFasterPtr = twotimesFasterPtr.NextNode.NextNode;
            }
            return slowPtr;
        }

        private static Node FindNthNode(Node head,int findNodeCount) {
            Node current = head.NextNode;
            int counter = 0;
            while (current != null && current.NextNode != null) {
                counter++;
                if (counter == findNodeCount) {
                    return current;
                }
                current = current.NextNode;
            }
            return null;
        }

        private static Node DetectLoopInLinkList(Node head) {
            IDictionary<int,Node> dictionary = new Dictionary<int,Node>();
            Node current = head.NextNode;
            while (current != null) {
                if (dictionary.ContainsKey(current.GetHashCode())) {
                    return current;
                }
                dictionary[current.GetHashCode()] = current;
                current = current.NextNode;
            }
            return null;
        }

        private static int FindLengthOfLoop(Node head) {
            IDictionary<int,Node> dictionary = new Dictionary<int,Node>();
            Node current = head.NextNode;
            while (current != null) {
                int hashKey = current.GetHashCode();
                if (dictionary.ContainsKey(hashKey)) {
                    var list = dictionary.Keys.ToList();
                    int pos = list.IndexOf(hashKey);
                    return list.Count - (pos + 1);
                }
                dictionary[hashKey] = current;
                current = current.NextNode;
            }
            return 0;
        }

        private static bool CheckPalindrome(Node head) {
            Node current = head.NextNode;
            ArrayList arr = new ArrayList();
            int counter = 0;
            int previousElementIndex = 0;
            bool found = false;
            bool skip = false;
            int skipCounter = 2;
            while (current != null) {
                if (skip) {
                    skipCounter++;
                }
                arr.Add(current.Value);
                if (!found) {
                    previousElementIndex = counter - skipCounter;
                } else {
                    previousElementIndex--;
                }

                skip = !found && counter > 0 && arr[counter - 1].Equals(current.Value);

                if(!skip)
                if (found || previousElementIndex > 0) {
                    found = arr[previousElementIndex].Equals(current.Value);
                }
                counter++;
                current = current.NextNode;
            }
            return found;
        }

        private static bool CheckPalindromeUsingStack(Node head) {
            Node currentNode = head.NextNode;
            Stack st = new Stack();
            bool found = false;
            while (currentNode != null) {
                st.Push(currentNode);
                currentNode = currentNode.NextNode;
            }

            currentNode = head.NextNode;
            while (currentNode.NextNode != null) {
                found = currentNode.Value.Equals(((Node) st.Pop()).Value);
                currentNode = currentNode.NextNode;
                if (!found) {
                    return false;
                }
            }
            return found;
        }

        private static Node ReverseLinkedList(Node head) {
            Node currentNode = head.NextNode;
            Stack st = new Stack();
            while (currentNode != null) {
                st.Push(currentNode);
                currentNode = currentNode.NextNode;
            }

            int length = st.Count;
            for (int i = 0; i < length; i++) {
                if (i == 0) {
                    head.NextNode = (Node)st.Pop();
                    currentNode = head.NextNode;
                    continue;
                }
                currentNode.NextNode = (Node)st.Pop();
                currentNode = currentNode.NextNode;
            }
            currentNode.NextNode = null;
            return head;
        }
    }
}
