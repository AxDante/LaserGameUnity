using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridInfo {


	public static string[] laserPattern = new string[] { "CL", "CT", "CB", "CR", "LT", "LB", "RT", "RB"};
	//public static string[] laserDirection

	public static Dictionary<string, int[]> NextGrid = new Dictionary<string, int[]>(){
		{"CT", new int[] {-1, 0}},
		{"RT", new int[] {-1, 0}},
		{"TR", new int[] {0, 1}},
		{"CR", new int[] {0, 1}},
		{"BR", new int[] {0, 1}},
		{"RB", new int[] {1, 0}},
		{"CB", new int[] {1, 0}},
		{"LB", new int[] {1, 0}},
		{"BL", new int[] {0, -1}},
		{"CL", new int[] {0, -1}},
		{"TL", new int[] {0, -1}},
		{"LT", new int[] {-1, 0}}
	};

	public static Dictionary<string, int[]> PreviousGrid = new Dictionary<string, int[]>(){
		{"T", new int[] {-1, 0}},
		{"R", new int[] {0, 1}},
		{"B", new int[] {1, 0}},
		{"L", new int[] {0, -1}},
	};

	/*
	public static Dictionary<string, Dictionary<string, Dictionary<string, string[]>>> Patterns = new Dictionary<string, Dictionary<string, Dictionary<string, string[]>>>(){
		{"CT", new Dictionary<string, Dictionary<string, string[]>>(){
				{"Empty", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"CB", "CT"}}, 
						{"NextPattern", new string[]{"CT"}}}
				},
				{"Invalid", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"CB", "CT"}}, 
						{"NextPattern", new string[]{"CT"}}}
				},
				{"Glass", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"CB", "CT"}}, 
						{"NextPattern", new string[]{"CT"}}}
				},
				{"MirrorBlock", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{"CB"}}}
				},
				{"Mirror_BR", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{"CB"}}}
				},
				{"Mirror_BL", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{"CB"}}}
				},
				{"Mirror_TR", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"CB", "CL"}}, 
						{"NextPattern", new string[]{"CL"}}}
				},
				{"Mirror_TL", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"CB", "CR"}}, 
						{"NextPattern", new string[]{"CR"}}}
				},
				{"Blackbody", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{}}}
				},
			}
		},
		{"RT", new Dictionary<string, Dictionary<string, string[]>>(){
				{"Empty", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"BL"}}, 
						{"NextPattern", new string[]{"BL"}}}
				},
				{"Invalid", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"BL"}}, 
						{"NextPattern", new string[]{"BL"}}}
				},
				{"Glass", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"CB", "CT"}}, 
						{"NextPattern", new string[]{"RT"}}}
				},
				{"MirrorBlock", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{"RB"}}}
				},
				{"Mirror_BR", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{"RB"}}}
				},
				{"Mirror_BL", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{"RB"}}}
				},
				{"Mirror_TR", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"BL"}}, 
						{"NextPattern", new string[]{"BL"}}}
				},
				{"Mirror_TL", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"BL"}}, 
						{"NextPattern", new string[]{"LB"}}}
				},
				{"Blackbody", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{}}}
				},
			}
		},
		{"TR", new Dictionary<string, Dictionary<string, string[]>>(){
				{"Empty", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"LB"}}, 
						{"NextPattern", new string[]{"LB"}}}
				},
				{"Invalid", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"LB"}}, 
						{"NextPattern", new string[]{"LB"}}}
				},
				{"Glass", new Dictionary<string, string[]>(){ 
						{"Laser", new string[]{"CL", "CR"}}, 
						{"NextPattern", new string[]{"TR"}}}
				},
				{"MirrorBlock", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{"TL"}}}
				},
				{"Mirror_BR", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"LB"}}, 
						{"NextPattern", new string[]{"BL"}}}
				},
				{"Mirror_BL", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{"TL"}}}
				},
				{"Mirror_TR", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"LB"}}, 
						{"NextPattern", new string[]{"LB"}}}
				},
				{"Mirror_TL", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{"TL"}}}
				},
				{"Blackbody", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{}}}
				},
			}
		},
		{"CR", new Dictionary<string, Dictionary<string, string[]>>(){
				{"Empty", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"CL", "CR"}},
						{"NextPattern", new string[]{"CR"}}}
				},
				{"Invalid", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"CL", "CR"}},
						{"NextPattern", new string[]{"CR"}}}
				},
				{"Glass", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"CL", "CR"}},
						{"NextPattern", new string[]{"CR"}}}
				},
				{"MirrorBlock", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{"CL"}}}
				},
				{"Mirror_BR", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"CL", "CT"}},
						{"NextPattern", new string[]{"CT"}}}
				},				
				{"Mirror_BL", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{"CL"}}}
				},
				{"Mirror_TR", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"CL", "CB"}}, 
						{"NextPattern", new string[]{"CB"}}}
				},
				{"Mirror_TL", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{"CL"}}}
				},
				{"Blackbody", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{}}}
				},
			}
		},
		{"BR", new Dictionary<string, Dictionary<string, string[]>>(){
				{"Empty", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"LT"}},
						{"NextPattern", new string[]{"LT"}}}
				},
				{"Invalid", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"LT"}},
						{"NextPattern", new string[]{"LT"}}}
				},
				{"Glass", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"CL", "CR"}},
						{"NextPattern", new string[]{"BR"}}}
				},
				{"MirrorBlock", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{"BL"}}}
				},
				{"Mirror_BR", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"LT"}},
						{"NextPattern", new string[]{"LT"}}}
				},
				{"Mirror_BL", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{"BL"}}}
				},
				{"Mirror_TR", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"LT"}}, 
						{"NextPattern", new string[]{"TL"}}}
				},
				{"Mirror_TL", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{"BL"}}}
				},
				{"Blackbody", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{}}}
				},
			}
		},
		{"RB", new Dictionary<string, Dictionary<string, string[]>>(){
				{"Empty", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"TL"}},
						{"NextPattern", new string[]{"TL"}}}
				},
				{"Invalid", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"TL"}},
						{"NextPattern", new string[]{"TL"}}}
				},
				{"Glass", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"CB", "CT"}},
						{"NextPattern", new string[]{"RB"}}}
				},
				{"MirrorBlock", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{"RT"}}}
				},
				{"Mirror_BR", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"TL"}}, 
						{"NextPattern", new string[]{"TL"}}}
				},
				{"Mirror_BL", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"TL"}}, 
						{"NextPattern", new string[]{"LT"}}}
				},
				{"Mirror_TR", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{"RT"}}}
				},
				{"Mirror_TL", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{"RT"}}}
				},
				{"Blackbody", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{}}}
				},
			}
		},
		{"CB", new Dictionary<string, Dictionary<string, string[]>>(){
				{"Empty", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"CT", "CB"}},
						{"NextPattern", new string[]{"CB"}}}
				},
				{"Invalid", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"CT", "CB"}},
						{"NextPattern", new string[]{"CB"}}}
				},
				{"Glass", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"CT", "CB"}},
						{"NextPattern", new string[]{"CB"}}}
				},
				{"MirrorBlock", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{"CT"}}}
				},
				{"Mirror_BR", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"CT", "CL"}}, 
						{"NextPattern", new string[]{"CL"}}}
				},
				{"Mirror_BL", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"CT", "CR"}}, 
						{"NextPattern", new string[]{"CR"}}}
				},
				{"Mirror_TR", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{"CT"}}}
				},
				{"Mirror_TL", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{"CT"}}}
				},
				{"Blackbody", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{}}}
				},
			}
		},
		{"LB", new Dictionary<string, Dictionary<string, string[]>>(){
				{"Empty", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"TR"}},
						{"NextPattern", new string[]{"TR"}}}
				},
				{"Invalid", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"TR"}},
						{"NextPattern", new string[]{"TR"}}}
				},
				{"Glass", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"CT", "CB"}},
						{"NextPattern", new string[]{"LB"}}}
				},
				{"MirrorBlock", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{"LT"}}}
				},
				{"Mirror_BR", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"RT"}}, 
						{"NextPattern", new string[]{"RT"}}}
				},
				{"Mirror_BL", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"TR"}},
						{"NextPattern", new string[]{"TR"}}}
				},
				{"Mirror_TR", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{"LT"}}}
				},
				{"Mirror_TL", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{"LT"}}}
				},
				{"Blackbody", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{}}}
				},
			}
		},
		{"BL", new Dictionary<string, Dictionary<string, string[]>>(){
				{"Empty", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"RT"}},
						{"NextPattern", new string[]{"RT"}}}
				},
				{"Invalid", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"RT"}},
						{"NextPattern", new string[]{"RT"}}}
				},
				{"Glass", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"CL", "CR"}},
						{"NextPattern", new string[]{"BL"}}}
				},
				{"MirrorBlock", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{"BR"}}}
				},
				{"Mirror_BR", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{"BR"}}}
				},
				{"Mirror_BL", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"RT"}},
						{"NextPattern", new string[]{"RT"}}}
				},
				{"Mirror_TR", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{"BR"}}}
				},
				{"Mirror_TL", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"RT"}}, 
						{"NextPattern", new string[]{"TR"}}}
				},
				{"Blackbody", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{}}}
				},
			}
		},
		{"CL", new Dictionary<string, Dictionary<string, string[]>>(){
				{"Empty", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"CR", "CL"}},
						{"NextPattern", new string[]{"CL"}}}
				},
				{"Invalid", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"CR", "CL"}},
						{"NextPattern", new string[]{"CL"}}}
				},
				{"Glass", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"CR", "CL"}},
						{"NextPattern", new string[]{"CL"}}}
				},
				{"MirrorBlock", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{"CR"}}}
				},
				{"Mirror_BR", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{"CR"}}}
				},
				{"Mirror_BL", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"CR", "CT"}},
						{"NextPattern", new string[]{"CT"}}}
				},
				{"Mirror_TR", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{"CR"}}}
				},
				{"Mirror_TL", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"CR", "CB"}}, 
						{"NextPattern", new string[]{"CB"}}}
				},
				{"Blackbody", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{}}}
				},
			}
		},
		{"TL", new Dictionary<string, Dictionary<string, string[]>>(){
				{"Empty", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"RB"}},
						{"NextPattern", new string[]{"RB"}}}
				},
				{"Invalid", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"RB"}},
						{"NextPattern", new string[]{"RB"}}}
				},
				{"Glass", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"CR", "CL"}},
						{"NextPattern", new string[]{"TL"}}}
				},
				{"MirrorBlock", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{"TR"}}}
				},
				{"Mirror_BR", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{"TR"}}}
				},
				{"Mirror_BL", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"RB"}}, 
						{"NextPattern", new string[]{"BR"}}}
				},
				{"Mirror_TR", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{"TR"}}}
				},
				{"Mirror_TL", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"RB"}},
						{"NextPattern", new string[]{"RB"}}}
				},
				{"Blackbody", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{}}}
				},
			}
		},
		{"LT", new Dictionary<string, Dictionary<string, string[]>>(){
				{"Empty", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"BR"}},
						{"NextPattern", new string[]{"BR"}}}
				},
				{"Invalid", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"BR"}},
						{"NextPattern", new string[]{"BR"}}}
				},
				{"Glass", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"CT", "CB"}},
						{"NextPattern", new string[]{"LT"}}}
				},
				{"MirrorBlock", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{"LB"}}}
				},
				{"Mirror_BR", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{"LB"}}}
				},
				{"Mirror_BL", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{"LB"}}}
				},
				{"Mirror_TR", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"BR"}}, 
						{"NextPattern", new string[]{"RB"}}}
				},
				{"Mirror_TL", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"BR"}},
						{"NextPattern", new string[]{"BR"}}}
				},
				{"Blackbody", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{}}}
				},
			}
		}
	};

	*/


	public static Dictionary<string, Dictionary<string, Dictionary<string, string[]>>> Patterns = new Dictionary<string, Dictionary<string, Dictionary<string, string[]>>>(){
		{"CT", new Dictionary<string, Dictionary<string, string[]>>(){
				{"Empty", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"BC", "CT"}}, 
						{"NextPattern", new string[]{"CT"}}}
				},
				{"Invalid", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"BC", "CT"}}, 
						{"NextPattern", new string[]{"CT"}}}
				},
				{"Glass", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"BC", "CT"}}, 
						{"NextPattern", new string[]{"CT"}}}
				},
				{"MirrorBlock", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{"CB"}}}
				},
				{"Mirror_BR", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{"CB"}}}
				},
				{"Mirror_BL", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{"CB"}}}
				},
				{"Mirror_TR", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"BC", "CL"}}, 
						{"NextPattern", new string[]{"CL"}}}
				},
				{"Mirror_TL", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"BC", "CR"}}, 
						{"NextPattern", new string[]{"CR"}}}
				},
				{"Blackbody", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{}}}
				},
			}
		},
		{"RT", new Dictionary<string, Dictionary<string, string[]>>(){
				{"Empty", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"BL"}}, 
						{"NextPattern", new string[]{"BL"}}}
				},
				{"Invalid", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"BL"}}, 
						{"NextPattern", new string[]{"BL"}}}
				},
				{"Glass", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"BC", "CT"}}, 
						{"NextPattern", new string[]{"RT"}}}
				},
				{"MirrorBlock", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{"RB"}}}
				},
				{"Mirror_BR", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{"RB"}}}
				},
				{"Mirror_BL", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{"RB"}}}
				},
				{"Mirror_TR", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"BL"}}, 
						{"NextPattern", new string[]{"BL"}}}
				},
				{"Mirror_TL", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"BL"}}, 
						{"NextPattern", new string[]{"LB"}}}
				},
				{"Blackbody", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{}}}
				},
			}
		},
		////////////HERE
		{"TR", new Dictionary<string, Dictionary<string, string[]>>(){
				{"Empty", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"LB"}}, 
						{"NextPattern", new string[]{"LB"}}}
				},
				{"Invalid", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"LB"}}, 
						{"NextPattern", new string[]{"LB"}}}
				},
				{"Glass", new Dictionary<string, string[]>(){ 
						{"Laser", new string[]{"LC", "CR"}}, 
						{"NextPattern", new string[]{"TR"}}}
				},
				{"MirrorBlock", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{"TL"}}}
				},
				{"Mirror_BR", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"LB"}}, 
						{"NextPattern", new string[]{"BL"}}}
				},
				{"Mirror_BL", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{"TL"}}}
				},
				{"Mirror_TR", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"LB"}}, 
						{"NextPattern", new string[]{"LB"}}}
				},
				{"Mirror_TL", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{"TL"}}}
				},
				{"Blackbody", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{}}}
				},
			}
		},
		{"CR", new Dictionary<string, Dictionary<string, string[]>>(){
				{"Empty", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"LC", "CR"}},
						{"NextPattern", new string[]{"CR"}}}
				},
				{"Invalid", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"LC", "CR"}},
						{"NextPattern", new string[]{"CR"}}}
				},
				{"Glass", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"LC", "CR"}},
						{"NextPattern", new string[]{"CR"}}}
				},
				{"MirrorBlock", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{"CL"}}}
				},
				{"Mirror_BR", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"LC", "CT"}},
						{"NextPattern", new string[]{"CT"}}}
				},				
				{"Mirror_BL", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{"CL"}}}
				},
				{"Mirror_TR", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"LC", "CB"}}, 
						{"NextPattern", new string[]{"CB"}}}
				},
				{"Mirror_TL", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{"CL"}}}
				},
				{"Blackbody", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{}}}
				},
			}
		},
		{"BR", new Dictionary<string, Dictionary<string, string[]>>(){
				{"Empty", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"LT"}},
						{"NextPattern", new string[]{"LT"}}}
				},
				{"Invalid", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"LT"}},
						{"NextPattern", new string[]{"LT"}}}
				},
				{"Glass", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"LC", "CR"}},
						{"NextPattern", new string[]{"BR"}}}
				},
				{"MirrorBlock", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{"BL"}}}
				},
				{"Mirror_BR", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"LT"}},
						{"NextPattern", new string[]{"LT"}}}
				},
				{"Mirror_BL", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{"BL"}}}
				},
				{"Mirror_TR", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"LT"}}, 
						{"NextPattern", new string[]{"TL"}}}
				},
				{"Mirror_TL", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{"BL"}}}
				},
				{"Blackbody", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{}}}
				},
			}
		},
		{"RB", new Dictionary<string, Dictionary<string, string[]>>(){
				{"Empty", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"TL"}},
						{"NextPattern", new string[]{"TL"}}}
				},
				{"Invalid", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"TL"}},
						{"NextPattern", new string[]{"TL"}}}
				},
				{"Glass", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"BC", "CT"}},
						{"NextPattern", new string[]{"RB"}}}
				},
				{"MirrorBlock", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{"RT"}}}
				},
				{"Mirror_BR", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"TL"}}, 
						{"NextPattern", new string[]{"TL"}}}
				},
				{"Mirror_BL", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"TL"}}, 
						{"NextPattern", new string[]{"LT"}}}
				},
				{"Mirror_TR", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{"RT"}}}
				},
				{"Mirror_TL", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{"RT"}}}
				},
				{"Blackbody", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{}}}
				},
			}
		},
		{"CB", new Dictionary<string, Dictionary<string, string[]>>(){
				{"Empty", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"TC", "CB"}},
						{"NextPattern", new string[]{"CB"}}}
				},
				{"Invalid", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"TC", "CB"}},
						{"NextPattern", new string[]{"CB"}}}
				},
				{"Glass", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"TC", "CB"}},
						{"NextPattern", new string[]{"CB"}}}
				},
				{"MirrorBlock", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{"CT"}}}
				},
				{"Mirror_BR", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"TC", "CL"}}, 
						{"NextPattern", new string[]{"CL"}}}
				},
				{"Mirror_BL", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"TC", "CR"}}, 
						{"NextPattern", new string[]{"CR"}}}
				},
				{"Mirror_TR", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{"CT"}}}
				},
				{"Mirror_TL", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{"CT"}}}
				},
				{"Blackbody", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{}}}
				},
			}
		},
		{"LB", new Dictionary<string, Dictionary<string, string[]>>(){
				{"Empty", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"TR"}},
						{"NextPattern", new string[]{"TR"}}}
				},
				{"Invalid", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"TR"}},
						{"NextPattern", new string[]{"TR"}}}
				},
				{"Glass", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"TC", "CB"}},
						{"NextPattern", new string[]{"LB"}}}
				},
				{"MirrorBlock", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{"LT"}}}
				},
				{"Mirror_BR", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"RT"}}, 
						{"NextPattern", new string[]{"RT"}}}
				},
				{"Mirror_BL", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"TR"}},
						{"NextPattern", new string[]{"TR"}}}
				},
				{"Mirror_TR", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{"LT"}}}
				},
				{"Mirror_TL", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{"LT"}}}
				},
				{"Blackbody", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{}}}
				},
			}
		},
		{"BL", new Dictionary<string, Dictionary<string, string[]>>(){
				{"Empty", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"RT"}},
						{"NextPattern", new string[]{"RT"}}}
				},
				{"Invalid", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"RT"}},
						{"NextPattern", new string[]{"RT"}}}
				},
				{"Glass", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"LC", "CR"}},
						{"NextPattern", new string[]{"BL"}}}
				},
				{"MirrorBlock", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{"BR"}}}
				},
				{"Mirror_BR", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{"BR"}}}
				},
				{"Mirror_BL", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"RT"}},
						{"NextPattern", new string[]{"RT"}}}
				},
				{"Mirror_TR", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{"BR"}}}
				},
				{"Mirror_TL", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"RT"}}, 
						{"NextPattern", new string[]{"TR"}}}
				},
				{"Blackbody", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{}}}
				},
			}
		},
		{"CL", new Dictionary<string, Dictionary<string, string[]>>(){
				{"Empty", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"RC", "CL"}},
						{"NextPattern", new string[]{"CL"}}}
				},
				{"Invalid", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"RC", "CL"}},
						{"NextPattern", new string[]{"CL"}}}
				},
				{"Glass", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"RC", "CL"}},
						{"NextPattern", new string[]{"CL"}}}
				},
				{"MirrorBlock", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{"CR"}}}
				},
				{"Mirror_BR", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{"CR"}}}
				},
				{"Mirror_BL", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"RC", "CT"}},
						{"NextPattern", new string[]{"CT"}}}
				},
				{"Mirror_TR", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{"CR"}}}
				},
				{"Mirror_TL", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"RC", "CB"}}, 
						{"NextPattern", new string[]{"CB"}}}
				},
				{"Blackbody", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{}}}
				},
			}
		},
		{"TL", new Dictionary<string, Dictionary<string, string[]>>(){
				{"Empty", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"RB"}},
						{"NextPattern", new string[]{"RB"}}}
				},
				{"Invalid", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"RB"}},
						{"NextPattern", new string[]{"RB"}}}
				},
				{"Glass", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"RC", "CL"}},
						{"NextPattern", new string[]{"TL"}}}
				},
				{"MirrorBlock", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{"TR"}}}
				},
				{"Mirror_BR", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{"TR"}}}
				},
				{"Mirror_BL", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"RB"}}, 
						{"NextPattern", new string[]{"BR"}}}
				},
				{"Mirror_TR", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{"TR"}}}
				},
				{"Mirror_TL", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"RB"}},
						{"NextPattern", new string[]{"RB"}}}
				},
				{"Blackbody", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{}}}
				},
			}
		},
		{"LT", new Dictionary<string, Dictionary<string, string[]>>(){
				{"Empty", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"BR"}},
						{"NextPattern", new string[]{"BR"}}}
				},
				{"Invalid", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"BR"}},
						{"NextPattern", new string[]{"BR"}}}
				},
				{"Glass", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"TC", "CB"}},
						{"NextPattern", new string[]{"LT"}}}
				},
				{"MirrorBlock", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{"LB"}}}
				},
				{"Mirror_BR", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{"LB"}}}
				},
				{"Mirror_BL", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{"LB"}}}
				},
				{"Mirror_TR", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"BR"}}, 
						{"NextPattern", new string[]{"RB"}}}
				},
				{"Mirror_TL", new Dictionary<string, string[]>(){
						{"Laser", new string[]{"BR"}},
						{"NextPattern", new string[]{"BR"}}}
				},
				{"Blackbody", new Dictionary<string, string[]>(){
						{"Laser", new string[]{}}, 
						{"NextPattern", new string[]{}}}
				},
			}
		}
	};
}
