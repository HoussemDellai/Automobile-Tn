using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Windows.Storage;
using AutomobileTn.Models;

namespace AutomobileTn.Repositories
{
	/// <summary>
	/// Repository that manages adding and removing cars from favorit list.
	/// </summary>
	public class FavoritCarsRepository
	{

		private readonly ApplicationDataContainer _localSettings;
		private const string FavoritCarsKey = "FavoritCars";

		public FavoritCarsRepository()
		{
			_localSettings = ApplicationData.Current.RoamingSettings;
			//_localSettings = ApplicationData.Current.LocalSettings;
		}

		/// <summary>
		/// Add car to the favorit list.
		/// </summary>
		/// <param name="car"></param>
		public void Add(Car car)
		{
			if (!_localSettings.Values.ContainsKey(FavoritCarsKey))
			{
				// first car to add to favorit
				_localSettings.Values[FavoritCarsKey] = ObjectSerializer<List<Car>>.ToXml(new List<Car> { car });

			}
			else // there's already existing cars in favorit
			{
				var favoritCarsString = _localSettings.Values[FavoritCarsKey] as string;

				var favoritCars = ObjectSerializer<List<Car>>.FromXml(favoritCarsString);

				if (favoritCars == null)
				{
					throw new Exception("Une erreur s'est parvenue !");
				}

				if (favoritCars.Contains(car))
				{
					throw new Exception("Ce modèle existe déjà !");
				}

				favoritCars.Add(car);
				_localSettings.Values[FavoritCarsKey] = ObjectSerializer<List<Car>>.ToXml(new List<Car>(favoritCars));
			}
		}

		//protected async override void SaveState(Dictionary<String, Object> pageState)
		//{
		//	try
		//	{
		//		string localData = ObjectSerializer<ObservableCollection<Person>>.ToXml(itemCollection);


		//		if (!string.IsNullOrEmpty(localData))
		//		{
		//			var localFile = await ApplicationData.Current.LocalFolder.CreateFileAsync("localData.txt", CreationCollisionOption.ReplaceExisting);
		//			await FileIO.WriteTextAsync(localFile, localData);
		//		}
		//	}
		//	catch (Exception ex)
		//	{

		//	}
		//}

		/// <summary>
		/// Remove a car from the favorit list.
		/// </summary>
		/// <param name="car"></param>
		public void Remove(Car car)
		{

			if (!_localSettings.Values.ContainsKey(FavoritCarsKey))
			{
				throw new Exception("Une erreur s'est parvenue !");
			}

			var favoritCarsString = _localSettings.Values[FavoritCarsKey] as string;

			var favoritCars = ObjectSerializer<List<Car>>.FromXml(favoritCarsString);

			if (favoritCars == null)
			{
				throw new Exception("Aucun modèle n'est trouvé !");
			}

			if (!favoritCars.Exists(c => c.ImageUrl == car.ImageUrl))
			{
				throw new Exception("Ce modèle n'existe pas déjà dans la liste des favoris !");
			}

			favoritCars.RemoveAll(c => c.ImageUrl == car.ImageUrl);

			favoritCarsString = ObjectSerializer<List<Car>>.ToXml(favoritCars);

			_localSettings.Values[FavoritCarsKey] = favoritCarsString;
		}

		public List<Car> GetAll()
		{
			try
			{
				if (!_localSettings.Values.ContainsKey(FavoritCarsKey))
				{
					throw new Exception("Aucun modèle n'est dans le favoris !");
				}

				var favoritCarsString = _localSettings.Values[FavoritCarsKey] as string;

				if (string.IsNullOrEmpty(favoritCarsString))
				{
					throw new Exception("Une erreur s'est parvenue dans la liste des favoris !");
				}

				var favoritCars = ObjectSerializer<List<Car>>.FromXml(favoritCarsString);

				return favoritCars;
			}
			catch (Exception)
			{
				throw new Exception("Une erreur s'est parvenue lors de la récupération de la liste des favoris !");
			}
		}
	}

	public static class ObjectSerializer<T>
	{
		// Serialize to xml 
		public static string ToXml(T value)
		{
			var serializer = new XmlSerializer(typeof(T));
			var stringBuilder = new StringBuilder();
			var settings = new XmlWriterSettings()
			{
				Indent = true,
				OmitXmlDeclaration = true,
			};

			using (var xmlWriter = XmlWriter.Create(stringBuilder, settings))
			{
				serializer.Serialize(xmlWriter, value);
			}
			return stringBuilder.ToString();
		}

		// Deserialize from xml 
		public static T FromXml(string xml)
		{
			var serializer = new XmlSerializer(typeof(T));
			T value;
			using (var stringReader = new StringReader(xml))
			{
				var deserialized = serializer.Deserialize(stringReader);
				value = (T) deserialized;
			}

			return value;
		}
	}
}
