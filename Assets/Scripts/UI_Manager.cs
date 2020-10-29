using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class UI_Manager : MonoBehaviour
{
    //handle to text
    [SerializeField]
    private Text _score_Text;

    [SerializeField]
    private GameManager GameManager;

    [SerializeField]
    private Sprite[] _livesSprite;
    [SerializeField]
    private Image _livesImage;

    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartText;





    private void Start()
    {
        GameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        _score_Text.text = "Score: " + 0;
        _gameOverText.gameObject.SetActive(false); // already is set to false in Hierarchy



    }



    void GameOverSequence()
    {
        _restartText.gameObject.SetActive(true);
        _gameOverText.gameObject.SetActive(true);
        StartCoroutine(TextFlicker());
        GameManager.GameOver();
    }


    public void UpdateLives(int currentLives)
    {
       currentLives = Mathf.Clamp(currentLives, 0, 3);
        print(currentLives);
        _livesImage.sprite = _livesSprite[currentLives];

        if (currentLives < 1)
        {
            GameOverSequence();
        }

    }

    IEnumerator TextFlicker()
    {
        while (true)
        {

            _gameOverText.text = "Game Over !!!";

            yield return new WaitForSeconds(0.1f);

            _gameOverText.text = " ";

            yield return new WaitForSeconds(0.1f);
        }

    }



    public void UpdateScore(int CurrentScore)
    {
        _score_Text.text = ("Score: " + CurrentScore + ".");
    }





}
