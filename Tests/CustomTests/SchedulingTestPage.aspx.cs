using System;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Scheduling;
using Telerik.Sitefinity.Scheduling.Model;
using Telerik.Sitefinity.Utilities.Json;

namespace SitefinityWebApp.CustomTests
{
    public partial class SchedulingTestPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadTaskStatuses();
        }

        private void LoadTaskStatuses()
        {
            string nameFilter = Session["nameFilter"] as string;

            Func<ScheduledTaskData, bool> filter = t => string.IsNullOrWhiteSpace(nameFilter) 
                || t.TaskName.ToUpperInvariant().Contains(nameFilter.ToUpperInvariant());

            uxStatus.Text = "";
            var sch = SchedulingManager.GetManager();
            foreach (var t in sch.GetTaskData().Where(filter))
            {
                uxStatus.Text += string.Format("{0} ({1} / {2}%) [ID: {3}, Message: '{4}']\n", t.TaskName, t.Status, t.Progress, t.Id, t.StatusMessage);
            }
        }

        protected void uxFilter_Click(object sender, EventArgs e)
        {
            Session["nameFilter"] = uxNameFilter.Text;
            LoadTaskStatuses();
        }

        protected void uxStart_Click(object sender, EventArgs e)
        {
            var task = new ListGenerationTask(new ListGenerationTask.Settings { Count = 5 });
                      
            var sch = SchedulingManager.GetManager();
            sch.AddTask(task);
            sch.SaveChanges();
        }

        protected void uxResume_Click(object sender, EventArgs e)
        {
            var sch = SchedulingManager.GetManager();
            foreach (var t in sch.GetTaskData().Where(t => t.TaskName == ListGenerationTask.Name && t.Status == Telerik.Sitefinity.Scheduling.Model.TaskStatus.Failed))
            {
                Scheduler.Instance.RestartTask(t.Id);
            }
        }
    }

    internal class ListGenerationTask : ScheduledTask
    {
        // A default constructor is required for task activation.
        public ListGenerationTask()
        {
            this.ExecuteTime = DateTime.UtcNow;
        }

        public ListGenerationTask(Settings settings)
            : this()
        {
            this.settings = settings;
        }

        public override string TaskName
        {
            get
            {
                return Name;
            }
        }

        public override string GetCustomData()
        {
            return JsonUtility.ToJson(this.settings);
        }

        public override void SetCustomData(string customData)
        {
            this.settings = JsonUtility.FromJson<Settings>(customData);
        }

        public override void ExecuteTask()
        {
            try
            {

                using (var app = App.Prepare().SetTransactionName(this.TransactionName).WorkWith())
                {
                    var list = app.List().CreateNew().Do(l =>
                    {
                        l.Title = string.Format("Test List ({0})", l.Id);
                        l.UrlName = string.Format("test-list-{0}", l.Id); ;
                    }).Get();

                    for (int i = 1; i <= this.settings.Count; i++)
                    {
                        app.List(list.Id).CreateListItem().Do(p =>
                        {
                            p.Title = string.Format("List Item {0}", i);
                            p.UrlName = string.Format("list-item-{0}", i);
                        }).Publish().SaveChanges();

                        System.Threading.Thread.Sleep(10000);

                        this.UpdateProgress(i * 100 / this.settings.Count, 
                            string.Format("{0} list item(s) created", i));
                    }
                }
            }
            catch (System.Threading.ThreadAbortException)
            {
                throw new TaskStoppedException();
            }
        }

        public static readonly string Name = typeof(ListGenerationTask).FullName;

        private Settings settings;

        [DataContract]
        internal class Settings
        {
            [DataMember]
            public int Count { get; set; }
        }
    }
}