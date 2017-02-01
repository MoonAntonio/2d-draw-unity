#region Librerias
using UnityEngine;
#endregion

public class clsSprite : MonoBehaviour 
{
    #region Propiedades
    public int id;
    public string nombre;
    public bool arrastable;
    public GameObject prefab;
    public Transform posFinal;
    public float desfase;
    public ValorInicialX valorX;
    public ValorInicialY valorY;
    #endregion
}

public enum ValorInicialX
{
    positivo,
    negativo,
}

public enum ValorInicialY
{
    positivo,
    negativo,
}