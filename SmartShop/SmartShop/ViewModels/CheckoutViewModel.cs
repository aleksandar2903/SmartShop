﻿using SmartShop.Models;
using SmartShop.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace SmartShop.ViewModels
{
    public class CheckoutViewModel : BaseViewModel
    {
        private string _firstName = "Aleksandar";
        private string _lastName = "Stojanovic";
        private string _phone = "+381644069038";
        private string _address = "Straza, bb";
        private string _city = "Gnjilane";
        private string _zipCode = "38252";
        private string _cardName = "Aleksandar Stojanovic";
        private string _cardNumber = "4242424242424242";
        private string _cvv = "123";
        private string _expireDate = "05/23" ;
        private decimal _totalAmount;

        public string FirstName { get => _firstName; set => SetProperty(ref _firstName, value); }
        public string LastName { get => _lastName; set => SetProperty(ref _lastName, value); }
        public string Phone { get => _phone; set => SetProperty(ref _phone, value); }
        public string Address { get => _address; set => SetProperty(ref _address, value); }
        public string City { get => _city; set => SetProperty(ref _city, value); }
        public string ZipCode { get => _zipCode; set => SetProperty(ref _zipCode, value); }
        public string CardName { get => _cardName; set => SetProperty(ref _cardName, value); }
        public string CardNumber { get => _cardNumber; set => SetProperty(ref _cardNumber, value); }
        public string Cvv { get => _cvv; set => SetProperty(ref _cvv, value); }
        public string ExpireDate { get => _expireDate; set => SetProperty(ref _expireDate, value); }
        public decimal TotalAmount { get => _totalAmount; set => SetProperty(ref _totalAmount, value); }
        public ObservableCollection<Cart> Cart { get;  }
        public Command SaveShippingInformationsCommand { get; }
        public Command PlaceOrderCommand { get; }

        public CheckoutViewModel()
        {
            SaveShippingInformationsCommand = new Command(async () => await Shell.Current.GoToAsync(nameof(CheckoutPage)), () => ValidateShippingAddress());
            PlaceOrderCommand = new Command(async () => await Shell.Current.GoToAsync(nameof(CheckoutPage)), () => ValidateCardInformations());
            Cart = new ObservableCollection<Cart>();
            this.PropertyChanged +=
                (_, __) => SaveShippingInformationsCommand.ChangeCanExecute();
            this.PropertyChanged +=
                (_, __) => PlaceOrderCommand.ChangeCanExecute();
        }

        public override async Task InitializeAsync()
        {
            await LoadDataAsync();
        }
        private bool ValidateShippingAddress()
        {
            return !string.IsNullOrWhiteSpace(FirstName) &&
                !string.IsNullOrWhiteSpace(LastName) &&
                !string.IsNullOrWhiteSpace(Address) &&
                !string.IsNullOrWhiteSpace(City) &&
                Phone.Length == 13 && ZipCode.Length == 5;
        }
        private bool ValidateCardInformations()
        {
            return !string.IsNullOrWhiteSpace(CardName) &&
                ExpireDate.Length == 5 &&
                CardNumber.Length == 16 && 
                Cvv.Length == 3;
        }
        private async Task LoadDataAsync()
        {
            if (!VerifyInternetConnection())
            {
                State = LayoutState.Custom;
                CustomStateKey = StateKeys.Offline;
                return;
            }

            State = LayoutState.Loading;
            try
            {
                var cartTask = CartService.GetCartAsync(SettingsService.AuthAccessToken);

                await Task.WhenAll(cartTask, Task.Delay(1000));

                var cart = await cartTask;

                TotalAmount = decimal.Zero;
                Cart.Clear();
                foreach (var product in cart)
                {
                    TotalAmount += product.Amount;
                    Cart.Add(product);
                }
            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                State = LayoutState.Error;
            }
            finally
            {
                if (State != LayoutState.Error)
                {
                    State = Cart.Count > 0 ? LayoutState.None : LayoutState.Error;
                }
            }
        }
    }
}
