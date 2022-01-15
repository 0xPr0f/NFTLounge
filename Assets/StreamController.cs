/*************************************************
 * Project Name: Unity implements video playback
  * Script creation: magic card
  * Script creation time: 2017.12.21
  * Script function: Control video play class
 * ***********************************************/
using System.Collections;
using System.Collections.Generic;
using RenderHeads.Media.AVProVideo;
using UnityEngine;
using UnityEngine.UI;

//Control video play class
public class StreamController : MonoBehaviour
{
    //Components holding control video playback
    public MediaPlayer mediaPlayer;

    //Hold play pause switch switch
    public Toggle m_videoToggle;

    //Hold a loop play switch
    public Toggle m_loopToggle;

    //Hold control play speed scroll bar
    public Slider m_playSpeedSlider;
    //Hold a drop-down list of control playback speed
    public Dropdown m_playSpeedDropdown;

    //Progress strip holding the progress of the video playback
    public Slider m_processSlider;

    //Hold the display of current playback and total playback
    public Text m_videoTimeTex;

    //Hold a button for a few seconds
    public Button m_backSecondsBtn;
    //Set the number of seconds returned each time you click
    private float m_backSeconds = 3f;

    //Slider holding control volume
    public Slider m_volumeSlider;
    //Silent switch
    public Toggle m_muteToggle;
    //Store the volume of the user setting before mute state
    private float m_customVolume;

    void Awake()
    {
        //Initialization
        //Register to play suspend switch event
        m_videoToggle.onValueChanged.AddListener(DoPlayOrPause);
        //Register whether loop play switch event
        m_loopToggle.onValueChanged.AddListener(DoSetLoop);
        //Registration control speed rolling day trigger event
        m_playSpeedSlider.onValueChanged.AddListener(DoChangeSpeed);

        //Add a drop-down list option
        m_playSpeedDropdown.options.Add(new Dropdown.OptionData("+4"));
        m_playSpeedDropdown.options.Add(new Dropdown.OptionData("+3"));
        m_playSpeedDropdown.options.Add(new Dropdown.OptionData("+2"));
        m_playSpeedDropdown.options.Add(new Dropdown.OptionData("+1"));
        m_playSpeedDropdown.options.Add(new Dropdown.OptionData("+0"));
        m_playSpeedDropdown.options.Add(new Dropdown.OptionData("-1"));
        m_playSpeedDropdown.options.Add(new Dropdown.OptionData("-2"));
        m_playSpeedDropdown.options.Add(new Dropdown.OptionData("-3"));
        m_playSpeedDropdown.options.Add(new Dropdown.OptionData("-4"));
        //Set the initial speed display value
        m_playSpeedDropdown.value = 3;
        m_playSpeedDropdown.captionText.text = m_playSpeedDropdown.options[3].text;
        //Registration control speed drop-down list trigger event
        m_playSpeedDropdown.onValueChanged.AddListener(DoChangeSpeed);

        //Register video play progress strip change trigger event
        m_processSlider.onValueChanged.AddListener(OnProcessSliderChange);

        //Register Returns 1 secondary button trigger event
        m_backSecondsBtn.onClick.AddListener(OnBackSecondsClick);

        //Registered volume Slider event
        m_volumeSlider.onValueChanged.AddListener(OnVolumeSliderChange);
        //Register Silent Switch Event
        m_muteToggle.onValueChanged.AddListener(OnMuteToggleClick);

        //Register video playback trigger event
        mediaPlayer.Events.AddListener(MediaEventHandler);
    }

    /// <summary>
    ///  Video play time trigger
    /// </summary>
    /// <param name="arg0"></param>
    /// <param name="arg1"></param>
    /// <param name="arg2"></param>
    private void MediaEventHandler(MediaPlayer arg0, MediaPlayerEvent.EventType arg1, ErrorCode arg2)
    {
        switch (arg1)
        {
            case MediaPlayerEvent.EventType.Closing:
                Debug.Log("Turn off the player trigger");
                break;
            case MediaPlayerEvent.EventType.Error:
                Debug.Log("Trigger when an error is reported");
                break;
            case MediaPlayerEvent.EventType.FinishedPlaying://Note: This item does not trigger if the video is set to loop play mode.
                Debug.Log("Play completion trigger");
                break;
            case MediaPlayerEvent.EventType.FirstFrameReady:
                Debug.Log("Ready to trigger");
                break;
            case MediaPlayerEvent.EventType.MetaDataReady:
                Debug.Log("Media data preparation preparation trigger");
                break;
            case MediaPlayerEvent.EventType.ReadyToPlay:
                Debug.Log("Ready to play trigger");
                break;
            case MediaPlayerEvent.EventType.Started://Note: Every time you start, playback will trigger once.
                Debug.Log("Start playback trigger");
                break;
            default:
                Debug.Assert(false);
                break;
        }
    }

