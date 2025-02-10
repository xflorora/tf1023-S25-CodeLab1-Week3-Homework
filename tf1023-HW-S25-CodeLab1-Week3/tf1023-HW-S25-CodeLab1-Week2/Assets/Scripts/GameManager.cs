using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //defining score
    public int scrollScore;

    //so that it can be called in other scripts
    public static GameManager Instance;

    //defining target number of scrolls needed to advance to next scene
    public int targetScroll = 1;
    
  
    void Start()
    {
        //persistent GameManager and also preventing multiple GameManager scripts in same scene
        if (Instance == null)
        {
            //instance is this script
            Instance = this;
            
            //don't destroy GameManager script in new scene
            DontDestroyOnLoad(this);
        }
        else
        {
            //the original game manager stays, and the "new" one created when loading a new scene
            //is destroyed so there aren't multiple GameManagers
            //destroy the new instance that was just created
            Destroy(gameObject);
        }
    }

   
    void Update()
    {
        //if scrollScore equals targetScroll, go to the next level
        if (targetScroll == scrollScore)
        {
            targetScroll += 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            scrollScore = 0;
        }
    }
}
