using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Networking;


public class NFTMYworld : MonoBehaviour

{
    public class Metadata
    {
        //List storing attributes of the NFT
        public List<Attributes> attributes { get; set; }
        //Description of the NFT
        public string description { get; set; }
        //An external_url related to the NFT (often a website)
        public string external_url { get; set; }
        //image stores the NFTs URI for image NFTs
        public string image { get; set; }
        //Name of the NFT
        public string name { get; set; }
    }
    private class NFTs
    {
        public string contract { get; set; }
        public string tokenId { get; set; }
        public string uri { get; set; }
        public string balance { get; set; }
    }

    async void Start()
    {
        string address = "0x772A4f348d85FDd00e89fDE4C7CAe8628c8DAd19";
        string Address = PlayerPrefs.GetString("Account");
        string chain = "polygon";
        string network = "testnet"; // mainnet ropsten kovan rinkeby goerli
        string account = address;
        string contract = "0x407D0E3BB4A87CCf78aAcDb5F1bb80214D147722";
        //Used for storing metadata
        Metadata metadata;

        string response = await EVM.AllErc721(chain, network, account, contract);
        try
        {
            NFTs[] erc721s = JsonConvert.DeserializeObject<NFTs[]>(response);
            print(erc721s[0].contract);
            print(erc721s[0].tokenId);
            print(erc721s[0].uri);
            print(erc721s[0].balance);

            foreach (var nft in erc721s)
            {
                //print(nft.uri);
                using (UnityWebRequest webRequest = UnityWebRequest.Get(nft.uri))
                {
                    //Sends webrequest
                    await webRequest.SendWebRequest();
                    //Gets text from webrequest
                    string metadataString = webRequest.downloadHandler.text;
                    //Converts the metadata string to the Metadata object
                    metadata = JsonConvert.DeserializeObject<Metadata>(metadataString);
                }

                //Performs another web request to collect the image related to the NFT
                using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(metadata.image))
                {
                    //Sends webrequest
                    await webRequest.SendWebRequest();
                    //Gets the image from the web request and stores it as a texture
                    Texture texture = DownloadHandlerTexture.GetContent(webRequest);
                    //Sets the objects main render material to the texture
                    GetComponent<MeshRenderer>().material.mainTexture = texture;
                }
            }
        }
        catch
        {
           print("Error: " + response);
        }
    }
}
