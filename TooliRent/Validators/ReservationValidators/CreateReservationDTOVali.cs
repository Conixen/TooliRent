//using FluentValidation;
//using TooliRent.DTO_s.ReservationDTOs;
//namespace TooliRent.Validators.ReservationValidators
//{
//    public class CreateReservationDTOVali : AbstractValidator<CreateReservationDTO>
//    {
//        public CreateReservationDTOVali()   // No update is in update reservation dto
//        { 
//            RuleFor(x => x.Date2Hire)
//                .NotEmpty().WithMessage("Start date is required")
//                .LessThan(x => x.Date2Return).WithMessage("Start date must be before end date");

//            RuleFor(x => x.Date2Return)
//                .NotEmpty().WithMessage("End date is required")
//                .GreaterThan(x => x.Date2Hire).WithMessage("End date must be after start date");

//            RuleFor(x => x.ToolIds)
//                .NotEmpty().WithMessage("At least one tool must be selected")
//                .Must(toolIds => toolIds != null && toolIds.Count > 0).WithMessage("At least one tool must be selected");
//        }
//    }
//}
