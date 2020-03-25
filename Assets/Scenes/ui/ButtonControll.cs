using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonControll : MonoBehaviour
{
    private Material uiMaterial;
    private Material buffMaterialUi;

    private void Start()
    {
        // uiMaterial = GameObject<MeshRenderer>().Material;
        // buffMaterialUi = uiMaterial;
    }
    public void quit()
    {
        Application.Quit();
    }
    public void pause()
    {
        uiMaterial.color = new Color(
            (uiMaterial.color.g + uiMaterial.color.r + uiMaterial.color.b) / 3,
            (uiMaterial.color.g + uiMaterial.color.r + uiMaterial.color.b) / 3,
            (uiMaterial.color.g + uiMaterial.color.r + uiMaterial.color.b) / 3);
    }
    public void resume(){
        uiMaterial = buffMaterialUi;
    }
}
