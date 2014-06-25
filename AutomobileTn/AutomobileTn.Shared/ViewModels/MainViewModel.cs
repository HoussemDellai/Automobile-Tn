using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using AutomobileTn.Lib;
using AutomobileTn.Models;
using AutomobileTn.Repositories;
using AutomobileTn.Services;
using AutomobileTn.Utils;
using Windows.ApplicationModel;

namespace AutomobileTn.ViewModels
{
	public class MainViewModel : BaseViewModel
	{

		#region private attributes

		private List<Car> _allCarsCollection;
		private List<Car> _filteredCarsCollection;
		private List<Video> _videosCollection;
		private List<Tweet> _tweetsCollection;
		private IEnumerable<IGrouping<string, Car>> _groupedCars;
		private List<GroupCars> _groupCarsCollection;
		private Car _selectedCar;
		private ICommand _selectSelectedCarCommand;
		private readonly FavoritCarsRepository _favoritCarsRepository;
		private ICommand _addCarToFavoritListCommand;
		private List<Car> _favoritCarsCollection;
		private ICommand _removeCarFromFavoritListCommand;
		private ICommand _pinCarManifacturerCommand;
		private SecondaryTilesService _secondaryTilesService;
		private List<Car> _searchedCarsCollection;
		private ICommand _searchCommand;
		private double _searchedPrice = 15000;
		private string _searchedKeyword = string.Empty;
		private ICommand _searchCarsByPriceCommand;
		private ICommand _searchCarsByKeywordCommand;
		private bool _isBusy = false;

		#endregion

		#region public properties

		public List<Car> AllCarsCollection
		{
			get
			{ return _allCarsCollection; }
			set
			{
				_allCarsCollection = value;
				OnPropertyChanged();
			}
		}

		public List<Car> FavoritCarsCollection
		{
			get
			{ return _favoritCarsCollection; }
			set
			{
				_favoritCarsCollection = value;
				OnPropertyChanged();
			}
		}

		public List<GroupCars> GroupCarsCollection
		{
			get
			{ return _groupCarsCollection; }
			set
			{
				_groupCarsCollection = value;
				OnPropertyChanged();
			}
		}

		//public IEnumerable<IGrouping<string, Car>> GroupedCars
		//{
		//	get
		//	{ return _groupedCars; }
		//	set
		//	{
		//		_groupedCars = value;
		//		OnPropertyChanged();
		//	}
		//}

		public List<Car> FilteredCarsCollection
		{
			get
			{ return _filteredCarsCollection; }
			set
			{
				_filteredCarsCollection = value;
				OnPropertyChanged();
			}
		}

		public List<Video> VideosCollection
		{
			get
			{ return _videosCollection; }
			set
			{
				_videosCollection = value;
				OnPropertyChanged();
			}
		}

		public List<Tweet> TweetsCollection
		{
			get
			{ return _tweetsCollection; }
			set
			{
				_tweetsCollection = value;
				OnPropertyChanged();
			}
		}

		//public bool IsAddCarToFavoritVisible
		//{
		//	get
		//	{ return _isAddCarToFavoritVisible; }
		//	set
		//	{
		//		_isAddCarToFavoritVisible = value;
		//		OnPropertyChanged();
		//	}
		//}

		/// <summary>
		/// Car to be selected to be added or removed from the favorit list
		/// </summary>
		/// <returns></returns>
		public Car SelectedCar
		{
			get
			{
				return _selectedCar;
			}
			set
			{
				_selectedCar = value;
				OnPropertyChanged();
			}
		}

		public static Video SelectedVideo
		{ get; set; }

		public List<Car> SearchedCarsCollection
		{
			get
			{ return _searchedCarsCollection; }
			set
			{
				_searchedCarsCollection = value;
				OnPropertyChanged();
			}
		}

		public double SearchedPrice
		{
			get
			{
				return _searchedPrice;
			}
			set
			{
				_searchedPrice = value;
				OnPropertyChanged();
			}
		}

		public string SearchedKeyword
		{
			get
			{
				return _searchedKeyword;
			}
			set
			{
				_searchedKeyword = value;
				OnPropertyChanged();
			}
		}

		public bool IsBusy
		{
			get
			{ return _isBusy; }
			set
			{
				_isBusy = value;
				OnPropertyChanged();
			}
		}

		#endregion

		#region Commands

		public RelayCommandGeneric<Video> SelectVideoCommand
		{
			get
			{
				return new RelayCommandGeneric<Video>(video =>
				{
					//Frame.Navigate(typeof(VideoPage));
					SelectedVideo = video;
				});
			}
		}

		public RelayCommandGeneric<string> FilterCarsCollectionCommand
		{
			get
			{
				return new RelayCommandGeneric<string>(filter =>
				{
					FilteredCarsCollection = AllCarsCollection.Where(car => car.Manifacturer == filter).ToList();
				});
			}
		}

		public RelayCommand RefreshCommand
		{
			get
			{
				return new RelayCommand(async () => await InitializeAsync());
			}
		}

		public ICommand SelectSelectedCarCommand
		{
			get
			{
				if (_selectSelectedCarCommand == null)
				{
					_selectSelectedCarCommand = new RelayCommandGeneric<Car>(car =>
					{
						if (car == null)
							return;

						SelectedCar = car;
					});
				}
				return _selectSelectedCarCommand;
			}
		}

