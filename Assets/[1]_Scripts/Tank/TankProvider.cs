using UnityEngine;
using UnityEngine.UI;

public class TankProvider : MonoBehaviour
{
    #region Properties

    public Canvas TankCanvas => tankCanvas;
    public Image HPBar => hpBar;
    public Transform FirePoint => firePoint;
    public Transform TankTurret => tankTurret;
    public Transform Eyes => eyes;

    #endregion


    #region Var

    [Header("[UI]")]
    [SerializeField] Canvas tankCanvas;
    [SerializeField] Image hpBar;

    [Space]
    [Header("[Weapon]")]
    [SerializeField] Transform firePoint;
    [SerializeField] Transform tankTurret;

    [Space]
    [Header("[AI]")]
    [SerializeField] Transform eyes;

    #endregion
}
