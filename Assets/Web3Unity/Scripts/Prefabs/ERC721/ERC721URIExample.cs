using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ERC721URIExample : MonoBehaviour
{
    async void Start()
    {
        //string chain = "polygon";
        // string network = "mainnet";
        string chain = "ethereum";
        string network = "rinkeby";
        string contract = "0x8Ed46331ef12B9DD3f3B856fD88D52F24CDEdf3F";
        string tokenId = "28";

        string uri = await ERC721.URI(chain, network, contract, tokenId);
        print(uri);
    }
}
