using System;
using System.Collections.Generic;
using UnityEngine;

// List for selecting random items with a weighted chance
public class WeightedList<T>
{
    public int Count => _items.Count;

    private List<Tuple<T, int>> _items = new List<Tuple<T, int>>();
    private int _weightTotal = 0;

    // Add an item with a weight to the list
    public void Add(T item, int weight)
    {
        _weightTotal += weight;
        _items.Add(new Tuple<T, int>(item, _weightTotal));
    }

    // Returns a random from the weighted list
    public T GetRandom()
    {
        int randomWeight = UnityEngine.Random.Range(0, _weightTotal + 1);

        foreach (Tuple<T, int> item in _items)
        {
            if (randomWeight <= item.Item2)
                return item.Item1;
        }

        Debug.LogWarning("Weighted random could not find valid item!");
        return default(T);
    }

    // Removes and returns a random from the weighted list
    public T RemoveRandom()
    {
        if (_items.Count == 0)
            return default(T);

        int randomWeight = UnityEngine.Random.Range(0, _weightTotal + 1);

        foreach (Tuple<T, int> item in _items)
        {
            if (randomWeight <= item.Item2)
            {
                _items.Remove(item);
                return item.Item1;
            }
        }

        Debug.LogWarning("Weighted random could not find valid item!");
        return default(T);
    }
}
