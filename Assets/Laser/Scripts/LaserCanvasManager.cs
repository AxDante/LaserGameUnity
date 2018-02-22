//using System;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;

public class LaserCanvasManager:  MonoBehaviour {

	public int[] gameBoardSize = new int[2] { 7, 7 };
	public int[] gameBoardSizeWithBoarder = new int[2] {11, 11};
	public int maxBounces = 30;
	public int extraRows = 2;
	public int extraCols = 2;

	public int loadedLevel;
	private static float loadingDelayTime = 0.1f;

	public GameObject gameBoardColumnPrefab;
	public GameObject gameBoardRowPrefab;

	public Sprite[] spriteLaserLineHorizontal;
	public Sprite[] spriteLaserLineSlanted;
	public Sprite[] spriteStart;
	public Sprite[] spriteGoalDark;
	public Sprite[] spriteGoalIgnited;

	public Button btnSetStart;
	public Button btnSetGoal;
	public Button btnSetGlass;
	public Button btnSetBlackbody;
	public Button btnSetMirror;
	public Button btnSetMirrorBlock;
	public Button btnSetInvalid;
	public Button btnErase;
	public Button btnReset;
	public Button btnRunGame;
	public Button btnGoToLevel;
	public Button btnSaveAsLevel;
	public Button btnContinue;
	public Button btnRestart;
	public Button btnBack;
	public Button btnCreateLevel;
	public Button btnTestLevel;

	public Dropdown ddStartColor;
	public Dropdown ddGoalColor;

	public InputField inputGoToLevel;
	public InputField inputSaveAsLevel;
	public InputField inputCreateLevelRow;
	public InputField inputCreateLevelCol;
	public InputField inputGlassNum;
	public InputField inputMirrorNum;
	public InputField inputBlackbodyNum;
	public InputField inputMirrorBlockNum;

	public Text txtPointerMode;
	public Text txtCurrentLevel;
	public Text txtBlackbodyNum;
	public Text txtMirrorNum;
	public Text txtMirrorBlockNum;
	public Text txtGlassNum;

	public GameObject gameBoard;
	public GameObject controlPanel;
	public GameObject gameItemPanel;
	public GameObject gameItemPanelBlackbody;
	public GameObject gameItemPanelMirror;
	public GameObject gameItemPanelMirrorBlock;
	public GameObject gameItemPanelGlass;
	public GameObject gameEndingCanvas;

	public LaserGameManager gameManager;
	public LaserGameConfig gameConfiguration;


	public string[] inventoryGameItemName = new string[4] {"Mirror", "MirrorBlock", "Glass", "Blackbody"};
	public int[] inventoryGameItemNum = new int[4] {0, 0, 0, 0};  // {Mirror, MirrorBlock, Glass, Blackbody}
	public int[] inventoryGameItemNumMax = new int[4] {0, 0, 0, 0};

	public string gameMode = "Debug";
	private string pointerDebugMode = "";
	private string pointerGameMode = "";

	public int[] debugBoardSize = new int[2] { 0, 0 };

	void Start () {
		
		SetupGUI();
		LoadSprites();

		gameConfiguration.LoadGameConfiguration(Application.dataPath);

		LaserGameProperties gameProperties = gameConfiguration.Config.properties[0];
		maxBounces = gameProperties.maxBounces;

		// Destroy previous level & load next level
		DestroyLevel();
		loadedLevel = gameProperties.defaultMode;
		Invoke("LoadLevel", loadingDelayTime);

	}

