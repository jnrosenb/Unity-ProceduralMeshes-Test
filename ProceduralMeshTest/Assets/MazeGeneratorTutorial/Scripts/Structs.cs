/*
 * STRUCT---------------------------------------*** 
 * struct for representing a 2d vector with ints.
 *
*/

[System.Serializable]
public struct intVector2
{
	public int x;
	public int z;

	public intVector2(int x, int z)
	{
		this.x = x;
		this.z = z;
	}

	public static intVector2 operator +(intVector2 A, intVector2 B)
	{
		return new intVector2 (A.x + B.x, A.z + B.z);
	}

	public static intVector2 operator -(intVector2 A, intVector2 B)
	{
		return new intVector2 (A.x - B.x, A.z - B.z);
	}

	public static intVector2 operator *(intVector2 A, int B)
	{
		return new intVector2 (A.x * B, A.z * B);
	}

	public static intVector2 operator *(int B, intVector2 A)
	{
		return new intVector2 (A.x * B, A.z * B);
	}

	public static intVector2 operator /(intVector2 A, int B)
	{
		return new intVector2 (A.x / B, A.z / B);
	}
}
