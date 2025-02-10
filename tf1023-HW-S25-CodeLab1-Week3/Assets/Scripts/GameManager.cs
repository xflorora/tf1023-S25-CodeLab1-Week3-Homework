using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;
using System.IO;
using Directory = System.IO.Directory;
using File = System.IO.File;

public class GameManager : MonoBehaviour
{
    //defining score
    public int scrollScore;

    //so that it can be called in other scripts
    public static GameManager Instance;

    //defining target number of scrolls needed to advance to next scene
    public int targetScroll = 1;
    
    //my version of high score
    private int totalScroll;

    private TextMeshProUGUI displayText;
    
    //save files
    private const string KeyTotalScroll = "TOTAL_SCROLL";
    private string filePathTotalScroll;
    private const string DirName = "/Data/";
    const string FileName = DirName + "totalScroll.txt";
    //string dirLocation = Application.dataPath + DirName;

    //property of scrollScore variable
    //set and get: basically now these things are only checked / changed when the score is changed instead of every frame.
    //code is more efficient and legible
    public int ScrollScore
    {
        set
        {
            //the variable is the value of this property
            scrollScore = value;

            //if scrollScore equals targetScroll, go to the next level
            if (targetScroll == scrollScore)
            {
                targetScroll += 1;
                totalScroll += scrollScore;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                scrollScore = 0;
            }

            if (scrollScore > totalScroll)
            {
                totalScroll = scrollScore;
            }
            
            displayText.text = "Scroll Score: " + scrollScore + " Total Scrolls: " + totalScroll;
        }
        //return and store the variable
        get
        {
            return scrollScore;
        }
    }
    
    //property of totalScroll variable
    public int TotalScroll
    {
        set
        {
            totalScroll = value;

            if (!File.Exists(filePathTotalScroll))
            {
                string dirLocation = Application.dataPath + DirName;
                if (!Directory.Exists(dirLocation))
                {
                    Directory.CreateDirectory(dirLocation);
                }
            }

            File.WriteAllText(filePathTotalScroll, scrollScore + "");
        }

        get
        {
            if (File.Exists(filePathTotalScroll))
            {
                string FileContents = File.ReadAllText(filePathTotalScroll);
                totalScroll = int.Parse(FileContents);
            }
            
            return totalScroll;
        }
    }
    
    void Start()
    {
        //persistent GameManager and also preventing multiple GameManager scripts in same scene
        if (Instance == null)
        {
            //instance is this script
            Instance = this;
            
            //don't destroy GameManager script in new scene
            DontDestroyOnLoad(this);
            
            displayText = GameObject.Find("ScoreDisplay").GetComponent<TextMeshProUGUI>();
            
            displayText.text = "Scroll Score: " + scrollScore + " Total Scrolls: " + totalScroll;
            
            Debug.Log(Application.dataPath);
            
            filePathTotalScroll = Application.dataPath + FileName;
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
       /* if (targetScroll == scrollScore)
        {
            targetScroll += 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            scrollScore = 0;
        }*/
    }
}
