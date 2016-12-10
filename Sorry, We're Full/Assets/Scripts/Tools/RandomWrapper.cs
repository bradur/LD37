// Date   : 10.12.2016 09:40
// Project: Sorry, We're Full
// Author : bradur

using System.Collections.Generic;

public class RandomWrapper
{

    private System.Random rng;

    public RandomWrapper()
    {
        rng = new System.Random();
    }

    public int Range(int min, int max)
    {
        return rng.Next(min, max);
    }

    public T Choose<T>(List<T> list)
    {
        if(list.Count > 0)
        {
            return list[rng.Next(0, list.Count)];
        }
        return default(T);
    }
}