	private void LoadLevel(){

		if (loadedLevel == 0) {
			controlPanel.SetActive(true);
			gameItemPanel.SetActive(false);
			if (debugBoardSize[0] != 0 && debugBoardSize[1] != 0) {
				gameBoardSize = (int[])debugBoardSize.Clone();
			} else {
				gameBoardSize = (int[])gameConfiguration.Config.properties[0].sandboxModeBoardSize.Clone();
			}
		} else {
			controlPanel.SetActive(false);
			gameItemPanel.SetActive(true);

			foreach (LaserLevel levelToSearch in gameConfiguration.Config.levels) {
				if (levelToSearch.level == loadedLevel) {
					gameBoardSize = (int[])levelToSearch.boardSize.Clone();
				}
			}
		}

		gameBoardSizeWithBoarder = new int[2] { gameBoardSize[0] + extraRows * 2, gameBoardSize[1] + extraCols * 2 };

		gameBoard.GetComponent<RectTransform>().sizeDelta = new Vector2(gameBoardSizeWithBoarder[1]* 70, gameBoardSizeWithBoarder[0] * 70);

		GameObject gameBoardRow;
		GameObject gameBoardCol;

		for (int row = 0; row < gameBoardSizeWithBoarder[0]; row++) {
			gameBoardRow = Instantiate(gameBoardRowPrefab, gameBoard.transform);
			gameBoardRow.name = "row_" + row.ToString();
			for (int col = 0; col < gameBoardSizeWithBoarder[1]; col++) {
				gameBoardCol = Instantiate(gameBoardColumnPrefab, gameBoardRow.transform);
				gameBoardCol.name = "grid_" + row.ToString() + "_" + col.ToString();
				gameBoardCol.GetComponent<LaserGameBoardGrid>().gameGridID = new int[] { row, col };
				if (row < extraRows || row >= gameBoardSizeWithBoarder[0] - 2 || col < extraCols || col >= gameBoardSizeWithBoarder[1] - 2) {
					gameBoardCol.GetComponent<LaserGameBoardGrid>().gameGridType = "Invalid";
				} else {
					gameBoardCol.GetComponent<LaserGameBoardGrid>().gameGridType = "Empty";
				}
			}
		}

		if (loadedLevel == 0) {
			AssignGridType(new int[]{extraRows, extraCols}, "Start", "CR");
			AssignGridType(new int[]{gameBoardSizeWithBoarder[0] - extraRows -1, gameBoardSizeWithBoarder[1] - extraCols - 1 }, "Goal", "T");

		} else {
			foreach (LaserLevel levelToSearch in gameConfiguration.Config.levels) {
				if (levelToSearch.level == loadedLevel) {
					foreach (LaserGameBoardItems item in levelToSearch.gameBoardItems) {
						foreach (LaserGameBoardItemParameters parameter in item.parameters){
							LaserGameBoardGrid targetGrid = gameBoard.transform.GetChild(parameter.location[0]).GetChild(parameter.location[1]).gameObject.GetComponent<LaserGameBoardGrid>();
							targetGrid.gameGridType = item.type;
							targetGrid.isFixedItem = true;
							switch(item.type){
								case ("Start"):
									targetGrid.startNode.Add(new StartNode((int[])parameter.location.Clone(), (int[])parameter.rgb.Clone(), parameter.direction));
									//startNodes.Add(new StartNode((int[])parameter.location.Clone(), (int[])parameter.rgb.Clone(), parameter.direction));
									break;
								case ("Goal"):
									targetGrid.goalNode = new GoalNode((int[])parameter.location.Clone(), (int[])parameter.rgb.Clone(), parameter.direction);
									//goalNodes.Add(new GoalNode((int[])parameter.location.Clone(), (int[])parameter.rgb.Clone(), parameter.direction));
									break;
								case ("Mirror"):
									targetGrid.mirrorPattern = parameter.direction;
									break;
								default:
									break;
							}
						}
					}
					inventoryGameItemNum = new int[4] { 0, 0, 0, 0 };
					inventoryGameItemNumMax = new int[4] { 0, 0, 0, 0 };
					foreach (LaserAvailableItems item in levelToSearch.availableItems) {
						int itemIndex = Array.IndexOf(inventoryGameItemName, item.type);
						inventoryGameItemNum[itemIndex] = item.quantity;
						inventoryGameItemNumMax[itemIndex] = item.quantity;
					}
				}
			}
			RefreshGameItemDisplay();
		}

		RunGame();
		RefreshNodeDisplay();
	}

	private void RefreshGameItemDisplay(){
		int itemCount = 0;
		for (int i = 0; i < inventoryGameItemNum.Length; i++){
			if (inventoryGameItemNum[i] == 0) {
				gameItemPanel.transform.GetChild(i).gameObject.SetActive(false);
			} else {
				gameItemPanel.transform.GetChild(i).gameObject.SetActive(true);
				itemCount++;
			}
		}
		gameItemPanel.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(190.0f, 100.0f * itemCount);
	}

