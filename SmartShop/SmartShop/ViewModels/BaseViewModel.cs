﻿using SmartShop.Models;
using SmartShop.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace SmartShop.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public IProductService ProductService => DependencyService.Get<ProductService>();
        public ICategoryBrandService CategoryBrandService => DependencyService.Get<CategoryBrandService>();
        public ISearchService SearchService => DependencyService.Get<SearchService>();

        bool isBusy = false;
        LayoutState state = LayoutState.None;

        public Command BackwardCommand { get; } = new Command(async () => await Shell.Current.Navigation.PopAsync());
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        public LayoutState State
        {
            get { return state; }
            set { SetProperty(ref state, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
