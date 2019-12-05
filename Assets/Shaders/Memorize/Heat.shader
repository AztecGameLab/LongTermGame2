﻿// Modified from https://lindenreid.wordpress.com/2018/03/05/heat-distortion-shader-tutorial/
Shader "Custom/Heat"
{
    Properties
    {
        _Noise("Noise", 2D) = "white" {}
        _StrengthFilter("Strength Filter", 2D) = "white" {}
        _Strength("Distort Strength", float) = 1.0
        _Speed("Distort Speed", float) = 1.0
        _BackgroundTexture ("Background Texture", 2D) = "white" {}
    }

    SubShader
    {
        Tags 
        {
            "Queue" = "Transparent"
            "DisableBatching" = "True"
        }

        // Render the object with the texture generated above, and invert the colors
        Pass
        {
            // ZTest Always

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            // Properties
            sampler2D _Noise;
            sampler2D _StrengthFilter;
            sampler2D _BackgroundTexture;
            float     _Strength;
            float     _Speed;

            struct vertexInput
            {
                float4 vertex : POSITION;
                float3 texCoord : TEXCOORD0;
            };

            struct vertexOutput
            {
                float4 pos : SV_POSITION;
                float4 grabPos : TEXCOORD0;
            };

            vertexOutput vert(vertexInput input)
            {
                vertexOutput output;
                
                // face camera
                float4 pos = input.vertex;
                pos = mul(UNITY_MATRIX_P, mul(UNITY_MATRIX_MV, float4(0, 0, 0, 1)) + float4(pos.x, pos.z, 0, 0));
                output.pos = pos;                

                // use ComputeGrabScreenPos function from UnityCG.cginc to get the correct texture coordinate
                output.grabPos = ComputeGrabScreenPos(output.pos);

                // distort based on noise & strength filter
                float noise = tex2Dlod(_Noise, float4(input.texCoord, 0)).rgb;
                float3 filt = tex2Dlod(_StrengthFilter, float4(input.texCoord, 0)).rgb;
                output.grabPos.x += cos(noise*_Time.x*_Speed) * filt * _Strength;
                output.grabPos.y += sin(noise*_Time.x*_Speed) * filt * _Strength;

                // flip vertical for direct3d
                if (_ProjectionParams.x < 0)
                    output.grabPos.y = 1 - output.grabPos.y;

                return output;
            }

            float4 frag(vertexOutput input) : COLOR
            {
                return tex2Dproj(_BackgroundTexture, input.grabPos);
            }

            ENDCG
        }
    }
}
