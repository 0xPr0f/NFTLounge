using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;


public class GetAccount : MonoBehaviour
{
    public Text MyAccount;
    public TextMeshProUGUI AccountWorld;
   
    // Start is called before the first frame update
    void Start()
    {
        // string Address = PlayerPrefs.GetString("Account");
        string Address = "0x772A4f348d85FDd00e89fDE4C7CAe8628c8DAd19";
        if (Address.Length > 30)
        {
            string FirstNO = Address.Substring(0, 7);
            string LastNO = Address.Substring(Address.Length - 7);

            MyAccount.text = FirstNO + "..." + LastNO;

        }

        // AccountWorld.text = Address;
        // AccountWorld.text = PlayerPrefs.GetString("Account");
        AccountWorld.text = "0x772A4f348d85FDd00e89fDE4C7CAe8628c8DAd19";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
