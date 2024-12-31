using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Behaviours.Instances.Settlement
{
    public class Building : MonoBehaviour
    {
        public Collider[] colliders;

        public Material oldMaterial;
        public Material newMaterial;


        public bool isPlaced;
        public int overlappingCount;
        //public GameObject one;
        //public GameObject two;

        //void OnCollisionEnter(Collision col)
        //{
        //    Debug.Log("collide");
        //}

        //void OnTriggerEnter(Collider col)
        //{
        //    Debug.Log("collide2");
        //}

        //void Update()
        //{
        //    //If the first GameObject's Bounds enters the second GameObject's Bounds, output the message
        //    if (two.GetComponent<Collider>().bounds.Intersects(one.GetComponent<Collider>().bounds))
        //    {
        //        Debug.Log("Bounds intersecting");
        //    }
        //}
        //void OnTriggerEnter(Collider other)
        //{
        //    Debug.Log("Collide " + gameObject.name + " " + other.gameObject.name);
        //    if (other.gameObject.tag != "SettlementBuilding")
        //    {
        //        Debug.Log("should ignore");
        //        Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), other.GetComponent<Collider>());
        //    }
        //}

        //void OnCollisionEnter(Collision col)
        //{
        //    Debug.Log("Collision");
        //}

        void OnTriggerEnter(Collider other)
        {
            if (!isPlaced)
            {
                Debug.Log("Collide " + gameObject.name + " " + other.gameObject.name);
              //  oldMaterial = GetComponent<Renderer>().material;
              //  GetComponent<Renderer>().material = newMaterial;
                overlappingCount++;
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (!isPlaced)
            {
                Debug.Log("Exit Collide " + gameObject.name + " " + other.gameObject.name);
                //   GetComponent<Renderer>().material = oldMaterial;
                overlappingCount--;
            }
        }


        //public bool IsOverlapping()
        //{
        //    //  LayerMask layerMask = 1 << LayerMask.NameToLayer("SettlementBuildings"); ;
        //    ////  layerMask = ~layerMask;

        //    //  var overlaps = Physics.OverlapBox(transform.position, transform.localScale / 2, Quaternion.identity, layerMask).Where(c => c.gameObject != gameObject).ToList();

        //    //  if (overlaps.Count() > 0)
        //    //  {
        //    //      if (overlaps.Any(o => o.tag == "SettlementBuilding"))
        //    //      {
        //    //          Debug.Log("Overlap" + overlaps.Count() + overlaps.FirstOrDefault().gameObject.name);
        //    //          return true;
        //    //      }
        //    //  }
        //    //  return false;
        //    return false;
        //}
    }
}
