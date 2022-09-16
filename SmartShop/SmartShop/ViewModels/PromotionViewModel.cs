using SmartShop.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;

namespace SmartShop.ViewModels
{
    public class PromotionViewModel : BaseViewModel
    {
        Promotion promotion;
        public Promotion Promotion { get => promotion; set => SetProperty(ref promotion, value); }
        int id;

        public async void OnInitialize(int id)
        {
            if(id > 0)
            {
                this.id = id;
                await LoadDataAsync(id);
            }
        }

        public async Task LoadDataAsync(int id)
        {
            if (!VerifyInternetConnection())
            {
                State = LayoutState.Custom;
                CustomStateKey = StateKeys.Offline;
                return;
            }

            try
            {
                var promotionTask = PromotionService.GetPromotion(id);
                await Task.WhenAll(promotionTask, Task.Delay(1000));
                Promotion = await promotionTask;
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
                    State = Promotion.Products.Count > 0 ? LayoutState.None : LayoutState.Empty;
                }
            }
        }

        protected override async Task RefreshData()
        {
            await LoadDataAsync(id);
        }
    }
}
