using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VisionSnap.FaceTracking
{
    public static class BlendshapeMapping
    {
        public static readonly IReadOnlyList<string> BlendshapesMapping = new[]
        {
            Constants.BlendshapeName._NEUTRAL,
            Constants.BlendshapeName.BROW_DOWN_LEFT,
            Constants.BlendshapeName.BROW_DOWN_RIGHT,
            Constants.BlendshapeName.BROW_INNER_UP,
            Constants.BlendshapeName.BROW_OUTER_UP_LEFT,

            Constants.BlendshapeName.BROW_OUTER_UP_RIGHT,
            Constants.BlendshapeName.CHEEK_PUFF,
            Constants.BlendshapeName.CHEEK_SQUINT_LEFT,
            Constants.BlendshapeName.CHEEK_SQUINT_RIGHT,

            Constants.BlendshapeName.EYE_BLINK_LEFT,
            Constants.BlendshapeName.EYE_BLINK_RIGHT,
            Constants.BlendshapeName.EYE_LOOK_DOWN_LEFT,
            Constants.BlendshapeName.EYE_LOOK_DOWN_RIGHT,

            Constants.BlendshapeName.EYE_LOOK_IN_LEFT,
            Constants.BlendshapeName.EYE_LOOK_IN_RIGHT,
            Constants.BlendshapeName.EYE_LOOK_OUT_LEFT,
            Constants.BlendshapeName.EYE_LOOK_OUT_RIGHT,

            Constants.BlendshapeName.EYE_LOOK_UP_LEFT,
            Constants.BlendshapeName.EYE_LOOK_UP_RIGHT,
            Constants.BlendshapeName.EYE_SQUINT_LEFT,
            Constants.BlendshapeName.EYE_SQUINT_RIGHT,
            Constants.BlendshapeName.EYE_WIDE_LEFT,
            Constants.BlendshapeName.EYE_WIDE_RIGHT,

            Constants.BlendshapeName.JAW_FORWARD,
            Constants.BlendshapeName.JAW_LEFT,
            Constants.BlendshapeName.JAW_OPEN,
            Constants.BlendshapeName.JAW_RIGHT,

            Constants.BlendshapeName.MOUTH_CLOSE,
            Constants.BlendshapeName.MOUTH_DIMPLE_LEFT,
            Constants.BlendshapeName.MOUTH_DIMPLE_RIGHT,
            Constants.BlendshapeName.MOUTH_FROWN_LEFT,

            Constants.BlendshapeName.MOUTH_FROWN_RIGHT,
            Constants.BlendshapeName.MOUTH_FUNNEL,
            Constants.BlendshapeName.MOUTH_LEFT,
            Constants.BlendshapeName.MOUTH_LOWER_DOWN_LEFT,

            Constants.BlendshapeName.MOUTH_LOWER_DOWN_RIGHT,
            Constants.BlendshapeName.MOUTH_PRESS_LEFT,
            Constants.BlendshapeName.MOUTH_PRESS_RIGHT,
            Constants.BlendshapeName.MOUTH_PUCKER,

            Constants.BlendshapeName.MOUTH_RIGHT,
            Constants.BlendshapeName.MOUTH_ROLL_LOWER,
            Constants.BlendshapeName.MOUTH_ROLL_UPPER,
            Constants.BlendshapeName.MOUTH_SHRUG_LOWER,

            Constants.BlendshapeName.MOUTH_SHRUG_UPPER,
            Constants.BlendshapeName.MOUTH_SMILE_LEFT,
            Constants.BlendshapeName.MOUTH_SMILE_RIGHT,
            Constants.BlendshapeName.MOUTH_STRETCH_LEFT,

            Constants.BlendshapeName.MOUTH_STRETCH_RIGHT,
            Constants.BlendshapeName.MOUTH_UPPER_UP_LEFT,
            Constants.BlendshapeName.MOUTH_UPPER_UP_RIGHT,
            Constants.BlendshapeName.NOSE_SNEER_LEFT,

            Constants.BlendshapeName.NOSE_SNEER_RIGHT,
            Constants.BlendshapeName.TONGUE_OUT,
        };

        public static readonly IReadOnlyList<string> BlendshapesMappingMirrored = new[]
        {
            Constants.BlendshapeName._NEUTRAL,

            Constants.BlendshapeName.BROW_DOWN_RIGHT,
            Constants.BlendshapeName.BROW_DOWN_LEFT,
            Constants.BlendshapeName.BROW_INNER_UP,
            Constants.BlendshapeName.BROW_OUTER_UP_RIGHT,

            Constants.BlendshapeName.BROW_OUTER_UP_LEFT,
            Constants.BlendshapeName.CHEEK_PUFF,
            Constants.BlendshapeName.CHEEK_SQUINT_RIGHT,
            Constants.BlendshapeName.CHEEK_SQUINT_LEFT,

            Constants.BlendshapeName.EYE_BLINK_RIGHT,
            Constants.BlendshapeName.EYE_BLINK_LEFT,
            Constants.BlendshapeName.EYE_LOOK_DOWN_RIGHT,
            Constants.BlendshapeName.EYE_LOOK_DOWN_LEFT,

            Constants.BlendshapeName.EYE_LOOK_IN_RIGHT,
            Constants.BlendshapeName.EYE_LOOK_IN_LEFT,
            Constants.BlendshapeName.EYE_LOOK_OUT_RIGHT,
            Constants.BlendshapeName.EYE_LOOK_OUT_LEFT,

            Constants.BlendshapeName.EYE_LOOK_UP_RIGHT,
            Constants.BlendshapeName.EYE_LOOK_UP_LEFT,
            Constants.BlendshapeName.EYE_SQUINT_RIGHT,
            Constants.BlendshapeName.EYE_SQUINT_LEFT,
            Constants.BlendshapeName.EYE_WIDE_RIGHT,
            Constants.BlendshapeName.EYE_WIDE_LEFT,

            Constants.BlendshapeName.JAW_FORWARD,
            Constants.BlendshapeName.JAW_RIGHT,
            Constants.BlendshapeName.JAW_OPEN,
            Constants.BlendshapeName.JAW_LEFT,

            Constants.BlendshapeName.MOUTH_CLOSE,
            Constants.BlendshapeName.MOUTH_DIMPLE_RIGHT,
            Constants.BlendshapeName.MOUTH_DIMPLE_LEFT,
            Constants.BlendshapeName.MOUTH_FROWN_RIGHT,

            Constants.BlendshapeName.MOUTH_FROWN_LEFT,
            Constants.BlendshapeName.MOUTH_FUNNEL,
            Constants.BlendshapeName.MOUTH_RIGHT,
            Constants.BlendshapeName.MOUTH_LOWER_DOWN_RIGHT,

            Constants.BlendshapeName.MOUTH_LOWER_DOWN_LEFT,
            Constants.BlendshapeName.MOUTH_PRESS_RIGHT,
            Constants.BlendshapeName.MOUTH_PRESS_LEFT,
            Constants.BlendshapeName.MOUTH_PUCKER,

            Constants.BlendshapeName.MOUTH_LEFT,
            Constants.BlendshapeName.MOUTH_ROLL_LOWER,
            Constants.BlendshapeName.MOUTH_ROLL_UPPER,
            Constants.BlendshapeName.MOUTH_SHRUG_LOWER,

            Constants.BlendshapeName.MOUTH_SHRUG_UPPER,
            Constants.BlendshapeName.MOUTH_SMILE_RIGHT,
            Constants.BlendshapeName.MOUTH_SMILE_LEFT,
            Constants.BlendshapeName.MOUTH_STRETCH_RIGHT,

            Constants.BlendshapeName.MOUTH_STRETCH_LEFT,
            Constants.BlendshapeName.MOUTH_UPPER_UP_RIGHT,
            Constants.BlendshapeName.MOUTH_UPPER_UP_LEFT,
            Constants.BlendshapeName.NOSE_SNEER_RIGHT,

            Constants.BlendshapeName.NOSE_SNEER_LEFT,
            Constants.BlendshapeName.TONGUE_OUT,
        };

        public static IList<int> GetMapping(Mesh mesh, IEnumerable<string> blendshapeNames,
            string blendshapeNameFormat = "{0}")
        {
            return blendshapeNames.Select(name =>
                {
                    var blendShapeName = string.Format(blendshapeNameFormat, name);
                    var index = mesh.GetBlendShapeIndex(blendShapeName);

                    if (index < 0)
                    {
                        Debug.LogError($"Could not find blendshape named {blendShapeName} in mesh {mesh.name}");
                    }
                    
                    return index;
                })
                .ToList();
        }
    }
}