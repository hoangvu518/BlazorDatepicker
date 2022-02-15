

namespace BlazorDatepicker.Store.Datepicker
{
	public enum CalendarViewMode
    {
		Decade,
		Year,
		Month,
    }

	[FeatureState]
	public class DatepickerState
	{
		public CalendarViewMode CurrentCalendarViewMode { get; } 
		public DateTime CurrentSelectedValue { get; }

		public string FinalSelectedValue { get; }

		public int DecadeBegin { get; }
		public int DecadeEnd { get; }

		public List<DateTime> MonthList { get; }

		public List<DateTime> DayList { get; }
		public DatepickerState() {
			CurrentCalendarViewMode = CalendarViewMode.Month;
			CurrentSelectedValue = DateTime.Now;
            FinalSelectedValue = "";
            int year = CurrentSelectedValue.Year;

            DecadeBegin = (int)(Math.Floor(year / 10.0d) * 10);
            DecadeEnd = (int)(Math.Ceiling(year / 10.0d) * 10);

            MonthList = new List<DateTime>();
            for (int i = 1; i <= 12; i++)
            {
                MonthList.Add(new DateTime(CurrentSelectedValue.Year, i, 1));
            }

			//DayList = new List<DateTime>();
			//var firstDayOfMonth = new DateTime(CurrentSelectedValue.Year, CurrentSelectedValue.Month, 1);
			//var lastDateOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
			//var dayDiff = (lastDateOfMonth - firstDayOfMonth).Days + 1;
			//for (int i = 0; i < dayDiff; i++)
			//{
			//    DayList.Add(firstDayOfMonth.AddDays(i));
			//}
			DayList = new List<DateTime>();
			var firstDayOfMonth = new DateTime(CurrentSelectedValue.Year, CurrentSelectedValue.Month, 1);
			var lastDateOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
			var dayDiff = (lastDateOfMonth - firstDayOfMonth).Days + 1;
			var firstDayIndex = 0;
			var isFirstDayOfMonthEqualSunday = firstDayOfMonth.DayOfWeek == DayOfWeek.Sunday;
			while (!isFirstDayOfMonthEqualSunday)
			{
				firstDayOfMonth = firstDayOfMonth.AddDays(-1);
				firstDayIndex--;
				isFirstDayOfMonthEqualSunday = firstDayOfMonth.DayOfWeek == DayOfWeek.Sunday;
			}
			var lastDayIndex = dayDiff;
			for (int i = 0; i < dayDiff - firstDayIndex; i++)
			{
				DayList.Add(firstDayOfMonth.AddDays(i));
			}
		}
		public DatepickerState(CalendarViewMode currentCalendarViewMode, DateTime currentModeValue)
		{
			CurrentCalendarViewMode = currentCalendarViewMode;
			CurrentSelectedValue = currentModeValue;
			FinalSelectedValue = "";
			int year = CurrentSelectedValue.Year;

			DecadeBegin = (int)(Math.Floor(year / 10.0d) * 10);
			DecadeEnd = (int)(Math.Ceiling(year / 10.0d) * 10);

			MonthList = new List<DateTime>();
			for (int i = 1; i <= 12; i++)
			{
				MonthList.Add(new DateTime(CurrentSelectedValue.Year, i, 1));
			}


			//DayList = new List<DateTime>();
			//var firstDayOfMonth = new DateTime(CurrentSelectedValue.Year, CurrentSelectedValue.Month, 1);
			//var lastDateOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
			//var dayDiff = (lastDateOfMonth - firstDayOfMonth).Days + 1;
			//for (int i = 0; i < dayDiff; i++)
			//{
			//	DayList.Add(firstDayOfMonth.AddDays(i));
			//}
			DayList = new List<DateTime>();
			var firstDayOfMonth = new DateTime(CurrentSelectedValue.Year, CurrentSelectedValue.Month, 1);
			var lastDateOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
			var dayDiff = (lastDateOfMonth - firstDayOfMonth).Days + 1;
			var firstDayIndex = 0;
			var isFirstDayOfMonthEqualSunday = firstDayOfMonth.DayOfWeek == DayOfWeek.Sunday;
			while (!isFirstDayOfMonthEqualSunday)
			{
				firstDayOfMonth = firstDayOfMonth.AddDays(-1);
				firstDayIndex--;
				isFirstDayOfMonthEqualSunday = firstDayOfMonth.DayOfWeek == DayOfWeek.Sunday;
			}
			var lastDayIndex = dayDiff;
			for (int i = 0; i < dayDiff - firstDayIndex; i++)
			{
				DayList.Add(firstDayOfMonth.AddDays(i));
			}
		}

