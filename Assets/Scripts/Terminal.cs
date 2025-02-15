using UnityEngine;
using TMPro;

public class Terminal : MonoBehaviour
{
    public TMP_InputField field;
    public Unit boundUnit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        field = GetComponent<TMP_InputField>();
        field.ActivateInputField();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
