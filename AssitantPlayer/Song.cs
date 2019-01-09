using System.ComponentModel;
using System.IO;
using System.Windows.Media.Imaging;
using Newtonsoft.Json;

namespace AssitantPlayer
{
    public class Song: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private int _index;
        public int Index
        {
            get { return _index; }
            set
            {
                _index = value;
                OnPropertyChanged(nameof(Index));
            }
        }

        public string Id { get; set; }
        public string Title { get; set; }
        public string YoutubeLink { get; set; }

        [JsonIgnore]
        public BitmapImage Image { get; set; }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [JsonProperty("LargeIcon")]
        public byte[] LargeIconSerialized
        {
            get
            {
                byte[] data;
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(Image));
                using (MemoryStream ms = new MemoryStream())
                {
                    encoder.Save(ms);
                    data = ms.ToArray();
                }
                return data;
            }
            set
            {
                using (var ms = new System.IO.MemoryStream(value))
                {
                    Image = new BitmapImage();
                    Image.BeginInit();
                    Image.CacheOption = BitmapCacheOption.OnLoad; // here
                    Image.StreamSource = ms;
                    Image.EndInit();
                }
            }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }

        private string _viewer;
        public string Viewer
        {
            get { return _viewer; }
            set
            {
                _viewer = value;
                OnPropertyChanged(nameof(Viewer));
            }
        }
    }
}
