using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorClasses
{
    public class InsuranceWaiverStatus
    {
        public string Pidm { get; set; }

        public string Term { get; set; }

        /// <summary>
        /// Maps to SWRGPCD_ATTR1
        /// </summary>
        public string HealthInsuranceBilled { get; set; }

        /// <summary>
        /// Maps to SWRGPCD_ATTR2
        /// </summary>
        public string InternationalStatus { get; set; }

        /// <summary>
        /// SWRGPCD_ATTR3
        /// </summary>
        public string RICredits { get; set; }

        /// <summary>
        /// SWRGPCD_ATTR4
        /// </summary>
        public string Campus { get; set; }

        /// <summary>
        /// SWRGPCD_ATTR5
        /// </summary>
        public string ColoradoSchoolofPublicHealth { get; set; }

        /// <summary>
        /// SWRGPCD_ATTR6
        /// </summary>
        public string BenefitEligibleEmployee { get; set; }

        /// <summary>
        /// SWRGPCD_ATTR7
        /// </summary>
        public string ThirdPartyWaiverStatus { get; set; }

        /// <summary>
        /// SWRGPCD_ATTR10
        /// </summary>
        public string OIPWaiverStatus { get; set; }

        /// <summary>
        /// SWRGPCD_ATTR14
        /// </summary>
        public string HNWaiverStatus { get; set; }

        // magic string land to match expected input from database
        public static string OnCampusCode { get { return "M"; } }
        public static string OffCampusCode { get { return "MC"; } }
        public static string ApprovedCode { get { return "AP"; } }
        public static string VoluntaryEnrollmentCode { get { return "VO"; } }
        public static string OptInCode { get { return "OI"; } }
        public static string PendingCode { get { return "PE"; } }
        public static string DeniedCode { get { return "DE"; } }
        public static string DomesticCode { get { return "DOMESTIC"; } }
        public static string InternationalCode { get { return "INTERNATIONAL"; } }
        public static string ContinuousRegistrationCode { get { return "CR"; } }
        public static string StudyAbroadCode { get { return "SA"; } }
        public static string ColoradoSchoolOfPublicHealthFullCode { get { return "CSPH-CSU-FULL"; } }
        public static string BenefitEligibleEmployeeYesCode { get { return "Y"; } }
        public static string ASHDCode { get { return "ASHD"; } }
        public static string ASHICode { get { return "ASHI"; } }

        public InsuranceWaiverStatus()
        {

        }

        public override string ToString()
        {
            return HealthInsuranceBilled + InternationalStatus + Campus + ColoradoSchoolofPublicHealth + BenefitEligibleEmployee + ThirdPartyWaiverStatus + OIPWaiverStatus + HNWaiverStatus;
        }

        public InsuranceWaiverStatus(object[] sqlResults)
        {
            Pidm = sqlResults[0] != null ? sqlResults[0].ToString() : string.Empty;
            Term = sqlResults[1] != null ? sqlResults[1].ToString() : string.Empty;
            HealthInsuranceBilled = sqlResults[2] != null ? sqlResults[2].ToString() : string.Empty;
            InternationalStatus = sqlResults[3] != null ? sqlResults[3].ToString() : string.Empty;
            RICredits = sqlResults[4] != null ? sqlResults[4].ToString() : string.Empty;
            Campus = sqlResults[5] != null ? sqlResults[5].ToString() : string.Empty;
            ColoradoSchoolofPublicHealth = sqlResults[6] != null ? sqlResults[6].ToString() : string.Empty;
            BenefitEligibleEmployee = sqlResults[7] != null ? sqlResults[7].ToString() : string.Empty;
            ThirdPartyWaiverStatus = sqlResults[8] != null ? sqlResults[8].ToString() : string.Empty;
            OIPWaiverStatus = sqlResults[9] != null ? sqlResults[9].ToString() : string.Empty;
            HNWaiverStatus = sqlResults[10] != null ? sqlResults[10].ToString() : string.Empty;
        }
    }
}
