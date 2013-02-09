using UnityEngine;
using System.Collections.Generic;

public class ImitatorCube : Cube {
	private GameObject imitated;
    public List<Object> cubos;
    private bool isClone;

    protected override void Start()
    {
        base.Start();
        isClone = false;
    }

    public override bool IsSelected
    {
        get
        {
            return base.IsSelected && !isClone;
        }
        set
        {
            base.IsSelected = value;
        }
    }
    void Update() {
        Vector3EqualityComparer comparer = new Vector3EqualityComparer();
        if (isClone && comparer.Equals(transform.position, imitated.transform.position)) {
            if (CubeHelper.GetEntityInPosition(imitated.transform.position + Vector3.down) is Cube)
            {
                Clone(CubeHelper.GetEntityInPosition(imitated.transform.position + Vector3.down));
            }
            else 
            {
                UnClone();
            }        
        }    
    }

 

    public override void MoveTo(Vector3 nextPosition)
    {
        if (CubeHelper.GetEntityInPosition(nextPosition + Vector3.down) is Cube)
        {
            Debug.Log("Clone!");
            collider.enabled = false;
            renderer.enabled = false;
            Debug.Log(renderer.enabled);
            transform.position = nextPosition;
            Clone(CubeHelper.GetEntityInPosition(nextPosition + Vector3.down));
            Command.EndExecution();
        }
        else 
        {
            base.MoveTo(nextPosition);
        }
    }

    private void UnClone()
    {
        isClone = false;
        this.transform.position = imitated.transform.position;
        Destroy(imitated);
        collider.enabled = true;
        renderer.enabled = true;
    }

    private void Clone(Entity e) {
        if(isClone){
            this.transform.position = imitated.transform.position;
            Level.Singleton.Entities.Remove(this.transform.position);
            Destroy(imitated);
        }

        Level.Singleton.Entities.Remove(this.transform.position);
        
        isClone = true;
        switch(e.name){
            case "Rock_Cube":
                imitated = (GameObject)Instantiate(cubos[1]);
                break;
            case "Cloud_Cube":
                    imitated = (GameObject)Instantiate(cubos[2]);
                    break;
            case "Ice_Cube":
                    imitated = (GameObject)Instantiate(cubos[3]);
                    break;
            case "Metal_Cube":
                    imitated = (GameObject)Instantiate(cubos[4]);
                    break;
            case "Rubber_Cube":
                    imitated = (GameObject)Instantiate(cubos[5]);
                    break;
            case "Hook_Cube":
                    imitated = (GameObject)Instantiate(cubos[6]);
                    break;
            case "Digital_Cube":
                    imitated = (GameObject)Instantiate(cubos[7]);
                    break;
            case "Twin2":
            case "Twin1":
            case "Twins" :
                    //Caso Extraño
                    GameObject twins = (GameObject)Instantiate(cubos[8]);
                    imitated = twins.transform.GetChild(0).gameObject;
                    break;
            case "Laser_Cube":
                    imitated = (GameObject)Instantiate(cubos[9]);
                    break;
            case "Duplicator_Cube":
                    imitated = (GameObject)Instantiate(cubos[10]);
                    break;
            case "Levitator_Cube":
                    imitated = (GameObject)Instantiate(cubos[11]);
                    break;
            default:
                    //Basic_Cube
                    imitated = (GameObject)Instantiate(cubos[0]);
                    break;
        }
        imitated.transform.position = this.transform.position;
        isClone = true;
    }
}
