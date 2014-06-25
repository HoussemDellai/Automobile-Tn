using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml.Data;

namespace AutomobileTn.Converters
{

	/// <summary>
	/// Source : http://compiledexperience.com/blog/posts/relative-date-time-converter
	/// </summary>
	public class RelativeDateTimeConverter : IValueConverter
	{
		private const int Minute = 60;
		private const int Hour = Minute * 60;
		private const int Day = Hour * 24;
		private const int Year = Day * 365;

		private readonly Dictionary<long, Func<TimeSpan, string>> _thresholds = new Dictionary<long, Func<TimeSpan, string>>
		{
			{2, t => "il y'a une seconde"},
			{Minute,  t => String.Format("il y'a {0} secondes", (int)t.TotalSeconds)},
			{Minute * 2,  t => "il y'a une minute"},
			{Hour,  t => String.Format("il y'a {0} minutes", (int)t.TotalMinutes)},
			{Hour * 2,  t => "il y'a une heure"},
			{Day,  t => String.Format("il y'a {0} heures", (int)t.TotalHours)},
			{Day * 2,  t => "hier"},
			{Day * 30,  t => String.Format("il y'a {0} jours", (int)t.TotalDays)},
			{Day * 60,  t => "le mois précedent"},
			{Year,  t => String.Format("il y'a {0} mois", (int)t.TotalDays / 30)},
			{Year * 2,  t => "l'année précedente"},
			{long.MaxValue,  t => string.Format("il y'a {0} années", (int)t.TotalDays / 365)}
		};

		public object Convert(object value, Type targetType, object parameter, string language)
		{
			var dateTime = (DateTime) value;
			var difference = DateTime.UtcNow - dateTime.ToUniversalTime();

			return _thresholds.First(t => difference.TotalSeconds < t.Key).Value(difference);
		}

		public object ConvertBack(object value,
			Type targetType,
			object parameter,
			string language)
		{
			throw new NotImplementedException();
		}
	}
}
