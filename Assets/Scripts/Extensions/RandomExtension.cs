using System.Collections.Generic;
using UnityEngine;

namespace ExtensionMethods
{
    public static class RandomExtension
    {
        public static List<int> GenerateRandomNumbers(int count, int minValue, int maxValue)
        {
            List<int> possibleNumbers = new List<int>();
            List<int> chosenNumbers = new List<int>();

            for (int index = minValue; index < maxValue; index++)
                possibleNumbers.Add(index);

            while (chosenNumbers.Count < count)
            {
                int position = Random.Range(0, possibleNumbers.Count);
                chosenNumbers.Add(possibleNumbers[position]);
                possibleNumbers.RemoveAt(position);
            }
            return chosenNumbers;
        }
    }
}