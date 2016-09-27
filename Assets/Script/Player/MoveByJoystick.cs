using UnityEngine;  

using System.Collections;  



public class MoveByJoystick : MonoBehaviour {  
	public GameObject player;
	public float speed=5;
	public float fireRate=1.5f;
	public GameObject bullet;
	public Transform shotSpawn;
	public AudioSource shotAudio;
	private float joyPositionX;
	private float joyPositionY;
	private float nextFire;
	 
	//方向向量
	private Vector3 rightVector3=new Vector3(0,0,270);
	private Vector3 leftVector3=new Vector3(0,0,90);
	private Vector3 backVector3=new Vector3(0,0,180);
	void OnEnable()  
	{  
		//player=GameObject.FindWithTag ("Player");
		EasyJoystick.On_JoystickMove += OnJoystickMove;  
		EasyJoystick.On_JoystickMoveEnd += OnJoystickMoveEnd;  
		
	}  
	
	
	
	
	
	//移动摇杆结束  
	
	void OnJoystickMoveEnd(MovingJoystick move)  
		
	{  
		
				//停止时，角色恢复idle  
		
				//if (move.joystickName == "MoveJoystick")  
			
				//{  
			
				//	animation.CrossFade("idle");  
			
				//}  
				joyPositionX = 0;
				joyPositionY = 0;
				if (player != null) {
						player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
				}
		}
	

	
	
	
	//移动摇杆中  
	
	void OnJoystickMove(MovingJoystick move)  
		
	{  
		if(player!=null)
		{
			if (move.joystickName != "MoveJoystick")  		
			{  	
				return;  
			}  
			//获取摇杆中心偏移的坐标  
			//float joyPositionX = move.joystickAxis.x;  
			//float joyPositionY = move.joystickAxis.y;  
			joyPositionX = move.joystickAxis.x;  
			
			joyPositionY = move.joystickAxis.y;  
			
			//print (joyPositionX + "," + joyPositionY);
			if (Mathf.Abs (joyPositionX) > Mathf.Abs (joyPositionY)) 
			{
				if (joyPositionX > 0)
					player.transform.eulerAngles = rightVector3;
				else
					player.transform.eulerAngles = leftVector3;
				player.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(joyPositionX)*speed,0);
			} 
			else if (Mathf.Abs (joyPositionX) < Mathf.Abs (joyPositionY)) 
			{
				if(joyPositionY>0)
					player.transform.eulerAngles=Vector3.zero;
				else
					player.transform.eulerAngles=backVector3;
				player.GetComponent<Rigidbody2D>().velocity = new Vector2(0,Mathf.Sign(joyPositionY)*speed);
			}	
		}
	}
	public void ShotFire()
	{
		if(Time.time>nextFire)
		{
			nextFire=Time.time+fireRate;
			Instantiate(bullet,shotSpawn.position,shotSpawn.rotation);
			//player.audio.Play();
			shotAudio.Play();
		}

	}
}  