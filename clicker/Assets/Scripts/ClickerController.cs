using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ClickerController : MonoBehaviour
{
	[SerializeField] Text scoreText, idleText;
	[SerializeField] private float score;

	[SerializeField] private float scoreAmount, idleAmount;

	private string[] currentValues;
	private string[] idleValues;
	private ulong[] lvls;
	private float coef = 3;

	float elapsed = 0f;

	public Dictionary<string, float> valueModule = new Dictionary<string, float>();
	public Dictionary<string, Text[]> upgradeModule = new Dictionary<string, Text[]>();
	List<char> alpha = new List<char>();
	List<string> keys = new List<string>();

	public static ClickerController Instance { get; private set; } = new ClickerController();

	public void OnClick()
	{
		if (coef != 3)
		{
			score += scoreAmount / float.Parse(Math.Pow(10, coef).ToString());
		}
		else
		{
			score += scoreAmount;
		}
	}

	public void UpgradeClick()
	{
		if (score >= float.Parse(currentValues[0]))
		{
			score -= float.Parse(currentValues[0]);
			scoreAmount *= 2;
			currentValues[0] = $"{float.Parse(currentValues[0]) * (float.Parse(currentValues[0]) / 2)}";
			lvls[0] += 1;
		}
	}

	public void UpgradeIdle()
	{
		int num = Int32.Parse(EventSystem.current.currentSelectedGameObject.name);

		if (NumSystem.Instance.IsContainChar(scoreText.text) == false
			&& NumSystem.Instance.IsContainChar(currentValues[num]) == false)
		{
			if (NumSystem.Instance.IsContainChar(idleText.text) == false)
			{
				if (score >= float.Parse(currentValues[num])) 
				{
					score -= float.Parse(currentValues[num]);
					idleAmount += float.Parse(idleValues[num]);
					currentValues[num] = $"{float.Parse(currentValues[num]) + float.Parse(currentValues[num]) / 10}";
					lvls[num] += 1;
				} 
			} else if (NumSystem.Instance.IsContainChar(idleText.text) == true)
			{
				if (score >= float.Parse(currentValues[num]))
				{
					score -= float.Parse(currentValues[num]);
					idleAmount += float.Parse(idleValues[num]) / float.Parse(Math.Pow(10, NumSystem.Instance.GetDegree(NumSystem.Instance.GetCoef(idleText.text))).ToString());
					currentValues[num] = $"{float.Parse(currentValues[num]) + float.Parse(currentValues[num]) / 10}";
					lvls[num] += 1;
				}
			}
		} else if (NumSystem.Instance.IsContainChar(scoreText.text) == true
			&& NumSystem.Instance.IsContainChar(currentValues[num]) == false)
		{
			if (NumSystem.Instance.IsContainChar(idleText.text) == false)
			{
				score -= float.Parse(currentValues[num]) / float.Parse(Math.Pow(10, NumSystem.Instance.GetDegree(NumSystem.Instance.GetCoef(scoreText.text))).ToString());
				idleAmount += float.Parse(idleValues[num]);
				currentValues[num] = currentValues[num] = $"{float.Parse(currentValues[num]) + float.Parse(currentValues[num]) / 10}";
				lvls[num] += 1;
			} else if (NumSystem.Instance.IsContainChar(idleText.text) == true)
			{
				score -= float.Parse(currentValues[num]) / float.Parse(Math.Pow(10, NumSystem.Instance.GetDegree(NumSystem.Instance.GetCoef(scoreText.text) + 6)).ToString());
				idleAmount += float.Parse(idleValues[num]) / float.Parse(Math.Pow(10, NumSystem.Instance.GetDegree(NumSystem.Instance.GetCoef(idleText.text))).ToString());
				currentValues[num] = $"{float.Parse(currentValues[num]) + float.Parse(currentValues[num]) / 10}";
				lvls[num] += 1;
			}
		} else if (NumSystem.Instance.IsContainChar(scoreText.text) == true
			&& NumSystem.Instance.IsContainChar(currentValues[num]) == true)
		{
			if (NumSystem.Instance.GetDiff(scoreText.text, currentValues[num]) == 0
				&& NumSystem.Instance.GetDiff(scoreText.text, currentValues[num]) != -1)
			{
				if (score > float.Parse(currentValues[num].Split(' ')[0]))
				{
					if (NumSystem.Instance.IsContainChar(idleText.text) == false)
					{
						score -= float.Parse(currentValues[num].Split(' ')[0]);

						if (NumSystem.Instance.IsContainChar(idleValues[num]) == false)
						{
							idleAmount += float.Parse(idleValues[num].Split(' ')[0]);
						} else if (NumSystem.Instance.IsContainChar(idleValues[num]) == true)
						{
							idleAmount += float.Parse(idleValues[num].Split(' ')[0]) * float.Parse(Math.Pow(10, NumSystem.Instance.GetCoef(idleValues[num])).ToString());
						}

						currentValues[num] = $"{float.Parse(currentValues[num].Split(' ')[0]) + float.Parse(currentValues[num].Split(' ')[0]) / 10} {currentValues[num].Split(' ')[1]}";
						lvls[num] += 1;
					} else if (NumSystem.Instance.IsContainChar(idleText.text) == true)
					{
						score -= float.Parse(currentValues[num].Split(' ')[0]);

						if (NumSystem.Instance.IsContainChar(idleValues[num]) == true)
						{
							idleAmount += float.Parse(idleValues[num].Split(' ')[0]);
						}
						else if (NumSystem.Instance.IsContainChar(idleValues[num]) == false)
						{
							idleAmount += float.Parse(idleValues[num].Split(' ')[0]) / float.Parse(Math.Pow(10, NumSystem.Instance.GetCoef(idleText.text)).ToString());
						}

						currentValues[num] = $"{float.Parse(currentValues[num].Split(' ')[0]) + float.Parse(currentValues[num].Split(' ')[0]) / 10} {currentValues[num].Split(' ')[1]}";
						lvls[num] += 1;
					}
				}
			} else if (NumSystem.Instance.GetDiff(scoreText.text, currentValues[num]) > 0
				&& NumSystem.Instance.GetDiff(scoreText.text, currentValues[num]) != -1)
			{
				score -= float.Parse(currentValues[num].Split(' ')[0]) / float.Parse(Math.Pow(10, NumSystem.Instance.GetDegree(NumSystem.Instance.GetCoef(scoreText.text))).ToString());
				if (NumSystem.Instance.IsContainChar(idleText.text) == false)
				{
					if (NumSystem.Instance.IsContainChar(idleValues[num]) == true)
					{
						idleAmount += float.Parse(idleValues[num].Split(' ')[0]) * float.Parse(Math.Pow(10, NumSystem.Instance.GetDegree(NumSystem.Instance.GetCoef(idleValues[num]))).ToString());
					} else if (NumSystem.Instance.IsContainChar(idleValues[num]) == false)
					{
						idleAmount += float.Parse(idleValues[num].Split(' ')[0]);
					}
				} else if (NumSystem.Instance.IsContainChar(idleText.text) == true)
				{
					if (NumSystem.Instance.IsContainChar(idleValues[num]) == true)
					{
						if (NumSystem.Instance.GetDiff(idleText.text, idleValues[num]) == 0)
						{
							idleAmount += float.Parse(idleValues[num].Split(' ')[0]);
						} else if (NumSystem.Instance.GetDiff(idleText.text, idleValues[num]) > 0)
						{
							idleAmount += float.Parse(idleValues[num].Split(' ')[0]) / float.Parse(Math.Pow(10, NumSystem.Instance.GetDiff(idleText.text, idleValues[num])).ToString());
						} else if (NumSystem.Instance.GetDiff(idleText.text, idleValues[num]) == -1)
						{
							idleAmount += float.Parse(idleValues[num].Split(' ')[0]) * float.Parse(Math.Pow(10, (NumSystem.Instance.GetCoef(idleText.text) - NumSystem.Instance.GetCoef(idleValues[num])) * -1).ToString());
						}
					} else if (NumSystem.Instance.IsContainChar(idleValues[num]) == false)
					{
						idleAmount += float.Parse(idleValues[num].Split(' ')[0]) / float.Parse(Math.Pow(10, NumSystem.Instance.GetDegree(NumSystem.Instance.GetCoef(idleText.text))).ToString());
					}
				}
				currentValues[num] = $"{float.Parse(currentValues[num].Split(' ')[0]) + float.Parse(currentValues[num].Split(' ')[0]) / 10} {currentValues[num].Split(' ')[1]}";
				lvls[num] += 1;
			}
		}
	}

	void UpdateScore()
	{
		foreach (string k in valueModule.Keys)
		{
			if (score <= Math.Pow(10, 6) && coef == 3 && !scoreText.text.Contains(k))
			{
				scoreText.text = $"{String.Format("{0:0}", score.ToString())}";
			}

			if (score > 999999)
			{
				coef += coef;
				score /= float.Parse(Math.Pow(10, 6).ToString());

				for (int i = 0; i < valueModule.Count; i++)
				{
					if (valueModule[keys[i]] == coef)
					{
						scoreText.text = $"{String.Format("{0:0.0}", score.ToString())} {keys[i]}";
					}
				}
			}

			if (score >= 1 && score <= 1000 && scoreText.text.Contains(k))
			{
				for (int i = 0; i < valueModule.Count; i++)
				{
					if (valueModule[keys[i]] == coef)
					{
						scoreText.text = $"{String.Format("{0:0.0}", score.ToString())} {keys[i]}";
					}
				}  
			}

			if (score > 999 && scoreText.text.Contains(k))
			{
				score /= float.Parse(Math.Pow(10, 3).ToString());
				coef += 3;

				for (int i = 0; i < valueModule.Count; i++)
				{
					if (valueModule[keys[i]] == coef)
					{
						scoreText.text = $"{String.Format("{0:0.0}", score.ToString())} {keys[i]}";
					}
				}
			}

			if (coef >= 6 && !scoreText.text.Contains(k))
			{
				for (int i = 0; i < valueModule.Count; i++)
				{
					if (valueModule[keys[i]] == coef)
					{
						scoreText.text = $"{String.Format("{0:0.0}", score.ToString())} {keys[i]}";
					}
				}
			}

			if (coef == 6 && score < 1)
			{
				scoreText.text = $"{score * Math.Pow(10, 6)}";
				coef -= 3;
			}

		}
	}

	public Text GetScore()
	{
		return scoreText;
	}

	// Start is called before the first frame update
	void Start()
	{
		NumSystem.Instance.NumInit(out valueModule, out keys);

		UpgradeController.Instance.UpgradeInit(out currentValues, out idleValues, out lvls);
		UpgradeController.Instance.SetValueModule(valueModule);
		UpgradeController.Instance.SetKeys(keys);

		IdleController.Instance.SetValueModule(valueModule);
		IdleController.Instance.SetKeys(keys);
		IdleController.Instance.SetText(scoreText, idleText);
		IdleController.Instance.SetScoreCoef(coef);
	}

	// Update is called once per frame
	void Update()
	{
		UpgradeController.Instance.UpdateText(currentValues, lvls);

		IdleController.Instance.SetIdleAmount(idleAmount);
		IdleController.Instance.UpdateIdleText(out idleAmount);
		UpdateScore();


		if (idleAmount != 0)
		{
			elapsed += Time.deltaTime;
			if (elapsed >= 1f)
			{
				elapsed %= 1f;
				IdleController.Instance.SetScore(score);
				IdleController.Instance.Idle(out score, out coef);
			}
		}
	}
}