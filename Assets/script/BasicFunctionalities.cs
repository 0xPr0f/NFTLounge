using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BasicFunctionalities : MonoBehaviour
{
   
    
    public void CopyToClipboard()
    {
        
         string Address = PlayerPrefs.GetString("Account");
    // string address = "0xF06cC0D929EfdF195e4D313Ccd108E40eB079491";
  
        TextEditor textEditor = new TextEditor();
        textEditor.text = "0x772A4f348d85FDd00e89fDE4C7CAe8628c8DAd19";
        textEditor.SelectAll();
        textEditor.Copy();
        Debug.Log("clicked");

    }
}
