using System;


namespace DynVis.Math
{
    public class ArraySizeException : Exception
    {
        public ArraySizeException()
        {
        }
        public ArraySizeException(string message): base(message)
        {
        }
    }
}
