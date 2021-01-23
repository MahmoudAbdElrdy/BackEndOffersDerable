using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Helpers.Enums
{
    public enum UserType
    {
        Admin = 1,
        ServiceIntroduction = 2,
        ServiceRecipient = 3 
    }
    public enum ServiceCondition
    {
        Free=1,
        Paid=2    
    }
 
    public enum DiscountType 
    {
        Value = 1,
        Rate= 2
    }
    public enum AccountType
    {
        Personal=1,
        Commercial=2 
    }
}
