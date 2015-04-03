using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stack
{
    public class LinkStack<T> : IStack<T>
    {
        private Node<T> top;
        //由于栈只能访问栈顶的数据元素，而链栈的栈顶指示器又不能指示栈的数据元素的个数
        //所以，求链栈的长度时，必须把栈中的数据元素一个个出栈或者再创建一个栈来接受
        //所以增设一个字段length表示链栈中结点的个数
        private int length;

        public Node<T> Top
        {
            get { return top; }
        }
        public int Length
        {
            get { return length; }
            set { length = value; }
        }
        public LinkStack()
        {
            top = null;
            length = 0;
        }

        public int GetLength()
        {
            return length;
        }

        public bool IsEmpty()
        {
            return top == null && length == 0;
        }

        public void Clear()
        {
            top = null;
            length = 0;
        }

        //注意链栈的next指针是从栈顶指向栈底的！！！
        public void Push(T elem)
        {
            Node<T> newNode = new Node<T>(elem);
            if (top == null)
            {
                top = newNode;
            }
            else
            {
                newNode.Next = top;
                top = newNode;
            }
            length++;
        }

        public T Pop()
        {
            T result = default(T);
            if (IsEmpty())
            {
                Console.WriteLine("链栈为空！");
            }
            else
            {
                result = top.Data;
                top = top.Next;
                length--;
            }
            return result;
        }

        public T GetTop()
        {
            T result = default(T);
            if (IsEmpty())
            {
                Console.WriteLine("链栈为空！");
            }
            else
            {
                result = top.Data;
            }
            return result;
        }
    }
}
