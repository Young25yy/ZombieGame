using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public Transform head;
    public Transform shootPoint;
    private float rotateSpeed = 30f;
    private TowerInfo info;
    private Zombie targetZombie;
    private List<Zombie> targetZombies = new List<Zombie>();
    private Vector3 targetPos;
    private float CD = 0;

    public void Init(TowerInfo info)
    {
        this.info = info;
    }
    private void Awake()
    {
        info = GameDataMgr.Instance.towerData[6];
    }
    // Update is called once per frame
    void Update()
    {
        CD += Time.deltaTime;
        if (info.atkType == 1)
        {
            if (targetZombie == null ||
                targetZombie.isDead ||
                Vector3.Distance(transform.position, targetZombie.transform.position) > info.atkRange)
            {
                targetZombie = GameLevelMgr.Instance.FindZombie(transform.position, info.atkRange);
            }
            if (targetZombie == null) return;
            targetPos = targetZombie.transform.position;
            targetPos.y = head.position.y;
            head.rotation = Quaternion.Slerp(head.rotation, Quaternion.LookRotation(targetPos - head.position), rotateSpeed * Time.deltaTime);
            if (Vector3.Angle(head.forward, targetPos - head.position) <= 5 && 
                CD >= info.offSetTime)
            {   GameObject effObj2 = Instantiate(Resources.Load<GameObject>("Eff/1"), targetPos, Quaternion.identity);
                Destroy(effObj2, 2f);
                targetZombie.Wound(info.atk);
                GameDataMgr.Instance.PlaySound("Music/Tower");
                GameObject effObj = Instantiate(Resources.Load<GameObject>(info.eff), shootPoint.position, shootPoint.rotation);
                effObj.transform.SetParent(shootPoint);
                Destroy(effObj, 0.2f);
                CD = 0;
            }
        }
        else if (info.atkType == 2)
        {
            if (targetZombie == null ||
                targetZombie.isDead ||
                Vector3.Distance(transform.position, targetZombie.transform.position) > info.atkRange)
            {
                targetZombie = GameLevelMgr.Instance.FindZombie(transform.position, info.atkRange);
            }
            if (targetZombie == null) return;
            targetPos = targetZombie.transform.position;
            targetPos.y = head.position.y;
            head.rotation = Quaternion.Slerp(head.rotation, Quaternion.LookRotation(targetPos - head.position), rotateSpeed * Time.deltaTime);
            if (Vector3.Angle(head.forward, targetPos - head.position) <= 5 && 
                CD >= info.offSetTime)
            {
                GameObject effObj2 = Instantiate(Resources.Load<GameObject>("Eff/3"), targetPos, Quaternion.identity);
                Destroy(effObj2, 2f);
                Collider[] colliders = Physics.OverlapSphere(targetPos, 5f, 1 << LayerMask.NameToLayer("Zombie"));
                foreach (Collider collider in colliders)
                {
                    Zombie zombie = collider.GetComponent<Zombie>();
                    if (zombie != null && !zombie.isDead)
                    {
                        zombie.Wound(info.atk);
                    }
                }
                GameDataMgr.Instance.PlaySound("Music/Tower");
                GameObject effObj = Instantiate(Resources.Load<GameObject>(info.eff), shootPoint.position, shootPoint.rotation);
                Destroy(effObj, 0.2f);
                CD = 0;
            }
        }
    }
}
