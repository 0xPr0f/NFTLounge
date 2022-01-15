using System;
using System.Collections;
using System.Collections.Generic;
using RenderHeads.Media.AVProVideo;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HandleStream : MonoBehaviour
{
    public MediaPlayer mediaPlayer;
    public TextMeshProUGUI streamtext;
  //  public Button pauseButton;
  //  public Button playButton;
    //https://cdn.livepeer.com/hls/d0be5ilukbwhfoyq/index.m3u8
    // Start is called before the first frame update

    public void PlayStream(){
        try
        {
            bool isOpening = mediaPlayer.OpenMedia(new MediaPath(streamtext.text, MediaPathType.AbsolutePathOrURL), autoPlay: true);
        }
        catch (Exception e)
        {
            print(e);
        }
    }

   
    // Update is called once per frame
    void Update()
    {

        
    }
 
}