		public ICommand AddCarToFavoritListCommand
		{
			get
			{
				if (_addCarToFavoritListCommand == null)
				{
					_addCarToFavoritListCommand = new RelayCommand(() =>
					{
						if (SelectedCar == null)
							return;

						try
						{
							_favoritCarsRepository.Add(SelectedCar);

							RefreshFavoritCars();
						}
						catch (Exception exc)
						{
							Debug.WriteLine(exc.Message);
						}
					});
				}
				return _addCarToFavoritListCommand;
			}
		}

		public ICommand RemoveCarFromFavoritListCommand
		{
			get
			{
				if (_removeCarFromFavoritListCommand == null)
				{
					_removeCarFromFavoritListCommand = new RelayCommand(() =>
					{
						if (SelectedCar == null)
							return;

						try
						{
							_favoritCarsRepository.Remove(SelectedCar);

							RefreshFavoritCars();
						}
						catch (Exception exc)
						{
							Debug.WriteLine(exc.Message);
						}
					});
				}
				return _removeCarFromFavoritListCommand;
			}
		}

		public ICommand SearchCarsByPriceCommand
		{
			get
			{
				if (_searchCarsByPriceCommand == null)
				{
					_searchCarsByPriceCommand = new RelayCommand(() =>
					{

						if (_allCarsCollection == null)
						{
							return;
						}

						var priceMarge = 10 * _searchedPrice / 100;

						SearchedCarsCollection =
							_allCarsCollection.FindAll(car => car.Price >= _searchedPrice - priceMarge
															   && car.Price <= _searchedPrice + priceMarge);
					});
				}
				return _searchCarsByPriceCommand;
			}
		}

		public ICommand SearchCarsByKeywordCommand
		{
			get
			{
				if (_searchCarsByKeywordCommand == null)
				{
					_searchCarsByKeywordCommand = new RelayCommand(() =>
					{
						if (_allCarsCollection == null)
						{
							return;
						}

						_searchedKeyword = _searchedKeyword.ToLower();

						SearchedCarsCollection =
							_allCarsCollection.FindAll(car => car.Manifacturer.ToLower().Contains(_searchedKeyword)
															  || car.Model.ToLower().Contains(_searchedKeyword));

					});
				}
				return _searchCarsByKeywordCommand;
			}
		}

		public ICommand SearchCommand
		{
			get
			{
				if (_searchCommand == null)
				{
					_searchCommand = new RelayCommand(() =>
					{
						if (_allCarsCollection == null)
						{
							return;
						}

						_searchedKeyword = _searchedKeyword.ToLower();

						var priceMarge = 10 * _searchedPrice / 100;

						SearchedCarsCollection =
							_allCarsCollection.FindAll(car => (car.Manifacturer.ToLower().Contains(_searchedKeyword)
															   || car.Model.ToLower().Contains(_searchedKeyword))
															  &&
															  (car.Price >= _searchedPrice - priceMarge
															   && car.Price <= _searchedPrice + priceMarge));
					});
				}
				return _searchCommand;
			}
		}

		public ICommand PinCarManifacturerCommand
		{
			get
			{
				if (_pinCarManifacturerCommand == null)
				{
					_pinCarManifacturerCommand = new RelayCommandGeneric<GroupCars>(groupCars =>
					{
						_secondaryTilesService.PinTile(groupCars.Manifacturer);
					});
				}
				return _pinCarManifacturerCommand;
			}
		}

		#endregion

		// Ctrl Shift Alt O
		// Ctrl Shift V

		public MainViewModel()
		{
			_favoritCarsRepository = new FavoritCarsRepository();
			_secondaryTilesService = new SecondaryTilesService();

			InitializeAsync();
		}

		private async Task InitializeAsync()
		{
			IsBusy = true;

			try
			{

				var dataService = new DataService();

				var carsCollection = await dataService.GetCarsCollectionAsync();

				AllCarsCollection = CarsFormatter.GetFormattedCarsCollection(carsCollection);

				RefreshFavoritCars();

				var videosService = new VideosService();

				VideosCollection = await videosService.GetYoutubeVideosAsync();

				var tweetsService = new TweetsService();

				TweetsCollection = await tweetsService.GetTweetsAsync();

				FilterCarsCollectionCommand.Execute("KIA");

				//IEnumerable<IGrouping<string, Car>> query = from car
				//											in _allCarsCollection
				//											group car
				//											by car.Manifacturer;

				//GroupedCars = query;

				var groupCarsCollection = new List<GroupCars>();

				foreach (var carsManifacturer in Constants.CarsManifacturers)
				{
					groupCarsCollection.Add(
						new GroupCars
					{
						Manifacturer = carsManifacturer,
						CarsCollection = AllCarsCollection.Where(car => car.Manifacturer == carsManifacturer).ToList(),
					});
				}

				GroupCarsCollection = groupCarsCollection;

#if WINDOWS_Phone || WINDOWS_APP
				if (DesignMode.DesignModeEnabled)
				{
					SelectedVideo = VideosCollection[0];

					FavoritCarsCollection = _allCarsCollection.GetRange(10, 18);
				}
#endif
			}
			catch (Exception exc)
			{
				Debug.WriteLine(exc.Message);
			}
			finally
			{
				IsBusy = false;
			}
		}

		private void RefreshFavoritCars()
		{
			try
			{
				FavoritCarsCollection = _favoritCarsRepository.GetAll();
			}
			catch (Exception exc)
			{
				//exc.Message
			}
		}
	}
}
