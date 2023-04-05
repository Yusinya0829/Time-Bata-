using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody2D myRigidbody2D;
    // Start is called before the first frame update
    public bool isOnTheGround = false;  //腳色是否在地上
    public float TimerBeforeHalfSecond = 0; 
    public float TimerAfterHalfSecond = 0;            //計時器
    public float fallSpeedAfterHalfSecond = 4;
    public float fallSpeedBeforeHalfSecond;
    public float glideSpeed = 5f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fall();
    }
    void OnCollisionEnter2D(Collision2D other) 
    {
        Debug.Log(TimerBeforeHalfSecond);
        if(other.gameObject.tag == "ground")
        {
            isOnTheGround = true;
        }
    }
    void OnCollisionExit2D(Collision2D other) 
    {
        if(other.gameObject.tag == "ground")
        {
            isOnTheGround = false;
        }
    }
    void fall()
    {
        if(isOnTheGround)   //在地板上不下降
        {
            Debug.Log("在地板上");
            TimerBeforeHalfSecond = 0;
            myRigidbody2D.velocity = new Vector2(0 , 0);
        }
        /*else if(Input.GetKey(KeyCode.S) == false)
        {
            Debug.Log("沒按S");
            TimerBeforeHalfSecond = 0;
        }*/
        else if(TimerBeforeHalfSecond <= 0.5)   //0.5秒前的下墜
        {       
            press_S_ToIncreaseFallSpeed();
            TimerBeforeHalfSecond += Time.deltaTime;    //Timer隨時間上升
            fallSpeedBeforeHalfSecond = 10f * (TimerBeforeHalfSecond + 0.435f) * (TimerBeforeHalfSecond + 0.435f) + 0.874f; //讓速度隨時間提升
            if(fallSpeedBeforeHalfSecond >= 9.616f)
            {
              fallSpeedBeforeHalfSecond = 9.616f; //最高提到9.616
            }
            press_J_ToGlideInTheSky();
            myRigidbody2D.velocity = new Vector2(0,-fallSpeedBeforeHalfSecond);
        }
        else    //0.5秒後的下墜
        {
            press_J_ToGlideInTheSky();
            press_S_ToIncreaseFallSpeed();
            myRigidbody2D.velocity = new Vector2(0,-fallSpeedAfterHalfSecond);
            if(fallSpeedAfterHalfSecond <= 14.95f)      
            {
                fallSpeedAfterHalfSecond += 0.01f;      //一直讓速度上升0.01直到14.95
            }
            else if(Input.GetKey(KeyCode.S))
            {
                fallSpeedAfterHalfSecond = 25f;         //如果有按S的下降極限速度
            }
            else
            {
                fallSpeedAfterHalfSecond -= 0.1f;       //S放掉後讓速度回到14.95
            }
        }
    }
    void press_S_ToIncreaseFallSpeed()
    {
        if(Input.GetKey(KeyCode.S) && TimerBeforeHalfSecond <= 0.5)
        {
            fallSpeedAfterHalfSecond += 0.1f;
        }
        else if(Input.GetKey(KeyCode.S) && TimerBeforeHalfSecond > 0.5)
        {
            fallSpeedAfterHalfSecond += 0.1f;
        }
    }
    void press_J_ToGlideInTheSky()
    {
        if(Input.GetKey(KeyCode.J) && !(Input.GetKey(KeyCode.S)))
        {
            fallSpeedAfterHalfSecond = glideSpeed;
            fallSpeedBeforeHalfSecond = glideSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.J))
        {   
            fallSpeedBeforeHalfSecond = 9.616f;
            fallSpeedAfterHalfSecond = 9.616f;
        }
    }    
}



