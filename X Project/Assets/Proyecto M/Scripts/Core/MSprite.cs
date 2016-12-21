//                                  ┌∩┐(◣_◢)┌∩┐
//                                                                              \\
// MSprite.cs (00/00/0000)													    \\
// Autor: Antonio Mateo (Moon Pincho) 									        \\
// Descripcion:																	\\
// Fecha Mod:		00/00/0000													\\
// Ultima Mod:																	\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
using System.Collections.Generic;

#endregion

public class MSprite : MonoBehaviour 
{
    #region Variables Estaticas
    public static bool arrastrando;
    #endregion

    #region Variables Publicas
    public List<clsSprite> elementos = new List<clsSprite>();
    #endregion

    #region Variables privadas
    private Vector3 centroSprite;
    private Vector3 nuevoCentroSprite;
    private Vector3 posTouch;
    private Vector3 offSet;
    private RaycastHit hit;
    #endregion

    #region Inicializadores
    /// <summary>
    /// <para>Inicializador de MSprite</para>
    /// </summary>
    public void Start()// Inicializador de MSprite
    {
        // Asignamos los nombres de los elementos
        for (int i = 0; i < elementos.Count; i++)
        {
            elementos[i].prefab.name = elementos[i].nombre;
        }
    }
    #endregion

    #region Actualizador
    /// <summary>
    /// <para>Actualizador de MSprite</para>
    /// </summary>
    public void Update()// Actualizador de MSprite
    {
#if UNITY_EDITOR
        #region Estados Mouse
        // Cuando bajas el mouse
        if (Input.GetMouseButtonDown(0))
        {
            MouseBaja();
        }

        // Cuando el mouse esta bajado
        if (Input.GetMouseButton(0))
        {
            Mouse();
        }

        // Cuando levantas el mouse
        if (Input.GetMouseButtonUp(0))
        {
            MouseSuelta();
        }
        #endregion
#else
        foreach (Touch touch in Input.touches)
        {
            // Fases del touch
            switch (touch.phase)
            {
        #region Estados Touch
                // Cuando haces el touch
                case TouchPhase.Began:
                    TouchBaja(touch);
                    break;

                // Si estas moviendo 
                case TouchPhase.Moved:
                    Touch();
                    break;

                // Si no hay touch
                case TouchPhase.Ended:
                    TouchSuelta();
                    break;

                // Ninguna de las fases anteriores
                default:
                    Debug.LogWarning("[Advertencia]: La fase del touch no es la correcta.");
                    break;
        #endregion

            }
        }
#endif
    }
    #endregion

    #region API
    private void MouseBaja()
    {
        // Crear el ray
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Si el ray choca con el collider
        if (Physics.Raycast(ray, out hit))
        {
            // Seleccionamos el elemento
            for (int i = 0; i < elementos.Count; i++)
            {
                // Asignamos los componentes y variables
                elementos[i].prefab = hit.collider.gameObject;
                centroSprite = elementos[i].prefab.transform.position;
                posTouch = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                offSet = posTouch - centroSprite;
                arrastrando = true;
            }
        }
    }

    private void Mouse()
    {
        // Si se esta arrastrando el sprite
        if (arrastrando == true)
        {
            // Seleccionamos el elemento
            for (int i = 0; i < elementos.Count; i++)
            {
                // Asignamos la nueva posicion
                posTouch = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                nuevoCentroSprite = posTouch - offSet;
                elementos[i].prefab.transform.position = new Vector3(nuevoCentroSprite.x, nuevoCentroSprite.y, centroSprite.z);
            }
        }
    }

    private void MouseSuelta()
    {
        // Deseleccionamos
        arrastrando = false;
    }

    private void TouchBaja(Touch t)
    {
        // Crear el ray
        Ray ray = Camera.main.ScreenPointToRay(t.position);

        // Si el ray choca con el collider
        if (Physics.SphereCast(ray, 0.3f, out hit))
        {
            // Seleccionamos el elemento
            for (int n = 0; n < elementos.Count; n++)
            {
                // Asignamos los componentes y variables
                elementos[n].prefab = hit.collider.gameObject;
                centroSprite = elementos[n].prefab.transform.position;
                posTouch = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                offSet = posTouch - centroSprite;
            }
        }
    }

    private void Touch()
    {
        // Si se esta arrastrando el sprite
        if (arrastrando)
        {
            // Seleccionamos el elemento
            for (int n = 0; n < elementos.Count; n++)
            {
                // Asignamos la nueva posicion
                posTouch = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                nuevoCentroSprite = posTouch - offSet;
                elementos[n].prefab.transform.position = new Vector3(nuevoCentroSprite.x, nuevoCentroSprite.y, centroSprite.z);
            }
        }
    }

    private void TouchSuelta()
    {
        // Deseleccionamos
        arrastrando = false;
    }
    #endregion
}