using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LaserGameItem : MonoBehaviour, IPointerDownHandler {

	private LaserCanvasManager canvasManager;

	public string gameItemName;


	void Start () {
		canvasManager = GameObject.Find("Canvas Manager").GetComponent<LaserCanvasManager>();
	}

	void Update () {
		
	}

	public void OnPointerDown(PointerEventData eventData){

		if (eventData.button == PointerEventData.InputButton.Left) {
			Debug.Log("Left clicked " + gameItemName + " item.");
			canvasManager.LeftClickedGameItem(gameItemName);
		}

	}
	public void SetGameItemName(string name){
		gameItemName = name;
	}
		
}
