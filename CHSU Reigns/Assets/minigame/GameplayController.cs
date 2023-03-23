using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class GameplayController : MonoBehaviour {
    [Header("Math 1")]
    public GameObject math_1;
    public TextMeshProUGUI mathText_1;
    //[Space(10)]
    
    [Header("Math 2")]
    public GameObject math_2;
    public TextMeshProUGUI mathText_2;
    
    [Header("Gameover panel")]
    public GameObject gameOverPanel;

    [Header("UI")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public GameObject finalAlpha;
    public GameObject startPanel;

    public GameObject trueButton;
    public GameObject falseButton;
    
    [Header("Speed")]
    public int speed;

    [SerializeField]  private float rangeMath;
    [SerializeField]  private float moveMath;
    private GameObject Flying;
    private bool check;
    private int flag;
    private int rightNumber;
    private int leftNumber;
    private int mathOperator;
    private int trueResult;
    private int falseResult;
    private int currentScore;
    private int highScore;

    [SerializeField]
    private float limitTime;
        
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip trueClip, falseClip;

    private float currentTime;



    public void Start()
    {
        //PlayerPrefs.SetInt("highscore", 0);
        //PlayerPrefs.Save();
        check = false;
        flag = 0;
        trueButton.SetActive(true);
        falseButton.SetActive(true);
        //limitTime = 1.0f;
         
        currentScore = 0;
        gameOverPanel.SetActive(false);
        limitTime = 120;
        currentTime = limitTime;   
        if (currentScore == 0 || currentScore % 2 == 0)
        {
            CreateMath(mathText_1);
        }
        else CreateMath(mathText_2);
    }

    public void Update()
    {
        if(startPanel.activeSelf) return;

        currentTime -= Time.deltaTime;
        if (currentTime <= 0)
        {
            if (flag == 0)
            {
                audioSource.PlayOneShot(falseClip);
                flag++;
            }
            
            Loss();
        }
        else
        {
            timerText.text = $"Осталось {currentTime.ToString("F0")} секунд";
        }
        Win();
    }

    private void CreateMath(TextMeshProUGUI mathText)
    {
        leftNumber = Random.Range(0, 10);
        rightNumber = Random.Range(0, 10);
        mathOperator = Random.Range(0, 1);
            switch (mathOperator)
        {
            case 0: // plus 
                trueResult = leftNumber + rightNumber;
                falseResult = trueResult + Random.Range(-1, 1);
                mathText.GetComponent<TextMeshProUGUI>().text = leftNumber.ToString() + " + " + rightNumber.ToString() + "= " + falseResult.ToString();
            
                break;

            case 1: // minus
                if (leftNumber >= rightNumber)
                {
                    trueResult = leftNumber - rightNumber;
                    falseResult = trueResult + Random.Range(-2, 2);
                    mathText.GetComponent<TextMeshProUGUI>().text = leftNumber.ToString() + " - " + rightNumber.ToString() + "= " + falseResult.ToString();

                }
                else
                {
                    trueResult = rightNumber - leftNumber;
                    falseResult = trueResult + Random.Range(-2, 2);
                    mathText.GetComponent<TextMeshProUGUI>().text = rightNumber.ToString() + " - " + leftNumber.ToString() + "= " + falseResult.ToString();

                }
                break;               
        }
        scoreText.GetComponent<TextMeshProUGUI>().text = $"{currentScore.ToString()}/20";
    } 

    public void OnTrueButtonClick()
    {
        if (trueResult == falseResult)
        {
            check = true;
            Invoke("Delay", 0.2f);
            audioSource.PlayOneShot(trueClip);
            currentScore += 1;
            if (currentScore == 0 || currentScore % 2 == 0)
            {
                CreateMath(mathText_1);
            }
            else CreateMath(mathText_2);
        }
        else 
        {
            audioSource.PlayOneShot(falseClip);
            flag++;
            Loss();
        }
    }

    public void OnFalseButtonClick() 
    {
        if(trueResult != falseResult)
        {
            check = true;
            Invoke("Delay", 0.2f);
            audioSource.PlayOneShot(trueClip);
            currentScore += 1;
            if (currentScore == 0 || currentScore % 2 == 0)
            {
                CreateMath(mathText_1);
            }
            else CreateMath(mathText_2);

        }
        else 
        {
            audioSource.PlayOneShot(falseClip);
            flag++;
            Loss();
        }
    }
    public void OnMainMenuButtonOnClick()
    {
        SaveManager.DeleteSaveGame();
        SceneManager.LoadScene("main");
    }   

    private void Loss()
    {
        trueButton.SetActive(false);
        falseButton.SetActive(false);
        scoreText.gameObject.SetActive(false);
        timerText.gameObject.SetActive(false);
        gameOverPanel.SetActive(true);
    }
    private void Win()
    {
        if(currentScore == 20)
        {
            currentScore = 0;
        trueButton.SetActive(false);
        falseButton.SetActive(false);
        scoreText.gameObject.SetActive(false);
        timerText.gameObject.SetActive(false);
        finalAlpha.SetActive(true);
        }
    }

    private void Move()
    {
        Vector3 temp = Flying.transform.position;
        temp.x -= speed * Time.deltaTime;
        Flying.transform.position = temp;
    }

    private void Delay()
    {
        check = false;
    }

    void FixedUpdate() // 0.02s
    {
        if ((check == true))
        {
            math_1.transform.localPosition += new Vector3(-moveMath, 0.0f);
            math_2.transform.localPosition += new Vector3(-moveMath, 0f);

        }

        if ((math_1.transform.localPosition.x == -rangeMath))
        {
            math_1.SetActive(false);
            math_1.transform.localPosition = new Vector3(rangeMath, 0f);
            if (math_1.transform.localPosition.x == rangeMath) math_1.SetActive(true);
        }

        if (math_2.transform.localPosition.x == -rangeMath)
        {
            math_2.SetActive(false);
            math_2.transform.localPosition = new Vector3(rangeMath, 0f);
            if (math_2.transform.localPosition.x == rangeMath) math_2.SetActive(true);
        }

    }
}

