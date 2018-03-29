using System;
using System.Linq;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.News.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Publishing;

namespace SitefinityWebApp
{
    public partial class AnyFluentTestPage : System.Web.UI.Page
    {
        private readonly static Type ITEM_TYPE = typeof(NewsItem);
        private readonly static Guid ITEM_ID = new Guid("{BDEB0FFF-38E4-4026-B7CD-BF69C79AFD3A}");

        protected void Page_Load(object sender, EventArgs e)
        {
            PageManager mgr = PageManager.GetManager();
            PageNode pageExists = mgr.GetPageNodes().Where(pN => pN.UrlName == pN.Name).FirstOrDefault();

            //var manager = ManagerBase.GetMappedManager(typeof(NewsItem));
            //var any = new AnyContentManager(manager, typeof(NewsItem));
        }

        protected void createNew_Click(object sender, EventArgs e)
        {
            //Create, checkout, edit, checkin-and-publish
            App.WorkWith()
                .AnyContentItem(ITEM_TYPE)
                .CreateNew(ITEM_ID)
                .CheckOut()
                .EditProperties()
                    .SetValue("Title", "Newly created item")
                    .SetValue("Content", @"<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam mollis orci ac urna lacinia tincidunt. Sed eget convallis quam. Maecenas ultricies pharetra luctus. Praesent sit amet tincidunt odio. Ut convallis lacinia nisl, vel pretium est facilisis ac. Fusce non orci odio. Donec semper rhoncus metus quis porttitor. Sed et justo ligula, vel consectetur metus. Praesent faucibus urna vitae nulla sodales facilisis. Integer elementum sapien a eros iaculis nec rhoncus sem porta. Donec gravida ultrices orci, ut dapibus enim dapibus non. Nullam eleifend, sem elementum elementum porta, nunc libero pretium nunc, at gravida quam sapien nec tellus. Quisque volutpat, augue non sodales commodo, neque leo volutpat odio, at egestas enim neque id metus. Integer suscipit dolor quis nibh lacinia ac lacinia eros gravida. Etiam dapibus vehicula nisl a convallis. Nulla facilisi. Fusce suscipit, erat at venenatis pulvinar, ligula lectus malesuada urna, quis varius lectus libero ac dui. Donec posuere cursus purus, sit amet egestas libero faucibus sodales.</p><p>Duis felis mauris, malesuada quis sodales id, commodo quis lacus. Nulla facilisi. Sed vel dignissim odio. In congue turpis vel massa pretium ac tempor leo posuere. Aenean porta, orci vitae commodo congue, ipsum purus bibendum elit, quis adipiscing erat neque ut ipsum. Nam sit amet massa erat, eget mollis ligula. Vestibulum egestas volutpat leo, eu suscipit quam sodales et. Mauris ac urna eu velit imperdiet ornare vitae vel metus. Pellentesque sit amet eros arcu, ut fermentum justo. Morbi nunc nunc, aliquet at faucibus ut, pretium in lacus.</p><p>Nam ac nisi ut nulla eleifend luctus. Nunc ultrices fringilla odio vel lacinia. Donec odio magna, varius eu egestas eu, consequat vel magna. Praesent sed tortor non metus condimentum sagittis ut et odio. Quisque viverra nunc sit amet massa convallis ut faucibus nisl aliquam. Aenean tempor enim vel massa mattis rutrum. Vivamus at vulputate est. Donec posuere, sem sit amet convallis pellentesque, est dolor sollicitudin sem, non hendrerit velit lorem at urna. Maecenas tempus euismod libero ac pellentesque. Mauris purus magna, tempus ac bibendum ac, ornare ut enim. Mauris hendrerit tortor eu est feugiat mattis. Integer vehicula varius metus nec dapibus. Cras tincidunt, turpis quis pretium condimentum, urna lectus vehicula ligula, in pharetra turpis turpis vitae leo. Integer sit amet tellus sapien.</p><p>Mauris nec odio non nulla pellentesque rutrum. Ut a turpis metus. Vestibulum laoreet ipsum in tellus ultricies scelerisque ac sagittis urna. In bibendum elementum nisl, nec malesuada lorem adipiscing sed. Nulla aliquet, arcu sit amet pulvinar venenatis, erat sapien auctor orci, at luctus velit mauris vel massa. Aliquam at varius ante. Quisque nec augue sit amet tellus aliquet lobortis. Integer ac pharetra augue. Sed ultrices scelerisque ultricies. Proin aliquet, tellus quis faucibus imperdiet, ante nisi dignissim magna, eget semper leo metus at augue. In felis felis, eleifend varius consequat vel, mollis ac elit. Phasellus vitae metus eu nibh sagittis commodo id a leo. Quisque imperdiet, justo et malesuada tristique, ipsum erat rhoncus velit, vel egestas velit tortor sit amet orci.</p><p>Vestibulum vitae ante at orci vehicula aliquam at id erat. Ut vestibulum rutrum dui, ut fringilla dui semper ac. Curabitur convallis vulputate faucibus. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec interdum accumsan orci id rhoncus. Donec nec nulla sapien. Aliquam erat volutpat. Suspendisse libero erat, cursus feugiat tincidunt consectetur, egestas mattis felis. Fusce sed elementum urna. Morbi tempor interdum arcu et lacinia. Aliquam posuere est sit amet nisi tincidunt vel blandit elit convallis. Pellentesque tellus dolor, cursus eu congue ut, convallis at metus. Pellentesque posuere velit sit amet nibh congue hendrerit. In pulvinar malesuada massa, sit amet tempor turpis sodales a. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Suspendisse posuere, neque sit amet vulputate scelerisque, orci urna iaculis sem, et egestas dui libero quis risus. Sed in orci dui.</p>")
                    .Done(false)
                .CheckInAndPublish()
                .SaveChanges();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            App.WorkWith()
                .AnyContentItem(ITEM_TYPE, ITEM_ID)
                .Delete()
                .SaveChanges();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            // Unpublish, chechkout, eidt, checkin
            App.WorkWith()
                .AnyContentItem(ITEM_TYPE, ITEM_ID)
                .GetLive()
                .Unpublish()
                .CheckOut()
                .EditProperties()
                    .SetValue("Title", "Edited news item")
                    .Done(false)
                .CheckIn()
                .SaveChanges();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            //PublishingManager.FlushAllPipes();

             //PublishingManager.FlushAllPipes();
           //// IQueryable<WrapperObject> items22 = null;
           // using (var facade = App.WorkWith())
           // {
           //     var newsItems = facade.NewsItems().Get();
           //     //items22  = pesho.Select(i=> new WrapperObject(i)).ToList().AsQueryable();

           //     //var dynamicNews = newsItems.Select(i => new DynamicBindingProxy(i)).AsQueryable();

           //     List<dynamic> dynamicNews = new List<dynamic>();
           //     foreach (var i in newsItems)
           //     {
           //         var wrapper = new WrapperObject(i);
           //         dynamic proxy = new DynamicBindingProxy(wrapper);
                    
           //         proxy.NewStatus = wrapper.Wrapper.Status;

           //         dynamicNews.Add(proxy);
           //     }
           //      var result = dynamicNews.AsQueryable().Where("NewStatus = Live");
           // }



            //dynamic item = new WrapperObject(App.WorkWith().NewsItems().First().Get());
            //var title = item.Title;


            using (var facade = App.WorkWith())
            {
                var items = facade.Events().Get();
                var items22 = items.Select(i => new WrapperObject(i)).ToList().AsQueryable();
                
                var test1 = items.Where("Tags.Contains((0d8307d7-99af-44ba-b22e-5d1a3fbc47b1))");
                var test =  items22.Where("Tags.Contains((0d8307d7-99af-44ba-b22e-5d1a3fbc47b1))");
                int result = test.Count();
                //var result = items22.Where("PublicationDate<= DateTime.UtcNow");

                //var result = items22.Where("Status = Live");
                //var result = items22.Where("Status = Live");
                //     //var dynamicNews = newsItems.Select(i => new DynamicBindingProxy(i)).AsQueryable();

                //     List<dynamic> dynamicNews = new List<dynamic>();
                //     foreach (var i in newsItems)
                //     {
                //         var wrapper = new WrapperObject(i);
                //         dynamic proxy = new DynamicBindingProxy(wrapper);

                //         proxy.NewStatus = wrapper.Wrapper.Status;

                //         dynamicNews.Add(proxy);
                //     }
                //      var result = dynamicNews.AsQueryable().Where("NewStatus = Live");
            }

            //var value = (int)ContentLifecycleStatus.Live;
            // var result = items22.Where("Status = Live");
            //var result = items22.Where("Status = "+value+"").ToList();
            //result = items22.Where("PublicationDate = DateTime.Now").ToList();

        }
    }
}