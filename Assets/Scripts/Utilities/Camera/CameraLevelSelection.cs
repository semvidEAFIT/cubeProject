using UnityEngine;
using System.Collections.Generic;

public class CameraLevelSelection : CameraDrive {
	
	public Material [] materials;
	public List<GameObject>cubes;
	
	public List<string> Levels; //= {"C01", "C02", "C03", "I01", "I02", "I03", "M01", "M02", "M03", "N01", "N02", "N03" };
	
	
	protected override void Start ()
	{
		base.Start ();

		for (int i = 0; i < cubes.Count;i++){
			if(i == 0){
				cubes[i].renderer.material=materials[0];
			}else if(i <= 2){
				cubes[i].renderer.material=materials[1];
			}else if (i == 3){
				cubes[i].renderer.material=materials[3];
			}else if(i <= 5){
				cubes[i].renderer.material=materials[2];
			}else if(i == 6){
				cubes[i].renderer.material=materials[5];
			}else if(i <= 8){
				cubes[i].renderer.material=materials[4];
			}else if(i==9){
				cubes[i].renderer.material=materials[7];
			}else if(i<=11){
				cubes[i].renderer.material=materials[6];
			}else{
				cubes[i].renderer.material=materials[24];
			}
		}
      
        setLevels(GameController.Singleton.Done);
		cubep.Set(cubep.x,cubep.y-5,cubep.z);
		error=0;
	}
	
	protected override void Update ()
	{
		base.Update ();
		
		if(Input.GetMouseButtonUp(0)){
			Ray ray=Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit = new RaycastHit();
			if(Physics.Raycast(ray,out hit,100)){
				string name = hit.collider.gameObject.name;
				if(hit.collider.gameObject.renderer.material.color != materials[24].color){
					for(int i = 0; i < cubes.Count ; i++){
						if(hit.collider.gameObject==cubes[i]){
							Debug.Log(i);
							Debug.Log(Levels[i]);
						}
					}
				}
			}
		}
	}
	
	public void setLevels(List<string> levelsWon){
		for(int i = 0; i < levelsWon.Count;i++){
			if(i < 12){
				if(i < 3){
					for(int j = 0; j < Levels.Count; j++){
						if(levelsWon[i]==Levels[j]){
							cubes[j].renderer.material = materials[0];
						}
					}
				}else if(i < 6){
					for(int j = 0; j < Levels.Count; j++){
						if(levelsWon[i]==Levels[j]){
							cubes[j].renderer.material = materials[3];
						}
					}					
				}else if(i < 9){
					for(int j = 0; j < Levels.Count; j++){
						if(levelsWon[i]==Levels[j]){
							cubes[j].renderer.material = materials[5];
						}
					}					
				}else if(i < 12){
					for(int j = 0; j < Levels.Count; j++){
						if(levelsWon[i]==Levels[j]){
							cubes[j].renderer.material = materials[7];
						}
					}
				}
				if(cubes[7].renderer.material.color == materials[5].color && cubes[11].renderer.material.color == materials[7].color){
					cubes[27].renderer.material = materials[13];
				}
				
				if(cubes[10].renderer.material.color == materials[5].color && cubes[2].renderer.material.color == materials[0].color){
					cubes[19].renderer.material = materials[15];
				}
				
				
				if(cubes[1].renderer.material.color == materials[0].color && cubes[5].renderer.material.color == materials[3].color){
					cubes[13].renderer.material = materials[9];
				}
				
				if(cubes[4].renderer.material.color == materials[2].color && cubes[8].renderer.material.color == materials[4].color){
					cubes[33].renderer.material = materials[11];
				}
			}else{
				if(i < 19){
					for(int j = 0; j < Levels.Count; j++){
						if(levelsWon[i]==Levels[j]){
							cubes[j].renderer.material = materials[8];
						}
					}	
				}else if(i < 26){
					for(int j = 0; j < Levels.Count; j++){
						if(levelsWon[i]==Levels[j]){
							cubes[j].renderer.material = materials[14];
						}
					}
				}else if(i < 33){
					for(int j = 0; j < Levels.Count; j++){
						if(levelsWon[i]==Levels[j]){
							cubes[j].renderer.material = materials[12];
						}
					}
				}else if(i < 33){
					for(int j = 0; j < Levels.Count; j++){
						if(levelsWon[i]==Levels[j]){
							cubes[j].renderer.material = materials[10];
						}
					}
				}
			}
		}
		for(int i = 12; i < cubes.Count; i++){
			if(cubes[i].renderer.material.color ==  materials[9].color || cubes[i].renderer.material.color ==  materials[11].color 
				|| cubes[i].renderer.material.color ==  materials[13].color || cubes[i].renderer.material.color ==  materials[15].color){
				
				if(cubes[i+1].renderer.material.color == materials[24].color){
					if(cubes[i] != cubes[18] || cubes[i] != cubes[19] || cubes[i] != cubes[25] || cubes[i] != cubes[26] 
						|| cubes[i] != cubes[32] || cubes[i] != cubes[33] || cubes[i] != cubes[39] || cubes[i] != cubes[40] 
						|| cubes[i] != cubes[42] || cubes[i] != cubes[44] || cubes[i] != cubes[46] || cubes[i] != cubes[48]){
						for(int j = 0; j < 25;j++){
							if(cubes[i].renderer.material.color==materials[j].color){
								cubes[i+1].renderer.material=materials[j];
								if(cubes[i] == cubes[17] || cubes[i] == cubes[24] || cubes[i] == cubes[31] || cubes[i] == cubes[38]){
									cubes[i+2].renderer.material=materials[j];
								}
							}
						}
					}
				}
			}
		}
	}
	

	
}