    void Start()
    {
       

        //Initialization trigger once (synchronous sound size)
        OnVolumeSliderChange(m_volumeSlider.value);
    }

    void Update()
    {
        //Time update play progress method
        DoUpdateVideoProcess();

        //Time update playback time display method
        UpdateTimeText();
    }

    /// <summary>
    ///  Load video method
    /// </summary>
  /*  void LoadVideo()
    {
        //Loaded in the plugin (parameter: 1. Load the path format (corresponding to the panel) 2. Loaded file name 3. Whether to start playing by default)
        //mediaPlayer.OpenVideoFromFile(MediaPlayer.FileLocation.RelativeToStreamingAssetsFolder, "BigBuckBunny_360p30.mp4", false);
    } */
    /// <summary>
    ///  Playback and pause switch Click to trigger
    /// </summary>
    /// <param name="s_isOn"></param>
    void DoPlayOrPause(bool s_isOn)
    {
        //If played, start playing and text display "play"
        if (s_isOn)
        {
            //Control through the MEDIAPLAYER class
            mediaPlayer.Control.Play();
            //Change the text displayed by playback switch
            m_videoToggle.transform.Find("VideoText").GetComponent<Text>().text = "pause";
        }
        //Otherwise, suspend
        else
        {
            mediaPlayer.Control.Pause();
            //Change the text displayed by playback switch
            m_videoToggle.transform.Find("VideoText").GetComponent<Text>().text = "Play";
        }
    }
    /// <summary>
    ///  Whether circulating play switches click trigger
    /// </summary>
    /// <param name="s_isOn"></param>
    void DoSetLoop(bool s_isOn)
    {
        //Control through the MEDIAPLAYER class
        mediaPlayer.Control.SetLooping(s_isOn);
    }
    /// <summary>
    ///  Change the playback speed method (positive acceleration show, negative reverse screen)
    /// </summary>
    void DoChangeSpeed(float s_speed)//Sliding strip value
    {
        //Some flaws (in front sequence reverse order is too fast, and related to computer configuration)
        mediaPlayer.Control.SetPlaybackRate(s_speed);
    }
    /// <summary>
    ///  Change the playback speed method (positive acceleration show, negative reverse screen)
    /// </summary>
    void DoChangeSpeed(int s_speed)//Drop-down list corresponding to the index value
    {
        int tSpeed = 1;
        switch (s_speed)
        {
            case 0:
                tSpeed = 4;
                break;
            case 1:
                tSpeed = 3;
                break;
            case 2:
                tSpeed = 2;
                break;
            case 3:
                tSpeed = 1;
                break;
            case 4:
                tSpeed = 0;
                break;
            case 5:
                tSpeed = -1;
                break;
            case 6:
                tSpeed = -2;
                break;
            case 7:
                tSpeed = -3;
                break;
            case 8:
                tSpeed = -4;
                break;
            default:
                Debug.Assert(false);
                break;
        }
        //Some flaws (in front sequence reverse order is too fast, and related to computer configuration)
        mediaPlayer.Control.SetPlaybackRate(tSpeed);
    }

    /// <summary>
    ///  Time to update the video progress to the slider
    /// </summary>
    void DoUpdateVideoProcess()
    {
        //Get the current playback time
        double tCurrentTime = mediaPlayer.Control.GetCurrentTime();
        //Get the total length of the video
        double tVideoTime = mediaPlayer.Info.GetDuration();
        //Calculate the corresponding playback schedule assignment to the progress of the progress of the playback
        m_processSlider.value = (float)(tCurrentTime / tVideoTime);
    }

