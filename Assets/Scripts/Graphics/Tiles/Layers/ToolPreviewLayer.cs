using UnityEngine;
using System;

namespace TWF.Graphics
{

    class ToolPreviewLayer : ITileLayer
    {
        public string Name => "tool_preview";

        private readonly Func<IToolPreviewOutcomeMap> toolPreviewProvider;
        private readonly Func<Color?> toolSuccessColorProvider;
        private readonly Color errorColor;

        public ToolPreviewLayer(Func<IToolPreviewOutcomeMap> toolPreviewProvider, Func<Color?> toolSuccessColorProvider, Color errorColor)
        {
            this.toolPreviewProvider = toolPreviewProvider;
            this.toolSuccessColorProvider = toolSuccessColorProvider;
            this.errorColor = errorColor;
        }

        public Color? GetColor(Vector pos)
        {
            ToolOutcome? outcome = toolPreviewProvider().GetPreview(pos);
            Color? success = toolSuccessColorProvider();
            Color error = errorColor;
            error.a = success?.a ?? 1.0f;
            if (null != outcome)
            {
                if (outcome == ToolOutcome.SUCCESS)
                {
                    return toolSuccessColorProvider();
                }
                else
                {
                    return error;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
