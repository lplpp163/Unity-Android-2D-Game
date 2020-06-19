Shader "Custom/Gray"
{
	Properties
	{
	   [PerRendererData]_MainTex("Sprite Texture", 2D) = "white" {}
	   _Color("Tint", Color) = (1,1,1,1)
	   [MaterialToggle] PixelSnap("Pixel snap", Float) = 0
	   [HideInInspector] _RendererColor("RendererColor", Color) = (1,1,1,1)
	   [HideInInspector] _Flip("Flip", Vector) = (1,1,1,1)
	   [PerRendererData] _AlphaTex("External Alpha", 2D) = "white" {}
	   [PerRendererData] _EnableExternalAlpha("Enable External Alpha", Float) = 0
	}
		SubShader
	   {
		  Tags
		  {
			 "Queue" = "Transparent"
			 "IgnoreProjector" = "True"
			 "RenderType" = "Transparent"
			 "PreviewType" = "Plane"
			 "CanUseSpriteAtlas" = "True"
		  }
		  Cull Off
		  Lighting Off
		  ZWrite Off
		  Blend One OneMinusSrcAlpha
		  Pass
			{
			CGPROGRAM
				#pragma vertex SpriteVert
				#pragma fragment MySpriteFrag
				#pragma target 2.0
				#pragma multi_compile_instancing
				#pragma multi_compile _ PIXELSNAP_ON
				#pragma multi_compile _ ETC1_EXTERNAL_ALPHA
				#include "UnityCG.cginc"
				#include "UnitySprites.cginc" 
				
				fixed4 MySampleSpriteTexture(float2 uv)
				{
				   fixed4 color = tex2D(_MainTex, uv);
				#if ETC1_EXTERNAL_ALPHA
				   fixed4 alpha = tex2D(_AlphaTex, uv);
				   color.a = lerp(color.a, alpha.r, _EnableExternalAlpha);
				#endif
				   return color;
				}

				fixed4 MySpriteFrag(v2f IN) : SV_Target
				{
				   fixed4 col = MySampleSpriteTexture(IN.texcoord) * IN.color;

					float gray = dot(col.rgb, float3(0.299, 0.587, 0.114));
					col.rgb = float3(gray, gray, gray);
					col.rgb *= col.a;
				   return col;
				}
			ENDCG
			}
	   }
}