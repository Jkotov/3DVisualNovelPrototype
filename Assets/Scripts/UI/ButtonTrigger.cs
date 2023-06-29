using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTrigger : MonoBehaviour
{
    public GameObject Tip;
    [TextAreaAttribute]
    public string textValue;
    public Text textElement;


    private void OnTriggerEnter(Collider other){
        
            Tip.SetActive(true);
            textElement.text = textValue;
    }
    private void OnTriggerExit(Collider other){
        
            Tip.SetActive(false);
    
    }
}
