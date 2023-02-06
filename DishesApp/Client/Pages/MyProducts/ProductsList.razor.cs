using AntDesign;
using AntDesign.ProLayout;
using Client.Models;
using Client.Services;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client.Pages.MyProducts
{
    public partial class ProductsList
    {
        private readonly ListGridType _listGridType = new ListGridType
        {
            Gutter = 16,
            Xs = 1,
            Sm = 2,
            Md = 3,
            Lg = 3,
            Xl = 4,
            Xxl = 4,
        };

        private readonly ListFormModel _model = new ListFormModel();
        private readonly IList<string> _selectCategories = new List<string>();



        [Inject] public IProjectService ProjectService { get; set; }
        [Inject] protected NavigationManager NavigationManager { get; set; }

        private string GetTabKey()
        {
            var url = NavigationManager.Uri.TrimEnd('/');
            var key = url.Substring(url.LastIndexOf('/') + 1);
            return key;
        }
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

        }
        private void HandleTabChange(string key)
        {
            var url = NavigationManager.Uri.TrimEnd('/');
            url = url.Substring(0, url.LastIndexOf('/'));
            NavigationManager.NavigateTo($"{url}/{key}");
        }
    }
}