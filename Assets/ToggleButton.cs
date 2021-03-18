using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ToggleButton : Button 
{
	public Sprite onSprite;
	public Sprite offSprite;
	public bool isOn;

    protected override void OnEnable()
    {
        base.OnEnable();
		(targetGraphic as Image).sprite = isOn ? onSprite : offSprite;
    }

	public override void OnPointerClick(PointerEventData eventData)
	{
		base.OnPointerClick(eventData);
		isOn = !isOn;
		(targetGraphic as Image).sprite = isOn ? onSprite : offSprite;
	}

	public override void OnSubmit(BaseEventData eventData)
	{
		base.OnSubmit(eventData);
		isOn = !isOn;
		(targetGraphic as Image).sprite = isOn ? onSprite : offSprite;
	}
}
