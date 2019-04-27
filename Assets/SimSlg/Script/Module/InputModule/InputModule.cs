// ********************************************************
// 描述：输入模块
// 作者：ShadowRabbit 
// 创建时间：2019-04-23 15:35:56
// ********************************************************


using UnityEngine;

public class InputModule : BaseModuleSingleton<InputModule>
{
    Ray ray;
    RaycastHit hit;
    /// <summary>
    /// 在gameEntry中调用
    /// </summary>
    public void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
             ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                BaseData baseData = new BaseData(hit.point, Quaternion.identity);
                BaseLogic baseLogic = new BaseLogic();
                EntityModule.Instance.SpawnEntity<BaseEntity>(baseData, baseLogic, "Cube", "Cube");
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
             ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.transform.gameObject.name);
                Debug.Log(hit.collider.gameObject.name);
                BaseEntity objEntity = hit.collider.gameObject.GetComponent<BaseEntity>();
                EntityModule.Instance.UnSpawnEntity(objEntity);
            }
        }
    }
}
