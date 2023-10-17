using VitalCareWeb.Constants;

namespace VitalCareWeb.Helpers;

public static class ModalHelper
{
    public static string GetModalTypeCssClass(ModelConstants.ModalType dialogType)
    {
        switch (dialogType)
        {
            case ModelConstants.ModalType.Primary:
                return "bg-primary";
            case ModelConstants.ModalType.Danger:
                return "bg-danger";
            case ModelConstants.ModalType.Warning:
                return "bg-warning";
            default:
                return "";
        }
    }

    public static string GetModalSizeCssClass(ModelConstants.ModalSize dialogSize)
    {
        switch (dialogSize)
        {
            case ModelConstants.ModalSize.Small:
                return "modal-sm";
            case ModelConstants.ModalSize.Medium:
                return "modal-md";
            case ModelConstants.ModalSize.Large:
                return "modal-lg";
            case ModelConstants.ModalSize.ExtraLarge:
                return "modal-xl";
            default:
                return "";
        }
    }
}
