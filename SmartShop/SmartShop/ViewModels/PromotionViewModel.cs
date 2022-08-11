using SmartShop.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

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
            //IsBusy = true;
            State = Xamarin.CommunityToolkit.UI.Views.LayoutState.Loading;
            try
            {
                var promotion = await PromotionService.GetPromotion(id);
                Promotion = promotion;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                //IsBusy = false;
                State = Xamarin.CommunityToolkit.UI.Views.LayoutState.None;
            }
        }
    }
}
