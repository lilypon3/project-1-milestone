using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    int key_count = 0;
    int rupee_count = 5;
    int bombCount = 0;


    public void AddRupee(int num_rupees)
    {
        rupee_count += num_rupees;
    }

    public void RemoveRupee(int num_rupees)
    {
        rupee_count -= num_rupees;
        if (rupee_count < 0)
        {
            rupee_count = 0;
        }
    }

    public int GetRupees()
    {
        return rupee_count;
    }

    public void AddKey(int num_keys)
    {
        key_count += num_keys;
    }

    public int GetKeys()
    {
        return key_count;
    }

    public void UseKey()
    {
        key_count = Mathf.Max(0, key_count - 1);
    }

    public void AddBomb(int num_bombs)
    {
        bombCount += num_bombs;
    }

    public int GetBombs()
    {
        return bombCount;
    }

    

}

