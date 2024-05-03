Shader "Unlit/3DFogShader" {
    Properties{
        _Wavenumber("Wavenumber", Float) = 3
        _Source("Source", Float) = (1,1,4,0)
    }
    SubShader
    {
        Tags {
            "Queue" = "Transparent"
            "RenderType" = "Transparent"
        }

        Blend One OneMinusSrcAlpha
        LOD 100

        Pass {
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            #define MAX_STEP_COUNT 64
            #define EPSILON 0.00001f

            struct MeshData {
                float4 vertex : POSITION;
            };

            struct Interpolators {
                float4 vertex : SV_POSITION;
                float3 objectVertex : TEXCOORD0;
                float3 vectorToSurface : TEXCOORD1;
            };
            float _Wavenumber;
            float3 _Source;

            //Vertex shader
            Interpolators vert(MeshData v) {
                Interpolators o;

                // Vertex in object space this will be the starting point of raymarching
                o.objectVertex = v.vertex;

                // Calculate vector from camera to vertex in world space
                float3 worldVertex = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.vectorToSurface = worldVertex - _WorldSpaceCameraPos;

                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }

            /*float4 BlendUnder(float4 color, float4 newColor) {
                color.rgb *= (1.0 - color.a) + newColor.a * newColor.rgb;
                color.a += newColor.a;
                return color;
            }*/

            float InverseLerp(float a, float b, float v) {
                return (v - a) / (b - a);
            }

            float Remap(float iMin, float iMax, float oMin, float oMax, float v) {
                float t = InverseLerp(iMin, iMax, v);
                return lerp(oMin, oMax, t);
            }

            float4 colorFunction(float3 position) {
                float t = _Time[1];
                float3 k = _Source;
                float w = 5;
                //float value = 0.1 * sin(k * x + w * t) * exp(-10 * (z * z + y * y));
                float value = 0.1 * sin(dot(k,position) - w * t);
                //value = 1000*value * value;
                float4 color = { value, 0, 0, value };

                return color;
            }

            //Fragment shader
            fixed4 frag(Interpolators i) : SV_Target {

                float3 rayOrigin = i.objectVertex;
                //float3 rayDirection = normalize(i.vectorToSurface);
                float3 rayDirection = normalize(mul(unity_WorldToObject, float4(normalize(i.vectorToSurface), 0)));

                float3 samplePosition = rayOrigin;

                float stepSize = 1.0 / float(MAX_STEP_COUNT);
                float accumulationAlpha = 0;
                float4 finalColor = float4(0,0,0,0);

                for (int i = 0; i < MAX_STEP_COUNT; i++) {
                        float4 sampledColor = colorFunction(samplePosition);
                        accumulationAlpha += abs(sampledColor[3]) * stepSize;
                        //finalColor.rgb = finalColor.rgb * (1 - sampledColor[3]) + sampledColor.rgb * sampledColor.a;
                        finalColor.rgb = finalColor.rgb + sampledColor.rgb * (1 - finalColor.a);
                        samplePosition += rayDirection * stepSize;
                }

                //return float4(finalColor.x, 0, finalColor.z, accumulationAlpha);
                //finalColor.a = accumulationAlpha/5;
                finalColor.a = stepSize*accumulationAlpha;
                return finalColor;
            }
            ENDCG
        }
    }
}
