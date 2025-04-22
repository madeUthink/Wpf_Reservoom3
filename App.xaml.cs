using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Wpf_Reservoom3.DbContexts;
using Wpf_Reservoom3.Exceptions;
using Wpf_Reservoom3.HostBuilders;
using Wpf_Reservoom3.Models;
using Wpf_Reservoom3.Services;
using Wpf_Reservoom3.Services.ReservationConflictValidators;
using Wpf_Reservoom3.Services.ReservationCreators;
using Wpf_Reservoom3.Services.ReservationProviders;
using Wpf_Reservoom3.Stores;
using Wpf_Reservoom3.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;


namespace Wpf_Reservoom3
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //private const string CONNECTION_STRING = "Data Source=reservoom.db";
        //private readonly Hotel _hotel;
        //private readonly NavigationStore _navigationStore;
        //private readonly HotelStore _hotelStore;
        //private ReservoomDbContextFactory _reservoomDbContextFactory;
        private IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .AddViewModels()
                //.ConfigureServices(services =>
                .ConfigureServices((hostContext, services) =>
                {
                    string connectionString = hostContext.Configuration.GetConnectionString("Default");

                    services.AddSingleton(new ReservoomDbContextFactory(connectionString));
                    services.AddSingleton<IReservationProvider, DatabaseReservationProvider>();
                    services.AddSingleton<IReservationCreator, DatabaseReservationCreator>();
                    services.AddSingleton<IReservationConflictValidator, DatabaseReservationConflictValidator>();

                    services.AddTransient<ReservationBook>();

                    string hotelName = hostContext.Configuration.GetValue<string>("HotelName");
                    services.AddSingleton((s) => new Hotel(hotelName, s.GetRequiredService<ReservationBook>()));

                    services.AddSingleton<HotelStore>();
                    services.AddSingleton<NavigationStore>();

                    services.AddSingleton(s => new MainWindow()
                    {
                        DataContext = s.GetRequiredService<MainViewModel>()
                    });
            
                    //string connectionString = hostContext.Configuration.GetConnectionString("Default");
                    ////services.AddSingleton(new ReservoomDbContextFactory(CONNECTION_STRING));
                    //services.AddSingleton(new ReservoomDbContextFactory(connectionString));
                    //services.AddSingleton<IReservationProvider, DatabaseReservationProvider>();
                    //services.AddSingleton<IReservationCreator, DatabaseReservationCreator>();
                    //services.AddSingleton<IReservationConflictValidator, DatabaseReservationConflictValidator>();

                    //services.AddTransient<ReservationBook>();
                    //services.AddSingleton((s) => new Hotel("Figen's Suites", s.GetRequiredService<ReservationBook>()));

                    //services.AddTransient<ReservationListingViewModel>((s) => CreateReservationListingViewModel(s));
                    //services.AddSingleton<Func<ReservationListingViewModel>>((s) => () => s.GetRequiredService<ReservationListingViewModel>());
                    //services.AddSingleton<NavigationService<ReservationListingViewModel>>();

                    //services.AddTransient<MakeReservationViewModel>();
                    //services.AddSingleton<Func<MakeReservationViewModel>>((s) => () => s.GetRequiredService<MakeReservationViewModel>());
                    //services.AddSingleton<NavigationService<MakeReservationViewModel>>();

                    //services.AddSingleton<HotelStore>();
                    //services.AddSingleton<NavigationStore>();

                    //services.AddSingleton<MainViewModel>();
                    //services.AddSingleton((s) => new MainWindow()
                    //{
                    //    DataContext = s.GetRequiredService<MainViewModel>()
                    //});
                })
                .Build();
            //_reservoomDbContextFactory = new ReservoomDbContextFactory(CONNECTION_STRING);
            //IReservationProvider reservationProvider = new DatabaseReservationProvider(_reservoomDbContextFactory);
            //IReservationCreator reservationCreator = new DatabaseReservationCreator(_reservoomDbContextFactory);
            //IReservationConflictValidator reservationConflictValidator = new DatabaseReservationConflictValidator(_reservoomDbContextFactory);

            //_hotel = new Hotel("Figen's Suites");
            
            //ReservationBook reservationBook= new ReservationBook(reservationProvider, reservationCreator,reservationConflictValidator);
            //_hotel = new Hotel("Figen's Suites", reservationBook);
            //_hotelStore = new HotelStore(_hotel);
            //_navigationStore = new NavigationStore();
        }

        private ReservationListingViewModel CreateReservationListingViewModel(IServiceProvider s)
        {
            return ReservationListingViewModel.LoadViewModel(
                s.GetRequiredService<HotelStore>(),
                s.GetRequiredService<NavigationService<MakeReservationViewModel>>()
                );
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();
            //Hotel hotel = new Hotel("Figen's Suites");
            //try
            //{
            //    hotel.MakeReservation(new Reservation(
            //       new RoomID(1, 3),
            //       "Figen",
            //       new DateTime(2025, 1, 1),
            //       new DateTime(2025, 1, 2)
            //       ));

            //    hotel.MakeReservation(new Reservation(
            //        new RoomID(1, 3),
            //        "Figen",
            //        new DateTime(2025, 1, 3),
            //        new DateTime(2025, 2, 4)
            //        ));

            //}
            //catch (ReservationConflictException)
            //{

            //    throw;
            //}
            // IEnumerable<Reservation> reservations = hotel.GetReservationsForUser("Figen");


            //DbContextOptions options = new DbContextOptionsBuilder().UseSqlite("Data Source=reservoom.db").Options;
            // using (ReservoomDbContext dbContext = new ReservoomDbContext(options)) {

            ReservoomDbContextFactory reservoomDbContextFactory = _host.Services.GetRequiredService<ReservoomDbContextFactory>();
            using (ReservoomDbContext dbContext = reservoomDbContextFactory.CreateDbContext()) {
                dbContext.Database.Migrate(); // you will find it under bin folder
            };


            //_navigationStore.CurrentViewModel = new ReservationListingViewModel(_navigationStore);
            //_navigationStore.CurrentViewModel = CreateReservationViewModel();

            //NavigationStore navigationStore = _host.Services.GetRequiredService<NavigationStore>();
            //navigationStore.CurrentViewModel = CreateReservationViewModel();

            NavigationService<ReservationListingViewModel> navigateService = _host.Services.GetRequiredService<NavigationService<ReservationListingViewModel>>();
            navigateService.Navigate();

            //MainWindow = new MainWindow()
            //{
            //    //DataContext = new MainViewModel() 
            //    //DataContext = new MainViewModel(_hotel)
            //    //DataContext = new MainViewModel(_navigationStore)
            //    DataContext = new MainViewModel(navigationStore)
            //};
            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();

            base.OnStartup(e);
        }
        //private MakeReservationViewModel CreateMakeReservationViewModel()
        //{
        //    // return new MakeReservationViewModel(_hotel, _navigationStore, CreateReservationViewModel);
        //    //return new MakeReservationViewModel(_hotel, new NavigationService(_navigationStore, CreateReservationViewModel));
        //    return new MakeReservationViewModel(_hotelStore, new NavigationService(_navigationStore, CreateReservationViewModel));
        //}

        //private ReservationListingViewModel CreateReservationViewModel()
        //{
        //    //return new ReservationListingViewModel(_navigationStore, CreateMakeReservationViewModel);
        //    //return new ReservationListingViewModel(_hotel, new NavigationService(_navigationStore, CreateMakeReservationViewModel));
        //    return ReservationListingViewModel.LoadViewModel(_hotelStore, CreateMakeReservationViewModel(), new NavigationService(_navigationStore, CreateMakeReservationViewModel));

        //}
        protected override void OnExit(ExitEventArgs e)
        {
            _host.Dispose();

            base.OnExit(e);
        }
    }

}
