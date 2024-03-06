using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Unity.Netcode;

public class ScoreManager : NetworkBehaviour
{

    public int score;
    public int waste;  // current waste
    public int wasteLimit;  // max waste before losing

    public int finalScore;
    public bool gameOver;

    public TMP_Text scoreText;
    public TMP_Text wasteText;
    public TMP_Text questionText;


    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        waste = 0;
        finalScore = 0;
        gameOver = false;
        questionText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver) {
            scoreText.text = "Score: " + score;
            wasteText.text = "Waste: " + waste + "/" + wasteLimit;
        }
        else {
            scoreText.text = "Final Score: " + finalScore;
            wasteText.text = "Game Over!";
            questionText.text = "If there are multiple processes that a CPU\nis working on and the one it's looking at is\nbusy, what should the CPU do?";
        }

        if (waste >= wasteLimit && !gameOver) {  // will happen once, right when game ends
            gameOver = true;
            finalScore = score;
        }
        
    }
    
}
