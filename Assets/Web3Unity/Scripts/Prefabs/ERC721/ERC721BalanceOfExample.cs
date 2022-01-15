using System.Collections;
using System.Numerics;
using System.Collections.Generic;
using UnityEngine;

public class ERC721BalanceOfExample : MonoBehaviour
{
    async void Start()
    {
        string chain = "ethereum";
        string network = "rinkeby";
        string contract = "0x8Ed46331ef12B9DD3f3B856fD88D52F24CDEdf3F";
        string account = "0x772A4f348d85FDd00e89fDE4C7CAe8628c8DAd19";

        int balance = await ERC721.BalanceOf(chain, network, contract, account);
       print(balance);
    }
}
