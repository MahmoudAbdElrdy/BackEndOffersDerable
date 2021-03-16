using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Service.Helper
{
    public class SwitchData
    {
        public static string getEquationName(int num)
        {
            try
            {
                string eq = "";
                switch(num)
                {
                    case 0:
                        {
                            eq = "( ٢ * العدد * الطول * الإرتفاع)+(٢* العدد * العرض * الإرتفاع )";
                            break;
                        }
                    case 1:
                        {
                            eq = "العدد * العرض * الإرتفاع";
                            break;
                        }
                    case 2:
                        {
                            eq = "العدد * الطول * العرض";
                            break;
                        }
                    case 3:
                        {
                            eq = "العدد";
                            break;
                        }
                    case 4:
                        {
                            eq = "العدد * الطول";
                            break;
                        }
                    case 5:
                        {
                            eq = "العدد * العرض";
                            break;
                        }
                    case 6:
                        {
                            eq = "العدد * الإرتفاع";
                            break;
                        }
                }
                return eq;
            }
            catch (Exception ex)
            {
                var error = "Error " + string.Format("{0} - {1} ", ex.Message, ex.InnerException != null ? ex.InnerException.FullMessage() : "");
                return "";
            }
        }

        public static decimal getEquationResult(int num, int CountRoom, decimal LengthRoom, decimal HeightRoom, decimal WidthRoom,decimal MinusArea)
        {
            try
            {
                decimal eq = 0.0m;
                switch (num)
                {
                    case 0:
                        {
                            //eq = "( ٢ * العدد * الطول * الإرتفاع)+(٢* العدد * العرض * الإرتفاع )";
                            eq = (2 * CountRoom * HeightRoom * WidthRoom) + (2 * CountRoom * HeightRoom * LengthRoom);
                            break;
                        }
                    case 1:
                        {
                            //eq = "العدد * العرض * الإرتفاع";
                            eq = (CountRoom * HeightRoom * WidthRoom);
                            break;
                        }
                    case 2:
                        {
                            //eq = "العدد * الطول * العرض";
                            eq = (CountRoom * LengthRoom * WidthRoom);
                            break;
                        }
                    case 3:
                        {
                            //eq = "العدد";
                            eq = (CountRoom);
                            break;
                        }
                    case 4:
                        {
                            //eq = "العدد * الطول";
                            eq = (CountRoom * LengthRoom);
                            break;
                        }
                    case 5:
                        {
                            //eq = "العدد * العرض";
                            eq = (CountRoom * WidthRoom);
                            break;
                        }
                    case 6:
                        {
                            //eq = "العدد * الإرتفاع";
                            eq = (CountRoom * HeightRoom);
                            break;
                        }
                }
                return eq - MinusArea;
            }
            catch (Exception ex)
            {
                var error = "Error " + string.Format("{0} - {1} ", ex.Message, ex.InnerException != null ? ex.InnerException.FullMessage() : "");
                return 0.0m;
            }
        }

        public static string getLicenseStatus(int Status)
        {
            try
            {
                string eq = "";
                switch(Status)
                {
                    case 0:
                        {
                            eq = "التصميم المعماري";
                            break;
                        }
                    case 1:
                        {
                            eq = "التصميم الانشائي";
                            break;
                        }
                    case 2:
                        {
                            eq = "مراجعة الاستشاري";
                            break;
                        }
                    case 3:
                        {
                            eq = "المجمعة العشرية";
                            break;
                        }
                    case 4:
                        {
                            eq = "صلاحية الموقع";
                            break;
                        }
                    case 5:
                        {
                            eq = "مراجعة الملف داخل الجهاز";
                            break;
                        }
                    case 6:
                        {
                            eq = "اصدار الرخصة";
                            break;
                        }
                    case 7:
                        {
                            eq = "استلام الرخصة";
                            break;
                        }
                }
                return eq;
            }
            catch (Exception ex)
            {
                var error = "Error " + string.Format("{0} - {1} ", ex.Message, ex.InnerException != null ? ex.InnerException.FullMessage() : "");
                return "";
            }
        }
    }
}
