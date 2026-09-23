using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;


public class RupeeDisplayer : MonoBehaviour
{

    public Inventory inventory;
    TextMeshProUGUI text_component;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text_component = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inventory != null && text_component != null)
        {
            text_component.text = inventory.GetRupees().ToString();
        }
        
    }
}
