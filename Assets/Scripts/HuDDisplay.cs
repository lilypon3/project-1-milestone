using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;


public class HuDDisplay : MonoBehaviour
{

    public Inventory inventory;
    public Health health;
    TextMeshProUGUI text_component;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text_component = GetComponent<TextMeshProUGUI>();

    }

    // Update is called once per frame
    void Update()
    {
        if (inventory != null && text_component != null && health != null)
        {
            string rupee_count_string = "Rupees: " + inventory.GetRupees().ToString();
            string key_count_string = "Keys: " + inventory.GetKeys().ToString();
            string heart_count_string = "Hearts: " + health.current_health.ToString();
            text_component.text = rupee_count_string + "\n" + key_count_string + "\n" + heart_count_string + "\n" + "X for Sword, Z for Alt Weapon";
        }
        
    }
}
