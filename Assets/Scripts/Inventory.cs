using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    int rupee_count = 0;
    int key_count = 0;


    public void AddRupee(int num_rupees)
    {
        rupee_count += num_rupees;
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


}

