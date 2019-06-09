namespace TWF.Graphics
{
    using System;
    using UnityEngine;

    internal class ToolPreviewLayer : ITileLayer
    {
        private readonly Func<IToolPreviewOutcomeMap> toolPreviewProvider;
        private readonly Func<Color?> toolSuccessColorProvider;
        private readonly Color errorColor;

        public ToolPreviewLayer(Func<IToolPreviewOutcomeMap> toolPreviewProvider, Func<Color?> toolSuccessColorProvider, Color errorColor)
        {
            this.toolPreviewProvider = toolPreviewProvider;
            this.toolSuccessColorProvider = toolSuccessColorProvider;
            this.errorColor = errorColor;
        }

        public string Name => "tool_preview";

        public Color? GetColor(Vector pos)
        {
            ToolOutcome? outcome = this.toolPreviewProvider().GetPreview(pos);
            Color? success = this.toolSuccessColorProvider();
            Color error = this.errorColor;
            error.a = success?.a ?? 1.0f;
            if (outcome != null)
            {
                if (outcome == ToolOutcome.Success)
                {
                    return this.toolSuccessColorProvider();
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