	private void AssignGridType(int[] gridID, string gridType, string direction){

		LaserGameBoardGrid targetGrid = gameBoard.transform.GetChild(gridID[0]).GetChild(gridID[1]).gameObject.GetComponent<LaserGameBoardGrid>();
		targetGrid.gameGridType = gridType;
		targetGrid.isFixedItem = true;
		switch(gridType){
			case ("Start"):
				targetGrid.startNode.Add(new StartNode((int[])gridID.Clone(), new int[3] { 0, 1, 0 }, direction));
				//startNodes.Add(new StartNode((int[])gridID.Clone(), new int[3] {0, 1, 0}, direction));
				break;
			case ("Goal"):
				targetGrid.goalNode = new GoalNode((int[])gridID.Clone(), new int[3] { 1, 1, 0 }, direction);
				//goalNodes.Add(new GoalNode((int[])gridID.Clone(), new int[3] {1, 1, 0}, direction));
				break;
			default:
				break;
		}

	}
		
	void Update () {
		txtPointerMode.text = pointerDebugMode;
		//txtCurrentLevel.text = loadedLevel.ToString();
		txtCurrentLevel.text = pointerGameMode;
		txtMirrorNum.text = "x " + inventoryGameItemNum[0].ToString();
		txtMirrorBlockNum.text = "x " + inventoryGameItemNum[1].ToString();
		txtGlassNum.text = "x " + inventoryGameItemNum[2].ToString();
		txtBlackbodyNum.text = "x " + inventoryGameItemNum[3].ToString();

		if (loadedLevel!= 0){
			gameItemPanelBlackbody.transform.Find("SelectionBox").GetComponent<RectTransform>().sizeDelta = new Vector2(160.0f + 12.8f * Mathf.Sin(3.0f * Time.time), 100.0f + 8.0f * Mathf.Sin(3.0f * Time.time));
			gameItemPanelGlass.transform.Find("SelectionBox").GetComponent<RectTransform>().sizeDelta = new Vector2(160.0f + 12.8f * Mathf.Sin(3.0f * Time.time), 100.0f + 8.0f * Mathf.Sin(3.0f * Time.time));
			gameItemPanelMirror.transform.Find("SelectionBox").GetComponent<RectTransform>().sizeDelta = new Vector2(160.0f + 12.8f * Mathf.Sin(3.0f * Time.time), 100.0f + 8.0f * Mathf.Sin(3.0f * Time.time));
			gameItemPanelMirrorBlock.transform.Find("SelectionBox").GetComponent<RectTransform>().sizeDelta = new Vector2(160.0f + 12.8f * Mathf.Sin(3.0f * Time.time), 100.0f + 8.0f * Mathf.Sin(3.0f * Time.time));
		}

		CheckMouseInput();
	}

	private void SetStart(){
		Debug.Log("Setting Starting Grid.");
		pointerDebugMode = "Start";
	}
	private void SetGoal(){
		Debug.Log("Setting Goal Grid.");
		pointerDebugMode = "Goal";
	}
	private void SetGlass(){
		Debug.Log("Setting Glass Grids.");
		pointerDebugMode = "Glass";
	}
	private void SetBlackbody(){
		Debug.Log("Setting Blackbody Grids.");
		pointerDebugMode = "Blackbody";
	}
	private void SetMirror(){
		Debug.Log("Setting Mirror Grids.");
		pointerDebugMode = "Mirror";
	}
	private void SetMirrorBlock(){
		Debug.Log("Setting Mirror Block Grids.");
		pointerDebugMode = "MirrorBlock";
	}
	private void SetInvalid(){
		Debug.Log("Setting Invalid Grid Type.");
		pointerDebugMode = "Invalid";
	}
	private void EraseGridType(){
		Debug.Log("Erasing Grid Type.");
		pointerDebugMode = "Empty";
	}

	private void RunGame(){
		gameManager.RunGame();
	}

	private void DebugCreateLevel(){
		int inputRow = Convert.ToInt32(inputCreateLevelRow.text);
		int inputCol = Convert.ToInt32(inputCreateLevelCol.text);
		debugBoardSize = new int[2] { inputRow, inputCol };
		DestroyLevel();
		loadedLevel = 0;
		Invoke("LoadLevel", loadingDelayTime);

	}
		
