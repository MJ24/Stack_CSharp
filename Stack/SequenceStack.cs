using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stack
{
    public class SequenceStack<T> : IStack<T>
    {
        private T[] data;
        private int maxSize;
        private int top;

        public T this[int index]
        {
            get { return data[index - 1]; }
            set { data[index - 1] = value; }
        }
        public int MaxSize
        {
            get { return maxSize; }
            set { maxSize = value; }
        }
        public int Top
        {
            get { return top; }
        }
        public SequenceStack(int size)
        {
            data = new T[size];
            maxSize = size;
            top = -1;
        }

        public bool IsEmpty()
        {
            return top == -1;
        }

        public int GetLength()
        {
            return top + 1;
        }


        public void Clear()
        {
            top = -1;
        }

        public bool IsFull()
        {
            return top == maxSize - 1;
        }

        public void Push(T elem)
        {
            if (IsFull())
            {
                Console.WriteLine("栈已经满了！");
            }
            else
            {
                data[++top] = elem;
            }
        }

        public T Pop()
        {
            T result = default(T);
            if (IsEmpty())
            {
                Console.WriteLine("栈为空！");
            }
            else
            {
                result = data[top];
                top--;
            }
            return result;
        }

        public T GetTop()
        {
            T result = default(T);
            if (IsEmpty())
            {
                Console.WriteLine("栈为空！");
            }
            else
            {
                result = data[top];
            }
            return result;
        }
    }
}
