using TMPro;
using UnityEngine;

public class UIScripts : MonoBehaviour
{
    public TMP_Text text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeText()
    {
        text.text = "Point with your right hand to shoot lazer\nThrow ghosts into orbs";
    }
}
