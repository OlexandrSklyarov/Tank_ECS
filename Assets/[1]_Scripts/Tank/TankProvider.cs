using UnityEngine;
using UnityEngine.UI;

public class TankProvider : MonoBehaviour
{
    #region Properties

    public Canvas TankCanvas => tankCanvas;
    public Image HPBar => hpBar;
    public Transform FirePoint => firePoint;
    public Transform Turret => turret;
    public Transform Barrel => barrel;
    public Transform Eyes => eyes;
    public WheelCollider[] Wheels => wheels;

    #endregion


    #region Var

    [Header("[UI]")]
    [SerializeField] Canvas tankCanvas;
    [SerializeField] Image hpBar;


    [Space, Header("[Weapon]")]
    [SerializeField] Transform firePoint;
    [SerializeField] Transform turret;
    [SerializeField] Transform barrel;

    
    [Space, Header("[AI]")]
    [SerializeField] Transform eyes;
    

    [Space, Header("[Wheels]")]
    [SerializeField] WheelCollider[] wheels;

    #endregion
}
