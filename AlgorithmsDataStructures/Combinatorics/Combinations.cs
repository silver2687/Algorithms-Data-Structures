using System.Collections.Generic;

namespace silver2687.Algorithms.Combinatorics
{
    public class Combinations
    {
        public static IEnumerable<T[]> Generate<T>(IList<T> set, int k)
        {
            var result = new T[k];
            Stack<int> stack = new Stack<int>();
            stack.Push(0);

            while (stack.Count > 0)
            {
                int index = stack.Count - 1;
                int value = stack.Pop();

                while (value < set.Count)
                {
                    result[index++] = set[value++];
                    stack.Push(value);

                    if (index == k)
                    {
                        var currentResult = new T[k];
                        Array.Copy(result, currentResult, k);
                        yield return currentResult;
                        break;
                    }
                }
            }
        }
    }

}
