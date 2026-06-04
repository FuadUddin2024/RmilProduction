using System.ComponentModel.DataAnnotations;

namespace RMIL.Prod.Enums
{
    public class Enums
    {
        public enum EnumUserType
        {
            [Display(Name = "Super Admin")]
            SuperAdmin = 1,

            [Display(Name = "Admin")]
            Admin = 2,

            [Display(Name = "Report User")]
            ReportUser = 3,

            [Display(Name = "Approval User")]
            ApprovalUser = 4,

            [Display(Name = "SR User")]
            SrUser = 5,

            [Display(Name = "SR Number User")]
            SrNumberUser = 6,

            [Display(Name = "Gate Pass User")]
            GatePassUser = 7,

            [Display(Name = "Distribution User")]
            DistributionUser = 8,

            [Display(Name = "Service User")]
            ServiceUser = 9,

            [Display(Name = "Field User")]
            FieldUser = 10,

            [Display(Name = "Feedback User")]
            FeedbackUser = 11,

            [Display(Name = "PS 1st approver")]
            Ps1Stapprover = 12,

            [Display(Name = "PS Dist receive User")]
            PsDistReceiveUser = 13,

            [Display(Name = "Pump Selection User")]
            PumpSelectionUser = 14,

            [Display(Name = "QC User")]
            QcUser = 15,

            [Display(Name = "Production User")]
            ProductionUser = 16
            
        }
    }
}