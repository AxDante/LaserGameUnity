using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LaserGameManager : MonoBehaviour {
	
	public LaserCanvasManager canvasManager;
	public List<StartNode> startNodes = new List<StartNode>();
	public List<GoalNode> goalNodes = new List<GoalNode>();


	void Start () {
	}

	void Update () {
		
	}

	void Awake(){
		canvasManager = GameObject.Find("Canvas Manager").GetComponent<LaserCanvasManager>();
	}

	public void RunGame(){

		for (int row = 0; row < canvasManager.gameBoardSizeWithBoarder[0]; row++) {
			for (int col = 0; col < canvasManager.gameBoardSizeWithBoarder[1]; col++) {
				ClearInfoOnGrid(new int[]{row, col});
				LaserGameBoardGrid currentGrid = GameObject.Find("grid_" + row.ToString() + "_" + col.ToString()).GetComponent<LaserGameBoardGrid>();
				foreach (StartNode startNode in currentGrid.startNode) {
					startNodes.Add(startNode);
				}
				if (currentGrid.goalNode != null) {
					goalNodes.Add(currentGrid.goalNode);
				}
			}
		}
			

		// Create a Laser from each starting nodes
		foreach (StartNode startNode in startNodes) {
			LaserGameBoardGrid StartGrid = GameObject.Find("grid_" + startNode.Location[0].ToString() + "_" + startNode.Location[1].ToString()).GetComponent<LaserGameBoardGrid>();
			string startPattern = StartGrid.startNode[0].Direction;
			int[] rgb = StartGrid.startNode[0].Rgb;
			if (startPattern == "") {
				return;
			} else {
				int[] nextGridID = new int[] {
					GridInfo.NextGrid[startPattern][0] + startNode.Location[0],
					GridInfo.NextGrid[startPattern][1] + startNode.Location[1]
				};
				CreateLaserOnGrid(nextGridID, startPattern, canvasManager.maxBounces, rgb);
			}
		}

		// Check each goal nodes if the incoming laser is in the corresponding color
		foreach (GoalNode goalNode in goalNodes) {

			int[] previousGridID = new int[2] { goalNode.Location[0] + GridInfo.PreviousGrid[goalNode.Direction][0],
												goalNode.Location[1] + GridInfo.PreviousGrid[goalNode.Direction][1]};
			LaserGameBoardGrid previousGrid = GameObject.Find("grid_" + previousGridID[0].ToString() + "_" + previousGridID[1].ToString()).GetComponent<LaserGameBoardGrid>();
			LaserGameBoardGrid goalGrid = GameObject.Find("grid_" + goalNode.Location[0].ToString() + "_" + goalNode.Location[1].ToString()).GetComponent<LaserGameBoardGrid>();

			foreach (LaserDirectionInfo directionInfo in previousGrid.CombinedLaserDirection){
				foreach (string pattern in GridInfo.laserPattern) {
					string previousPattern = (directionInfo.Heading == 1) ? directionInfo.Location : new string(directionInfo.Location.ToCharArray().Reverse().ToArray());

					if (IsLaserReachGoal(previousPattern, goalNode.Direction, directionInfo.Rgb , goalNode.Rgb)) {
						goalGrid.isGoalNodeIgnited = true;
						Debug.Log("IGN");
					} else {
						goalGrid.isGoalNodeIgnited = false;
					}
					int[] previousGridRgb = (int[])previousGrid.LaserPattern[pattern].Clone();
				}



				int[] previousGridRgb = (int[])previousGrid.LaserPattern[pattern].Clone();
				if (!previousGridRgb.SequenceEqual(new int[3]{ 0, 0, 0 })) {
					if (IsLaserReachGoal(pattern, goalNode.Direction, previousGridRgb, goalNode.Rgb)) {
						goalGrid.isGoalNodeIgnited = true;
						Debug.Log("IGN");
					} else {
						goalGrid.isGoalNodeIgnited = false;
					}
				}
			}
				
			foreach ( string pattern in GridInfo.laserPattern) {
				int[] previousGridRgb = (int[])previousGrid.LaserPattern[pattern].Clone();
				if (!previousGridRgb.SequenceEqual(new int[3]{ 0, 0, 0 })) {
					if (IsLaserReachGoal(pattern, goalNode.Direction, previousGridRgb, goalNode.Rgb)) {
						goalGrid.isGoalNodeIgnited = true;
						Debug.Log("IGN");
					} else {
						goalGrid.isGoalNodeIgnited = false;
					}
				}
			}
		}
	}

	private void CreateLaserOnGrid(int[] gridID, string previousPattern, int bounce, int[] rgb){

		// Return when the maximum bounces is reached
		if (bounce < 0)
			return;
		
		// Return when the beam goes out of boundary
		if (gridID[0] < 0 || gridID[1] < 0 || gridID[0] >= canvasManager.gameBoardSizeWithBoarder[0] || gridID[1] >= canvasManager.gameBoardSizeWithBoarder[1])
			return;
		GameObject currentGirdGO = GameObject.Find("grid_" + gridID[0].ToString() + "_" + gridID[1].ToString());
		LaserGameBoardGrid currentGrid = currentGirdGO.GetComponent<LaserGameBoardGrid>();
		string gridType = currentGrid.gameGridType;

		if (gridType == "Goal"){
			return;
		}

		if (gridType == "Start") {
			gridType = "Empty";
		}

		if (gridType == "Mirror") {
			gridType = "Mirror_" + currentGrid.mirrorPattern;
		}

		if (GridInfo.Patterns[previousPattern][gridType]["NextPattern"].Length == 0) {
			return;
		}

		string[] gridLaserPattern = GridInfo.Patterns[previousPattern][gridType]["Laser"];
		for (int i = 0; i < gridLaserPattern.Length; i++) {

			//foreach(string pattern in GridInfo.laserPattern){
			//}
			//LaserDirectionInfo newLaserOnGrid = new LaserDirectionInfo(rgb, laserLocation,  

			//if (previousPattern == gridLaserPattern[i]) ;
			int laserHeading = LaserHeadingCheck(gridLaserPattern[i]);
			string laserLocation = (laserHeading == 1) ? gridLaserPattern[i] : new string(gridLaserPattern[i].ToCharArray().Reverse().ToArray());


			//LaserDirectionInfo newLaserOnGrid = new LaserDirectionInfo(rgb, laserLocation, 
			//currentGrid.LaserDirection.Add

			//Image targetLaserLoactionImage = currentGrid.transform.Find("Laser").Find(laserLocation).gameObject.GetComponent<Image>();
			//targetLaserLoactionImage.enabled = true;
			//int[] combineRgb = CombineLaserInfo(currentGirdGO, laserLocation, rgb);
			//int combineRgbIndex = combineRgb[0] + combineRgb[1] * 2 + combineRgb[2] * 4 - 1;
			//if (laserLocation[0] == 'C') {
			//	targetLaserLoactionImage.sprite = canvasManager.spriteLaserLineHorizontal[combineRgbIndex];
			//} else {
			//	targetLaserLoactionImage.sprite = canvasManager.spriteLaserLineSlanted[combineRgbIndex];
			//}
		}

		string nextPattern = GridInfo.Patterns[previousPattern][gridType]["NextPattern"][0];

		int[] nextGridID = new int[] {
			GridInfo.NextGrid[nextPattern][0] + gridID[0],
			GridInfo.NextGrid[nextPattern][1] + gridID[1]
		};
		CreateLaserOnGrid(nextGridID, nextPattern, bounce - 1, rgb);

	}

	private bool IsLaserReachGoal(string inputPattern, string goalPattern, int[] inputRgb, int[] goalRgb){
		if (inputRgb.SequenceEqual(goalRgb)) {
			if (inputPattern[1] == 'R' && goalPattern == "L" ||
			   inputPattern[1] == 'L' && goalPattern == "R" ||
			   inputPattern[1] == 'B' && goalPattern == "T" ||
			   inputPattern[1] == 'T' && goalPattern == "B") {
				return true;
			}
		}
		return false;
	}

	private void ClearInfoOnGrid(int[] gridID){
		
		if (gridID[0] < 0 || gridID[1] < 0 || gridID[0] >= canvasManager.gameBoardSizeWithBoarder[0] || gridID[1] >= canvasManager.gameBoardSizeWithBoarder[1])
			return;
		LaserGameBoardGrid currentGrid = GameObject.Find("grid_" + gridID[0].ToString() + "_" + gridID[1].ToString()).GetComponent<LaserGameBoardGrid>();
		currentGrid.isGoalNodeIgnited = false;
		string gridType = currentGrid.gameGridType;
		for (int i = 0; i < GridInfo.laserPattern.Length; i++) {
			currentGrid.transform.Find("Laser").Find(GridInfo.laserPattern[i]).gameObject.GetComponent<Image>().enabled = false;
			currentGrid.GetComponent<LaserGameBoardGrid>().LaserPattern[GridInfo.laserPattern[i]] = new int[3] { 0, 0, 0 };
		}
	}

	private int LaserHeadingCheck(string location){
		if (location == "TL" || location == "BL" || location == "TR" || location == "BR"){
			return -1;
		}
		else if (location == "TC" || location == "BC" || location == "RC" || location == "LC"){
			return -1;
		}
		else{
			return 1;
		}

					/*
		switch (location) {
			case("TL"):
				return "LT";
			case("BL"):
				return "LB";
			case("TR"):
				return "RT";
			case("BR"):
				return "RB";
		}
		return location;
		*/
	}

	private int[] CombineLaserInfo(GameObject targetGrid, string pattern, int[] inputRgb){
		int[] previousRgb = (int[])targetGrid.GetComponent<LaserGameBoardGrid>().LaserPattern[pattern].Clone();
		//Debug.Log(previousRgb[0].ToString() + 's' + previousRgb[1].ToString() + 's' + previousRgb[2].ToString());
		int combineR = previousRgb[0] + inputRgb[0] >= 1 ? 1 : 0;
		int combineG = previousRgb[1] + inputRgb[1] >= 1 ? 1 : 0;
		int combineB = previousRgb[2] + inputRgb[2] >= 1 ? 1 : 0;
		int[] returnRgb = new int[3] { combineR, combineG, combineB };
		targetGrid.GetComponent<LaserGameBoardGrid>().LaserDirection.Add(LaserDirectionInfo)


		LaserPattern[pattern] = (int[]) returnRgb.Clone();
		return returnRgb;
	}

}