	private void SaveAsLevel(){
		// Debug mode
		if (loadedLevel != 0)
			return;

		int inputLevel = Convert.ToInt32(inputSaveAsLevel.text);

		LaserLevel newLevel = new LaserLevel();

		newLevel.level = Convert.ToInt32(inputSaveAsLevel.text);
		newLevel.boardSize = (int[])gameBoardSize.Clone();

		LaserAvailableItems glass = new LaserAvailableItems();
		glass.type = "Glass";
		glass.quantity = Convert.ToInt32(inputGlassNum.text);
		LaserAvailableItems mirror = new LaserAvailableItems();
		mirror.type = "Mirror";
		mirror.quantity = Convert.ToInt32(inputMirrorNum.text);
		LaserAvailableItems mirrorBlock = new LaserAvailableItems();
		mirrorBlock.type = "MirrorBlock";
		mirrorBlock.quantity = Convert.ToInt32(inputMirrorBlockNum.text);
		LaserAvailableItems blackbody = new LaserAvailableItems();
		blackbody.type = "Blackbody";
		blackbody.quantity = Convert.ToInt32(inputBlackbodyNum.text);

		newLevel.availableItems = new List<LaserAvailableItems>();
		newLevel.availableItems.Add(glass);
		newLevel.availableItems.Add(mirror);
		newLevel.availableItems.Add(mirrorBlock);
		newLevel.availableItems.Add(blackbody);

		newLevel.gameBoardItems = new List<LaserGameBoardItems>();
		string[] searchTypes = new string[7] {"Start", "Goal", "Glass", "Mirror", "MirrorBlock", "Blackbody", "Invalid" };
		for (int index = 0; index < searchTypes.Length; index++) {
			LaserGameBoardItems fixedItem = new LaserGameBoardItems();
			fixedItem.type = searchTypes[index];
			for (int row = 0; row < gameBoardSize[0]; row++) {
				for (int col = 0; col < gameBoardSize[1]; col++) {

					LaserGameBoardGrid targetGrid = GameObject.Find("grid_" + (row + extraRows).ToString() + "_" + (col + extraCols).ToString()).GetComponent<LaserGameBoardGrid>();
					string gridType = targetGrid.gameGridType;
					if (gridType == searchTypes[index]) {

						LaserGameBoardItemParameters parameter = new LaserGameBoardItemParameters();
						parameter.location = (int[])targetGrid.gameGridID.Clone();

						switch (searchTypes[index]) {
							case ("Start"):
								parameter.direction = targetGrid.startNode[0].Direction;
								break;
							case ("Goal"):
								parameter.direction = targetGrid.goalNode.Direction;
								break;
							case ("Mirror"):
								parameter.direction = targetGrid.mirrorPattern;
								break;
							default:
								break;
						}
						fixedItem.parameters.Add(parameter);
					}
				}
			}
			newLevel.gameBoardItems.Add(fixedItem);
		}

		gameConfiguration.SaveGameConfiguration(newLevel);

	}
		
	private void GoToLevel(){
		int inputLevel = Convert.ToInt32(inputGoToLevel.text);
		DestroyLevel();
		loadedLevel = inputLevel;
		Invoke("LoadLevel", loadingDelayTime);
	}
		
	private void DestroyLevel(){
		// Destroy previous gameBoard
		foreach (Transform child in gameBoard.transform) {
			GameObject.Destroy(child.gameObject);
		}
	}

	private void StartNextLevel(){
		gameEndingCanvas.GetComponent<Canvas>().enabled = false;
		DestroyLevel();
		loadedLevel += 1;
		Invoke("LoadLevel", loadingDelayTime);
	}

	private void RestartLevel(){
		gameEndingCanvas.GetComponent<Canvas>().enabled = false;
		DestroyLevel();
		Invoke("LoadLevel", loadingDelayTime);
	}

	public void StageCleared(){
		gameEndingCanvas.GetComponent<Canvas>().enabled = true;
	}

	private void BackToLevelPanel(){
	}
		
	private void ResetGridType(){
		Debug.Log("Resetting Grid Type.");

		DestroyLevel();
		loadedLevel = 0;
		Invoke("LoadLevel", loadingDelayTime);
	}

