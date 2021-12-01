using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class IdleController
{
	private float idleAmount, score;
	private float scoreCoef;
	private float coef = 3;

	private Dictionary<string, float> valueModule = new Dictionary<string, float>();
	private List<string> keys = new List<string>();
	private Text scoreText, idleText;

	public static IdleController Instance { get; private set; } = new IdleController();

	public void UpdateIdleText(out float return1)
	{
		foreach (string k in valueModule.Keys)
		{
			if (idleAmount <= Math.Pow(10, 6) && coef == 3 & !idleText.text.Contains(k))
			{
				idleText.text = $"{String.Format("{0:0}", idleAmount)}";
			}

			if (idleAmount > 999999 && coef == 3)
			{
				coef += coef;
				idleAmount /= float.Parse(Math.Pow(10, 6).ToString());

				for (int i = 0; i < valueModule.Count; i++)
				{
					if (valueModule[keys[i]] == coef)
					{
						idleText.text = $"{String.Format("{0:0.0}", idleAmount)} {keys[i]}";
					}
				}
			}

			if (idleAmount >= 1 && idleAmount <= 1000 & idleText.text.Contains(k))
			{
				for (int i = 0; i < valueModule.Count; i++)
				{
					if (valueModule[keys[i]] == coef)
					{
						idleText.text = $"{String.Format("{0:0.0}", idleAmount)} {keys[i]}";
					}
				}
			}

			if (idleAmount > 999 && idleText.text.Contains(k))
			{
				idleAmount /= float.Parse(Math.Pow(10, 3).ToString());
				coef += 3;

				for (int i = 0; i < valueModule.Count; i++)
				{
					if (valueModule[keys[i]] == coef)
					{
						idleText.text = $"{String.Format("{0:0.0}", idleAmount)} {keys[i]}";
					}
				}
			}
		}
		return1 = idleAmount;
	}

	public void SetValueModule (Dictionary<string, float> valueModule)
	{
		this.valueModule = valueModule;
	}

	public void SetKeys (List<string> keys)
	{
		this.keys = keys;
	}

	public void SetText (Text scoreText, Text idleText)
	{
		this.scoreText = scoreText;
		this.idleText = idleText;
	}

	public void SetIdleAmount (float idleAmount)
	{
		this.idleAmount = idleAmount;
	}

	public void SetScoreCoef (float scoreCoef)
	{
		this.scoreCoef = scoreCoef;
	}

	public void SetScore (float score)
	{
		this.score = score;
	}

	public float GetIdleAmount()
	{
		return idleAmount;
	}

	public float GetTempCoef(float scoreCoef)
	{
		float tempCoef = 0;
		float tempIdleCoef = this.coef;
		for (int i = 0; i < valueModule.Count; i++)
		{
			if (tempIdleCoef < scoreCoef)
			{
				tempCoef += 3;
				tempIdleCoef += 3;
			}

			if (tempIdleCoef > scoreCoef)
			{
				tempCoef += 3;
				tempIdleCoef -= 3;
			} else { break; }
		}

		return tempCoef;
	}

	public void Idle(out float return1, out float return2)
	{
		string[] temp = scoreText.text.Split(' ');
		if (temp.Length > 1)
		{
			foreach (string k in valueModule.Keys)
			{
				if (temp[1] == k)
				{
					scoreCoef = valueModule[k];
				}
			}
		} else
		{
			scoreCoef = coef;
		}

		if (coef == 3)
		{
			if (coef == scoreCoef)
			{
				score = float.Parse(temp[0]) + idleAmount;
			}

			if (scoreCoef == 6 && GetTempCoef(scoreCoef) < 12)
			{
				score = float.Parse(temp[0]) + (idleAmount / float.Parse(Math.Pow(10, scoreCoef).ToString()));
			}

			if (scoreCoef > 6 && GetTempCoef(scoreCoef) < 12)
			{
				score += idleAmount / float.Parse(Math.Pow(10, scoreCoef).ToString());
			}
		}

		if (coef >= 6 && GetTempCoef(scoreCoef) < 12)
		{
			if (coef == scoreCoef)
			{
				score += idleAmount;
			}

			if (coef < scoreCoef && GetTempCoef(scoreCoef) < 12)
			{
				score += idleAmount / float.Parse(Math.Pow(10, scoreCoef).ToString());
			}

			if (coef > scoreCoef)
			{
				scoreCoef = coef;
				score /= float.Parse(Math.Pow(10, scoreCoef).ToString());
			}
		}

		return1 = score;
		return2 = scoreCoef;
	}
}
