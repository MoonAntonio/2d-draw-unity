//                                  ┌∩┐(◣_◢)┌∩┐
//                                                                              \\
// clsSprite.cs (00/00/0000)													\\
// Autor: Antonio Mateo (Moon Pincho) 									        \\
// Descripcion:																	\\
// Fecha Mod:		00/00/0000													\\
// Ultima Mod:																	\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
using UnityEngine.UI;
#endregion

public class clsSprite : MonoBehaviour 
{
    public int id;
    public string nombre;
    public bool arrastable;
    public bool arrastrando;
    public GameObject prefab;

    public Vector3 centroSprite;
    public Vector3 posTouch;
    public Vector3 offSet;
    public Vector3 nuevoCentroSprite;

    RaycastHit hit;

    private float distancia;

    public void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                prefab = hit.collider.gameObject;
                centroSprite = prefab.transform.position;
                posTouch = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                offSet = posTouch - centroSprite;
                arrastrando = true;
            }
        }

        if (Input.GetMouseButton(0))
        {
            if (arrastrando == true)
            {
                posTouch = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                nuevoCentroSprite = posTouch - offSet;
                prefab.transform.position = new Vector3(nuevoCentroSprite.x, nuevoCentroSprite.y, centroSprite.z);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            arrastrando = false;
        }
#else
        foreach (Touch touch in Input.touches)
        {
            switch (touch.phase)
            {
                // Cuando haces el touch
                case TouchPhase.Began:
                    // Crear el ray
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);

                    // Si el ray choca con el collider
                    if (Physics.SphereCast(ray, 0.3f, out hit))
                    {
                        prefab = hit.collider.gameObject;
                        centroSprite = prefab.transform.position;
                        posTouch = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        offSet = posTouch - centroSprite;
                    }
                    break;
                // Si estas moviendo 
                case TouchPhase.Moved:
                    if (arrastrando)
                    {
                        posTouch = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        nuevoCentroSprite = posTouch - offSet;
                        prefab.transform.position = new Vector3(nuevoCentroSprite.x, nuevoCentroSprite.y, centroSprite.z);
                    }
                    break;
                // Si no hay touch
                case TouchPhase.Ended:
                    arrastrando = false;
                    break;

                default:
                    // TODO Mirar fix
                    break;
            }
        }
#endif
    }
}