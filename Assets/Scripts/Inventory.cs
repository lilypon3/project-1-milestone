using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    int rupee_count = 5;


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


}

