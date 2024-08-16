
using UnityEngine;

using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{

    //Declaring variables
    private bool _countdownstart, _gamepaused, _startgame, _suzieknowstruth, _fearmeterdisabled;
    private Vector3 _playerinitialposition, _ghostinitialposition, _lastcheckpointposition;
    private int _countdown, _noofcluesinamanadaroom, _cluesfoundinamandaroom;
    private string _lastcheckpoint;
    public bool _skipintro;

    private OpenDoor _amandadoor, _bookshelfdoorComponent;
    private DissolveController _startdissolveanimation;
    private TriggerPlayAudio _bookshelfopenaudio;
    private Light _dissolvelight, _basementcam;
    private Read _photos, _richardscoffin;
    private LevelManagerUI _levelmanager;
    private MediaManager _mediamanager;   
    private OpenDrawer _poisondrawer;    
    private PlayerController _player;
    private GhostController _ghost;
    private Objectives _objective;
    private Camera _camera;
   
    void Start()
    {
        // Get objects with tag
        _levelmanager = GameObject.FindWithTag("LevelManager").GetComponent<LevelManagerUI>();
        _mediamanager = GameObject.FindWithTag("MediaManager").GetComponent<MediaManager>();
        _amandadoor = GameObject.FindWithTag("Amandadoor").GetComponent<OpenDoor>();
        _bookshelfdoorComponent = GameObject.FindWithTag("BookShelf").GetComponent<OpenDoor>();
        _bookshelfopenaudio = GameObject.FindWithTag("BookShelf").GetComponent<TriggerPlayAudio>();
        _richardscoffin = GameObject.FindWithTag("RichardCoffin").GetComponent<Read>();
        _poisondrawer = GameObject.FindWithTag("PoisonDrawer").GetComponent<OpenDrawer>();
        _photos = GameObject.FindWithTag("SeePhotos").GetComponent<Read>();
        _dissolvelight = GameObject.FindWithTag("DissolveLight").GetComponent<Light>();
        _basementcam = GameObject.FindWithTag("BasementCam").GetComponent<Light>();
                
        //Get Object of type
        _player = FindObjectOfType<PlayerController>();
        _ghost = FindAnyObjectByType<GhostController>();
        _objective = FindObjectOfType<Objectives>();

        //get Component with gameobject
        _startdissolveanimation = _ghost.gameObject.GetComponent<DissolveController>();
        _camera = _player.GetPlayerCamera();


        //Intializing
        //Start  Game        
        _gamepaused = true;                                          //game paused at start
        _startgame = false;                                         //game not started because the video is playing
        _countdownstart = false;
        _suzieknowstruth = false;
        _fearmeterdisabled = false;        
        _noofcluesinamanadaroom = 2;
        _cluesfoundinamandaroom = 0;
        _countdown = 1;                                                //countdown for timer events
        _ghostinitialposition = new Vector3(10, 13, -2);            //seting initial position of th ghost 
        _playerinitialposition = new Vector3(-8, 12.5f, 13);        //setting player initial position
       // _playerinitialposition = new Vector3(23, 10, 125);       //Debug Checkp[oint6 
        _camera.GetComponent<Camera>().enabled = true;

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(_countdown);

       //This is for the start of the game
        if (GetStartGame())
        {
            _lastcheckpoint = "Checkpoint0";
            _player.SetPosition(_playerinitialposition);
            _mediamanager.PlayAudio(_player.gameObject, "Weird Dream.wav");
            _levelmanager.ShowInstructions(_levelmanager.GetMoveInstruction());
            SetGameStart(false);
            _mediamanager.Getbgaudioaudiocomponent().enabled = true;
            _levelmanager.SetCanShowMainMenu(false);
           
            

        }



         //if game is not paused
        if (!_gamepaused)
        {
            //Player can move
            _player.SetCanMove(true);
            
            //Things to do when checkpoint reached
            CheckpointReached();

            //Check for timer events
            TimerEvents();

            if(_levelmanager.GetFearMeter() > 97)
            {

                ReloadlastCheckpoint();

            }



        }
        //game is paused
        else
        {
            _player.SetCanMove(false);
           
            _ghost.SetFollowPlayer(false);
            

        }

       
    }


    //Accesors
    public PlayerController GetPlayer() { return _player; }
    public GhostController GetGhost() { return _ghost; }
    public LevelManagerUI GetLevelManager() { return _levelmanager; }
    public MediaManager GetMediaManager() { return _mediamanager; }    
    public bool GetStartGame() { return _startgame; }
    public bool GetGamePaused() { return _gamepaused; }   
    public Camera GetCamera() { return _camera; }   
    public string GetLastCheckpoint() { return _lastcheckpoint; }
    public int GetCluesFoundInAmandaRoom() { return _cluesfoundinamandaroom; }
    public int GetTotalCluesInAmandaRoom() { return _noofcluesinamanadaroom; }
    public OpenDrawer GetPoissonDrawer() { return _poisondrawer; }
    public Read GetRichardCoffin() { return _richardscoffin; }
    public bool GetSkipIntro() { return _skipintro; }




    //Mutators 
    public void SetGamePaused() { _gamepaused = true; }
    public void ResumeGame() { _gamepaused = false; } 
    public void SetGameStart(bool value) { _startgame = value; }
    public void SetCoutndownstart(bool _value) { _countdownstart = _value; }
   
    public void SetFearMeterActive(bool _value) { _fearmeterdisabled = _value; }
   


    //This Method checks if the coutdown timer has started and then performs actions based on the conditions
    //This Method is used for displaying images and instruction and hiding them after a certain time
    private void TimerEvents()
    {       

        if (_countdownstart)
        {
            //start coutdown
            _countdown++;

            

            //Hide Move Instruction
            if (_countdown % 150 == 0 && _levelmanager.GetMoveInstruction().enabled)
            {
                _levelmanager.HideInstructions(_levelmanager.GetMoveInstruction());
                _mediamanager.PlayAudioOnce(_player.gameObject, "FindMom.wav");
                _objective.GetSpriteRenderer().enabled = true;
                _countdownstart = false;
                _countdown = 1;
            }

            //Hide Respawn Image
            if (_countdown % 80 == 0 && _levelmanager.GetRespawnImage().enabled)
            {
                _levelmanager.HideImage(_levelmanager.GetRespawnImage());                
                _countdownstart = false;
                _countdown = 1;
            }

            if (_lastcheckpoint == "Checkpoint6" && _countdown % 180 == 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                _countdownstart = false;
                _countdown = 1;
            }

            //This is called when suzie find the coffin            
            if ( _countdown % 180 == 0 && _suzieknowstruth)
            {
                StartCoroutine(_startdissolveanimation.DissolveCo());
                _fearmeterdisabled = true;
                _countdownstart = false;
                _countdown = 1;

            }

            
        }

        

    }


    //This funciton saves the checkpoint on trigger
    //This used by the Checkpoint script which is a trigger box when player enters this saves the position and the tag of the checpoint which is later used for comparision
    public void SaveCheckpoint(Vector3 position, string tag)
    {
        _lastcheckpointposition = position;
        _lastcheckpoint = tag;

    }

    //This function checks for the last checkpoint and updates the state of the events in the game
    private void CheckpointReached()
    {
        switch (_lastcheckpoint)
        {
            case "Checkpoint0":
               
                break;
            case "Checkpoint1":
                _amandadoor.SetDoorOpen(false);
                _amandadoor.SetCanInteract(false);
                _ghost.SetFollowPlayer(false);
                _ghost.agent.SetDestination(_ghostinitialposition);
                


                if (_poisondrawer.Getinteracted() && !_poisondrawer.GetClueFound())
                {
                    _cluesfoundinamandaroom++;
                    _poisondrawer.SetClueFound(true);


                }
                if (_photos.Getinteracted() && !_photos.GetClueFound())
                {
                    _cluesfoundinamandaroom++;
                    _photos.SetClueFound(true);

                }
                if (_cluesfoundinamandaroom == 2) { _bookshelfdoorComponent.SetCanInteract(true); }
                else { _bookshelfdoorComponent.SetCanInteract(false); }

                if (_bookshelfdoorComponent.Getinteracted()) { _bookshelfopenaudio.SetCanPLay(false); }

                break;
            case "Checkpoint2":

                _ghost.agent.SetDestination(new Vector3(1, 13, 2));
                _ghost.Setposition(new Vector3(1,13, 2));   
                break;
            case "Checkpoint3":

                _ghost.agent.SetDestination(_player.transform.position);
               
                break;
            case "Checkpoint4":
                if (_richardscoffin.GetAudioFinished())
                {
                    _countdownstart = true;
                    _suzieknowstruth = true;

                    //flashing lights and switch camera
                    if (!_startdissolveanimation.GetDissolveEffectCompleted())
                    {
                        _dissolvelight.intensity = 500 * Mathf.Sin(Time.time * 1500);
                        _camera.GetComponent<Camera>().enabled = false;
                    }
                    else
                    {
                        _dissolvelight.intensity = 0;
                        _camera.GetComponent<Camera>().enabled = true;
                        _richardscoffin.SetClueFound(true);
                    }


                    //After 80 frames set the ghost to this position
                    if (_fearmeterdisabled)
                    {
                        // ghost position to back of the player
                        _ghost.SetFollowPlayer(false);
                        _ghost.Setposition(new Vector3(21, 1.5f, -5));
                        _ghost.agent.SetDestination(new Vector3(21, 1.5f, -5));

                    }
                    if (_richardscoffin.GetClueFound()) { _basementcam.intensity = 3; }

                }
                break;
            case "Checkpoint6":
                //end game
                _countdownstart = true;           
                
                break;
        }
    }

    //Reset interactbles
    //This is used when the player respawns
    private void ResetDoors()
    {
        OpenDoor[] doors = FindObjectsOfType<OpenDoor>();
        foreach (OpenDoor door in doors)
        {
            door.SetAnimatorbool(false);
            door.GetAnimator().Play("Door - Idle", 0, 0f);
        }
    }


    //Sets position fro player and ghost for differnt checkpoints
    private void ReloadlastCheckpoint()
    {
        //Show respawn message and reset everything
        _levelmanager.ShowImage(_levelmanager.GetRespawnImage());
        _player.SetPosition(_lastcheckpointposition);
        _countdownstart = true;
        _ghost.SetFollowPlayer(false);

        //Close all the doors
        ResetDoors();

        //set player and ghost positions for differnt checkpoints
        switch (_lastcheckpoint)
        {
            case "Checkpoint0":
                _ghost.Setposition(_ghostinitialposition);
                _ghost.agent.SetDestination(_ghostinitialposition);
                break;

            case "Checkpoint1":
                _ghost.Setposition(_ghostinitialposition);
                _ghost.agent.SetDestination(_ghostinitialposition);
                break;

            case "Checkpoint2":
                _ghost.Setposition(new Vector3(1, 13, 2));
                _ghost.agent.SetDestination(new Vector3(1, 13, 2));
                break;

            case "Checkpoint3":
                _ghost.Setposition(new Vector3(2, 13, -2));
                _ghost.agent.SetDestination(new Vector3(2, 13, -2));
                break;
        }


    }




}


