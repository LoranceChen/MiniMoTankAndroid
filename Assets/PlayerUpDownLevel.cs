using UnityEngine;
using System.Collections;

public class PlayerUpDownLevel : MonoBehaviour {
	public GameObject star;//吃星星会升级
	public float health;//升级影响血量
	public Color colorChange;
	public AudioSource audioSource;
		//s升级会改变坦克颜色
	private SpriteRenderer tankSprite;  
	private GameController gameController;

	void Awake()
	{
		GameObject tankImage = transform.GetChild (0).gameObject;//索引第一个是0(最下面)，第二个是1（倒数第二个）
		//if (tankImage != null)
		//	print ("tank01");
		tankSprite = tankImage.GetComponent<SpriteRenderer>();
	}
	void Start()
	{
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		if ((gameControllerObject) != null)
		{
			gameController = gameControllerObject.GetComponent <GameController>();
			//playerUpDownlevel=playerObject.GetComponent<PlayerUpDownLevel>();
		}
		if (gameController == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}
	}
	void OnTriggerEnter2D(Collider2D col)
	{
		//关于交互，星和玩家，有两种角度
		//1、玩家吃腥，星消失。destory在星物体上处理，吃星玩家变墙在Player上处理
		//2、被动形式，星被吃，星消失在Player上处理，星被玩家吃，玩家会受到星的影响，玩家的增强在星上处理
		//比较后发现，应选择1方式，避免了不不必要的关联。通过内置的碰撞检测来交互。方法2会多于的调用getCompont/GameObject额外花费时间
		if(col.tag=="star")//tag用来获取gameobject物体，也可用来检测碰撞.标记完全相同的对象为一个tag，每个重要的profab都应该不同
		{
			if(health<3)//只有血量<3，吃了才会有效
			{
				//print("health:"+health);
				audioSource.Play();
				health++;
				print("health:"+health);
				colorChange=new Color(0.25f,0.25f,0.25f,0.0f);
				tankSprite.color-=colorChange;//变黑了，bug vector4有加法操作符
			}
		}
		else if(col.tag=="enemyBullet")//与敌方子弹碰撞生命减一，颜色变为响应等级
		{
			health--;
			if(health>0)
			{
				colorChange=new Color(0.25f,0.25f,0.25f,0.0f);
				tankSprite.color+=colorChange;
			}
			else 
			{
				gameController.GameOver();
				Destroy(gameObject);
			}
		}
	}
}
