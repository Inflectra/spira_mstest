using System;
using System.Configuration;

namespace Inflectra.SpiraTest.AddOns.SpiraTestMSTestExtension
{
    /// <summary>
    /// This stores the configuration information needed to access the SpiraTest server
    /// </summary>
    //[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public sealed class SpiraTestConfigurationAttribute : Attribute
    {
        #region Fields

        /// <summary>
        /// Url
        /// </summary>
        private string url;

        /// <summary>
        /// Login
        /// </summary>
        private string login;

        /// <summary>
        /// Password
        /// </summary>
        private string password;

        /// <summary>
        /// Project id
        /// </summary>
        private int projectId;

        /// <summary>
        /// Release id
        /// </summary>
        private Nullable<int> releaseId;

        /// <summary>
        /// Test Set id
        /// </summary>
        private Nullable<int> testSetId;

        #endregion // Fields

        #region Properties

        /// <summary>
        /// The URL to the SpiraTest instance 
        /// </summary>
        public string Url
        {
            get
            {
                return this.url;
            }
            set
            {
                this.url = value;
            }
        }

        /// <summary>
        /// A valid SpiraTest login
        /// </summary>
        public string Login
        {
            get
            {
                return this.login;
            }
            set
            {
                this.login = value;
            }
        }

        /// <summary>
        /// A valid SpiraTest password
        /// </summary>
        public string Password
        {
            get
            {
                return this.password;
            }
            set
            {
                this.password = value;
            }
        }

        /// <summary>
        /// The SpiraTest project being tested
        /// </summary>
        public int ProjectId
        {
            get
            {
                return this.projectId;
            }
            set
            {
                this.projectId = value;
            }
        }

        /// <summary>
        /// The SpiraTest release being tested
        /// </summary>
        public Nullable<int> ReleaseId
        {
            get
            {
                return this.releaseId;
            }
            set
            {
                this.releaseId = value;
            }
        }

        /// <summary>
        /// The SpiraTest test set being tested
        /// </summary>
        public Nullable<int> TestSetId
        {
            get
            {
                return this.testSetId;
            }
            set
            {
                this.testSetId = value;
            }
        }

        #endregion

        #region Constructors


        /// <summary>
        /// The constructor is called when the attribute is set.
        /// </summary>
        public SpiraTestConfigurationAttribute()
            : this("Default", "Default")
        {
        }

        /// <summary>
        /// The constructor is called when the attribute is set.
        /// </summary>
        /// <param name="project">The SpiraTest project being tested</param>
        public SpiraTestConfigurationAttribute(string project)
            : this(project, "Default")
        {
        }

        /// <summary>
        /// The constructor is called when the attribute is set.
        /// </summary>
        /// <param name="project">The SpiraTest project being tested</param>
        /// <param name="release">The ID of the release being tested</param>
        public SpiraTestConfigurationAttribute(string project, string release)
            : this(-1, -1)
        {
            //Update the local member variables
            this.projectId = Int32.Parse(ConfigurationSettings.AppSettings["Project:" + project]);
            this.releaseId = Int32.Parse(ConfigurationSettings.AppSettings["Release:" + release]);
        }

        /// <summary>
        /// The constructor is called when the attribute is set.
        /// </summary>
        /// <param name="projectId">The SpiraTest project being tested</param>
        public SpiraTestConfigurationAttribute(int projectId)
            : this("Default", "Default")
        {
            //Update the local member variables
            this.projectId = projectId;
        }

        /// <summary>
        /// The constructor is called when the attribute is set.
        /// </summary>
        /// <param name="projectId">The SpiraTest project being tested</param>
        /// <param name="releaseId">The ID of the release being tested</param>
        public SpiraTestConfigurationAttribute(int projectId, int releaseId)
        {
            //Update the local member variables
            this.url = ConfigurationSettings.AppSettings["Url"];
            this.login = ConfigurationSettings.AppSettings["Login"];
            this.password = ConfigurationSettings.AppSettings["Password"];
            this.projectId = projectId;
            this.releaseId = releaseId;
        }

        /// <summary>
        /// The constructor is called when the attribute is set.
        /// </summary>
        /// <param name="projectId">The SpiraTest project being tested</param>
        /// <param name="releaseId">The ID of the release being tested</param>
        /// <param name="testSetId">The ID of the test set being tested</param>
        public SpiraTestConfigurationAttribute(int projectId, int releaseId, int testSetId)
        {
            //Update the local member variables
            this.url = ConfigurationSettings.AppSettings["Url"];
            this.login = ConfigurationSettings.AppSettings["Login"];
            this.password = ConfigurationSettings.AppSettings["Password"];
            this.projectId = projectId;
            this.releaseId = releaseId;
            this.testSetId = testSetId;
        }

        /// <summary>
        /// The constructor is called when the attribute is set.
        /// </summary>
        /// <param name="url">The URL to the SpiraTest instance</param>
        /// <param name="login">A valid SpiraTest login</param>
        /// <param name="password">A valid SpiraTest password</param>
        /// <param name="projectId">The SpiraTest project being tested</param>
        public SpiraTestConfigurationAttribute(string url, string login, string password, int projectId) :
            this(url, login, password, projectId, -1)
        {
        }

        /// <summary>
        /// The constructor is called when the attribute is set.
        /// </summary>
        /// <param name="url">The URL to the SpiraTest instance</param>
        /// <param name="login">A valid SpiraTest login</param>
        /// <param name="password">A valid SpiraTest password</param>
        /// <param name="projectId">The SpiraTest project being tested</param>
        /// <param name="releaseId">The ID of the release being tested</param>
        public SpiraTestConfigurationAttribute(string url, string login, string password, int projectId, int releaseId)
        {
            //Update the local member variables
            this.url = url;
            this.login = login;
            this.password = password;
            this.projectId = projectId;
            this.releaseId = releaseId;
        }

        /// <summary>
        /// The constructor is called when the attribute is set.
        /// </summary>
        /// <param name="url">The URL to the SpiraTest instance</param>
        /// <param name="login">A valid SpiraTest login</param>
        /// <param name="password">A valid SpiraTest password</param>
        /// <param name="projectId">The SpiraTest project being tested</param>
        /// <param name="releaseId">The ID of the release being tested</param>
        /// <param name="testSetId">The ID of the test set being tested</param>
        public SpiraTestConfigurationAttribute(string url, string login, string password, int projectId, int releaseId, int testSetId)
        {
            //Update the local member variables
            this.url = url;
            this.login = login;
            this.password = password;
            this.projectId = projectId;
            this.releaseId = releaseId;
            this.testSetId = testSetId;
        }

        #endregion // Properties

    }
}
