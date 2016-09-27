using UnityEngine;
using System.Collections;

public class DestoryIfUsed : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D col){
		if(col.tag=="Player"){
			//audio.Play();在物体销毁之前播放音频，会无效
			Destroy(gameObject);
		}
	}
}
