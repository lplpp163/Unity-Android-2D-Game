using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropShaderScript : MonoBehaviour
{
    Dropdown dpd;
    // Start is called before the first frame update
    void Start()
    {
        dpd = GetComponent<Dropdown>();
        dpd.onValueChanged.AddListener(shaderMode);
    }

    void shaderMode(int index)
    {
        switch (index)
        {
            case 0:
                SpriteShader.shaderMode = ShaderMode.Normal;
                break;
            case 1:
                SpriteShader.shaderMode = ShaderMode.Gray;
                break;
            case 2:
                SpriteShader.shaderMode = ShaderMode.Crazy;
                break;
            case 3:
                SpriteShader.shaderMode = ShaderMode.Shining;
                break;
            case 4:
                SpriteShader.shaderMode = ShaderMode.Negtive;
                break;
        }
    }
}
