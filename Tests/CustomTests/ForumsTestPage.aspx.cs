using System;
using System.Linq;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Forums;
using Telerik.Sitefinity.Forums.Events;
using Telerik.Sitefinity.Forums.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Modules.GenericContent.Events;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Events;

namespace SitefinityWebApp.CustomTests
{
    public partial class ForumsTestPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        #region Validation

        const string invalidString = "inject";

        protected void uxTestValidation_Click(object sender, EventArgs e)
        {
            EventHub.Subscribe<IForumPostCreatingEvent>(evt => this.ValidatePost(evt.Item));
            EventHub.Subscribe<IForumPostUpdatingEvent>(evt => this.ValidatePost(evt.Item));

            EventHub.Subscribe<ICommentCreatingEvent>(evt => this.ValidateComment(evt.DataItem));
            EventHub.Subscribe<ICommentUpdatingEvent>(evt => this.ValidateComment(evt.DataItem));
        
            Response.Write("Validation enabled. Invalid string: " + invalidString);
        }

        private void ValidatePost(ForumPost post)
        {
            if (post.Content.Contains(invalidString))
            {
                throw new ValidationException("Invalid post content.");

                //post.IsMarkedSpam = true;
                //post.Content = post.Content.Replace(invalidString, "");
            }
        }

        private void ValidateComment(Comment comment)
        {
            if (comment.Content.Contains(invalidString))
            {
                throw new ValidationException("Invalid comment content.");

                //comment.CommentStatus = CommentStatus.Spam;
            }
        }

        #endregion

        #region Page Modules

        protected void uxTestPageModules_Click(object sender, EventArgs e)
        {
            var pageManager = PageManager.GetManager();

            var pages = pageManager.GetPageNodes().Where(p => p.RootNodeId == SiteInitializer.BackendRootNodeId);

            foreach (var page in pages)
            {
                string moduleName;
                if (page.Attributes.TryGetValue("ModuleName", out moduleName))
                {
                    page.ModuleName = page.Attributes["ModuleName"];
                    page.Attributes.Remove("ModuleName");
                }
            }
            pageManager.SaveChanges();

            //for (int i = 0; i < 1000; i++)
            //{
            //    ThreadStart threadStart = delegate { TestMethod(); };
            //    Thread thread = new System.Threading.Thread(threadStart);
            //    thread.IsBackground = true;
            //    thread.Start();
            //}
        }

        static void TestMethod()
        {
            //Thread.Sleep(TimeSpan.FromMinutes(2));
        }

        #endregion

        #region Permissons

        protected void uxTestPermissions_Click(object sender, EventArgs e)
        {
            // Permissions tests
            var forumsManager = ForumsManager.GetManager();

            // Create update forum
            var forumTitle = "Forum 1";
            var forum = forumsManager.GetForums().Where(f => f.Title == forumTitle).SingleOrDefault();
            // Create update thread

            var threadId = new Guid("02B35FC4-4DED-4B6D-9C2E-8198C0AED25E");
            var thread = forumsManager.GetThreads().Where(t => t.Id == threadId).SingleOrDefault();
            if (thread == null)
            {
                thread = forumsManager.CreateThread(threadId);
                thread.Title = "MyThread";
                thread.Forum = forum;
                thread.UrlName = thread.Title.ToLower().Replace(' ', '-');
                forumsManager.RecompileItemUrls(thread);

            }
            else
            {
                //thread.Title = "MyThreadNew " + Guid.NewGuid().ToString();
                thread.ThreadType = Telerik.Sitefinity.Forums.Model.ForumThreadType.StickOnTop;
            }
            forumsManager.SaveChanges();
            thread = forumsManager.GetThread(threadId);

            //var postId = new Guid("03B35FC4-4DED-4B6D-9C2E-8198C0AED25B");
            //var post = forumsManager.GetPosts().Where(p => p.Id == postId).SingleOrDefault();
            //if (post == null)
            //{
            //    post = forumsManager.CreatePost(postId);
            //    post.Thread = thread;
            //    post.Content = "my post";
            //}
            //else
            //{
            //    post.Content = "My post new " + Guid.NewGuid().ToString();
            //}
            //forumsManager.SaveChanges();


            //var groupId = new Guid("00B35FC4-4DED-4B6D-9C2E-8198C0AED25F");
            //var group = forumsManager.GetGroups().Where(g => g.Id == groupId).SingleOrDefault();
            //if (group == null)
            //{
            //    group = forumsManager.CreateGroup(groupId);
            //    group.Title = "MyGroup";
            //}
            //else
            //{
            //    group.Title = "MyGroupNew " + Guid.NewGuid().ToString();
            //}
            //forumsManager.SaveChanges();
            //group = forumsManager.GetGroup(groupId);

            //// Create update forum
            //var forumId = new Guid("01B35FC4-4DED-4B6D-9C2E-8198C0AED25C");
            //forum = forumsManager.GetForums().Where(f => f.Id == forumId).SingleOrDefault();
            //if (forum == null)
            //{
            //    forum = forumsManager.CreateForum(forumId);
            //    forum.Group = group;
            //    forum.Title = "MyForum";
            //    forum.UrlName = forum.Title.ToLower().Replace(' ', '-');
            //    forumsManager.RecompileItemUrls(forum);
            //}
            //else
            //{
            //    forum.Title = "MyForumNew " + Guid.NewGuid().ToString();
            //}
            //forumsManager.SaveChanges();
            //forum = forumsManager.GetForum(forumId);

            //// Create update thread
            //var threadId = new Guid("02B35FC4-4DED-4B6D-9C2E-8198C0AED25E");
            //var thread = forumsManager.GetThreads().Where(t => t.Id == threadId).SingleOrDefault();
            //if (thread == null)
            //{
            //    thread = forumsManager.CreateThread(threadId);
            //    thread.Title = "MyThread";
            //    thread.Forum = forum;
            //    thread.UrlName = thread.Title.ToLower().Replace(' ', '-');
            //    forumsManager.RecompileItemUrls(thread);

            //}
            //else
            //{
            //    thread.Title = "MyThreadNew " + Guid.NewGuid().ToString();
            //}
            //forumsManager.SaveChanges();
            //thread = forumsManager.GetThread(threadId);

            //var postId = new Guid("03B35FC4-4DED-4B6D-9C2E-8198C0AED25B");
            //var post = forumsManager.GetPosts().Where(p => p.Id == postId).SingleOrDefault();
            //if (post == null)
            //{
            //    post = forumsManager.CreatePost(postId);
            //    post.Thread = thread;
            //    post.Content = "my post";
            //}
            //else
            //{
            //    post.Content = "My post new " + Guid.NewGuid().ToString();
            //}
            //forumsManager.SaveChanges();

        }

        #endregion
    }
}