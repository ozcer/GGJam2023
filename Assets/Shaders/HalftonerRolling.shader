// Modified version of Unity's UI default shader that displays alpha as halftones, rolling with world space (WIP)

Shader "UI/HalftonerRolling"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _RepeatedPatternTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        _HalftoneSize ("Halftone Size (UV units)", Range(0, 1)) = 0.05
        _HalftoneSoftness ("Halftone Softness (UV units)", Range(0, 1)) = 0.05
        _StencilComp ("Stencil Comparison", Float) = 8
        _Stencil ("Stencil ID", Float) = 0
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilReadMask ("Stencil Read Mask", Float) = 255

        _ColorMask ("Color Mask", Float) = 15

        [Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Stencil
        {
            Ref [_Stencil]
            Comp [_StencilComp]
            Pass [_StencilOp]
            ReadMask [_StencilReadMask]
            WriteMask [_StencilWriteMask]
        }

        Cull Off
        Lighting Off
        ZWrite Off
        ZTest [unity_GUIZTestMode]
        Blend SrcAlpha OneMinusSrcAlpha
        ColorMask [_ColorMask]

        Pass
        {
            Name "Default"
        CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0

            #include "UnityCG.cginc"
            #include "UnityUI.cginc"

            #pragma multi_compile_local _ UNITY_UI_CLIP_RECT
            #pragma multi_compile_local _ UNITY_UI_ALPHACLIP

            struct appdata_t
            {
                float4 vertex   : POSITION;
                float4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float4 vertex   : SV_POSITION;
                fixed4 color    : COLOR;
                float2 texcoord  : TEXCOORD0;
                float4 worldPosition : TEXCOORD1;
                float2 mainTexcoord : TEXCOORD2;
                float4 screenPosition : TEXCOORD3;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            sampler2D _MainTex;
            sampler2D _RepeatedPatternTex;
            float _HalftoneSize;
            float _HalftoneSoftness;
            fixed4 _Color;
            fixed4 _TextureSampleAdd;
            float4 _ClipRect;
            float4 _MainTex_ST;
            float4 _MainTex_TexelSize;
            float4 _RepeatedPatternTex_ST;

            v2f vert(appdata_t v)
            {
                v2f OUT;
                UNITY_SETUP_INSTANCE_ID(v);
                OUT.worldPosition = v.vertex;
                OUT.vertex = UnityObjectToClipPos(OUT.worldPosition);

                OUT.mainTexcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
                float ratio = _MainTex_TexelSize.z/_MainTex_TexelSize.w;
                float2 texCoord = float2(v.texcoord.x * ratio, v.texcoord.y);
                OUT.texcoord = TRANSFORM_TEX(texCoord, _RepeatedPatternTex);
                float4 toWorld = mul(unity_ObjectToWorld, v.vertex);
                OUT.screenPosition = ComputeScreenPos(v.vertex);
                OUT.color = v.color * _Color;
                return OUT;
            }

            fixed4 frag(v2f IN) : SV_Target
            {
                // tileable texture colour
                half4 color = (tex2D(_RepeatedPatternTex, IN.texcoord) + _TextureSampleAdd) * IN.color;
                
                // not really a true halftone because each pixel operates on its own opacity
                // true circles would require matching up UV and screen coords exactly
                // but I prefer the morphed effect because it looks cooler
                float4 sampledColour = tex2D(_MainTex, IN.mainTexcoord) * IN.color;
                
                float2 screenCoordSample = IN.screenPosition.xy / (IN.screenPosition.w * 50);
                
                // center of the halftone
                float2 center = float2(floor(screenCoordSample.x/_HalftoneSize) + 0.5, floor(screenCoordSample.y/ _HalftoneSize) + 0.5) * _HalftoneSize;
                float distanceFromCenter = distance(screenCoordSample, center)/_HalftoneSize;
                
                // actual alpha of the pixel
                float alpha = 1 - smoothstep(sampledColour.a, sampledColour.a + _HalftoneSoftness, distanceFromCenter);

                color *= ((sampledColour + _TextureSampleAdd) * IN.color);
                color.a = alpha;
                
                #ifdef UNITY_UI_CLIP_RECT
                color.a *= UnityGet2DClipping(IN.worldPosition.xy, _ClipRect);
                #endif

                #ifdef UNITY_UI_ALPHACLIP
                clip (color.a - 0.001);
                #endif

                return color;
            }
        ENDCG
        }
    }
}
