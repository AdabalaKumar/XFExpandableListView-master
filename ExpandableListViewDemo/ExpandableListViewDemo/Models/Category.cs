using System.ComponentModel;

namespace ExpandableListViewDemo.Models
{
    public class Category:INotifyPropertyChanged
    {
		public Category()
		{
			Rotation = 270;
		}
        public int CategoryId { get; set; }
        public string CategoryTitle { get; set; }
		public int _Rotation;
		public int Rotation
		{
			get { return _Rotation; }
			set { _Rotation = value;
			 OnPropertyChanged("Rotation");}
		}
		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged(string propertyname)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
			}
		}
    }
}
