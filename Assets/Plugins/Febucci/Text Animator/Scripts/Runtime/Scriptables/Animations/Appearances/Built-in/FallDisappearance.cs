using Febucci.UI.Core;
using Febucci.UI.Effects;
using UnityEngine;

namespace Febucci.UI.Effects
{
    [UnityEngine.Scripting.Preserve]
    [CreateAssetMenu(fileName = "Fall Effect", menuName = "Text Animator/Animations/Appearances/Fall")]
    [EffectInfo("fall", EffectCategory.Appearances)]
    [DefaultValue(nameof(baseDuration), .7f)]
    public sealed class FallEffect : AppearanceScriptableBase
    {
        public float baseDistance = 60f;
        public float baseXJitter = 6f;
        public float baseRotate = 8f;

        float distance, xJitter, rotate;
        bool invert; // ключ

        public override void ResetContext(TAnimCore animator)
        {
            base.ResetContext(animator);
            distance = baseDistance;
            xJitter = baseXJitter;
            rotate = baseRotate;
            invert = false;
        }

        public override void ApplyEffectTo(ref CharacterData character, TAnimCore animator)
        {
            float t = Mathf.Clamp01(character.passedTime / duration);
            if (invert) t = 1f - t;

            float eased = Tween.EaseInOut(t);

            float y = Mathf.Lerp(0f, -distance, eased);

            float seed = (character.index + 1) * 0.137f;
            float x = Mathf.Sin(seed * 12.9898f) * xJitter * eased;

            character.current.positions.MoveChar(new Vector3(x, y, 0f));

            if (rotate != 0f)
                character.current.positions.RotateChar(Mathf.Lerp(0f, rotate, eased));

            byte a = (byte)Mathf.RoundToInt(Mathf.Lerp(255f, 0f, eased));
            for (int i = 0; i < character.current.colors.Length; i++)
            {
                var c = character.current.colors[i];
                c.a = a;
                character.current.colors[i] = c;
            }
        }

        public override void SetModifier(ModifierInfo modifier)
        {
            switch (modifier.name)
            {
                case "d": distance = baseDistance * modifier.value; break;
                case "x": xJitter = baseXJitter * modifier.value; break;
                case "r": rotate = baseRotate * modifier.value; break;
                case "inv": invert = modifier.value > 0.5f; break;
                default: base.SetModifier(modifier); break;
            }
        }
    }
}