		public DatepickerState(CalendarViewMode currentCalendarViewMode, DateTime currentModeValue, string finalSelectedValue)
		{
			CurrentCalendarViewMode = currentCalendarViewMode;
			CurrentSelectedValue = currentModeValue;
			FinalSelectedValue = finalSelectedValue;
			int year = CurrentSelectedValue.Year;
			

			DecadeBegin = (int)(Math.Floor(year / 10.0d) * 10);
			DecadeEnd = (int)(Math.Ceiling(year / 10.0d) * 10);

			MonthList = new List<DateTime>();
			for (int i = 1; i <= 12; i++)
			{
				MonthList.Add(new DateTime(CurrentSelectedValue.Year, i, 1));
			}


			DayList = new List<DateTime>();
			var firstDayOfMonth = new DateTime(CurrentSelectedValue.Year, CurrentSelectedValue.Month, 1);
			var lastDateOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
			var dayDiff = (lastDateOfMonth - firstDayOfMonth).Days + 1;
			var firstDayIndex = 0;
			var isFirstDayOfMonthEqualSunday = firstDayOfMonth.DayOfWeek == DayOfWeek.Sunday;
			while (!isFirstDayOfMonthEqualSunday)
			{
				firstDayOfMonth = firstDayOfMonth.AddDays(-1);
				firstDayIndex--;
				isFirstDayOfMonthEqualSunday = firstDayOfMonth.DayOfWeek == DayOfWeek.Sunday;
			}
			var lastDayIndex = dayDiff;
			for (int i = 0; i < dayDiff - firstDayIndex; i++)
			{
				DayList.Add(firstDayOfMonth.AddDays(i));
			}
		}


	}

	public static class Reducers
	{
        [ReducerMethod]
        public static DatepickerState ReduceChangeCalendarViewModeAction(DatepickerState state, ChangeCalendarViewModeAction action)
        {
			switch (state.CurrentCalendarViewMode)
            {
				case CalendarViewMode.Decade:
					return new DatepickerState(state.CurrentCalendarViewMode, state.CurrentSelectedValue);
				case CalendarViewMode.Year:
					return new DatepickerState(CalendarViewMode.Decade, state.CurrentSelectedValue);
				case CalendarViewMode.Month:
					return new DatepickerState(CalendarViewMode.Year, state.CurrentSelectedValue);
				default:
					throw new Exception("Invalid CurrentCalendarViewMode.");

            }
        }

		[ReducerMethod]
		public static DatepickerState ReduceChangeCalendarModeValueAction(DatepickerState state, ChangeCalendarModeValueAction action)
		{
			return new DatepickerState(state.CurrentCalendarViewMode, action.ModeValue);
		}

		[ReducerMethod]
		public static DatepickerState ReduceChangeCalendarViewModeAndValueAction(DatepickerState state, ChangeCalendarViewModeAndValueAction action)
		{
			return new DatepickerState(action.NewMode, action.ModeValue);
		}

		[ReducerMethod]
		public static DatepickerState ReduceChangeCalendarFinalSelectedValueAction(DatepickerState state, ChangeCalendarFinalSelectedValueAction action)
		{
			return new DatepickerState(state.CurrentCalendarViewMode, state.CurrentSelectedValue, action.FinalSelectedValue);
		}

	}

	public class ChangeCalendarViewModeAction
	{

		public ChangeCalendarViewModeAction()
        {
        }
	}

	public class ChangeCalendarModeValueAction
    {
		public DateTime ModeValue { get; }
		public ChangeCalendarModeValueAction(DateTime modeValue)
        {
			ModeValue = modeValue; 
        }
	}

	public class ChangeCalendarViewModeAndValueAction
    {
		public CalendarViewMode NewMode { get; }
		public DateTime ModeValue { get; }
		public ChangeCalendarViewModeAndValueAction(CalendarViewMode newMode, DateTime modeValue)
		{
			NewMode = newMode;
			ModeValue = modeValue;
		}
	}

	public class ChangeCalendarFinalSelectedValueAction
    {
		public string FinalSelectedValue { get; }
		public ChangeCalendarFinalSelectedValueAction(string finalSelectedValue)
        {
			FinalSelectedValue = finalSelectedValue;
        }
	}
}
