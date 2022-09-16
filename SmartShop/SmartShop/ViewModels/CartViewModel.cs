using MonkeyCache.FileStore;
using SmartShop.Models;
using SmartShop.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

namespace SmartShop.ViewModels
{
    public class CartViewModel : BaseViewModel
    {
        public ObservableCollection<Cart> Cart { get; set; }
        public ICommand ToggleProductCommand { get; }
        public ICommand DecreaseProductQuatityCommand { get; }
        private CancellationTokenSource cts = new CancellationTokenSource();
        private DateTime timerStarted { get; set; } = DateTime.UtcNow.AddYears(-1);
        public CartViewModel()
        {
            Cart = new ObservableCollection<Cart>();
            ToggleProductCommand = new Command<Cart>(async (cart) => await ToggleProduct(cart));
            DecreaseProductQuatityCommand = new Command(() => Throttle(500, _ => DecreaseQuantity()));
        }

        private async Task ToggleProduct(Cart product)
        {
            if (product == null)
            {
                return;
            }

            if (!VerifyInternetConnection())
            {
                State = LayoutState.Custom;
                CustomStateKey = StateKeys.Offline;
                return;
            }

            State = LayoutState.Loading;

            try
            {
                if (IsLoggedIn())
                {
                    await CartService.ToggleProductAsync(product.Id, SettingsService.AuthAccessToken);
                }
                else
                {
                    Barrel.Current.Empty(product.Id.ToString());
                }

                Cart.Remove(product);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                State = Cart.Count > 0 ? LayoutState.None : LayoutState.Empty;
            }
        }

        public async void OnAppearing()
        {
            await LoadDataAsync();
        }

        protected override async Task RefreshData()
        {
            await LoadDataAsync();
        }

        private async Task  DecreaseQuantity()
        {
            await ToggleProduct(new Cart());
        }
        private void Throttle(int interval, Action<object> action, object param = null)
        {
            cts.Cancel();
            cts = new CancellationTokenSource();

            var curTime = DateTime.UtcNow;
            if (curTime.Subtract(timerStarted).TotalMilliseconds < interval)
                interval -= (int)curTime.Subtract(timerStarted).TotalMilliseconds;

            Task.Run(async delegate
            {
                await Task.Delay(interval, cts.Token);
                action.Invoke(param);
            });

            timerStarted = curTime;
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
                Cart.Clear();

                if (IsLoggedIn())
                {
                    var task = CartService.GetCartProductsAsync(SettingsService.AuthAccessToken);
                    await Task.WhenAll(task, Task.Delay(1000));
                    var result = await task;
                    foreach (var cart in result)
                    {
                        Cart.Add(cart);
                    }
                }
                else
                {
                    string ids = string.Join(",", Barrel.Current.GetKeys());
                    var task = ProductService.GetBulkProductsAsync(ids);
                    await Task.WhenAll(task, Task.Delay(1000));
                    var result = await task;

                    foreach (var product in result)
                    {
                        Cart.Add(new Cart
                        {
                            Id = product.Id,
                            Quantity = Barrel.Current.Get<int>(product.Id.ToString()),
                            Product = product
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                State = LayoutState.Error;
            }
            finally
            {
                if (State != LayoutState.Error)
                {
                    State = Cart.Count > 0 ? LayoutState.None : LayoutState.Empty;
                }
            }
        }
    }
}
