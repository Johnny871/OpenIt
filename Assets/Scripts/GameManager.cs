using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField] private GameObject[] chestsObj; 
    private Chest chestscr;
    //お題オブジェクト配列作成(9つの要素を保持するString型の配列)(複数作成)
    [SerializeField] private GameObject[] topicObject1;
    [SerializeField] private GameObject[] topicObject2;
    [SerializeField] private GameObject[] topicObject3;
    //お題プールを作成
    [SerializeField] private string[] topicList;
    private int randomTopic;
    private float timer = 0;
    private bool timerActive = true;
    private float timeLimit = 10;
    private bool roundReset = true;

    [SerializeField] private GameObject textobj;
    [SerializeField] private GameObject scoreobj;
    private Text topicText;
    private Text scoreText;
    private int score;
    private string topic;
    private string topicKeeper;

    [SerializeField] private GameObject hpObj;
    private Text hpText;
    private int hp = 3;

    [SerializeField] GameObject gameoverTile;
    [SerializeField] GameObject finalscoreObj;
    private Text finalscoreText;

    void Awake(){
        //9つのチェストを取得
        chestsObj = GameObject.FindGameObjectsWithTag("Chest");
        topicText = textobj.GetComponent<Text>();
        scoreText = scoreobj.GetComponent<Text>();
        hpText = hpObj.GetComponent<Text>();
        finalscoreText = finalscoreObj.GetComponent<Text>();
        foreach(GameObject chest in chestsObj){
                chestscr = chest.gameObject.GetComponent<Chest>();
        }
        topicText.text = "覚えて！";
        }

    void Start(){
        scoreText.text = "Score:" + score;
        hpText.text = "HP:" + hp; 
    }


    void Update(){
        //ifラウンドがリセットされていたらRoundLoadを呼び出し
        if(roundReset){
            roundReset = false;
            RoundLoad();
            if(timeLimit >= 3.0f){
            timeLimit -=0.2f;
        }
        }
        timer += Time.deltaTime;
        //オブジェクトの表示時間を設定
        //表示時間終了時にChestのTimeKeeperを呼ぶ
        if(timer >= timeLimit && timerActive){
            timer = 0;
            timerActive = false;
            for(int i = 0; i < chestsObj.Length; i++){
            chestscr = chestsObj[i].GetComponent<Chest>();
            chestscr.AnimationStart();
            }
        }
        if(gameoverTile.activeSelf){
            if(Input.GetKeyDown("r")){
                gameoverTile.SetActive(false);
                score = 0;
                hp = 3;
                ResetChest();
            }
        }
    }

    void RoundLoad(){
        //9つのチェストをシャッフルの後生成
        switch (Random.Range(0,3)){
            case 0:
                Shuffle(topicObject1);
                break;
            case 1:
                Shuffle(topicObject2);
                break;
            case 2:
                Shuffle(topicObject3);
                break;
        }
        timerActive = true;
    　　topicText.text = "覚えて！";
        
    }

    public void RoundStart(){
        
        topicKeeper = topicList[Random.Range(0,topicList.Length)];
        //お題プールからランダムに問題を表示
        switch(topicKeeper){
            case "red":
                topic = "赤色の図形";
                break;
            case "blue":
                topic = "青色の図形";
                break;
            case "yellow":
                topic = "黄色の図形";
                break;
            case "circle":
                topic = "丸型の図形";
                break;
            case "square":
                topic = "四角の図形";
                break;
            case "triangle":
                topic = "三角の図形";
                break;
        }    
        topicText.text = topic + "の入った箱を1つ開けろ！";

    }

    //お題配列シャッフラー
    void Shuffle(GameObject[] obj){
        for(int i = 0; i < obj.Length; i++){
            GameObject temp = obj[i];
            int randomIndex = Random.Range(0,obj.Length);
            obj[i] = obj[randomIndex];
            obj[randomIndex] = temp;
        }
        //順番にランダムで選別したお題オブジェクトをシャッフルしたチェストに流し、生成させる(引数はString)
        for(int i = 0; i < chestsObj.Length; i++){
            chestscr = chestsObj[i].GetComponent<Chest>();
            chestscr.SpawnObject(obj[i]);
        }
    }  

    public void ReceiveChestClick(GameObject spawnedObj){
        for(int i = 0; i < chestsObj.Length; i++){
            chestscr = chestsObj[i].GetComponent<Chest>();
            chestscr.ResetChestCollision();
        }
        Debug.Log(topicKeeper);
        //正解ならスコア加算、不正解ならスコア減算
        switch(topicKeeper){
            case "red":
                if(spawnedObj.gameObject.tag == "RedCircle" || spawnedObj.gameObject.tag == "RedSquare" || spawnedObj.gameObject.tag == "RedTriangle"){
                    CorrectAnswer();
                }else{
                    IncorrectAnswer();
                }
                break;
            case "blue":
                if(spawnedObj.gameObject.tag == "BlueCircle" || spawnedObj.gameObject.tag == "BlueSquare" || spawnedObj.gameObject.tag == "BlueTriangle"){
                    CorrectAnswer();
                }else{
                    IncorrectAnswer();
                }
                break;
            case "yellow":
                if(spawnedObj.gameObject.tag == "YellowCircle" || spawnedObj.gameObject.tag == "YellowSquare" || spawnedObj.gameObject.tag == "YellowTriangle"){
                    CorrectAnswer();
                }else{
                    IncorrectAnswer();
                }
                break;
            case "circle":
                if(spawnedObj.gameObject.tag == "RedCircle" || spawnedObj.gameObject.tag == "BlueCircle" || spawnedObj.gameObject.tag == "YellowCircle"){
                    CorrectAnswer();
                }else{
                    IncorrectAnswer();
                }
                break;
            case "square":
                if(spawnedObj.gameObject.tag == "RedSquare" || spawnedObj.gameObject.tag == "BlueSquare" || spawnedObj.gameObject.tag == "YellowSquare"){
                    CorrectAnswer();
                }else{
                    IncorrectAnswer();
                }
                break;
            case "triangle":
                if(spawnedObj.gameObject.tag == "RedTriangle" || spawnedObj.gameObject.tag == "BlueTriangle" || spawnedObj.gameObject.tag == "YellowTriangle"){
                    CorrectAnswer();
                }else{
                    IncorrectAnswer();
                }
                break;
        }
        
    }

    private void CorrectAnswer(){
        score += 10;
        scoreText.text = "Score:" + score;
        StartCoroutine("Answer");
    }

    private void IncorrectAnswer(){
        hp --;
        hpText.text = "HP:" + hp;
        if(hp <= 0){
            GameOver();
        }else{
            StartCoroutine("Answer");
        }
       
    }

    IEnumerator Answer(){
        yield return new WaitForSeconds(1.0f);
        ResetChest();
    }

    private void ResetChest(){
        //次のラウンド移行処理
        for(int i = 0; i < chestsObj.Length; i++){
            chestscr = chestsObj[i].GetComponent<Chest>();
            chestscr.Reset();
        }
    }

    public void RoundRestart(){
        roundReset = true;
        topicKeeper = null;
        //hp = 3;
        hpText.text = "HP:" + hp;
        //score = 0;
        scoreText.text = "Score:" + score;
    }

    private void GameOver(){
        gameoverTile.SetActive(true);
        finalscoreText.text = "FinalScore:" + score;
    }
}
