using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teststring : MonoBehaviour
{
    // Start is called before the first frame update
    string text = "0xF06cC0D929EfdF195e4D313Ccd108E40eB079491";
    void Start()
    {
        string str = text.Substring(0, 7);
        string ltr = text.Substring(text.Length - 7);
      //  print(str);
       // print (ltr);
      //  print(str + "..." + ltr);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
