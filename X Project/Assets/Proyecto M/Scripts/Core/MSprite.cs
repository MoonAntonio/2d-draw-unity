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
#endif
#if UNITY_ANDROID
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
    /// <summary>
    /// <paraz>Cuando el mouse baja</paraz>
    /// </summary>
    private void MouseBaja()// Cuando el mouse baja
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

    /// <summary>
    /// <para>Cuando el mouse esta bajado</para>
    /// </summary>
    private void Mouse()// Cuando el mouse esta bajado
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

    /// <summary>
    /// <para>Cuando se suelta el mouse</para>
    /// </summary>
    private void MouseSuelta()// Cuando se suelta el mouse
    {
        // Deseleccionamos
        arrastrando = false;
        ActualizarEstadoPos();
    }

    /// <summary>
    /// <para>Cuando el touch baja</para>
    /// </summary>
    /// <param name="t">El touch</param>
    private void TouchBaja(Touch t)// Cuando el touch baja
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

    /// <summary>
    /// <para>Cuando el touch esta bajado</para>
    /// </summary>
    private void Touch()// Cuando el touch esta bajado
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

    /// <summary>
    /// <para>Cuando se suelta el touch</para>
    /// </summary>
    private void TouchSuelta()// Cuando se suelta el touch
    {
        // Deseleccionamos
        arrastrando = false;
        ActualizarEstadoPos();
    }

    /// <summary>
    /// <para>Actualiza la posicion de los sprite</para>
    /// </summary>
    private void ActualizarEstadoPos()// Actualiza la posicion de los sprite
    {
        // Seleccionamos el elemento
        for (int n = 0; n < elementos.Count; n++)
        {
            // Comprueba si son arrastrables
            if (elementos[n].arrastable == true)
            {
                // Si la posicion del sprite es menos a la de la posicion final en x mas el desfase
                if (elementos[n].prefab.transform.position.x <= elementos[n].posFinal.position.x + elementos[n].desfase)
                {
                    // Si la posicion del sprite es menos a la de la posicion final en y mas el desfase
                    if (elementos[n].prefab.transform.position.y <= elementos[n].posFinal.position.y + elementos[n].desfase)
                    {
                        // El sprite se coloca en la posicion final
                        elementos[n].prefab.transform.position = elementos[n].posFinal.position;
                    }
                }
            }
        }
    }
    #endregion
}