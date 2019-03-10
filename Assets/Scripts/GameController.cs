using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Player
{
    public Image panel;
    public Text text;
    public Button button;
}

[System.Serializable]
public class PlayerColor
{
    public Color panelColor;
    public Color textColor;
}

public class GameController : MonoBehaviour
{
    public Text[] buttonList;
    public GameObject gameOverPanel;
    public Text gameOverText;
    public GameObject restartButton;
    public GameObject startInfo;

    public Player playerX;
    public Player playerO;
    public PlayerColor activePlayerColor;
    public PlayerColor inactivePlayerColor;

    private string playerSide;
    private int moveCount;


    void SetGameControllerReferenceOnButtons()
    {
        for(int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<GridSpace>().SetGameControllerReference(this);
        }
    }

    void Awake()
    {
        SetGameControllerReferenceOnButtons();
        SetStartingSide("X");
        moveCount = 0;
        Debug.Log("1. " + gameOverPanel);
        startInfo.SetActive(false);
        gameOverPanel.SetActive(false);
        restartButton.SetActive(false);
    }

    void StartGame()
    {
        SetBoardInteractable(true);
        SetPlayerButtons(false);
        startInfo.SetActive(false);
    }

    void ChangeSides()
    {
        if(playerSide == "X")
        {
            playerSide = "O";
            SetPlayerColors(playerO, playerX);
        }
        else if(playerSide == "O")
        {
            playerSide = "X";
            SetPlayerColors(playerX, playerO);
        }
        //playerSide = (playerSide == "X") ? "O" : "X";
    }

    void SetPlayerColors(Player newPlayer, Player oldPlayer)
    {
        newPlayer.panel.color = activePlayerColor.panelColor;
        newPlayer.text.color = activePlayerColor.textColor;
        oldPlayer.panel.color = inactivePlayerColor.panelColor;
        oldPlayer.text.color = inactivePlayerColor.textColor;
    }

    void SetPlayerButtons(bool toggle)
    {
        playerX.button.interactable = toggle;
        playerO.button.interactable = toggle;
    }

    void SetPlayerColorsInactive()
    {
        playerX.panel.color = inactivePlayerColor.panelColor;
        playerX.text.color = inactivePlayerColor.textColor;
        playerO.panel.color = inactivePlayerColor.panelColor;
        playerO.text.color = inactivePlayerColor.textColor;
    }

    public string GetPlayerSide()
    {
        return playerSide;
    }

    public void SetStartingSide(string startingSide)
    {
        playerSide = startingSide;
        if(playerSide == "X")
        {
            SetPlayerColors(playerX, playerO);
        }
        else
        {
            SetPlayerColors(playerO, playerX);
        }
        StartGame();
    }

    public void EndTurn ()
    {
        moveCount++;
        //Debug.Log("EndTurn is not implemented");
        //Horizontal
        if (buttonList[0].text == playerSide && buttonList[1].text == playerSide && buttonList[2].text == playerSide)
        {
            GameOver();
        }
        else if (buttonList[3].text == playerSide && buttonList[4].text == playerSide && buttonList[5].text == playerSide)
        {
            GameOver();
        }
        else if (buttonList[6].text == playerSide && buttonList[7].text == playerSide && buttonList[8].text == playerSide)
        {
            GameOver();
        }

        //Vertical
        else if (buttonList[0].text == playerSide && buttonList[3].text == playerSide && buttonList[6].text == playerSide)
        {
            GameOver();
        }
        else if (buttonList[4].text == playerSide && buttonList[1].text == playerSide && buttonList[7].text == playerSide)
        {
            GameOver();
        }
        else if (buttonList[5].text == playerSide && buttonList[8].text == playerSide && buttonList[2].text == playerSide)
        {
            GameOver();
        }

        //Diagonal
        else if (buttonList[0].text == playerSide && buttonList[4].text == playerSide && buttonList[8].text == playerSide)
        {
            GameOver();
        }
        else if (buttonList[2].text == playerSide && buttonList[4].text == playerSide && buttonList[6].text == playerSide)
        {
            GameOver();
        }
 
        //Draw
        else if(moveCount >= 9)
        {
            GameOver("draw");
        }else
        {
            ChangeSides();
        }
    }

    public void RestartGame()
    {
        SetStartingSide("X");
        moveCount = 0;

        Debug.Log(startInfo);
        //Debug.Log("3. " + gameOverPanel);

        gameOverPanel.SetActive(false);
        restartButton.SetActive(false);
        startInfo.SetActive(true);

        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].text = "";
        }
        SetPlayerButtons(true);
        SetPlayerColorsInactive();
    }

    void GameOver()
    {
        SetBoardInteractable(false);
        //Debug.Log("2. " + gameOverPanel);
        GameOver("wins");

    }

    void SetGameOverText(string value)
    {
        Debug.Log("2. " + gameOverPanel);
        gameOverPanel.SetActive(true);
        gameOverText.text = value;
    }

    void SetBoardInteractable(bool toggle)
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<Button>().interactable = toggle;
        }
    }

    void GameOver(string winningPlayer)
    {
        if(winningPlayer == "draw")
        {
            SetGameOverText("It's a Draw!");
            SetPlayerColorsInactive();
        }
        else
        {
            SetGameOverText(playerSide + " Wins!");
        }
        restartButton.SetActive(true);
    }
}
