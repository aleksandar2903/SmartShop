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

        public async void OnInitialize(int id)
        {
            await LoadPromotion(id);
        }

        public async Task LoadPromotion(int id)
        {
            if (!VerifyInternetConnection())
            {
                State = LayoutState.Custom;
                CustomStateKey = StateKeys.Offline;
                return;
            }

            try
            {
                Promotion = await PromotionService.GetPromotion(id);
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
    }
}
