using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Dros.Data.Models;
using Xamarin.Forms;

using Dros.Models;
using Dros.Views;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Dros.ViewModels
{
    public class AuthorsViewModel : BaseViewModel
    {
        public ObservableCollection<Author> Authors { get; set; }

        public Command LoadAuthorsCommand { get; set; }

        public AuthorsViewModel()
        {
            Title = "Authors";
            Authors = new ObservableCollection<Author>();
            LoadAuthorsCommand = new Command(async () => await ExecuteLoadAuthorsCommand());

//            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
//            {
//                var _item = item as Item;
//                Items.Add(_item);
//                await DataStore.AddItemAsync(_item);
//            });
        }

        async Task ExecuteLoadAuthorsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Authors.Clear();
                var authors = await Context.Authors.ToListAsync();
                authors.ForEach(x => Authors.Add(x));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}