    /// <summary>
    ///  Time display of updating playback progress
    /// </summary>
    void UpdateTimeText()
    {
        //Current play time conversion time format
        //Transformation into seconds
        int tCurrentSeconds = (int)mediaPlayer.Control.GetCurrentTime() / 1000;
        //Get the current score
        int tCurrentMin = tCurrentSeconds / 60;
        //How many seconds remain rest
        tCurrentSeconds = tCurrentSeconds % 60;
        string tCurrentSecondStr = tCurrentSeconds < 10 ? "0" + tCurrentSeconds : tCurrentSeconds.ToString();

        //Total time conversion time format
        //Transformation into seconds
        int tVideoTimeSeconds = (int)mediaPlayer.Info.GetDuration() / 1000;
        //Get the total score
        int tVideoTimeMin = tVideoTimeSeconds / 60;
        //How many seconds remain rest
        tVideoTimeSeconds = tVideoTimeSeconds % 60;
        string tVideoTimeSecondStr = tVideoTimeSeconds < 10 ? "0" + tVideoTimeSeconds : tVideoTimeSeconds.ToString();

        //Splicing time display string
        string tTime = string.Format("<color=red>{0}:{1}/{2}:{3}</color>", tCurrentMin, tCurrentSecondStr, tVideoTimeMin, tVideoTimeSecondStr);
        //string tTime = string.Format("<b>{0}:{1}/{2}:{3}</b>", tCurrentMin, tCurrentSeconds, tVideoTimeMin, tVideoTimeSeconds);
        //string tTime = string.Format("<i>{0}:{1}/{2}:{3}</i>", tCurrentMin, tCurrentSeconds, tVideoTimeMin, tVideoTimeSeconds);

        m_videoTimeTex.text = tTime; ;
    }

    /// <summary>
    ///  Video play progress strip change trigger
    /// </summary>
    /// <param name="value"></param>
    void OnProcessSliderChange(float value)
    {
        //Get the total length of the video
        double tVideoTime = mediaPlayer.Info.GetDuration();
        //Current video time
       double tCurrentTime = m_processSlider.value * tVideoTime;
        //Turn the video time to the corresponding node
        mediaPlayer.Control.Seek(tCurrentTime);
    }


    /// <summary>
    ///  Playing progress strips start drag trigger (eventtribrigger triggered)
    /// </summary>
    public void OnProcessSliderDragBegin()
    {
        //Pause playback
        mediaPlayer.Control.Pause();
    }

    /// <summary>
    ///  Play progress bar end drag trigger (EventTrigger trigger)
    /// </summary>
    public void OnProcessSliderDragEnd()
    {
        //Start playing
        mediaPlayer.Control.Play();
    }


    /// <summary>
    ///  Return to a few seconds
    /// </summary>
    void OnBackSecondsClick()
    {
        //Get current play progress time
       double tCurrentTime = mediaPlayer.Control.GetCurrentTime();

        //Pre-progress time before 10S (if there is a top ten seconds, there is no existence or the current time progress)
        tCurrentTime = (tCurrentTime - m_backSeconds * 1000) >= 0 ? tCurrentTime - m_backSeconds * 1000 : tCurrentTime;

        //Set progress time
        mediaPlayer.Control.Seek(tCurrentTime);
    }

    /// <summary>
    ///  Volume progress bar change trigger
    /// </summary>
    /// <param name="value"></param>
    void OnVolumeSliderChange(float value)
    {
        //Save the volume currently set
        if (value != 0)
        {
            m_customVolume = m_volumeSlider.value;
        }
        //Set volume
        mediaPlayer.Control.SetVolume(value);
        //If the volume is manually adjusted to zero, the mute mode opens
        if (value > 0)
        {
            m_muteToggle.isOn = false;
        }
        else
        {
            m_muteToggle.isOn = true;
        }
    }

    /// <summary>
    ///  Silent switch trigger
    /// </summary>
    /// <param name="s_isOn"></param>
    void OnMuteToggleClick(bool s_isOn)
    {
        //If you mute
        if (s_isOn)
        {
            //Set mute
            m_volumeSlider.value = 0;
            mediaPlayer.Control.SetVolume(0);
        }
        //Unmounted
        else
        {
            m_volumeSlider.value = m_customVolume;
            mediaPlayer.Control.SetVolume(m_customVolume);
        }
    }
}