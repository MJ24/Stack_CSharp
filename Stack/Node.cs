using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stack
{
    public class Node<T>
    {
        private T data;
        private Node<T> next;

        public T Data
        {
            get { return data; }
            set { data = value; }
        }
        public Node<T> Next
        {
            get { return next; }
            set { next = value; }
        }

        public Node(T dataVal, Node<T> nextVal)
        {
            data = dataVal;
            next = nextVal;
        }
        //头节点
        public Node(Node<T> nextVal)
        {
            next = nextVal;
        }
        //尾节点
        public Node(T dataVal)
        {
            data = dataVal;
            next = null;
        }
        //空节点
        public Node()
        {
            data = default(T);
            next = null;
        }
    }
}
