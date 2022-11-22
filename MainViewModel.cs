using System;
using System.ComponentModel;
using System.Windows.Input;
using LibVLCSharp.Platforms.UWP;
using LibVLCSharp.Shared;
using Windows.Storage.AccessCache;
using Windows.Storage.Pickers;
using Windows.UI.Popups;
using System.Diagnostics;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.UI.Xaml;
using System.Threading.Tasks;

namespace VideoDemo
{
    /// <summary>
    /// Main view model
    /// </summary>
    public class MainViewModel : INotifyPropertyChanged, IDisposable
    {
        /// <summary>
        /// Occurs when a property value changes
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        public static MainViewModel Instance;

        /// <summary>
        /// Initialized a new instance of <see cref="MainViewModel"/> class
        /// </summary>
        public MainViewModel()
        {
            InitializedCommand = new RelayCommand<InitializedEventArgs>(Initialize);
            Instance = this;
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~MainViewModel()
        {
            Dispose();
        }

        /// <summary>
        /// Gets the command for the initialization
        /// </summary>
        public ICommand InitializedCommand { get; }

        private LibVLC LibVLC { get; set; }

        private MediaPlayer _mediaPlayer;
        /// <summary>
        /// Gets the media player
        /// </summary>
        public MediaPlayer MediaPlayer
        {
            get => _mediaPlayer;
            private set => Set(nameof(MediaPlayer), ref _mediaPlayer, value);
        }

        private void Set<T>(string propertyName, ref T field, T value)
        {
            if (field == null && value != null || field != null && !field.Equals(value))
            {
                field = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private async void Initialize(InitializedEventArgs eventArgs)
        {
            LibVLC = new LibVLC(enableDebugLogs: true, eventArgs.SwapChainOptions);
            LibVLC.Log += (sender, e) =>
            {
                Debug.WriteLine($"[{e.Level.ToString()}] - ${e.Message}");
            };

            MediaPlayer = new MediaPlayer(LibVLC);

            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.Downloads;
            openPicker.FileTypeFilter.Add("*");

            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/game_of_thrones_demo.mp4"));

            /*Windows.Storage.StorageFile file = await openPicker.PickSingleFileAsync();
            StorageApplicationPermissions.FutureAccessList.Add(file);*/
            var path = file.Path;
            Debug.WriteLine("path is " + path);
            try
            {
                VideoProperties videoProperties = await file.Properties.GetVideoPropertiesAsync();

            Debug.WriteLine("videoProperties  info duration is " + videoProperties.Duration);


            using (Media media = new Media(LibVLC, path, FromType.FromPath))
            {
                _ = MediaPlayer.Play(media);
            }
            }
            catch { 
            }
            
        }

        public void Add_Subtitle()
        {
            _ = LoadSubtitleMrlAsync();
        }

        public void Set_Color()
        {
            SetSubtitleColor("16711680");
        }

        public void Set_Font_Size()
        {
            SetSubtitleFontSize(55);
        }

        public void Set_Encode()
        {
            SetSubtitleEncoding("Windows-125");
        }

        public async Task<bool> LoadSubtitleMrlAsync()
        {
            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/Game_of_Thrones.srt"));
            string mrl = "winrt://" + StorageApplicationPermissions.FutureAccessList.Add(file);
            var ret = MediaPlayer.AddSlave(MediaSlaveType.Subtitle, mrl, true);
            Debug.WriteLine($"LoadSubtitleMrl {mrl} with ret {ret}");
            return ret;
        }


        public void SetSubtitleFontSize(int size)
        {
            MediaPlayer.Media?.AddOption($":freetype-rel-fontsize={size}"); // Usually 10, 13, 16 or 19
        }

        public void SetSubtitleColor(string decimalColor)
        {
            MediaPlayer.Media?.AddOption($":freetype-color={decimalColor}");
        }

        public void SetSubtitleEncoding(string encode)
        {
            MediaPlayer.Media?.AddOption($":subsdec-encoding={encode}");
        }

        /// <summary>
        /// Cleaning
        /// </summary>
        public void Dispose()
        {
            var mediaPlayer = MediaPlayer;
            MediaPlayer = null;
            mediaPlayer?.Dispose();
            LibVLC?.Dispose();
            LibVLC = null;
        }
    }
}
