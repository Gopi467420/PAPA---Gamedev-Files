
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class LevelManagerUI : MonoBehaviour
{

    //Level Manager is responsible for all the UI elements

    //Decalring variables    
    private TextMeshProUGUI _moveinstructiontmpcomponent,_fearmetertext, _skiptmpcomponent, _cluesfound;
    private GameObject _findallcluetrigger, _fearmeterintortrigger;
    private TriggerShowInstruction _poisonimagetrigger;
    public GameObject _pausemenu, _mainmenu;
    private GameManager _gamemanager;
    private Image  _respawnmessage, _endgame ;
    public GameObject _helpinstruction, _walkingspeedslider;


    private int _fearmeternum;
    private bool _canshowmainmenu, _settingmode;



    // Start is called before the first frame update
    void Start()
    {
        //Getting all the Text components
        _moveinstructiontmpcomponent = GameObject.FindWithTag("1st Instruction").GetComponent<TextMeshProUGUI>();
        _fearmetertext = GameObject.FindWithTag("FearMeter").GetComponent<TextMeshProUGUI>();
        _skiptmpcomponent = GameObject.FindWithTag("Skip").GetComponent<TextMeshProUGUI>();
        _cluesfound = GameObject.FindWithTag("CluesFound").GetComponent<TextMeshProUGUI>();
        

        //Get all thr trigger components        
        _poisonimagetrigger = GameObject.FindWithTag("PoisonDrawer").GetComponent<TriggerShowInstruction>();
        _fearmeterintortrigger = GameObject.FindWithTag("FearMeterIntroTrigger");
        _findallcluetrigger = GameObject.FindWithTag("FindClueTrigger");

        //Getting all the Image components
        _respawnmessage = GameObject.FindWithTag("RespawnMessage").GetComponent<Image>();
        _endgame = GameObject.FindWithTag("EndGame").GetComponent<Image>();
       

        //Getting GameManger Component
        _gamemanager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

       


        _pausemenu.SetActive(false);
        _fearmetertext.enabled = false;
        _helpinstruction.SetActive(false);
        _canshowmainmenu = true;
        _walkingspeedslider.SetActive(false);

        _walkingspeedslider.GetComponent<Slider>().value = 1.6f;

        Cursor.visible = true;






    }

    // Update is called once per frame
    void Update()
    {
        //if escape key is down then switch to pause manu
        if (Input.GetKeyDown(KeyCode.Escape) && !_canshowmainmenu )
        {
            _gamemanager.SetGamePaused();
            _pausemenu.SetActive(true);
            _mainmenu.SetActive(false);
            Cursor.visible = true;


        }

        if (Input.GetKeyDown(KeyCode.Escape) && _canshowmainmenu)
        {
            _mainmenu.SetActive(true);
            _helpinstruction.SetActive(false);
            Cursor.visible = true;

        }


        //When Game is not paused
        if (!_gamemanager.GetGamePaused())
        {
            UpdateFearMeter();
            UpdateUIAtCheckpoint();
            Cursor.visible = false;


        }

        if (_settingmode && Input.GetKeyDown(KeyCode.Escape))
        {
            _settingmode = false;
            _pausemenu.SetActive(true);
            _walkingspeedslider.SetActive(false);
            Cursor.visible = true;


        }


    }

    //Accesors
    public  TextMeshProUGUI GetMoveInstruction() { return _moveinstructiontmpcomponent; }
    public TextMeshProUGUI GetSkipTMPComponent() { return _skiptmpcomponent; }
    public int GetFearMeter() { return _fearmeternum; }
    public Image GetRespawnImage() { return _respawnmessage; }
   
    



    //Mutators         
    public void ShowInstructions(TextMeshProUGUI _instructiontoshow) { _instructiontoshow.enabled = true; }
    public void HideInstructions(TextMeshProUGUI _instructiontoshow) { _instructiontoshow.enabled = false; }   
    public void ShowImage(Image image) { image.enabled = true; }    
    public void HideImage(Image image) { image.enabled = false; }
    public void SetCanShowMainMenu(bool value) { _canshowmainmenu = value; }



    //Calcualted the distance between the player and the ghost and sets the fear, higher fear higher chances of death
    public void UpdateFearMeter()
    {
       
            _fearmeternum = (int)(100 - Vector3.Distance(_gamemanager.GetPlayer().GetPosition(), _gamemanager.GetGhost().GetPosition()));
            _fearmetertext.text = "FearMeter: " + _fearmeternum.ToString("0");

        
       

    }

    public void ShowMainMenuUI(bool _value)
    {
        if (_value)
        {
            _mainmenu.SetActive(true);

        }
        else if(!_value)
        {
            _mainmenu.SetActive(false);
        }
       
    }

    //Play Buttons
    public void Playbutton()
    {
       if( _gamemanager.GetSkipIntro())
        {
            _gamemanager.GetMediaManager().GetVideoPlayer().enabled = false;
            _gamemanager.ResumeGame();
            _gamemanager.SetGameStart(true);
            _mainmenu.SetActive(false);
            _gamemanager.SetCoutndownstart(true);

        }
       else if (!_gamemanager.GetSkipIntro())
        {
            _gamemanager.GetMediaManager().GetVideoPlayer().Play();
            _mainmenu.SetActive(false);
        }
       
       
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void HelpButton()
    {
        _helpinstruction.SetActive(true);
        _mainmenu.SetActive(false);

    }

    //Resume Button
    public void Resumebutton()
    {
        _pausemenu.SetActive(false);
        _gamemanager.ResumeGame();
      
       
    }
    public void QuitToMainMenu()
    {
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Settingbutton()
    {
        _pausemenu.SetActive(false);
        _walkingspeedslider.SetActive(true);
        _settingmode  = true;
    }


    public void SetWalkingSlider()
    {
        _gamemanager.GetPlayer().SetPlayerWalkSpeed( _walkingspeedslider.GetComponent<Slider>().value);
    }

   //Updates Ui for each Checkpoint  
    public void UpdateUIAtCheckpoint()
    {
        string _checkpoint = _gamemanager.GetLastCheckpoint();
        switch (_checkpoint)
        {
            case "Checkpoint0":
                if (_fearmeterintortrigger.GetComponent<TriggerShowInstruction>().GetInstructionShown()) { _fearmetertext.enabled = true; }
                _poisonimagetrigger.SetCanShow(false);
                break;
            case "Checkpoint1":               
               //diable Fearmeter
               _fearmetertext.enabled = false;
                //Set clues Ui
                _cluesfound.enabled = true;
                _cluesfound.text = "Clues Found:" + _gamemanager.GetCluesFoundInAmandaRoom() + "/" + _gamemanager.GetTotalCluesInAmandaRoom();
                if (_gamemanager.GetPoissonDrawer().Getinteracted()) { _poisonimagetrigger.SetCanShow(true); }                               
                //If all clues are found player can interact with the bookshelf
                //This will hide the image which says find all the clues
                if (_gamemanager.GetCluesFoundInAmandaRoom() >= 2) { _findallcluetrigger.GetComponent<TriggerShowInstruction>().SetCanShow(false); }
                break;
            case "Checkpoint2":
                //diable clues Text
                _cluesfound.enabled = false;
                break;
            case "Checkpoint3":
                //enable Fearmeter
                _fearmetertext.enabled = true;                
                break;
            case "Checkpoint4":
                _fearmetertext.enabled = false;
               
                break;
            case "Checkpoint6":
                ShowImage(_endgame);
                break;
        }
        
    } 


    
}
