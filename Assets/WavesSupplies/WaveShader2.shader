Shader "Unlit/WaveShader2" {
    Properties{
        _PlaneSourceCount("PlaneSourceCount", Int) = 0
        _PlaneSource0("PlaneSource0", Float) = (0,0,0,0)
        _PlaneSource1("PlaneSource1", Float) = (0,0,0,0)

        _PointSourceCount("PointSourceCount", Int) = 0
        _PointSource0("PointSource0", Float) = (0,0,0,0)
        _PointSource1("PointSource1", Float) = (0,0,0,0)

        _ObjectScale("ObjectScale", Float) = (0,0,0)
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
            int _PlaneSourceCount;
            float4 _PlaneSource0;
            float4 _PlaneSource1;

            int _PointSourceCount;
            float4 _PointSource0;
            float4 _PointSource1;

            float4 _ObjectScale;

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

            float2 fieldOfPlaneWave(float3 position, float4 planeWaveData) {
                float t = _Time[1];
                float4 scale = _ObjectScale;
                float speed = .1;
                float3 k = planeWaveData.xyz;
                float kMag = length(k);
                float omega = speed * kMag;
                float intensity = planeWaveData.w;
                float sinVal = sin(dot(k * scale, position) - omega * t);
                if (sinVal > .99) {
                    return float2(0, 2);
                }

                float field = intensity * sinVal;
                return float2(field, 0);
            }

            float fieldOfPointWave(float3 position, float4 pointWaveData) {
                float t = _Time[1];
                float4 scale = _ObjectScale;
                float speed = .1;
                float kMag = pointWaveData.w;
                float omega = speed * kMag;
                //float3 scaledDisplacement = scale * (position - pointWaveData.xyz);
                float3 scaledDisplacement = scale * position - pointWaveData.xyz;

                float eNaught = .003;
                float rMag = length(scaledDisplacement);

                return eNaught * sin(rMag * pointWaveData.w - omega * t) / rMag;
                //return 0.01*sin(1*t);

            }

            float4 colorFunction(float3 position) {
                // block to calculate field value
                float value = 0;
                if (_PlaneSourceCount >= 1) {
                    float2 p0 = fieldOfPlaneWave(position, _PlaneSource0);
                    if (p0.y > 1) {
                        float intensity = .02;
                        return float4(intensity, intensity, intensity, 1);
                    }
                    value += fieldOfPlaneWave(position, _PlaneSource0).x;
                }
                if (_PlaneSourceCount >= 2) {
                    value += fieldOfPlaneWave(position, _PlaneSource1).x;
                }

                if (_PointSourceCount >= 1) {
                    value += fieldOfPointWave(position, _PointSource0);
                }
                if (_PointSourceCount >= 2) {
                    value += fieldOfPointWave(position, _PointSource1);
                }



                // block sets color from field value
                float alphaScale = 1000;

                if (value > 0) {
                    return float4(value, 0, 0, alphaScale * value);
                }
                if (value < 0) {
                    return float4(0, .0001, -value, -alphaScale * value);
                }
                return float4(0, 0, 0, 0);
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
                finalColor.a = stepSize * accumulationAlpha;
                return finalColor;
            }
            ENDCG
        }
    }
}