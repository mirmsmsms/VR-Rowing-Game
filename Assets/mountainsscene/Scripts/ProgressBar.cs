// Decompiled with JetBrains decompiler
// Type: ProgressBar
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 64BA2C9F-3C9E-490A-96CF-A377973DDDC9
// Assembly location: C:\Users\ASUS\Desktop\My project (1)_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ProgressBar : MonoBehaviour
{
  public float max;
  public float current;
  public Image mask;

  private void Start()
  {
  }

  private void Update()
  {
    this.GetCurrentFill();
    this.current = Player.hp;
  }

  private void GetCurrentFill() => this.mask.fillAmount = this.current / this.max;
}