	public void LeftClickedGrid(int[] gameGridID){
		
		LaserGameBoardGrid targetGrid = GameObject.Find("grid_" + gameGridID[0].ToString() + "_" + gameGridID[1].ToString()).GetComponent<LaserGameBoardGrid>();
		string targetGridType = targetGrid.gameGridType;

		if (targetGridType == "Disabled") {
			Debug.Log("Disabled Grid");
			return;
		}
			
		if (loadedLevel == 0) {
			switch (pointerDebugMode) {
				case ("Start"):
					if (targetGridType == "Goal")
						break;
					if (targetGridType == "Start") { 
						targetGrid.RotateSelectedGrid();
					} else {
						// COLOR SWITCH
						int indexRgb = ddStartColor.value + 1;
						int[] startRgb = new int[3] { indexRgb % 2 , (indexRgb / 2) % 2,  indexRgb / 4 }; 
						//startNodes.Add(new StartNode((int[])gameGridID.Clone(), startRgb, "CT"));
						targetGrid.startNode.Add(new StartNode((int[])gameGridID.Clone(), startRgb, "CT"));
						targetGrid.gameGridType = "Start";
					}
					break;

				case ("Goal"):
					if (targetGridType == "Start")
						break;
					if (targetGridType == "Goal") { 
						targetGrid.RotateSelectedGrid();
					} else {
						int indexRgb = ddGoalColor.value + 1;
						int[] goalRgb = new int[3] { indexRgb % 2 , (indexRgb / 2) % 2,  indexRgb / 4 }; 
						//goalNodes.Add(new GoalNode((int[])gameGridID.Clone(), goalRgb, "T"));
						targetGrid.goalNode = new GoalNode((int[])gameGridID.Clone(), goalRgb, "T");
						targetGrid.gameGridType = "Goal";
					}
					break;

				case ("Glass"):
					if (targetGridType == "Start" || targetGridType == "Goal" || targetGridType == "Invalid")
						break;
					targetGrid.gameGridType = "Glass";
					break;

				case ("Blackbody"):
					if (targetGridType == "Start" || targetGridType == "Goal" || targetGridType == "Invalid")
						break;
					targetGrid.gameGridType = "Blackbody";
					break;

				case ("Mirror"):
					if (targetGridType == "Start" || targetGridType == "Goal" || targetGridType == "Invalid")
						break;
					if (targetGridType == "Mirror") {
						targetGrid.RotateSelectedGrid();
					} else {
						targetGrid.gameGridType = "Mirror";
						targetGrid.mirrorPattern = "BR";
					}
					break;

				case ("MirrorBlock"):
					if (targetGridType == "Start" || targetGridType == "Goal" || targetGridType == "Invalid")
						break;
					targetGrid.gameGridType = "MirrorBlock";
					break;

				case ("Empty"):
					// Setting starting or ending grid as obstacles is not allowed.
					if (targetGridType == "Start" || targetGridType == "Goal" || targetGridType == "Invalid")
						break;
					targetGrid.gameGridType = "Empty";
					break;

				case ("Invalid"):
					// Setting starting or ending grid as obstacles is not allowed.
					if (targetGridType == "Start" || targetGridType == "Goal")
						break;
					targetGrid.gameGridType = "Invalid";
					break;

				default:
					break;
			}
		} else if (loadedLevel != 0) {
			
			int itemIndex = Array.IndexOf(inventoryGameItemName, pointerGameMode);

			if (targetGrid.isFixedItem)
				return;
			
			if (pointerGameMode == "") {
				if (targetGridType != "Empty") {
					targetGrid.gameGridType = "Empty";
					inventoryGameItemNum[Array.IndexOf(inventoryGameItemName, targetGridType)] += 1;
				}
				return;
			}

			if (inventoryGameItemNum[itemIndex] <= 0) {
				if (pointerGameMode == "Mirror" && targetGridType == "Mirror") {
					targetGrid.RotateSelectedGrid();
				}
				return;
			}
			
			targetGrid.gameGridType = pointerGameMode;
			inventoryGameItemNum[itemIndex] -= 1;
			if (targetGridType != "Empty") {
				inventoryGameItemNum[Array.IndexOf(inventoryGameItemName, targetGridType)] += 1;
			}
				
			switch (pointerGameMode) {
				case ("Glass"):
					if (targetGridType == "Start" || targetGridType == "Goal" || targetGridType == "Invalid" ) // || inventoryGameItemNum[Array.IndexOf(inventoryGameItemName, "Glass")]  <= 0)
						break;
					targetGrid.gameGridType = "Glass";
					break;

				case ("Blackbody"):
					if (targetGridType == "Start" || targetGridType == "Goal" || targetGridType == "Invalid" )
						break;
					targetGrid.gameGridType = "Blackbody";
					break;

				case ("Mirror"):
					if (targetGridType == "Start" || targetGridType == "Goal" || targetGridType == "Invalid")
						break;
					if (targetGridType == "Mirror") {
						targetGrid.RotateSelectedGrid();
					} else {
						targetGrid.gameGridType = "Mirror";
						targetGrid.mirrorPattern = "BR";
					}
					break;

				case ("MirrorBlock"):
					if (targetGridType == "Start" || targetGridType == "Goal" || targetGridType == "Invalid")
						break;
					targetGrid.gameGridType = "MirrorBlock";
					break;
				default:
					break;
			}
		}
	}

