using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameBehaviour : MonoBehaviour
{
    public GameObject enemy;

    public string labelText = "4つのアイテムをあつめて自由を勝ち取ろう！";
    public int maxItems = 4;

    protected enum GameStatus { Playing = 0, WinScreen = 1, LossScreen = -1 }
    private GameStatus _gameStatus = GameStatus.Playing;
    private int _itemCollected = 0;
    public int Items
    {
        get
        {
            return _itemCollected;
        }
        set
        {
            _itemCollected = value;
            if (_itemCollected >= maxItems)
            {
                labelText = "アイテムを全部みつけたね!";
                _gameStatus = GameStatus.WinScreen ;
                Time.timeScale = 0f;
            }
            else
            {
                labelText = "アイテムをみつけたね。あと、" + (maxItems - _itemCollected) + "個だよ！";
            }
        }
    }
    private int _playerHP = 10;
    public int HP
    {
        get
        {
            return _playerHP;
        }
        set
        {
            _playerHP = value;
            //Debug.LogFormat("HP: {0}", _playerHP);
            if (_playerHP <= 0)
            {
                labelText = "もうひとつライフが欲しい？";
                _gameStatus = GameStatus.LossScreen;
                Time.timeScale = 0f;
            }
            else
            {
                labelText = "いててっ．．．　やられたぜ。";
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        NewEnemy();
    }

    // Update is called once per frame
    /*
    void Update()
    {
        
    }
    */

    void OnGUI()
    {
        GUI.Box(new Rect(20, 20, 150, 25), "プレイヤーのHP：" + _playerHP);
        GUI.Box(new Rect(20, 50, 150, 25), "あつめたアイテム：" + _itemCollected);
        GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height - 50, 300, 50), labelText);

        if (_gameStatus == GameStatus.WinScreen)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "きみの勝ちだ！"))
            {
                RestartLevel();
            }
        }
        else if (_gameStatus == GameStatus.LossScreen)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "きみの負けだ．．．"))
            {
                RestartLevel();
            }
        }
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1.0f;
    }

    public void NewEnemy()
    {
        GameObject newEnemy = Instantiate(enemy,
            this.gameObject.transform.position,
            Quaternion.identity
        ) as GameObject;
        
        newEnemy.GetComponent<EnemyBehaviour>().EnemyDestroyed += NewEnemy;
    }
}
