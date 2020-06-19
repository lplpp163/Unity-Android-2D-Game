using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShaderMode
{
    Normal, Gray, Crazy, Shining, Negtive
}

public class SpriteShader : MonoBehaviour
{
    static public ShaderMode shaderMode = ShaderMode.Normal;

    public Material[] materials;
    SpriteRenderer[] PlayerSprites;

    public SpriteRenderer Body, 
        RightEye, LeftEye, 
        RightArm, RightHand, 
        LeftArm, LeftHand, 
        RightFoot, LeftFoot;



    private void Start()
    {
        PlayerSprites = new SpriteRenderer[9];

        PlayerSprites[0] = Body;
        PlayerSprites[1] = RightEye;
        PlayerSprites[2] = LeftEye;
        PlayerSprites[3] = RightArm;
        PlayerSprites[4] = RightHand;
        PlayerSprites[5] = LeftArm;
        PlayerSprites[6] = LeftHand;
        PlayerSprites[7] = RightFoot;
        PlayerSprites[8] = LeftFoot;

        for (int i = 0; i < PlayerSprites.Length; i++) 
        {
            PlayerSprites[i].material = materials[(int)shaderMode];
        }
    }

    private void Update()
    {
        if (shaderMode == ShaderMode.Crazy)
        {
            float _Hue = Mathf.PingPong(Time.time, 1.0F);
            for(int i = 0; i < PlayerSprites.Length; i++)
            {
                PlayerSprites[i].material.SetFloat("_Hue", _Hue);
            }
        }
        if (shaderMode == ShaderMode.Shining)
        {
            Color intensity = Color.white;
            intensity.a = Mathf.PingPong(Time.time, 0.5f);
            for (int i = 0; i < PlayerSprites.Length; i++)
            {
                PlayerSprites[i].material.SetColor("_AddColor", intensity);
            }
        }
    }
}
