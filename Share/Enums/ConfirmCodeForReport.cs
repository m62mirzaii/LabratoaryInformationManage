using System.ComponentModel.DataAnnotations;

namespace Share.Enums
{
    public enum ConfirmCodeForReport
    {
        [Display(Name = "تمام ارزیابی ها")]
        FisrtLevel = 0,

        [Display(Name = " ارزیابی تایید شده")]
        SendToKartabl = 1,

        [Display(Name = "ارزیابی تایید نشده")]
        ReturnToUser = 2, 
    }
}
