using System.ComponentModel.DataAnnotations;

namespace Share.Enums
{
    public enum ConfirmCode 
    {
        [Display(Name = "ثبت اولیه")]
        FisrtLevel = 0,

        [Display(Name = " کارتابل")]
        SendToKartabl = 1,

        [Display(Name = "عودت")]
        ReturnToUser = 2,

        [Display(Name = "تایید نهایی")]
        Confirm = 3,
    }
}