	public void CheckMouseInput(){
		if (Input.GetMouseButtonDown(1)) { //RightClicked 
			pointerDebugMode = "";
			pointerGameMode = "";
			foreach (Transform child in gameItemPanel.transform){
				child.Find("SelectionBox").gameObject.SetActive(false);
			}
		}
	}

	public void LeftClickedGameItem(string gameItemName){
		pointerGameMode = gameItemName;
		foreach (Transform child in gameItemPanel.transform){
			child.Find("SelectionBox").gameObject.SetActive(false);
		}
		GameObject.Find("GameItem_" + gameItemName).transform.Find("SelectionBox").gameObject.SetActive(true);
	}


	public void RightClickedGrid(int[] gameGridID){
		//Debug.Log(gameGridID[0].ToString() + "," + gameGridID[1].ToString()  + "was right clicked.");
	}

	public void RefreshNodeDisplay()
	{
		for (int row = 0; row < gameBoardSizeWithBoarder[0]; row++) {
			for (int col = 0; col < gameBoardSizeWithBoarder[1]; col++) {
				GameObject targetGrid = GameObject.Find("grid_" + row.ToString() + "_" + col.ToString());
				string gridType = targetGrid.GetComponent<LaserGameBoardGrid>().gameGridType;

				Image tileImage = targetGrid.transform.Find("TileImage").GetComponent<Image>();

				DeativateAllGridTypes(targetGrid);

				switch (gridType) {
					case("Start"):
						//tileImage.color = new Color32(255, 149, 149, 255);
						break;
					case("Goal"):
						//tileImage.color = new Color32(11, 246, 127, 255);
						break;
					case("Glass"):
						targetGrid.transform.Find("Glass").GetComponent<RawImage>().enabled = true;
						break;
					case("Blackbody"):
						targetGrid.transform.Find("Blackbody").GetComponent<RawImage>().enabled = true;
						break;
					case("MirrorBlock"):
						targetGrid.transform.Find("MirrorBlock").GetComponent<RawImage>().enabled = true;
						break;
					case("Mirror"):
						switch (targetGrid.GetComponent<LaserGameBoardGrid>().mirrorPattern) {
							case ("BR"):
								targetGrid.transform.Find("Mirror").Find("M_BR").gameObject.GetComponent<Image>().enabled = true;
								break;
							case ("BL"):
								targetGrid.transform.Find("Mirror").Find("M_BL").gameObject.GetComponent<Image>().enabled = true;
								break;
							case ("TR"):
								targetGrid.transform.Find("Mirror").Find("M_TR").gameObject.GetComponent<Image>().enabled = true;
								break;
							case ("TL"):
								targetGrid.transform.Find("Mirror").Find("M_TL").gameObject.GetComponent<Image>().enabled = true;
								break;
						}
						break;
					case("Invalid"):
						tileImage.color = new Color32(0, 0, 0, 0);
						break;
					case("Empty"):
						tileImage.color = new Color32(158, 158, 158, 255);
						break;
				}

				targetGrid.GetComponent<LaserGameBoardGrid>().DisplayIcons();
			}
		}
	}

	private void DeativateAllGridTypes(GameObject targetGrid)
	{
		targetGrid.transform.Find("Glass").gameObject.GetComponent<RawImage>().enabled = false;
		targetGrid.transform.Find("Blackbody").gameObject.GetComponent<RawImage>().enabled = false;
		targetGrid.transform.Find("MirrorBlock").gameObject.GetComponent<RawImage>().enabled = false;
		targetGrid.transform.Find("Mirror").Find("M_BR").gameObject.GetComponent<Image>().enabled = false;
		targetGrid.transform.Find("Mirror").Find("M_BL").gameObject.GetComponent<Image>().enabled = false;
		targetGrid.transform.Find("Mirror").Find("M_TR").gameObject.GetComponent<Image>().enabled = false;
		targetGrid.transform.Find("Mirror").Find("M_TL").gameObject.GetComponent<Image>().enabled = false;
	}

