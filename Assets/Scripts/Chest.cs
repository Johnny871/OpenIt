using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Chest : MonoBehaviour
{

    [SerializeField] private Sprite[] sprites;
    private SpriteRenderer spriteRenderer;
    private GameObject spawnedObj;

    private BoxCollider2D boxCollider2D;

    private GameObject gameManagerObj;
    private GameManager gameManagerscr;
    Vector3 openpos;

    void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        boxCollider2D = this.GetComponent<BoxCollider2D>();
        boxCollider2D.enabled = false;
        gameManagerObj = GameObject.FindGameObjectWithTag("GameManager");
        gameManagerscr = gameManagerObj.GetComponent<GameManager>();
        openpos = new Vector3(this.transform.position.x,this.transform.position.y + 1.2f,this.transform.position.z);

    }

   
    void Update()
    {
        
    }

    public void SpawnObject(GameObject obj){
        //送られてきたオブジェクトを生成
        spawnedObj = Instantiate(obj,new Vector3(this.transform.position.x,this.transform.position.y + 1.2f,this.transform.position.z),Quaternion.identity);

    } 

    public void OnClickChest(){
        //ゲームマネージャーにspawnedObjを渡す
        gameManagerscr.ReceiveChestClick(spawnedObj);
        spriteRenderer.sprite = sprites[1];
        spawnedObj.transform.DOLocalMove(openpos,1.0f);
    }

    public void AnimationStart(){
        //時間になったら生成したオブジェクトをチェストの中に隠す
        spawnedObj.transform.DOLocalMove(this.transform.position,1.0f);
        StartCoroutine("PutInChest");
           
    }

    IEnumerator PutInChest(){
        Debug.Log("呼ばれた");
        yield return new WaitForSeconds(1.0f);
        //チェストを閉じたスプライトに変更する
        //タッチの当たり判定を有効化する
        spriteRenderer.sprite = sprites[0]; 
        boxCollider2D.enabled = true;
        gameManagerscr.RoundStart();
    }

    public void Reset(){
        spriteRenderer.sprite = sprites[1];
        spawnedObj.transform.DOLocalMove(openpos,1.0f);
        StartCoroutine("DestroyObject");
    }

    IEnumerator DestroyObject(){
        yield return new WaitForSeconds(1.0f);
        Destroy(spawnedObj);
        gameManagerscr.RoundRestart();
    }

    public void ResetChestCollision(){
        boxCollider2D.enabled = false;
    }
}
