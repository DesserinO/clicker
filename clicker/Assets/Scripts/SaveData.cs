using System;
using System.Text;
using UnityEngine;

public class SaveData
{
	public int ID { get; set; }
	public string Score { get; set; }
	public ulong[] Lvls { get; set; }

	public void PrintData()
    {
		Debug.Log($"ID: {ID}, Score: {Score}");
    }
	public byte[] ToBytes(int id, string score, ulong[] lvls)
	{
		PrintData();
		var byteID = Encoding.UTF8.GetBytes(id.ToString() + '\t');
		var byteScore = Encoding.UTF8.GetBytes(score + '\t');
		var temp = "";

		for (int i = 0; i < lvls.Length; i++)
		{
			temp += $"{lvls[i]} ";
		}

		var byteLvls = Encoding.UTF8.GetBytes(temp);
		var result = new byte[byteID.Length + byteScore.Length + byteLvls.Length];

		Array.ConstrainedCopy(byteID, 0, result, 0, byteID.Length);
		Array.ConstrainedCopy(byteScore, 0, result, byteID.Length, byteScore.Length);
		Array.ConstrainedCopy(byteLvls, 0, result, byteID.Length + byteScore.Length, byteLvls.Length);

		return result;
	}
}
