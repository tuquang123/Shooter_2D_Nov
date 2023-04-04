using Assets.HeroEditor.Common.CharacterScripts;
using Assets.HeroEditor.Common.ExampleScripts;
using HeroEditor.Common.ExampleScripts;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.HeroEditor.Common.EditorScripts
{
    /// <summary>
    /// A helper used in character editor scenes.
    /// </summary>
    public class CharacterReferencesResolver : MonoBehaviour
    {
        public CharacterEditor CharacterEditor;
        public AnimationManager AnimationManager;
        public AttackingExample AttackingExample;
        public BowExample BowExample;

        public Slider WidthSlider;
        public Slider HeightSlider;
        public Button WidthReset;
        public Button HeightReset;

        public void OnValidate()
        {
           
        }

        public void Awake()
        {
           
        }
    }
}