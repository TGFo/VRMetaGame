using TMPro;
using UnityEngine;

public class UIScripts : MonoBehaviour
{
    public TMP_Text text;
    public TMP_Text ScoreText;
    public TMP_Text healthText;
    public GameObject tutorialPanel;
    public GameObject gameplayPanel;
    public GameObject gameOverPanel;
    public TMP_Text gameOverText;
    bool tutHidden = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthText.text = $"HP: {GameManager.instance.playerHealth.ToString()} / {GameManager.instance.playerMaxHealth}";
        ScoreText.text = $"Score: {GameManager.instance.score.ToString()}";
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = $"HP: {GameManager.instance.playerHealth.ToString()} / {GameManager.instance.playerMaxHealth}";
        ScoreText.text = $"Score: {GameManager.instance.score.ToString()}";
        if(GameManager.instance.gameOver)
        {
            GameOver();
        }
    }
    public void ChangeText()
    {
        if(tutHidden == false)
        {
            HideUnhideTutorial();
            return;
        }
        text.text = "Point with your right hand to shoot lazer\nThrow ghosts into orbs to score points\nPress again to hide this screen";
        tutHidden = false;
    }
    public void HideUnhideTutorial()
    {
        tutorialPanel.SetActive(!tutHidden);
        gameplayPanel.SetActive(tutHidden);
    }
    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        gameplayPanel.SetActive(false);
        tutorialPanel.SetActive(false);
        gameOverText.text = $"Game Over\nScore: {GameManager.instance.score}";
    }
}
