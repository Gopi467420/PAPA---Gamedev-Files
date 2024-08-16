
using UnityEngine;

public class Objectives : MonoBehaviour
{
    //Get Objectives and there poisition on teh level
    [SerializeField] public Sprite[] _objectivelist;
    [SerializeField] public GameObject[] _positions;

    //Get Game manager 
    private GameManager _gamemanager;
    //This is where we change the sprite each time 
    private SpriteRenderer _spriterenderer;

    // Start is called before the first frame update
    void Start()
    {
        _spriterenderer = GetComponentInChildren<SpriteRenderer>();    // dont show objective in the start of the game
        _gamemanager = FindObjectOfType<GameManager>();
        _spriterenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_gamemanager.GetGamePaused())
        {
            LookAtCamera(_spriterenderer.gameObject);
            UpdateObjective();
        }
        
       



    }


    //Accesors
    public SpriteRenderer GetSpriteRenderer() { return _spriterenderer; }
   

    //This method make the objective text on level always look towards the camera
    public void LookAtCamera(GameObject gameobject)
    {
        Vector3 targetPosition = _gamemanager.GetCamera().transform.position;
        targetPosition.y = gameobject.transform.position.y; // Ignore Y axis

        gameobject.transform.LookAt(targetPosition);

        Vector3 direction = _gamemanager.GetCamera().transform.position - gameobject.transform.position;
        direction.y = 0; // Keep the object upright

        Quaternion rotation = Quaternion.LookRotation(direction);
        gameobject.transform.rotation = rotation * Quaternion.Euler(0, 180, 0);
    }

    //Update Obejctiive when checkpoint is reached
    public void UpdateObjective()
    {
        switch (_gamemanager.GetLastCheckpoint())
        {
            case "Checkpoint0":
                _spriterenderer.sprite = _objectivelist[1];
                _spriterenderer.transform.position = _positions[1].transform.position;
                break;
            case "Checkpoint1":
                //if Amanda door is closed then the player can explore
                //reached checkpoint1
                //Explore Mom's room
                _spriterenderer.sprite = _objectivelist[2];
                _spriterenderer.transform.position = _positions[2].transform.position;
                break;
            case "Checkpoint2":
                //richard's room
                _spriterenderer.sprite = _objectivelist[3];
                _spriterenderer.transform.position = _positions[3].transform.position;
                break;
            case "Checkpoint4":
                //explore the basement
                _spriterenderer.sprite = _objectivelist[4];
                _spriterenderer.transform.position = _positions[4].transform.position;

                if (_gamemanager.GetRichardCoffin().GetClueFound())
                {
                    //Escape Basement
                    _spriterenderer.sprite = _objectivelist[5];
                    _spriterenderer.transform.position = _positions[5].transform.position;
                }
                break;
           

        }

        
    }




}
