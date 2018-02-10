using System;
using System.Collections.Generic;
using Dros.Data.Models;
using Dros.ViewModels;
using Xamarin.Forms;

namespace Dros.Views
{
    public partial class AuthorsPage : ContentPage
    {
        readonly AuthorsViewModel _viewModel;
        private Author _author;

        public AuthorsPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new AuthorsViewModel();
        }

        //async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        //{
        //    _author = args.SelectedItem as Author;
        //    if (_author == null)
        //        return;

        //    await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(_author)));

        //     Manually deselect item.
        //    ItemsListView.SelectedItem = null;
        //}

        //        async void AddItem_Clicked(object sender, EventArgs e)
        //        {
        //            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        //        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (_viewModel.Authors.Count == 0)
                _viewModel.LoadAuthorsCommand.Execute(null);
        }
    }
}
