sampler2D  inputSampler : register(S0);
/// <summary>The center of the gradient. </summary>
/// <minValue>0,0</minValue>
/// <maxValue>1,1</maxValue>
/// <defaultValue>.5,.5</defaultValue>
float2 CenterPoint : register(C0);

/// <summary>The primary color of the gradient. </summary>
/// <defaultValue>Blue</defaultValue>
float4 StartColor : register(C1);

/// <summary>The secondary color of the gradient. </summary>
/// <defaultValue>Red</defaultValue>
float4 EndColor : register(C2);

/// <summary>The starting angle of the gradient, counterclockwise from X-axis</summary>
/// <minValue>0</minValue>
/// <maxValue>360</maxValue>
/// <defaultValue>0</defaultValue>
float StartAngle : register(C3);

/// <summary>The arc length angle of the gradient, counterclockwise from X-axis</summary>
/// <minValue>-360</minValue>
/// <maxValue>360</maxValue>
/// <defaultValue>360</defaultValue>
float ArcLength : register(C4);

static float4 transparent = float4(0, 0, 0, 0); // WPF uses pre-multiplied alpha everywhere internally for a number of performance reasons.
static float Pi = 3.141592f;

float AngleTo(float2 v1, float2 v2, bool clockWise)
{
    int sign = clockWise ? -1 : 1;
    float a1 = atan2(v1.y, v1.x) * sign;
    float a2 = atan2(v2.y, v2.x) * sign;
    float a = a2 - a1;
    if (a < 0)
    {
        return 2 * Pi + a; ;
    }
    return a;
}
float2x2 RotationMatrix(float rotation)
{
    float c = cos(rotation);
    float s = sin(rotation);
    return float2x2(c, -s, s, c);
}

float4 main(float2 uv : TEXCOORD) : COLOR
{
  float4 src = tex2D(inputSampler, uv);
  if (src.a < 0.01 || abs(ArcLength) < 0.01)
  {
      return transparent;
  }
  float2 v = uv - CenterPoint;
  float2 vs = mul(float2(1, 0), RotationMatrix(radians(StartAngle)));
  float a = degrees(AngleTo( vs,v, ArcLength < 0));
  float f = abs(a) < 0.1 ? 0 : a / abs(ArcLength);
  if (f < 0 || f > 1)
  {
      return transparent;
  }
  float3 rgb = lerp(StartColor.rgb, EndColor.rgb, f);
  return float4(rgb, src.a);
}