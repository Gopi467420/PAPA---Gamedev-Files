using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Video;

using System.IO;


public class MediaManager : MonoBehaviour
{
    private VideoPlayer _videoPlayer;
    private bool _videoisplaying;
    private GameManager _gamemanager;
    public GameObject _bgsoundgameobject;    
    private AudioSource _bgaudiosource;
    

    
    private TriggerPlayAudio _notthiswayaudiotrigger;
   


    void Start()
    {
        _videoPlayer = GetComponent<VideoPlayer>();
        _gamemanager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();      
        _notthiswayaudiotrigger = GameObject.FindWithTag("NotThisWay").GetComponent<TriggerPlayAudio>();                
        PlayVideo("Assets/StreamingAssets/Flashback.mp4");
       _videoPlayer.Pause();
        _bgaudiosource = _bgsoundgameobject.GetComponent<AudioSource>();
        _bgaudiosource.enabled = false;
        

    }

    private void Update()
    {
        if (!_gamemanager.GetGamePaused())
        {
            UpdateMediaAtCheckpoint();
        }

    }


    //Accesors
    public VideoPlayer GetVideoPlayer() {  return _videoPlayer; }
    public AudioSource Getbgaudioaudiocomponent() { return _bgaudiosource; }
   


    //THis Method plays the Video
    public void PlayVideo(string source)
    {
        _videoPlayer.enabled = true;
        _videoPlayer.url = source;
        _gamemanager.SetGamePaused();
        _videoPlayer.loopPointReached += OnVideoFinished;
        _videoPlayer.Play();

    }
    void OnVideoFinished(VideoPlayer Videoplayer)
    {
        Videoplayer.Stop();
        Videoplayer.enabled = false;
        _videoisplaying = false;

        _gamemanager.ResumeGame();
        _gamemanager.SetGameStart(true);
        _gamemanager.SetCoutndownstart(true);
        

    }

    
    private IEnumerator LoadAndPlayAudio(AudioSource _audiosource, string filename)
    {
        string path = Path.Combine(Application.streamingAssetsPath, filename);

       

        // Load the audio file using UnityWebRequest
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("file://" + path, AudioType.WAV))
        {
            yield return www.SendWebRequest();

            // Check for errors
            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Failed to load audio: " + www.error);
            }
            else
            {
                // Create an AudioClip from the loaded data
                AudioClip clip = DownloadHandlerAudioClip.GetContent(www);

                // Assign the audio clip to the AudioSource
                _audiosource.clip = clip;

                // Play the audio
                _audiosource.Play();

                
            }
        }
    }

    //This Methods Load and play Audio 
    public void PlayAudio(GameObject _source, string filename)
    {
        AudioSource _audiosource = _source.GetComponent<AudioSource>();
        StartCoroutine(LoadAndPlayAudio(_audiosource, filename));
    }

    //This Method plays audio once
    public void PlayAudioOnce(GameObject _source, string filename)
    {
        bool _isplaying = false;
        if (!_isplaying)
        {
            AudioSource _audiosource = _source.GetComponent<AudioSource>();
            StartCoroutine(LoadAndPlayAudio(_audiosource, filename));
            _isplaying = true;
        }

    }


    //This Method Update the media and audio when the checkpoint is reached
    public void UpdateMediaAtCheckpoint()
    {
        string _checkpoint = _gamemanager.GetLastCheckpoint();

        switch (_checkpoint)
        {
            case "Checkpoint0":
                _notthiswayaudiotrigger.SetCanPLay(false);
                break;
            case "Checkpoint1":
                _notthiswayaudiotrigger.SetCanPLay(true);
                break;
        }
        

    }

}
