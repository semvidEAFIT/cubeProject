using UnityEngine;
using System.Collections.Generic;
/**
 * Esta clase funciona tanto como laser emisor o como cubo laser, su funcionalidad se maneja usando el atributo isAlive
 * */
public class LaserCube : Cube, ILaserReactive{

    private LineRenderer renderer;
    public Material laserMat;
    private Ray ray;
    private ILaserReactive reactabletHit;
    private Vector3 hitPosition;
    public bool isAlive = true;
    private int lasersHitting;

    public override bool IsSelected
    {
        get
        {
            return base.IsSelected && isAlive;
        }
        set
        {
            base.IsSelected = value;
        }
    }

    protected override void Start()
    {
        base.Start();
        lasersHitting = 0;
        renderer = new GameObject("LineRenderer").AddComponent<LineRenderer>();
        renderer.material = laserMat;
        renderer.SetVertexCount(2);
        renderer.SetPosition(0, transform.position);
    }

    void Update()
    {
        ray = new Ray(transform.position, transform.forward);
        if (!isAlive || lasersHitting > 0)
        {
            float distance = Mathf.Abs(Level.Dimension - transform.position.x);
            distance = Mathf.Abs(Vector3.Dot(transform.position, transform.forward) - Level.Dimension);
            if (distance > 10)
            {
                distance -= (Level.Dimension);
            }
            
            RaycastHit[] hits = Physics.RaycastAll(ray,distance);

            RaycastHit hit = new RaycastHit();
            bool isRealHit = false;
            if(hits.Length > 0 && hits[0].transform.tag != "Selector"){
                hit = hits[0];
                isRealHit = true;
            }else{
                if (hits.Length > 1 && hits[1].transform.tag != "Selector")
                {
                    hit = hits[1];
                    isRealHit = true;
                }
            }
            /*Debug.Log(isAlive);
            foreach(RaycastHit h in hits){
                Debug.Log(CubeHelper.GetEntityInPosition(h.transform.position));
            }*/

            if (isRealHit )
            {
                hitPosition = hit.transform.position;
                renderer.SetPosition(1, hitPosition);
                if (!CubeHelper.IsFree(hitPosition) && CubeHelper.GetEntityInPosition(hitPosition) is ILaserReactive)
                {
                    ILaserReactive newHit = (ILaserReactive)CubeHelper.GetEntityInPosition(hitPosition);
                    if (!newHit.Equals(reactabletHit))
                    {
                        newHit.HitByLaser(this.transform.position);
                        reactabletHit = newHit;
                    }
                }
                else
                {
                    if (reactabletHit != null)
                    {
                        reactabletHit.StopHit(this.transform.position);
                        reactabletHit = null;
                    }
                }
            }
            else
            {
                if (reactabletHit !=null)
                {
                    reactabletHit.StopHit(this.transform.position);
                    reactabletHit = null;
                }
                renderer.SetPosition(1, transform.position+transform.forward * distance);
            }
        }
        else 
        {
            renderer.SetPosition(1, this.transform.position);
        }
    }

    public void HitByLaser(Vector3 laserPosition)
    {
        lasersHitting++;
    }

    public override void MoveTo(Vector3 nextPosition)
    {
        base.MoveTo(nextPosition);
        renderer.SetPosition(0, nextPosition);
    }

    public void StopHit(Vector3 laserPosition)
    {
        lasersHitting--;
    }
}
