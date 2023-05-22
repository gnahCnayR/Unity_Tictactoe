using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour 
{
    public int whoTurn; //0 = X and 1 = O
    public int turnCount; //counts the number of turn played
    public GameObject[] turnIcons;//displays whos turn
    public Sprite[] playIcons;//0 = X icons and 1 = O icon
    public Button[] tictactoeSpaces;//playable space for game
    public int[] markedSpace;//IDs which space was marked by which player
    public Text winnerText;//holds the text component for winner text.
    public GameObject[] winningLine;//holds all the lines that show what line won
    public GameObject winnerDisplayPanel;
    public int xPlayerScore;
    public int oPlayerScore;
    public Text xPlayerScoreTxt;
    public Text oPlayerScoreTxt;
    public GameObject drawText;
    

    // Start is called before the first frame update
    void Start()
    {
        GameSetup();
    }

    void GameSetup()
    {
        whoTurn = 0;
        turnCount = 0;
        turnIcons[0].SetActive(true);
        turnIcons[1].SetActive(false);
        for(int i = 0; i < tictactoeSpaces.Length; i++)
        {
            tictactoeSpaces[i].interactable = true;
            tictactoeSpaces[i].GetComponent<Image>().sprite = null;
        }
        for(int i = 0; i < markedSpace.Length; i++)
        {
            markedSpace[i] = -100;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TicTacToeButton(int whichNumber)
    {
        tictactoeSpaces[whichNumber].image.sprite = playIcons[whoTurn];
        tictactoeSpaces[whichNumber].interactable = false;

        markedSpace[whichNumber] = whoTurn + 1;
        turnCount++;
        if(turnCount > 4)
        {
            bool isWinner = WinnerCheck();
           if(turnCount == 9 && isWinner == false)
            {
                Draw();
            }
            
        }

        if(whoTurn == 0)
        {
            whoTurn = 1;
            turnIcons[0].SetActive(false);
            turnIcons[1].SetActive(true);
        }
        else
        {
            whoTurn = 0;
            turnIcons[0].SetActive(true);
            turnIcons[1].SetActive(false);
        }
    }

    bool WinnerCheck()
    {
        int sol1 = markedSpace[0] + markedSpace[1] + markedSpace[2];
        int sol2 = markedSpace[3] + markedSpace[4] + markedSpace[5];
        int sol3 = markedSpace[6] + markedSpace[7] + markedSpace[8];
        int sol4 = markedSpace[0] + markedSpace[3] + markedSpace[6];
        int sol5 = markedSpace[1] + markedSpace[4] + markedSpace[7];
        int sol6 = markedSpace[2] + markedSpace[5] + markedSpace[8];
        int sol7 = markedSpace[0] + markedSpace[4] + markedSpace[8];
        int sol8 = markedSpace[0] + markedSpace[4] + markedSpace[6];
        var solutions = new int[] { sol1, sol2, sol3, sol4, sol5, sol6, sol7, sol8 };
        for(int i = 0; i< solutions.Length; i++)
        {
            if(solutions[i] == 3 * (whoTurn+1))
            {
                WinnerDisplay(i);
                return true;
            }
        }
        return false;
    }

    void WinnerDisplay(int indexIn)
    {
        winnerDisplayPanel.gameObject.SetActive(true);
        if(whoTurn == 0)
        {
            xPlayerScore++;
            xPlayerScoreTxt.text = xPlayerScore.ToString();
            winnerText.text = "Player X wins!";
            
        }else if(whoTurn == 1)
        {
            oPlayerScore++;
            oPlayerScoreTxt.text = oPlayerScore.ToString();
            winnerText.text = "Player O wins!";
            
        }
     
        winningLine[indexIn].SetActive(true);
       
    }
    
    public void Replay()
    {
        GameSetup();
        for(int i = 0; i < winningLine.Length; i++)
        {
            winningLine[i].SetActive(false);
        }
        winnerDisplayPanel.SetActive(false);
        drawText.SetActive(false);
    }
    public void Restart()
    {
        Replay();
        xPlayerScore = 0;
        oPlayerScore = 0;
        xPlayerScoreTxt.text = "0";
        oPlayerScoreTxt.text = "0";
    }
    void Draw()
    {
        winnerDisplayPanel.SetActive(true);
        drawText.SetActive(true);
        winnerText.text = "CAT";
    }
}