	private void LoadSprites(){
		/*string[] colorSequence = new string[] { "Red", "Green", "Yellow", "Blue", "Magenta", "Cyan", "White"};
		string[] loadSequence = new string[] {"I_Laser01_", "I_Laser02_", "I_Start_", "I_End01_", "I_End02_"};

		for( byte i = 0; i < loadSequence.Length; i++) {
			Sprite[] images = new Sprite[7];
			for( byte j = 0; j < colorSequence.Length; j++){
				images[j] = Resources.Load<Sprite>("Image/" + loadSequence[i] + colorSequence[j] + ".png");
				Debug.Log("Image/" + loadSequence[i] + colorSequence[j]);
			}
			Images[loadSequence[i]] = (Sprite[])images.Clone();
		}
*/
	}
	void SetupGUI () {

		gameBoard = GameObject.Find("GameBoard");
		gameManager = GameObject.Find("GameManager").GetComponent<LaserGameManager>();
		gameConfiguration = GameObject.Find("GameManager").GetComponent<LaserGameConfig>();
		controlPanel = GameObject.Find("ControlPanel");
		gameItemPanel = GameObject.Find("GameItemPanel");

		gameItemPanelBlackbody = GameObject.Find("GameItem_Blackbody");
		gameItemPanelMirror = GameObject.Find("GameItem_Mirror");
		gameItemPanelMirrorBlock = GameObject.Find("GameItem_MirrorBlock");
		gameItemPanelGlass = GameObject.Find("GameItem_Glass");

		gameEndingCanvas = GameObject.Find("GameEndingCanvas");

		btnSetStart = GameObject.Find("Btn_Start").GetComponent<Button>();
		btnSetGoal = GameObject.Find("Btn_Goal").GetComponent<Button>();
		btnSetGlass = GameObject.Find("Btn_SetGlass").GetComponent<Button>();
		btnSetBlackbody = GameObject.Find("Btn_SetBlackbody").GetComponent<Button>();
		btnSetMirror = GameObject.Find("Btn_SetMirror").GetComponent<Button>();
		btnSetMirrorBlock = GameObject.Find("Btn_SetMirrorBlock").GetComponent<Button>();
		btnSetInvalid = GameObject.Find("Btn_SetInvalid").GetComponent<Button>();
		btnErase = GameObject.Find("Btn_Erase").GetComponent<Button>();
		btnReset = GameObject.Find("Btn_Reset").GetComponent<Button>();
		btnRunGame = GameObject.Find("Btn_RunGame").GetComponent<Button>();
		btnGoToLevel = GameObject.Find("Btn_GoToLevel").GetComponent<Button>();
		btnSaveAsLevel = GameObject.Find("Btn_SaveAsLevel").GetComponent<Button>();
		btnContinue = GameObject.Find("Btn_Continue").GetComponent<Button>();
		btnRestart = GameObject.Find("Btn_Restart").GetComponent<Button>();
		btnBack = GameObject.Find("Btn_Back").GetComponent<Button>();
		btnCreateLevel = GameObject.Find("Btn_CreateLevel").GetComponent<Button>();
		btnTestLevel = GameObject.Find("Btn_TestLevel").GetComponent<Button>();

		ddStartColor = GameObject.Find("Dropdown_StartColor").GetComponent<Dropdown>();
		ddGoalColor = GameObject.Find("Dropdown_GoalColor").GetComponent<Dropdown>();

		inputGoToLevel =  GameObject.Find("Input_GoToLevel").GetComponent<InputField>();
		inputSaveAsLevel =  GameObject.Find("Input_SaveAsLevel").GetComponent<InputField>();
		inputCreateLevelRow =  GameObject.Find("Input_CreateLevelRow").GetComponent<InputField>();
		inputCreateLevelCol =  GameObject.Find("Input_CreateLevelCol").GetComponent<InputField>();
		inputGlassNum =  GameObject.Find("Input_GlassNum").GetComponent<InputField>();
		inputMirrorNum =  GameObject.Find("Input_MirrorNum").GetComponent<InputField>();
		inputBlackbodyNum =  GameObject.Find("Input_BlackbodyNum").GetComponent<InputField>();
		inputMirrorBlockNum =  GameObject.Find("Input_MirrorBlockNum").GetComponent<InputField>();

		txtPointerMode = GameObject.Find("Txt_PointerMode").GetComponent<Text>();
		txtCurrentLevel = GameObject.Find("Txt_CurrentLevel").GetComponent<Text>();
		txtBlackbodyNum = gameItemPanelBlackbody.transform.Find("GameItem").Find("Text").GetComponent<Text>();
		txtMirrorNum = gameItemPanelMirror.transform.Find("GameItem").Find("Text").GetComponent<Text>();
		txtMirrorBlockNum = gameItemPanelMirrorBlock.transform.Find("GameItem").Find("Text").GetComponent<Text>();
		txtGlassNum = gameItemPanelGlass.transform.Find("GameItem").Find("Text").GetComponent<Text>();

		Assert.IsTrue(gameBoard != null);
		Assert.IsTrue(gameManager != null);

		Assert.IsTrue(btnSetStart != null);
		Assert.IsTrue(btnSetGoal != null);
		Assert.IsTrue(btnSetGlass != null);
		Assert.IsTrue(btnSetBlackbody != null);
		Assert.IsTrue(btnSetMirror != null);
		Assert.IsTrue(btnSetMirrorBlock != null);
		Assert.IsTrue(btnSetInvalid != null);
		Assert.IsTrue(btnErase != null);
		Assert.IsTrue(btnReset != null);
		Assert.IsTrue(btnRunGame != null);
		Assert.IsTrue(btnGoToLevel != null);
		Assert.IsTrue(btnSaveAsLevel != null);
		Assert.IsTrue(btnContinue != null);
		Assert.IsTrue(btnRestart != null);
		Assert.IsTrue(btnBack != null);

		Assert.IsTrue(inputGoToLevel != null);
		Assert.IsTrue(inputSaveAsLevel != null);

		Assert.IsTrue(txtPointerMode != null);
		Assert.IsTrue(txtCurrentLevel != null);

		// Set up the listeners of buttons
		btnSetStart.onClick.AddListener(delegate {SetStart();});
		btnSetGoal.onClick.AddListener(delegate {SetGoal();});
		btnSetGlass.onClick.AddListener(delegate {SetGlass();});
		btnSetBlackbody.onClick.AddListener(delegate {SetBlackbody();});
		btnSetMirror.onClick.AddListener(delegate {SetMirror();});
		btnSetMirrorBlock.onClick.AddListener(delegate {SetMirrorBlock();});
		btnSetInvalid.onClick.AddListener(delegate {SetInvalid();});
		btnErase.onClick.AddListener(delegate {EraseGridType();});
		btnReset.onClick.AddListener(delegate {ResetGridType();});
		btnRunGame.onClick.AddListener(delegate {RunGame();});
		btnGoToLevel.onClick.AddListener(delegate {GoToLevel();});
		btnContinue.onClick.AddListener(delegate {StartNextLevel();});
		btnRestart.onClick.AddListener(delegate {RestartLevel();});
		btnBack.onClick.AddListener(delegate {BackToLevelPanel();});
		btnSaveAsLevel.onClick.AddListener(delegate {SaveAsLevel();});
		btnCreateLevel.onClick.AddListener(delegate {DebugCreateLevel();});
		btnTestLevel.onClick.AddListener(delegate {});
		foreach (Transform child in gameItemPanel.transform){
			child.Find("SelectionBox").gameObject.SetActive(false);
		}
		gameEndingCanvas.GetComponent<Canvas>().enabled = false;

		inputCreateLevelCol.text = gameBoardSize[0].ToString();
		inputCreateLevelRow.text = gameBoardSize[1].ToString();
		inputGlassNum.text = "0";
		inputMirrorNum.text = "0";
		inputMirrorBlockNum.text = "0";
		inputBlackbodyNum.text = "0";

	}
}
	
public class StartNode
{
	public StartNode(int[] location, int[] rgb, string direction){
		Location = (int[])location.Clone();
		Rgb = (int[])rgb.Clone();
		Direction = direction;
	}

	public int IndexRgb(){
		return Rgb[0] + Rgb[1] * 2 + Rgb[2] * 4 - 1;
	}
		
	public int[] Location;
	public int[] Rgb;
	public string Direction;
}

public class GoalNode
{
	public GoalNode(int[] location, int[] rgb, string direction){
		Location = (int[])location.Clone();
		Rgb = (int[])rgb.Clone();
		Direction = direction;
	}
	public int IndexRgb(){
		return Rgb[0] + Rgb[1] * 2 + Rgb[2] * 4 - 1;
	}
	public int[] Location;
	public int[] Rgb;
	public string Direction;
}
public class LaserDirectionInfo{

	public LaserDirectionInfo(int[] rgb, string location){
		Rgb = (int[]) Rgb.Clone();
		Location = location;
	}

	public bool isLaserPatternExist(string location){
	
		return false;
	}

	public int[] Rgb;
	public string Location;

}