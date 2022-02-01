using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Voice.Unity;
using Photon.Voice.PUN;

public class PushToTalk : MonoBehaviourPun
{
    public KeyCode PushButton = KeyCode.P;
    public Recorder voicerecorder;
    private PhotonView view;
    public bool isTransmiting;
    public Text talkingstatus;

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
        voicerecorder.TransmitEnabled = false;
        talkingstatus.text = "Not Talking";
        isTransmiting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine)
        {
            if (Input.GetKeyDown(PushButton) && isTransmiting == false)
            {
                voicerecorder.TransmitEnabled = true;
                isTransmiting = true;
                talkingstatus.text = "Talking";
            }
            else if (Input.GetKeyDown(PushButton) && isTransmiting == true)
            {
                voicerecorder.TransmitEnabled = false;
                isTransmiting = false;
                talkingstatus.text = "Not Talking";
            }
        }
        
    }
    public void Talk()
    {
        if (view.IsMine)
        {
            if (isTransmiting == false)
            {
                talkingstatus.text = "Talking";
                voicerecorder.TransmitEnabled = true;
                isTransmiting = true;
            }
            else if (isTransmiting == true)
            {
                talkingstatus.text = "Not Talking";
                voicerecorder.TransmitEnabled = false;
                isTransmiting = false;
            }
        }
    }
}
