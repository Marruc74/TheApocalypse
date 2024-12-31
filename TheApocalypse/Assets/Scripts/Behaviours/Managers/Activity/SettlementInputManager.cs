using Assets.Scripts.Behaviours.Instances.Settlement;
using Assets.Scripts.Data.Settlement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Behaviours.Managers.Activity
{
    
    public class SettlementInputManager : InputManager
    {
        public GameObject placableObjectPrefab;
        private GameObject currentPlaceableObject;

        void Update()
        {
            HandleNewObject();

            if (currentPlaceableObject != null)
            {
                MoveCurrentPlaceableObjectToMouse();
                RotatePlaceableObject();
                ReleaseIfClicked();
            }
        }

        public void ClickBuilding(int id)
        {
            Debug.Log(id);

            if (currentPlaceableObject != null)          
            {
                Destroy(currentPlaceableObject);
            }

            var assetManager = GetComponent<AssetManager>();
            //var buildingdata = assetManager.GetBuilding(id);

            //if (buildingdata != null)
            //{
            //    currentPlaceableObject = Instantiate(buildingdata.Prefab);

            //    currentPlaceableObject.GetComponent<Building>().colliders = GameObject.FindGameObjectsWithTag("SettlementBuilding")
            //        .Where(go => go != currentPlaceableObject)
            //        .Select(go => go.GetComponent<Collider>())
            //        .ToArray();
            //}
        }

        private void ReleaseIfClicked()
        {
            if (Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
                if (currentPlaceableObject.GetComponent<Building>().overlappingCount == 0)
                {
                    currentPlaceableObject.GetComponent<Building>().isPlaced = true;
                    currentPlaceableObject.name = "test" + UnityEngine.Random.Range(1, 100);
                    currentPlaceableObject = null;
                }
                else
                {
                    Debug.Log("Can't add it there");
                }
            }
        }        

        private void RotatePlaceableObject()
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                currentPlaceableObject.transform.Rotate(Vector3.up * 50f * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                currentPlaceableObject.transform.Rotate(-(Vector3.up * 50f * Time.deltaTime));
            }
        }

        private void MoveCurrentPlaceableObjectToMouse()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfo;

            LayerMask layerMask = 1 << LayerMask.NameToLayer("SettlementBuildings"); ;
            layerMask = ~layerMask;

            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, layerMask))
            {
                currentPlaceableObject.transform.position = hitInfo.point;              
            }
        }

        private void HandleNewObject()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                if (currentPlaceableObject == null)
                {
                    currentPlaceableObject = Instantiate(placableObjectPrefab);

                    currentPlaceableObject.GetComponent<Building>().colliders = GameObject.FindGameObjectsWithTag("SettlementBuilding")
                        .Where(go => go != currentPlaceableObject)
                        .Select(go => go.GetComponent<Collider>())
                        .ToArray();
                }
                else
                {
                    Destroy(currentPlaceableObject);
                }
            }
        }

        //private BuildingData buildingData;
        //private GameObject lastMouseOver;
        //private GameObject lastMouseOverGameObject;

        //public void Start()
        //{
        //    var assetManager = gameObject.GetComponent<AssetManager>();
        //    buildingData = assetManager.GetBuilding(1);
        //}

        //public void Update()
        //{
        //    ChangePrefab();

        //    var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;
        //    if (Physics.Raycast(ray, out hit))
        //    {
        //        if (lastMouseOver != hit.transform.parent.gameObject)
        //        {
        //            if (lastMouseOverGameObject != null)
        //            {
        //                Destroy(lastMouseOverGameObject);
        //            }

        //            // Debug.Log(string.Format("Create {0}/{1}", hit.transform.parent.name, lastMouseOver));
        //            var script = hit.transform.GetComponent<GridSquare>();
        //            if (CanFit(script.xPosition, script.zPosition))
        //            {
        //                lastMouseOver = hit.transform.gameObject;
        //                var posX = ((buildingData.sizeX - 1) * 5);
        //                //var posZ = ((buildingData.sizeZ - 1) * 10);
        //                var position = hit.transform.transform.position + new Vector3(posX, 0, 0);
        //                lastMouseOverGameObject = Instantiate(buildingData.Prefab, position, Quaternion.identity) as GameObject;
        //            }
        //        }
        //        //else
        //        //{
        //        ////    Debug.Log(string.Format("Destroy {0}/{1}", hit.transform.parent.name, lastMouseOver));
        //        //    Destroy(lastMouseOverGameObject);
        //        //}
        //    }
        //    else if (lastMouseOverGameObject != null || lastMouseOver != null)
        //    {
        //        lastMouseOver = null;

        //        if (lastMouseOverGameObject != null)
        //        {
        //            Destroy(lastMouseOverGameObject);
        //        }
        //    }

        //    Click();
        //}    

        //private void ChangePrefab()
        //{
        //    var assetManager = gameObject.GetComponent<AssetManager>();
        //    if (Input.GetKeyUp(KeyCode.Alpha1))
        //    {
        //        buildingData = assetManager.GetBuilding(1);
        //    }
        //    else if (Input.GetKeyUp(KeyCode.Alpha2))
        //    {
        //        buildingData = assetManager.GetBuilding(2);
        //    }
        //    else if (Input.GetKeyUp(KeyCode.Alpha3))
        //    {
        //        buildingData = assetManager.GetBuilding(3);
        //    }
        //}
        
        //private void Click()
        //{            

        //    if (Input.GetMouseButtonDown(0))
        //    {

        //        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //        RaycastHit hit;

        //        if (Physics.Raycast(ray, out hit))
        //        {
        //            var script = hit.transform.GetComponent<GridSquare>();

        //            if (CanFit(script.xPosition, script.zPosition))
        //            {
        //                var posX = ((buildingData.sizeX - 1) * 5);
        //                //var posZ = ((buildingData.sizeZ - 1) * 10);
        //                var position = hit.transform.transform.position + new Vector3(posX, 0, 0);
        //                Instantiate(buildingData.Prefab, position, Quaternion.identity);
        //                Debug.Log(hit.transform.name);
        //                Debug.Log(string.Format("{0}-{1}", posX, 0));
                       

        //                for (var x = 0; x < buildingData.sizeX; x++)
        //                {
        //                    for (var z = 0; z < buildingData.sizeZ; z++)
        //                    {
        //                        GameManagerInstance.Instance.GameInfo.PlayerFaction.Settlements[0].Grid[script.xPosition + x, script.zPosition + z].Occupied = true;
        //                    }
        //                }

        //            }
        //            else
        //            {
        //                Debug.Log("Occupied");
        //            }
        //        }
        //        else
        //        {
        //            Debug.Log("no hit");
        //        }
        //    }
        //}

        //private bool CanFit(int posX, int posZ)
        //{
        //    var sizeX = buildingData.sizeX;
        //    var sizeZ = buildingData.sizeZ;

        //    var grid = GameManagerInstance.Instance.GameInfo.PlayerFaction.Settlements[0].Grid;

        //    for (var x = 0; x<sizeX; x++)
        //    {
        //        for (var z = 0; z < sizeZ; z++)
        //        {
        //            Debug.Log("X: " + posX + " " + x + " " + grid.GetLength(0));
        //            if (posX + x >= grid.GetLength(0))
        //            {
        //                return false;
        //            }

        //            if (posZ + z >= grid.GetLength(1))
        //            {
        //                return false;
        //            }

        //            if (grid[posX + x, posZ + z].Occupied)
        //            {
        //                return false;
        //            }
        //        }
        //    }
        //    return true;
        //}
    }
}
