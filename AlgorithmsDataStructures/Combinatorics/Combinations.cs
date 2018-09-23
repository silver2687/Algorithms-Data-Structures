using System.Collections.Generic;

namespace silver2687.Algorithms.Combinatorics
{
    public class Combinations
    {
        public IEnumerable<T[]> Generate<T>(IList<T> set, int k)
        {
            var result = new T[k];
            Stack<int> stack = new Stack<int>();
            stack.Push(0);

            while (stack.Count > 0)
            {
                int index = stack.Count - 1;
                int value = stack.Pop();

                while (value < k)
                {
                    result[index++] = set[++value];
                    stack.Push(value);

                    if (index == k)
                    {
                        yield return result;
                        break;
                    }
                }
            }
        }
    }
}
