using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class block : MonoBehaviour
{
    const string TAG_BREAKABLE = "Breakable";

    //config params
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject blockSparklesVFX;
    [SerializeField] Sprite[] hitSprites;

    //cached reference
    Level level;
    GameStatus gameStatus;

    //state variable
    [SerializeField] int timesHit; //TODO remove seralize 

    private void Start()
    {
        level = FindObjectOfType<Level>();
        if (tag == TAG_BREAKABLE) {
            level.CountBreakableBlocks();
        }
        gameStatus = FindObjectOfType<GameStatus>();
        timesHit = 0;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        gameStatus.AddToScore();
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);

        if (tag == TAG_BREAKABLE) {
            TriggerSparklesVFX();
            timesHit++;
            if (timesHit >= hitSprites.Length+1)
            {
                Destroy(gameObject);
                level.BlockDestory();
            }
            else
            {
                ShowNextHitSprite();
            }

        }
    }

    private void ShowNextHitSprite()
    {
        int spriteIndex = timesHit - 1;
        if (hitSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        else {
            Debug.LogError("block not correct");
        }
    }

    private void TriggerSparklesVFX() {
        GameObject sparkles = Instantiate(blockSparklesVFX,transform.position,transform.rotation);
        Destroy(sparkles, 1f);
    }
}
