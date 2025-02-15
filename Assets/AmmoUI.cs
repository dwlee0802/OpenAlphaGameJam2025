using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AmmoUI : MonoBehaviour
{
    public Unit boundUnit = null;

    TMP_Text text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(boundUnit != null)
        {
            text.text = "Ammo: " + boundUnit.ammoCount;
        }
    }
}
