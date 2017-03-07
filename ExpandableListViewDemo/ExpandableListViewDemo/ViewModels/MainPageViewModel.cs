using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ExpandableListViewDemo.Infrastructure;
using ExpandableListViewDemo.Models;
using Microsoft.Practices.ObjectBuilder2;
using System.ComponentModel;

namespace ExpandableListViewDemo.ViewModels
{
    public class SelectCategoryViewModel
    {
		public Category _Category;
		public Category Category { 
		get
			{
				return _Category;
			}
			set
			{
				_Category = value;

			}
		}
        public bool Selected { get; set; }


	}

    public class MainPageViewModel : BindableBase,INotifyPropertyChanged
    {
		
       // public ObservableCollection<Grouping<SelectCategoryViewModel, Item>> Categories { get; set; }

		private ObservableCollection<Grouping<SelectCategoryViewModel, Item>> _Categories;
		public ObservableCollection<Grouping<SelectCategoryViewModel, Item>> Categories
		{
			get
			{
				return _Categories;
			}
			set
			{
				_Categories = value;
				//OnPropertyChanged("Categories");
			}
		}


		public DelegateCommand<Grouping<SelectCategoryViewModel, Item>> HeaderSelectedCommand
        {
            get
            {
                return new DelegateCommand<Grouping<SelectCategoryViewModel, Item>>(g =>
                {
                    if (g == null) return;
                    g.Key.Selected = !g.Key.Selected;


					
                    if (g.Key.Selected)
                    {
						
						


                        
						Data.DataFactory.DataItems.Where(i => (i.Category.CategoryId == g.Key.Category.CategoryId))
                            .ForEach(g.Add);
						
						
                    }
                    else
                    {
                        g.Clear();
                    }
					if (g.Key.Category.Rotation == 270)
						g.Key.Category.Rotation = 0;
					else
						g.Key.Category.Rotation = 270;

					Categories.Where(u => u.Key.Category.CategoryId == g.Key.Category.CategoryId).Select(w => w.Key.Category.Rotation = g.Key.Category.Rotation).ToList();
					
                });
            }
        }

        public MainPageViewModel()
        {
            Categories = new ObservableCollection<Grouping<SelectCategoryViewModel, Item>>();
            var selectCategories =
                Data.DataFactory.DataItems.Select(x => new SelectCategoryViewModel {Category = x.Category, Selected = false})
                    .GroupBy(sc => new {sc.Category.CategoryId})
                    .Select(g => g.First())
                    .ToList();
            selectCategories.ForEach(sc => Categories.Add(new Grouping<SelectCategoryViewModel, Item>(sc, new List<Item>())));
        
		}
    }
}
