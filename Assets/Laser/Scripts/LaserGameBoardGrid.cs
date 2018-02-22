using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class LaserGameBoardGrid : MonoBehaviour, IPointerDownHandler {

	private LaserCanvasManager canvasManager;
	public int[] gameGridID;
	public bool isFixedItem = false;
	public string gameGridType = "Empty";

	public string[] startIndices = new string[] { "CT", "RT", "TR", "CR", "BR", "RB", "CB", "LB", "BL", "CL", "TL", "LT" };
	public string[] goalIndices = new string[] { "T", "R", "B", "L" };

	/*
	public Dictionary<string, int[]> LaserPattern = new Dictionary<string, int[]>(){
	}

	{"CT", new int[] {0, 0, 0}},
		{"CL", new int[] {0, 0, 0}},
		{"CB", new int[] {0, 0, 0}},
		{"CR", new int[] {0, 0, 0}},
		{"LT", new int[] {0, 0, 0}},
		{"LB", new int[] {0, 0, 0}},
		{"RT", new int[] {0, 0, 0}},
		{"RB", new int[] {0, 0, 0}},
	};
	*/
	public List<LaserDirectionInfo> LaserDirection = new List<LaserDirectionInfo>();





	public List<StartNode> startNode = new List<StartNode>(); 
	public GoalNode goalNode;
	public bool isGoalNodeIgnited = false;
	public string mirrorPattern = "";

	void Awake()
	{
		canvasManager = GameObject.Find("Canvas Manager").GetComponent<LaserCanvasManager>();
	}


	void Start () {

	}

	void Update () {

	}

	public void OnPointerDown(PointerEventData eventData){
		switch(canvasManager.gameMode){
			case ("Debug"):
				if (eventData.button == PointerEventData.InputButton.Left) {
					canvasManager.LeftClickedGrid(gameGridID);
				} else if (eventData.button == PointerEventData.InputButton.Right) {
					canvasManager.RightClickedGrid(gameGridID);
				}
				break;
			case ("Game"):
				break;
			default:
				break;
		}

		canvasManager.gameManager.RunGame();
		canvasManager.RefreshNodeDisplay();
	}

	public void RotateSelectedGrid(){
		switch (gameGridType) {
			case("Start"):
				foreach (StartNode node in startNode) {
					int startIndex = Array.IndexOf(startIndices, node.Direction) + 1;
					startIndex = (startIndex >= startIndices.Length) ? 0 : startIndex;
					node.Direction = startIndices[startIndex];
				}
				break;
			case("Goal"):
				if (goalNode != null) {
					int goalIndex = Array.IndexOf(goalIndices, goalNode.Direction) + 1;
					goalIndex = (goalIndex >= goalIndices.Length) ? 0 : goalIndex;
					goalNode.Direction = goalIndices[goalIndex];
				}
				break;
			case("Mirror"):
				switch (mirrorPattern) {
					case ("BR"):
						mirrorPattern = "BL";
						break;
					case ("BL"):
						mirrorPattern = "TR";
						break;
					case ("TR"):
						mirrorPattern = "TL";
						break;
					case ("TL"):
						mirrorPattern = "BR";
						break;
				}
				break;
			default:
				break;
		}
	}
		
	public void DisplayIcons(){
		foreach (StartNode node in startNode){
			for (int j = 0; j < startIndices.Length; j++) {
				Image startGridImage = transform.Find("Start").Find(startIndices[j][1].ToString()).gameObject.GetComponent<Image>();
				if (startIndices[j] != node.Direction) {
					startGridImage.enabled = false;
				} else {
					startGridImage.enabled = true;
					startGridImage.sprite = canvasManager.spriteStart[node.IndexRgb()];

				}
			}
		}
		if (goalNode != null) {
			for (int j = 0; j < goalIndices.Length; j++) {
				Image goalIgnitedImage = transform.Find("GoalIgnited").Find(goalIndices[j]).gameObject.GetComponent<Image>();
				Image goalDarkImage = transform.Find("GoalDark").Find(goalIndices[j]).gameObject.GetComponent<Image>();
				if (isGoalNodeIgnited) {
					if (goalIndices[j] != goalNode.Direction) {
						goalIgnitedImage.enabled = false;
					} else {
						goalDarkImage.enabled = false;
						goalIgnitedImage.enabled = true;
						goalIgnitedImage.sprite = canvasManager.spriteGoalIgnited[goalNode.IndexRgb()];
					}
				} else {
					if (goalIndices[j] != goalNode.Direction) {
						goalDarkImage.enabled = false;
					} else {
						goalDarkImage.enabled = true;
						goalIgnitedImage.enabled = false;
						goalDarkImage.sprite = canvasManager.spriteGoalDark[goalNode.IndexRgb()];
					}
				}
			}
		}
	}

}
