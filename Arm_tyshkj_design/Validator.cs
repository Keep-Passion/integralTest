using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Arm_tyshkj_design
{
    public class Validator
    {
        private Validator()
        {
        }
        public static bool checkRequired(string str)
        {
            if (str == null || str.Trim().Length == 0) return false;
            return true;
        }
        public static bool checkPositiveInteger(string str)
        {
            string pattern = @"^([1-9]\d*|0)$";
            return Regex.IsMatch(str, pattern);
        }
        public static bool checkInteger(string str)
        {
            string pattern = @"^-?[1-9]\d*$";
            return Regex.IsMatch(str, pattern);
        }
        public static bool checkDouble(string str)
        {
            string pattern = @"^(-?([1-9]\d*|0?)\.\d*|0)$";
            return Regex.IsMatch(str, pattern);
        }
        public static bool checkPhone(string phone)
        {
            //电信手机号正则
            string dianxin = @"^1[3578][01379]\d{8}$";
            Regex dReg = new Regex(dianxin);
            //联通手机号正则        
            string liantong = @"^1[34578][01256]\d{8}$";
            Regex tReg = new Regex(liantong);
            //移动手机号正则        
            string yidong = @"^(134[012345678]\d{7}|1[34578][012356789]\d{8})$";
            Regex yReg = new Regex(yidong);
            if (dReg.IsMatch(phone) || tReg.IsMatch(phone) || yReg.IsMatch(phone))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
