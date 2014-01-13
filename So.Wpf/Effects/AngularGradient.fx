// no input texture, the output is completely generated in code
sampler2D  inputSampler : register(S0);
/// <summary>The center of the gradient. </summary>
/// <minValue>0,0</minValue>
/// <maxValue>1,1</maxValue>
/// <defaultValue>.5,.5</defaultValue>
float2 centerPoint : register(C0);

/// <summary>The primary color of the gradient. </summary>
/// <defaultValue>Blue</defaultValue>
float4 StartColor : register(C1);

/// <summary>The primary color of the gradient. </summary>
/// <defaultValue>Red</defaultValue>
float4 EndColor : register(C2);

/// <summary>The starting angle of the gradient, counterclockwise from X-axis</summary>
/// <minValue>0</minValue>
/// <maxValue>360</maxValue>
/// <defaultValue>0</defaultValue>
float StartAngle : register(C3);

/// <summary>The end angle of the gradient, counterclockwise from X-axis</summary>
/// <minValue>0</minValue>
/// <maxValue>360</maxValue>
/// <defaultValue>360</defaultValue>
float EndAngle : register(C4);

static const float PI = 3.14159265f;
float4 main(float2 uv : TEXCOORD) : COLOR
{
	float4 src= tex2D(inputSampler, uv);
	float2 p = uv - float2(centerPoint);
	float degAngle =degrees((atan2(p.y,-1* p.x) + PI));
	float diff = EndAngle - StartAngle;
	float b = (degAngle - StartAngle) /diff;
	float3 f = lerp(StartColor.rgb, EndColor.rgb, b);
	float4 color = float4(src.a < 0.01 
								? float3(0, 0, 0) // WPF uses pre-multiplied alpha everywhere internally for a number of performance reasons.
								: f, src.a < 0.01 ? 0 : src.a);
	return color;
}